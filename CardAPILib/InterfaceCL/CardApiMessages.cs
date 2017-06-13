using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GemCard;
using log4net;

namespace CardAPILib.InterfaceCL
{
    public class CardApiMessages
    {
        private static ILog log = LogManager.GetLogger(typeof(CardApiAPDUMessages));

        public string LastOperationStatus { get; set; }

        public string CardUUID { get; set; }

        public string PublicKey { get; set; }

        public string SignedToken { get; set; }

        public int CertificateSize { get; set; }

        public CardApiMessages()
        {
            iCard = new CardNative();
        }

        private APDUResponse apduResp;

        private CardNative iCard;

        const ushort SC_OK = 0x9000;
        const byte SC_PENDING = 0x9F;

        public int Connect2Card()
        {
            try
            {

                string[] readers = iCard.ListReaders();

                string[] SpecReaders = (from reader in readers
                                       where reader.Contains("ICC")
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
            catch(Exception ex)
            {
                
                log.Info(ex.Message);

                throw new ApduCommandException("Uneble to connect to Card");
            }

            return 4;
        }

        public int SelectFile()
        {
            try
            {
                APDUCommand apduSelectFile = new APDUCommand(0x00, 0xA4, 0x04, 0x00, null, 0);

                byte[] selfile = new byte[] { 0x73, 0x63, 0x70, 0x6B, 0x69 };
                APDUParam apduParam = new APDUParam();
                apduParam.Data = selfile;
                apduSelectFile.Update(apduParam);

                apduResp = iCard.Transmit(apduSelectFile);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    log.Info("Select File Transmit Error");
                    return 5;
                }

                return 0;
            }
            catch(Exception ex)
            {
                log.Info(ex.Message);
            }

            return 5;
        }

        public int GetRfId()
        {
            try
            {
                APDUCommand apduRfid = new APDUCommand(0xFF, 0xCA, 0x00, 0x00, null, 4);

                apduResp = iCard.Transmit(apduRfid);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    return 5;
                }

                Int32 rfid = 0;

                if (apduResp.Data[3] >= 0x80)
                {
                    rfid += ((0xFF - apduResp.Data[0]) + 1);
                    rfid += ((0xFF - apduResp.Data[1]) * 0x100);
                    rfid += ((0xFF - apduResp.Data[2]) * 0x10000);
                    rfid += ((0xFF - apduResp.Data[3]) * 0x1000000);
                    rfid *= (-1);
                }
                else
                {
                    rfid += apduResp.Data[0];
                    rfid += apduResp.Data[1] * 0x100;
                    rfid += apduResp.Data[2] * 0x10000;
                    rfid += apduResp.Data[3] * 0x1000000;
                }

                return rfid;
            }
            catch(Exception ex)
            {
                log.Info(ex.Message);
            }

            return 0;
        }

        protected int getUUID()
        {
            try
            {
                APDUCommand apduUUID = new APDUCommand(0x00, 0x30, 0x00, 0x06, null, 20);

                apduResp = iCard.Transmit(apduUUID);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    return 6;
                }

                byte[] destination = new byte[20];

                Array.Copy(apduResp.Data, destination, 20);

                CardUUID = Convert.ToBase64String(destination);
            }
            catch(Exception ex)
            {
                log.Info(ex.Message);

                return 6;
            }

            LastOperationStatus = "Success!";

            return 0;
        }

        /// <summary>
        /// This command is to be issued by the card’s user to login to the smart card before being capable of 
        /// performing the RSA signature operation and the changing of user’s PIN operation. Userlogin 
        /// PIN have a maximum retry of 3 retries before the User PIN becomes blocked. 
        /// </summary>
        /// <param name="pinCode"></param>
        protected int userLogin(byte[] pinCode)
        {

            try
            {
                APDUCommand apduLogin = new APDUCommand(0x00, 0x10, 0x00, 0x01, null, 0);

                APDUParam apduParam = new APDUParam();

                apduParam.Data = pinCode;

                apduLogin.Update(apduParam);

                apduResp = iCard.Transmit(apduLogin);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6984"))
                    {
                        string code = "Invalid login data due to User PIN being locked due to too many bad retries or simply wrong PIN code";
                        LastOperationStatus = code;
                        return 7;
                    }

                    return 8;
                }
                
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                return 8;
            }

            LastOperationStatus = "Success!";

            return 0;

        }


        /// <summary>
        /// This command is to be issued by the card’s administrator to login to the smart card before 
        /// being capable of performing card administrative operations for resetting and unblocking the User PIN, 
        /// uploading of card certificate and changing the administrator’s own Admin PIN. Administrative login 
        /// PIN have a maximum retry of 3 retries before the Admin PIN becomes blocked.
        /// </summary>
        /// <param name="pukCode"></param>
        protected int adminLogin(byte[] pukCode)
        {
            try
            {
                APDUCommand apduLogin = new APDUCommand(0x00, 0x10, 0x00, 0x02, null, 0);

                APDUParam apduParam = new APDUParam();

                apduParam.Data = pukCode;

                apduLogin.Update(apduParam);

                apduResp = iCard.Transmit(apduLogin);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6984"))
                    {
                        string code = "Invalid login data due to User PIN being locked due to too many bad retries or simply wrong PIN code";
                        LastOperationStatus = code;
                        return 9;
                    }

                    return 10;
                }

            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                return 10;
            }

            LastOperationStatus = "Success!";

            return 0;
        }

        /// <summary>
        /// This command is for a user to issue for the changing of the User PIN. 
        /// The user must already be logged in to proceed to issue this command to change User PIN. 
        /// </summary>
        /// <param name="newPinCode"></param>
        protected int userChangePin(byte[] newPinCode)
        {
            try
            {
                APDUCommand apduLogin = new APDUCommand(0x00, 0x20, 0x01, 0x01, null, 0);

                APDUParam apduParam = new APDUParam();

                apduParam.Data = newPinCode;

                apduLogin.Update(apduParam);

                apduResp = iCard.Transmit(apduLogin);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6700"))
                    {
                        string code = "Wrong length of PIN. PIN must be 4 to 16 bytes long";
                        LastOperationStatus = code;
                        return 11;
                    }
                    else if (apduResp.ToString().Contains("6982"))
                    {
                        string code = "Login is required for this command to execute";
                        LastOperationStatus = code;
                        return 12;
                    }

                    return 13;
                }

            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                return 13;
            }

            LastOperationStatus = "Success!";

            return 0;
        }

        /// <summary>
        /// This command is for am administrator to issue for the changing of the PUK. 
        /// The administrator must already be logged in to proceed to issue this command to change the PUK 
        /// </summary>
        /// <param name="newPinCode"></param>
        protected int adminChangePuk(byte[] newPukCode)
        {
            try
            {
                APDUCommand apduLogin = new APDUCommand(0x00, 0x20, 0x01, 0x02, null, 0);

                APDUParam apduParam = new APDUParam();

                apduParam.Data = newPukCode;

                apduLogin.Update(apduParam);

                apduResp = iCard.Transmit(apduLogin);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6700"))
                    {
                        string code = "Wrong length of PIN. PIN must be 4 to 16 bytes long";
                        LastOperationStatus = code;
                        return 14;
                    }
                    else if (apduResp.ToString().Contains("6982"))
                    {
                        string code = "Login is required for this command to execute";
                        LastOperationStatus = code;
                        return 15;
                    }

                    return 16;
                }

            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                return 16;
            }

            LastOperationStatus = "Success!";

            return 0;
        }


        /// <summary>
        /// This command is for the administrator to reset and unblock the User PIN to the default User PIN (123456). 
        /// The administrator must already be logged in to proceed to issue this command.
        /// </summary>
        protected int adminResetUserPin()
        {
            try
            {
                APDUCommand apduLogin = new APDUCommand(0x00, 0x20, 0x02, 0x01, null, 0);

                apduResp = iCard.Transmit(apduLogin);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6982"))
                    {
                        string code = "Login is required for this command to execute";
                        LastOperationStatus = code;
                        return 17;
                    }

                    return 18;
                }

            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                return 18;
            }

            LastOperationStatus = "Success!";

            return 0;

        }

        /// <summary>
        ///  This command allows the reading of the RSA Public Key Modulus from the card. 
        ///  No login is required for issuing this command. 
        ///  The RSA Public Key Exponent will always be fixed to 0x010001. 
        ///  256 byte RSA Public Key Modulus
        /// </summary>
        protected int readPublicKeyModulus()
        {
            try
            {
                APDUCommand apduPublicKey = new APDUCommand(0x00, 0x30, 0x00, 0x03, null, 256);

                apduResp = iCard.Transmit(apduPublicKey);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A83"))
                    {
                        string code = "Public Key Modulus cannot be found due to possible fault in the card. Reinstallation of card applet is necessary";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 19;
                    }

                    return 20;
                }

                byte[] destination = new byte[256];

                Array.Copy(apduResp.Data, destination, 256);

                PublicKey = Convert.ToBase64String(destination);
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                return 20;
            }

            LastOperationStatus = "Success!";

            return 0;
        }


        /// <summary>
        /// This command allows the card user to create an RSA digital signature by feeding a 64 byte SHA-512 hash. 
        /// The card user must already be logged in to proceed the creation of RSA signature. 
        /// 256 byte RSA digital signature
        /// </summary>
        /// <param name="token"></param>
        protected int signToken(byte[] token)
        {
            try
            {
                APDUCommand apduToken = new APDUCommand(0x00, 0x40, 0x00, 0x00, null, 256);

                APDUParam apduParam = new APDUParam();

                apduParam.Data = token;

                apduToken.Update(apduParam);

                apduResp = iCard.Transmit(apduToken);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6982"))
                    {
                        string code = "Login is required for this command to execute";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 21;
                    }

                    return 22;
                }

                byte[] destination = new byte[256];

                Array.Copy(apduResp.Data, destination, 256);

                SignedToken = Convert.ToBase64String(destination);
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                return 22;
            }

            LastOperationStatus = "Success!";

            return 0;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CA"></param>
        protected int uploadCA(byte[] CA)
        {
            log.Info("uploadCA");

            int CAPartsCount = CA.Length / 255;
            var CACurrentPart = new byte[255];
            var CAFinalizePart = new byte[CA.Length % 255];
            for (int i = 0; i < CAPartsCount; i++)
            {
                for (int j = 0; j < 255; j++)
                {
                    CACurrentPart[j] = CA[j + i * 255];
                }
                var retcode = uploadCAPart(CACurrentPart);
                if (retcode != 0)
                {
                    return retcode;
                }
            }

            for (int i = 0; i < (CA.Length % 255); i++)
            {
                CAFinalizePart[i] = CA[i + CAPartsCount * 255];
            }

            var retcodef = finalizeCA(CAFinalizePart);
            if (retcodef != 0)
            {
                return retcodef;
            }

            LastOperationStatus = "Upload Certificate Success!";

            return 0;
        }

        /// <summary>
        /// This command allows the upload of a CA Signed Certificate. 
        /// The new CA Signed Certificate will overwrite any old CA Signed Certificate existing in the storage space. 
        /// This command requires an administrator to be logged in to proceed to issue this command. 
        /// </summary>
        /// <param name="CA"></param>
        protected int uploadCAPart(byte[] CA)
        {
            try
            {
                APDUCommand apduLogin = new APDUCommand(0x00, 0x50, 0x00, 0x04, null, 0);

                APDUParam apduParam = new APDUParam();

                apduParam.Data = CA;

                apduLogin.Update(apduParam);

                apduResp = iCard.Transmit(apduLogin);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6700"))
                    {
                        string code = "Wrong length of data have been supplied";
                        LastOperationStatus = code;
                        return 23;
                    }
                    else if (apduResp.ToString().Contains("6982"))
                    {
                        string code = "Admin login is required for this command to execute";
                        LastOperationStatus = code;
                        return 24;
                    }
                    else if (apduResp.ToString().Contains("6985"))
                    {
                        string code = "An internal error in the ‘Upload Certificate’ state has been detected. Re-installing the card applet is required";
                        LastOperationStatus = code;
                        return 25;
                    }

                    return 23;
                }

            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                return 23;
            }

            LastOperationStatus = "Success!";

            return 0;

        }

        /// <summary>
        ///  This commands is issued to finalize the CA Signed Certificate upload. 
        ///  Once this command has been issued with a proper response (90 00), a certificate is now present in the system for reading. 
        ///  This command requires an administrator to be logged in to proceed to issue this command. 
        ///  Administrator may upload the final data fragment (less than 256 bytes) for the CA Signed 
        ///  Certificate or also choose to upload all data fragments using the ‘Upload CA Signed Certificate’ 
        ///  command and not have any remaining data fragments to be uploaded. 
        /// </summary>
        /// <param name="CA"></param>
        protected int finalizeCA(byte[] CA)
        {
            try
            {
                APDUCommand apduLogin = new APDUCommand(0x00, 0x50, 0x03, 0x04, null, 0);

                APDUParam apduParam = new APDUParam();

                apduParam.Data = CA;

                apduLogin.Update(apduParam);

                apduResp = iCard.Transmit(apduLogin);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6700"))
                    {
                        string code = "Wrong length of data have been supplied";
                        LastOperationStatus = code;
                        return 23;
                    }
                    else if (apduResp.ToString().Contains("6982"))
                    {
                        string code = "Admin login is required for this command to execute";
                        LastOperationStatus = code;
                        return 24;
                    }
                    else if (apduResp.ToString().Contains("6985"))
                    {
                        string code = "An internal error in the ‘Upload Certificate’ state has been detected. Re-installing the card applet is required";
                        LastOperationStatus = code;
                        return 25;
                    }

                    return 23;
                }

            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                return 23;
            }

            LastOperationStatus = "Success!";

            return 0;

        }

        /// <summary>
        /// This command allows the enquiry of the size of the stored CA Signed Certificate without needing login. 
        /// The size of the stored CA Signed Certificate will be returned in 2 bytes representing a ‘short’ data type. 
        /// </summary>
        /// <param name="CA"></param>
        protected int readCA(ref byte[] CA)
        {
            int CASize = 0;
            var retcode = readCASize(ref CASize);
            if (retcode != 0)
            {
                return retcode;
            }

            CertificateSize = CASize;

            for (int i = 0; i < CASize / 256; i++)
            {
                APDUCommand apduRead = new APDUCommand(0x00, 0x30, 0x00, 0x04, null, 256);

                APDUParam apduParam = new APDUParam();

                apduParam.Data = new byte[4];
                apduParam.Data[0] = (byte)i;
                apduParam.Data[1] = 0x00;
                apduParam.Data[2] = (byte)(i + 1);
                apduParam.Data[3] = 0x00;

                apduRead.Update(apduParam);

                apduResp = iCard.Transmit(apduRead);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A83"))
                    {
                        string code = "A CA Signed Certificate upload process have not been completed successfully and thus the certificate data could not be safely read. Finalizing the CA Signed Certificate upload is required to be executed before being able to read the certificate data fragments.";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 29;
                    }

                    if (apduResp.ToString().Contains("6984"))
                    {
                        string code = "An invalid certificate read range have been issued";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 30;
                    }

                    if (apduResp.ToString().Contains("6F00"))
                    {
                        string code = "An invalid certificate read range with negative reading values have been issued";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 31;
                    }

                    return 32;
                }

                var certificatePart = Encoding.UTF8.GetString(apduResp.Data);

                for (int j = 0; j < 256; j++)
                {
                    CA[i * 256 + j] = apduResp.Data[j];
                }
            }

            APDUCommand apduReadf = new APDUCommand(0x00, 0x30, 0x00, 0x04, null, 256);

            APDUParam apduParamf = new APDUParam();

            apduParamf.Data = new byte[4];
            apduParamf.Data[0] = (byte)(CASize / 256);
            apduParamf.Data[1] = 0x00;
            apduParamf.Data[2] = (byte)(CASize / 256);
            apduParamf.Data[3] = (byte)(CASize % 256);

            apduReadf.Update(apduParamf);

            apduResp = iCard.Transmit(apduReadf);
            if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
            {
                if (apduResp.ToString().Contains("6A83"))
                {
                    string code = "A CA Signed Certificate upload process have not been completed successfully and thus the certificate data could not be safely read. Finalizing the CA Signed Certificate upload is required to be executed before being able to read the certificate data fragments.";
                    LastOperationStatus = code;
                    log.Error(code);

                    return 29;
                }

                if (apduResp.ToString().Contains("6984"))
                {
                    string code = "An invalid certificate read range have been issued";
                    LastOperationStatus = code;
                    log.Error(code);

                    return 30;
                }

                if (apduResp.ToString().Contains("6F00"))
                {
                    string code = "An invalid certificate read range with negative reading values have been issued";
                    LastOperationStatus = code;
                    log.Error(code);

                    return 31;
                }

                return 32;
            }

            for (int j = 0; j < (CASize % 256); j++)
            {
                CA[(CASize / 256) * 256 + j] = apduResp.Data[j];
            }

            return 0;
        }

        /// <summary>
        /// This command allows the traversing and reading of the storage area containing the CA Signed Certificate without the need to login. 
        /// The minimum and maximum range of bytes (in 2 byte ‘short’ representation) are fed into the command to allow traversing and read 
        /// of data stored under the condition that the length of data to be read out should not exceed 256 bytes. 
        /// </summary>
        /// <param name="CAsize"></param>
        private int readCASize(ref int CAsize)
        {
            try
            {
                APDUCommand apduSize = new APDUCommand(0x00, 0x30, 0x00, 0x05, null, 2);

                apduResp = iCard.Transmit(apduSize);
                if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
                {
                    if (apduResp.ToString().Contains("6A83"))
                    {
                        string code = "A CA Signed Certificate upload process have not been completed successfully and thus the certificate size could not be determined. Finalizing the CA Signed Certificate upload is required to be executed before being able to read the certificate size.";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 27;
                    }

                    return 28;
                }

            }
            catch (Exception ex)
            {
                log.Info(ex.Message);

                return 28;
            }

            CAsize = apduResp.Data[0] * 256 + apduResp.Data[1];

            LastOperationStatus = "Success!";

            return 0;
        }
    }
}
