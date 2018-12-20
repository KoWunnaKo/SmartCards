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
    /// <summary>
    /// 
    /// </summary>
    public class SecureMessaging
    {
        private static ILog log = LogManager.GetLogger(typeof(SecureMessaging));
        private APDUResponse apduResp;

        private CardNative iCard;

        const ushort SC_OK = 0x9000;
        const byte SC_PENDING = 0x9F;

        public string LastOperationStatus { get; set; }

        private int Connect2Card()
        {
            try
            {
                log.Info("Connect2Card");

                string[] readers = iCard.ListReaders();

                string[] SpecReaders = (from reader in readers
                                        where reader.Contains("CK") || reader.Contains("CL")
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

        /// <summary>
        /// 
        /// </summary>
        public SecureMessaging()
        {
            log.Info("SecureMessaging");

            iCard = new CardNative();
        }

        /// <summary>
        /// 
        /// </summary>
        public void EstablishSecureChannel()
        {
            byte[] get_challenge = new byte[8];

            if (Connect2Card() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            ExternalAuthentificate ext = new ExternalAuthentificate();

            if (ext.ExternalAuth() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }


            {
                log.Info("00 A4 04 00 [07] A0 00 00 02 48 02 00 [00]");
                #region 00 A4 04 00 [07] A0 00 00 02 48 02 00 [00]
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0xA4, 0x04, 0x00, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = new byte[7] { 0xA0 ,0x00 ,0x00 ,0x02 ,0x48 ,0x02 ,0x00 };

                apduSize6_3.Update(apduParam6_3);

                apduResp = iCard.TransmitLe(apduSize6_3, 27);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    return;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }


            {
                log.Info("80CA0055 [00] ");
                #region 80CA0055 [00] 
                APDUCommand apduSize6_3 = new APDUCommand(0x80, 0xCA, 0x00, 0x55, null, 0);

                apduResp = iCard.TransmitLe(apduSize6_3, 7);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    return;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            {
                log.Info("002231A4 [06] 80|01<01> 84|01<08> ");
                #region 002231A4 [06] 80|01<01> 84|01<08> 
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0x22, 0x31, 0xA4, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = new byte[6] { 0x80, 0x01, 0x01, 0x84, 0x01, 0x08 };

                apduSize6_3.Update(apduParam6_3);

                apduResp = iCard.Transmit(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    return;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }


            {
                log.Info("002231B8 [06] 80|01<01> 84|01<08>  ");
                #region 002231B8 [06] 80|01<01> 84|01<08> 
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0x22, 0x31, 0xB8, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = new byte[6] { 0x80, 0x01, 0x01, 0x84, 0x01, 0x08 };

                apduSize6_3.Update(apduParam6_3);

                apduResp = iCard.Transmit(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    return;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            {
                log.Info("00840000 [08]  ");
                #region 00840000 [08] 
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0x84, 0x00, 0x00, null, 8);

                apduResp = iCard.Transmit(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    return;
                }

                Array.Copy(apduResp.Data, get_challenge, 8);

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            byte [] mut_al = BAC_Calculate.CalcBAC_Res(get_challenge,null);

            {
                log.Info("00820000 [28] MUTUAL [00]  ");
                #region 00820000 [28]  MUTUAL [00] 
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0x82, 0x00, 0x00, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = mut_al;

                apduSize6_3.Update(apduParam6_3);

                apduResp = iCard.TransmitLe(apduSize6_3, 40);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    return;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void InstallAppletV1()
        {
            if (Connect2Card() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            ExternalAuthentificate ext = new ExternalAuthentificate();

            if (ext.ExternalAuth() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            {
                log.Info("80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 48 02 00 01 08 07 C9 05 4C 00 FD 42 42 00");

                #region 80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 48 02 00 01 08 07 C9 05 4C 00 FD 42 42 00

                APDUCommand apduSize5 = new APDUCommand(0x80, 0xE6, 0x0C, 0x00, null, 0);

                APDUParam apduParam5 = new APDUParam();

                apduParam5.Data = new byte[36] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x45, 0x41, 0x43, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x47, 0x10, 0x01, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x01, 0x01, 0x08, 0x07, 0xC9, 0x05, 0x4C, 0x00, 0xFD, 0x42, 0x42, 0x00 };

                apduSize5.Update(apduParam5);

                apduResp = iCard.TransmitLe(apduSize5, 1, true);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File Not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    return;
                }
                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

        }

        public string ReadCardNumber()
        {
            string cardNumber = string.Empty;

            string[] readers = iCard.ListReaders();

            string[] SpecReaders = (from reader in readers
                                    where reader.Contains("CK") || reader.Contains("CL")
                                    select reader).ToArray();

            if (SpecReaders.Any())
            {
                cardNumber = GetCardNumber(iCard, SpecReaders.First());
            }

            return cardNumber;
        }

        private string GetCardNumber(CardNative _cardx, string m_reader)
        {
            _cardx.Disconnect(DISCONNECT.Reset);

            _cardx.Connect(m_reader, SHARE.Shared, PROTOCOL.T0orT1);

            var AtrBytes = CallApduCommandLeData(0xFF, 0xCA, 0x00, 0x00, null, 5, _cardx, m_reader);

            StringBuilder hex1 = new StringBuilder((5) * 2);
            foreach (byte b in AtrBytes)
                hex1.AppendFormat("{0:X2}", b);
            var uid_temp = hex1.ToString();

            return uid_temp;
        }

        private byte[] CallApduCommandLeData(byte Cla, byte Ins, byte P1, byte P2, byte[] data, uint _recieveLength, CardNative _cardx, string readerInfo)
        {

            APDUResponse apduResp;

            const ushort SC_OK = 0x9000;
            const byte SC_PENDING = 0x9F;
            const ushort SC_FileEnd = 0x6282;

            APDUCommand apduSize5 = new APDUCommand(Cla, Ins, P1, P2, null, 0);

            APDUParam apduParam5 = new APDUParam();

            apduParam5.Data = data;

            apduSize5.Update(apduParam5);

            _cardx.Disconnect(DISCONNECT.Reset);

            _cardx.Connect(readerInfo, SHARE.Shared, PROTOCOL.T0orT1);

            //_logService.Info(apduSize5.ToString());

            apduResp = _cardx.TransmitLe(apduSize5, _recieveLength);

            if (apduResp == null)
            {
                _cardx.Disconnect(DISCONNECT.Reset);

                _cardx.Connect(readerInfo, SHARE.Shared, PROTOCOL.T0orT1);

                return null;
            }

            if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING && apduResp.Status != SC_FileEnd)
            {
                return null;
            }

            return apduResp.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InstallAppletV3()
        {
            Console.WriteLine("Begin Reset Card");

            if (Connect2Card() != 0)
            {
                log.Info("Connect to Card failed");
                Console.WriteLine("Connect to Card failed");
                return;
            }

            Console.WriteLine("Successfully Connected");

            ExternalAuthentificate ext = new ExternalAuthentificate();

            if (ext.ExternalAuth() != 0)
            {
                log.Info("External Authentification failed");
                Console.WriteLine("External Authentification failed");
                return;
            }

            Console.WriteLine("External Authentification passed");

            {
                log.Info("80 E4 00 00 [09] 4F|07<A0000002480200>");

                Console.WriteLine("Start Applet delete");

                #region 80 E4 00 00 [09] 4F|07<A0000002480200>

                APDUCommand apduSize5 = new APDUCommand(0x80, 0xE4, 0x00, 0x00, null, 0);

                APDUParam apduParam5 = new APDUParam();
                
                apduParam5.Data = new byte[] { 0x4F, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00 };

                apduSize5.Update(apduParam5);

                apduResp = iCard.TransmitLe(apduSize5, 1, true);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File Not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        //return;
                    }

                    Console.WriteLine("Applet delete failed");

                    //return;
                }
                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }


            {
                log.Info("80 E4 00 00 [09] 4F|07<A0000002480500>");

                Console.WriteLine("Start Metka Applet delete");

                #region 80 E4 00 00 [09] 4F|07<A0000002480200>

                APDUCommand apduSize5 = new APDUCommand(0x80, 0xE4, 0x00, 0x00, null, 0);

                APDUParam apduParam5 = new APDUParam();

                apduParam5.Data = new byte[] { 0x4F, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x48, 0x05, 0x00 };

                apduSize5.Update(apduParam5);

                apduResp = iCard.TransmitLe(apduSize5, 1, true);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File Not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        //return;
                    }

                    Console.WriteLine("Metka Applet delete failed");

                    //return;
                }
                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }



            if (ext.ExternalAuth() != 0)
            {
                log.Info("External Authentification failed");
                return;
            }

            Console.WriteLine("External Authentification passed");

            {
                Console.WriteLine("SetUp Install Applet");

                log.Info("80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 48 02 00 01 08 07 C9 05 4C 00 00 02 0A 00");

                #region 80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 48 02 00 01 08 07 C9 05 4C 00 00 02 0A 00

                APDUCommand apduSize5 = new APDUCommand(0x80, 0xE6, 0x0C, 0x00, null, 0);

                APDUParam apduParam5 = new APDUParam();
                                                //0x08 0xD2  0x76  0x00  0x00  0x98  0x45  0x41  0x43  0x07  0xA0  0x00  0x00  0x02  0x47  0x10  0x01  0x07  0xA0  0x00  0x00  0x02  0x48  0x02  0x00 0x01 0C 07 C9 05 4C 00 00 02 0A 00
                apduParam5.Data = new byte[36] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x45, 0x41, 0x43, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x47, 0x10, 0x01, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00, 0x01, 0x0C, 0x07, 0xC9, 0x05, 0x4C, 0x00, 0x00, 0x02, 0x0A, 0x00 };

                apduSize5.Update(apduParam5);

                apduResp = iCard.TransmitLe(apduSize5, 1, true);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File Not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    Console.WriteLine("SetUp Install Applet failed");

                    return;
                }
                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            Console.WriteLine("-----------------------");
            Console.WriteLine("Process Fully Completed");
            Console.WriteLine("-----------------------");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyValue"></param>
        public void InstallAppletV3(string KeyValue)
        {
            Console.WriteLine("Begin Reset Card");

            if (Connect2Card() != 0)
            {
                log.Info("Connect to Card failed");
                Console.WriteLine("Connect to Card failed");
                return;
            }

            Console.WriteLine("Successfully Connected");

            ExternalAuthentificate ext = new ExternalAuthentificate();

            if (ext.ExternalAuth(KeyValue) != 0)
            {
                log.Info("External Authentification failed");
                Console.WriteLine("External Authentification failed");
                return;
            }

            Console.WriteLine("External Authentification passed");

            {
                log.Info("80 E4 00 00 [09] 4F|07<A0000002480200>");

                Console.WriteLine("Start Applet delete");

                #region 80 E4 00 00 [09] 4F|07<A0000002480200>

                APDUCommand apduSize5 = new APDUCommand(0x80, 0xE4, 0x00, 0x00, null, 0);

                APDUParam apduParam5 = new APDUParam();

                apduParam5.Data = new byte[] { 0x4F, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00 };

                apduSize5.Update(apduParam5);

                apduResp = iCard.TransmitLe(apduSize5, 1, true);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File Not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        //return;
                    }

                    Console.WriteLine("Applet delete failed");

                    //return;
                }
                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            {
                log.Info("80 E4 00 00 [09] 4F|07<A0000002480500>");

                Console.WriteLine("Start Metka Applet delete");

                #region 80 E4 00 00 [09] 4F|07<A0000002480200>

                APDUCommand apduSize5 = new APDUCommand(0x80, 0xE4, 0x00, 0x00, null, 0);

                APDUParam apduParam5 = new APDUParam();

                apduParam5.Data = new byte[] { 0x4F, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x48, 0x05, 0x00 };

                apduSize5.Update(apduParam5);

                apduResp = iCard.TransmitLe(apduSize5, 1, true);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File Not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        //return;
                    }

                    Console.WriteLine("Metka Applet delete failed");

                    //return;
                }
                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            if (ext.ExternalAuth(KeyValue) != 0)
            {
                log.Info("External Authentification failed");
                return;
            }

            Console.WriteLine("External Authentification passed");

            {
                Console.WriteLine("SetUp Install Applet");

                log.Info("80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 48 02 00 01 08 07 C9 05 4C 00 00 02 0A 00");

                #region 80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 48 02 00 01 08 07 C9 05 4C 00 00 02 0A 00

                APDUCommand apduSize5 = new APDUCommand(0x80, 0xE6, 0x0C, 0x00, null, 0);

                APDUParam apduParam5 = new APDUParam();
                //0x08 0xD2  0x76  0x00  0x00  0x98  0x45  0x41  0x43  0x07  0xA0  0x00  0x00  0x02  0x47  0x10  0x01  0x07  0xA0  0x00  0x00  0x02  0x48  0x02  0x00 0x01 0C 07 C9 05 4C 00 00 02 0A 00
                apduParam5.Data = new byte[36] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x45, 0x41, 0x43, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x47, 0x10, 0x01, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00, 0x01, 0x0C, 0x07, 0xC9, 0x05, 0x4C, 0x00, 0x00, 0x02, 0x0A, 0x00 };

                apduSize5.Update(apduParam5);

                apduResp = iCard.TransmitLe(apduSize5, 1, true);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File Not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    Console.WriteLine("SetUp Install Applet failed");

                    return;
                }
                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            Console.WriteLine("-----------------------");
            Console.WriteLine("Process Fully Completed");
            Console.WriteLine("-----------------------");

        }

        /// <summary>
        /// 
        /// </summary>
        public void InstallAppletV5()
        {
            if (Connect2Card() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            ExternalAuthentificate ext = new ExternalAuthentificate();

            if (ext.ExternalAuth() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            {
                log.Info("80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 48 02 00 01 08 07 C9 05 4C 00 FD 42 42 00");

                #region 80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 48 02 00 01 08 07 C9 05 4C 00 FD 42 42 00

                APDUCommand apduSize5 = new APDUCommand(0x80, 0xE6, 0x0C, 0x00, null, 0);

                APDUParam apduParam5 = new APDUParam();

                apduParam5.Data = new byte[36] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x45, 0x41, 0x43, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x47, 0x10, 0x01, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00, 0x01, 0x08, 0x07, 0xC9, 0x05, 0x4C, 0x00, 0xFD, 0x42, 0x42, 0x00 };

                apduSize5.Update(apduParam5);

                apduResp = iCard.TransmitLe(apduSize5, 1, true);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File Not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    return;
                }
                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public void InstallAppletV2()
        {
            if (Connect2Card() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            ExternalAuthentificate ext = new ExternalAuthentificate();

            if (ext.ExternalAuth() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            {
                log.Info("80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 47 10 01 01 08 07 C9 05 4C 00 FD 42 42 00");

                #region 80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 47 10 01 01 08 07 C9 05 4C 00 FD 42 42 00

                APDUCommand apduSize5 = new APDUCommand(0x80, 0xE6, 0x0C, 0x00, null, 1);

                APDUParam apduParam5 = new APDUParam();

                apduParam5.Data = new byte[36] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x45, 0x41, 0x43, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x47, 0x10, 0x01, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x47, 0x10, 0x01, 0x01, 0x08, 0x07, 0xC9, 0x05, 0x4C, 0x00, 0xFD, 0x42, 0x42, 0x00 };

                apduSize5.Update(apduParam5);

                apduResp = iCard.Transmit(apduSize5);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File Not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return ;
                    }

                    return ;
                }
                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void InstallAppletV4()
        {
            if (Connect2Card() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            ExternalAuthentificate ext = new ExternalAuthentificate();

            if (ext.ExternalAuth() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            {
                log.Info("80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 47 10 01 01 08 07 C9 05 4C 00 FD 42 42 00");

                #region 80 E6 0C 00 [24] 08 D2 76 00 00 98 45 41 43 07 A0 00 00 02 47 10 01 07 A0 00 00 02 47 10 01 01 08 07 C9 05 4C 00 FD 42 42 00

                APDUCommand apduSize5 = new APDUCommand(0x80, 0xE6, 0x0C, 0x00, null, 1);

                APDUParam apduParam5 = new APDUParam();

                apduParam5.Data = new byte[36] { 0x08, 0xD2, 0x76, 0x00, 0x00, 0x98, 0x45, 0x41, 0x43, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x47, 0x10, 0x01, 0x07, 0xA0, 0x00, 0x00, 0x02, 0x47, 0x10, 0x02, 0x01, 0x08, 0x07, 0xC9, 0x05, 0x4C, 0x00, 0x00, 0x02, 0x0A, 0x00 };

                apduSize5.Update(apduParam5);

                apduResp = iCard.Transmit(apduSize5);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File Not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    return;
                }
                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public void EstablishSecureChannelV1()
        {
            byte[] get_challenge = new byte[8];

            if (Connect2Card() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            ExternalAuthentificate ext = new ExternalAuthentificate();

            if (ext.ExternalAuth() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }


            {
                log.Info("00 A4 04 0C [07] A0 00 00 02 48 02 00 [00]");
                #region 00 A4 04 0C [07] A0 00 00 02 48 02 00 [00]
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0xA4, 0x04, 0x0C, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = new byte[7] { 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00 };

                apduSize6_3.Update(apduParam6_3);

                log.Info(apduSize6_3);

                apduResp = iCard.TransmitLe(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    log.Info(apduResp.Status);
                    log.Info(apduResp.ToString());

                    return;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }


            {
                log.Info("00840000 [08]  ");
                #region 00840000 [08] 
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0x84, 0x00, 0x00, null, 8);

                log.Info(apduSize6_3);

                apduResp = iCard.Transmit(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    log.Info(apduResp.Status);
                    log.Info(apduResp.ToString());

                    return;
                }

                Array.Copy(apduResp.Data, get_challenge, 8);

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            byte[] mut_al = BAC_Calculate.CalcBAC_Res(get_challenge, null);

            {
                log.Info("00820000 [28] MUTUAL [00]  ");
                #region 00820000 [28]  MUTUAL [00] 
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0x82, 0x00, 0x00, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = mut_al;

                apduSize6_3.Update(apduParam6_3);

                log.Info(apduSize6_3);

                apduResp = iCard.TransmitLe(apduSize6_3, 40);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    log.Info(apduResp.Status);
                    log.Info(apduResp.ToString());

                    return;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mrzInfo"></param>
        public void EstablishSecureChannelV2(byte[] mrzInfo)
        {
            byte[] get_challenge = new byte[8];

            if (Connect2Card() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }

            ExternalAuthentificate ext = new ExternalAuthentificate();

            if (ext.ExternalAuth() != 0)
            {
                log.Info("Connect to Card failed");
                return;
            }


            {
                log.Info("00 A4 04 0C [07] A0 00 00 02 48 02 00 [00]");
                #region 00 A4 04 0C [07] A0 00 00 02 48 02 00 [00]
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0xA4, 0x04, 0x0C, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = new byte[7] { 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00 };

                apduSize6_3.Update(apduParam6_3);

                log.Info(apduSize6_3);

                apduResp = iCard.TransmitLe(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    log.Info(apduResp.Status);
                    log.Info(apduResp.ToString());

                    return;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }


            {
                log.Info("00840000 [08]  ");
                #region 00840000 [08] 
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0x84, 0x00, 0x00, null, 8);

                log.Info(apduSize6_3);

                apduResp = iCard.Transmit(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    log.Info(apduResp.Status);
                    log.Info(apduResp.ToString());

                    return;
                }

                Array.Copy(apduResp.Data, get_challenge, 8);

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            byte[] mut_al = BAC_Calculate.CalcBAC_Res(get_challenge, mrzInfo);

            {
                log.Info("00820000 [28] MUTUAL [00]  ");
                #region 00820000 [28]  MUTUAL [00] 
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0x82, 0x00, 0x00, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = mut_al;

                apduSize6_3.Update(apduParam6_3);

                log.Info(apduSize6_3);

                apduResp = iCard.TransmitLe(apduSize6_3, 40);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return;
                    }

                    log.Info(apduResp.Status);
                    log.Info(apduResp.ToString());

                    return;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mrzInfo"></param>
        public int CheckValidityOfKey(byte[] mrzInfo)
        {
            byte[] get_challenge = new byte[8];

            if (Connect2Card() != 0)
            {
                log.Info("Connect to Card failed");
                return -1;
            }

            ExternalAuthentificate ext = new ExternalAuthentificate();

            if (ext.ExternalAuth() != 0)
            {
                log.Info("Connect to Card failed");
                return -2;
            }


            {
                log.Info("00 A4 04 0C [07] A0 00 00 02 48 02 00 [00]");
                #region 00 A4 04 0C [07] A0 00 00 02 48 02 00 [00]
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0xA4, 0x04, 0x0C, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = new byte[7] { 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00 };

                apduSize6_3.Update(apduParam6_3);

                log.Info(apduSize6_3);

                apduResp = iCard.TransmitLe(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return -3;
                    }

                    log.Info(apduResp.Status);
                    log.Info(apduResp.ToString());

                    return -4;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }


            {
                log.Info("00840000 [08]  ");
                #region 00840000 [08] 
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0x84, 0x00, 0x00, null, 8);

                log.Info(apduSize6_3);

                apduResp = iCard.Transmit(apduSize6_3);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return -5;
                    }

                    log.Info(apduResp.Status);
                    log.Info(apduResp.ToString());

                    return -6;
                }

                Array.Copy(apduResp.Data, get_challenge, 8);

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            byte[] mut_al = BAC_Calculate.CalcBAC_Res(get_challenge, mrzInfo);

            {
                log.Info("00820000 [28] MUTUAL [00]  ");
                #region 00820000 [28]  MUTUAL [00] 
                APDUCommand apduSize6_3 = new APDUCommand(0x00, 0x82, 0x00, 0x00, null, 0);

                APDUParam apduParam6_3 = new APDUParam();

                apduParam6_3.Data = mut_al;

                apduSize6_3.Update(apduParam6_3);

                log.Info(apduSize6_3);

                apduResp = iCard.TransmitLe(apduSize6_3, 40);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A82"))
                    {
                        string code = "File not Found";
                        LastOperationStatus = code;
                        log.Error(code);

                        return -7;
                    }

                    if (apduResp.ToString().Contains("6985"))
                    {
                        string code = "Conditions of use not satisﬁed ";
                        LastOperationStatus = code;
                        log.Error(code);

                        return -99;
                    }

                    if (apduResp.ToString().Contains("6300"))
                    {
                        string code = "No information given";
                        LastOperationStatus = code;
                        log.Error(code);

                        return -8;
                    }

                    if (apduResp.ToString().Contains("6A86"))
                    {
                        string code = "Incorrect parameters P1-P2 ";
                        LastOperationStatus = code;
                        log.Error(code);

                        return -8;
                    }

                    log.Info(apduResp.Status);
                    log.Info(apduResp.ToString());

                    return -8;
                }

                #endregion

                log.Info(apduResp.Status);
                log.Info(apduResp.ToString());
            }

            return 0;
        }
    }
}
