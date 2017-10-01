using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemCard;
using log4net;
using System.Security.Cryptography;
using System.IO;


namespace CardAPILib.InterfaceCL
{
    class ExternalAuthentificate
    {
        private static ILog log = LogManager.GetLogger(typeof(ExternalAuthentificate));
        private APDUResponse apduResp;

        private CardNative iCard;

        const ushort SC_OK = 0x9000;
        const byte SC_PENDING = 0x9F;

        public string LastOperationStatus { get; set; }

        public ExternalAuthentificate()
        {
            log.Info("ExternalAuthentificate");

            iCard = new CardNative();
        }
        private int Connect2Card()
        {
            try
            {
                log.Info("Connect2Card");

                string[] readers = iCard.ListReaders();

                string[] SpecReaders = (from reader in readers
                                        where reader.Contains("CL")
                                        select reader).ToArray();


                foreach (string readerInfo in SpecReaders)
                {
                    try
                    {
                        iCard.Disconnect(DISCONNECT.Unpower);

                    }
                    catch (Exception)
                    {
                        //
                    }

                    try
                    {
                        iCard.Connect(readerInfo, SHARE.Shared, PROTOCOL.T0orT1);

                        return 0;
                    }
                    catch (Exception)
                    {
                        //throw new ApduCommandException("Uneble to connect to Card");
                    }
                }

            }
            catch (Exception ex)
            {

                log.Info(ex.Message);

                throw new ApduCommandException("Uneble to connect to Card");
            }

            return 4;
        }

        #region ExternalAuth

        public int ExternalAuth()
        {
            log.Info("ExternalAuth");
            byte[] key = new byte[16];

            Connect2Card();

            #region Keys
            key[0] = 0x40;
            key[1] = 0x41;
            key[2] = 0x42;
            key[3] = 0x43;
            key[4] = 0x44;
            key[5] = 0x45;
            key[6] = 0x46;
            key[7] = 0x47;
            key[8] = 0x48;
            key[9] = 0x49;
            key[10] = 0x4A;
            key[11] = 0x4B;
            key[12] = 0x4C;
            key[13] = 0x4D;
            key[14] = 0x4E;
            key[15] = 0x4F;
            #endregion

            byte[] InitializeUpdateReturn = new byte[28];

            byte[] secureRandomBytes = SecureRandom.GetBytes(8);

            var host_challange = ByteArrayToString(secureRandomBytes);

            {
                log.Info("00 A4 04 00 [08] A0 00 00 01 51 00 00 00 [00]");
                #region 00 A4 04 00 [08] A0 00 00 01 51 00 00 00 [00]
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0xA4, 0x04, 0x00, null, 102);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = new byte[8] { 0xA0, 0x00, 0x00, 0x01, 0x51, 0x00, 0x00, 0x00 };

                apduSize6_3.Update(apduParam6_3);

                apduResp = iCard.Transmit(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 100;
                    }

                    return 101;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            {
                log.Info("80 CA 00 66 [00]");
                #region 80 CA 00 66 [00]

                APDUCommand apduSize6_2 = new APDUCommand(0x80, 0xCA, 0x00, 0x66, null, 0);

                apduResp = iCard.TransmitLe(apduSize6_2, 77);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 100;
                    }

                    return 101;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            {
                log.Info("80 50 00 00 [08]");
                #region 80 50 00 00 [08]
                APDUCommand apduSize6_3 = new APDUCommand(0x80, 0x50, 0x00, 0x00, null, 28);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = secureRandomBytes;

                apduSize6_3.Update(apduParam6_3);

                apduResp = iCard.Transmit(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 100;
                    }

                    return 101;
                }

                Array.Copy(apduResp.Data, InitializeUpdateReturn, 28);

                var _initializeUpdateReturn = ByteArrayToString(InitializeUpdateReturn);

                #region DerivasionData
                byte[] derivation_data = new byte[16];

                //01 82 00 04 00 00 00 00 00 00 00 00 00 00 00 00

                derivation_data[0] = 0x01;
                derivation_data[1] = 0x82;
                derivation_data[2] = InitializeUpdateReturn[12];
                derivation_data[3] = InitializeUpdateReturn[13];
                derivation_data[4] = 0x00;
                derivation_data[5] = 0x00;
                derivation_data[6] = 0x00;
                derivation_data[7] = 0x00;
                derivation_data[8] = 0x00;
                derivation_data[9] = 0x00;
                derivation_data[10] = 0x00;
                derivation_data[11] = 0x00;
                derivation_data[12] = 0x00;
                derivation_data[13] = 0x00;
                derivation_data[14] = 0x00;
                derivation_data[15] = 0x00;

                #endregion

                byte[] encData = encrypt3DES(key, derivation_data);

                byte[] host_auth_data = new byte[24];
                //sequence_counter
                host_auth_data[0] = InitializeUpdateReturn[12];
                host_auth_data[1] = InitializeUpdateReturn[13];

                //card_challenge
                host_auth_data[2] = InitializeUpdateReturn[14];
                host_auth_data[3] = InitializeUpdateReturn[15];
                host_auth_data[4] = InitializeUpdateReturn[16];
                host_auth_data[5] = InitializeUpdateReturn[17];
                host_auth_data[6] = InitializeUpdateReturn[18];
                host_auth_data[7] = InitializeUpdateReturn[19];

                //host_challenge
                host_auth_data[8] = secureRandomBytes[0];
                host_auth_data[9] = secureRandomBytes[1];
                host_auth_data[10] = secureRandomBytes[2];
                host_auth_data[11] = secureRandomBytes[3];
                host_auth_data[12] = secureRandomBytes[4];
                host_auth_data[13] = secureRandomBytes[5];
                host_auth_data[14] = secureRandomBytes[6];
                host_auth_data[15] = secureRandomBytes[7];

                host_auth_data[16] = 0x80;
                host_auth_data[17] = 0x00;
                host_auth_data[18] = 0x00;
                host_auth_data[19] = 0x00;
                host_auth_data[20] = 0x00;
                host_auth_data[21] = 0x00;
                host_auth_data[22] = 0x00;
                host_auth_data[23] = 0x00;

                byte[] host_cryptogram = encrypt3DES(encData, host_auth_data);

                byte[] ExternalAuthData = new byte[8];

                EXTERNAL_AUTHENTICATE_data[0] = host_cryptogram[16];
                EXTERNAL_AUTHENTICATE_data[1] = host_cryptogram[17];
                EXTERNAL_AUTHENTICATE_data[2] = host_cryptogram[18];
                EXTERNAL_AUTHENTICATE_data[3] = host_cryptogram[19];
                EXTERNAL_AUTHENTICATE_data[4] = host_cryptogram[20];
                EXTERNAL_AUTHENTICATE_data[5] = host_cryptogram[21];
                EXTERNAL_AUTHENTICATE_data[6] = host_cryptogram[22];
                EXTERNAL_AUTHENTICATE_data[7] = host_cryptogram[23];

                var _EXTERNAL_AUTHENTICATE_data = ByteArrayToString(EXTERNAL_AUTHENTICATE_data);

                //Generate MAC
                byte[] sequance_num = new byte[2];
                sequance_num[0] = InitializeUpdateReturn[12];
                sequance_num[1] = InitializeUpdateReturn[13];

                CMacKey = GenerateSessionKey(sequance_num, key);

                CMac = GenerateCmac(EXTERNAL_AUTHENTICATE_data);

                var _CMac = ByteArrayToString(CMac);

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            {
                log.Info("84 82 00 00 [10] CryptoGram MAC ");
                #region 84 82 00 00 [10] CryptoGram MAC 
                APDUCommand apduSize6_3 = new APDUCommand(0x84, 0x82, 0x00, 0x00, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = new byte[16];

                apduParam6_3.Data[0] = EXTERNAL_AUTHENTICATE_data[0];
                apduParam6_3.Data[1] = EXTERNAL_AUTHENTICATE_data[1];
                apduParam6_3.Data[2] = EXTERNAL_AUTHENTICATE_data[2];
                apduParam6_3.Data[3] = EXTERNAL_AUTHENTICATE_data[3];
                apduParam6_3.Data[4] = EXTERNAL_AUTHENTICATE_data[4];
                apduParam6_3.Data[5] = EXTERNAL_AUTHENTICATE_data[5];
                apduParam6_3.Data[6] = EXTERNAL_AUTHENTICATE_data[6];
                apduParam6_3.Data[7] = EXTERNAL_AUTHENTICATE_data[7];

                apduParam6_3.Data[8] = CMac[0];
                apduParam6_3.Data[9] = CMac[1];
                apduParam6_3.Data[10] = CMac[2];
                apduParam6_3.Data[11] = CMac[3];
                apduParam6_3.Data[12] = CMac[4];
                apduParam6_3.Data[13] = CMac[5];
                apduParam6_3.Data[14] = CMac[6];
                apduParam6_3.Data[15] = CMac[7];

                apduSize6_3.Update(apduParam6_3);

                log.Info(apduSize6_3.ToString());

                apduResp = iCard.Transmit(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 100;
                    }

                    return 101;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            return 0;
        }

        private TripleDESCryptoServiceProvider e1 = new TripleDESCryptoServiceProvider();
        private DESCryptoServiceProvider d1 = new DESCryptoServiceProvider();

        private byte[] encryptDES(byte[] key, byte[] data)
        {
            d1.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            if (key.Length != 0x08 &&
                key.Length != 0x10 &&
                key.Length != 0x18)
                throw new Exception("Invalid key length");
            d1.Padding = PaddingMode.None;
            d1.Mode = CipherMode.CBC;
            d1.Key = key;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, d1.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                return ms.ToArray();
            }

        }

        private byte[] encrypt3DES(byte[] key, byte[] data)
        {
            e1.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            if (key.Length != 0x08 &&
                key.Length != 0x10 &&
                key.Length != 0x18)
                throw new Exception("Invalid key length");
            e1.Padding = PaddingMode.None;
            e1.Mode = CipherMode.CBC;
            e1.Key = key;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, e1.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                return ms.ToArray();
            }
        }

        private byte[] encrypt3DES(byte[] key, byte[] data, byte[] iv)
        {
            e1.IV = iv;
            if (key.Length != 0x08 &&
                key.Length != 0x10 &&
                key.Length != 0x18)
                throw new Exception("Invalid key length");
            e1.Padding = PaddingMode.None;
            e1.Mode = CipherMode.CBC;
            e1.Key = key;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, e1.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                return ms.ToArray();
            }
        }

        private byte[] Algorithm3(byte[] data, byte[] key)
        {
            if (data.Length % 8 != 0)
            {
                throw new ArgumentException("Data must be padded to 8-byte blocks.");
            }

            if (key.Length != 16)
            {
                throw new ArgumentException("key.Length must be 16 bytes");
            }

            int numBlocks = data.Length / 8;

            byte[] iv = new byte[8];

            if (numBlocks > 1)
            {
                byte[] firstBlocks = data.Take((numBlocks - 1) * 8).ToArray();

                byte[] encFirstBlocks = encryptDES(key.Take(8).ToArray(), firstBlocks);

                Array.Copy(encFirstBlocks, encFirstBlocks.Length - 8, iv, 0, 8);
                //iv = encFirstBlocks.TakeLast(8).ToArray();
            }
            else
            {
                //iv = icv;
            }

            byte[] lastBlock = new byte[8];
            Array.Copy(data, data.Length - 8, lastBlock, 0, 8);


            byte[] encLastBlock = encrypt3DES(key, lastBlock, iv);
            byte[] mac = new byte[8];
            Array.Copy(encLastBlock, encLastBlock.Length - 8, mac, 0, 8);

            return mac;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        private static byte[] GetDataForCmac(byte[] cryptogram)
        {
            var apduData = new List<byte> { (byte)0x84, (byte)0x82, 0x00, 0x00 };

            byte newLc = checked((byte)(16));

            apduData.Add(newLc);
            apduData.AddRange(cryptogram);

            byte[] pad = new byte[3] { 0x80, 0x00, 0x00 };

            apduData.AddRange(pad);

            return apduData.ToArray();
        }

        private byte[] GenerateDerivationData(byte[] SequenceCounter)
        {
            var data = new List<byte> { 0x01, 0x01 };

            data.AddRange(SequenceCounter);
            data.AddRange(Enumerable.Repeat<byte>(0x00, 12));

            return data.ToArray();
        }

        private byte[] CMac = new byte[8];

        private byte[] EXTERNAL_AUTHENTICATE_data = new byte[8];

        public byte[] CMacKey { get; private set; }

        private byte[] GenerateSessionKey(byte[] SequenceCounter, byte[] staticKey)
        {
            return encrypt3DES(staticKey, GenerateDerivationData(SequenceCounter));
        }

        private byte[] GenerateCmac(byte[] cryptogram)
        {
            //byte[] icv = this.CMac.All(x => x == 0x00) ? this.CMac : DES.Encrypt(this.CMac, this.CMacKey.Take(8).ToArray());

            var data = GetDataForCmac(cryptogram);

            //

            byte[] mac = Algorithm3(data, this.CMacKey);

            return mac;
        }

        #endregion
    }
}
