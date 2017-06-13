using log4net;
using System;
using System.Text;
using System.Threading;

namespace CardAPILib.InterfaceCL
{
    public class CardApiAPDUMessages
    {
        public int hContext;     //card reader context handle

        public int hCard;          //card connection handle

        private int ActiveProtocol;

        private int _retcode;

        protected int retcode
        {
            get
            {
                return _retcode;
            }

            set
            {
                _retcode = value;


            }
        }

        private static ILog log = LogManager.GetLogger(typeof(CardApiAPDUMessages));

        private int Aprotocol;
        private byte[] rdrlist = new byte[100];
        private byte[] array = new byte[256];
        private byte[] SendBuff = new byte[262];
        private byte[] RecvBuff = new byte[262];
        private byte[] tmpArray = new byte[56];
        private ModWinsCard.APDURec apdu = new ModWinsCard.APDURec();
        private int indx, SendBuffLen, RecvBuffLen;
        private string sTemp;
        private string returnValue;
        private string specRetCode;
        private bool connActive = false;

        private int retryCount = 0;

        public string LastOperationStatus { get; set; }

        public string SelectedReader { get; set; }

        public string CardUUID { get; set; }

        public string PublicKey { get; set; }

        public string SignedToken { get; set; }

        public int CertificateSize { get; set; }

        public int Connect2Card()
        {
            try
            {

                log.Info("Card Connection Begin");

                byte[] returnData = null;   // Will hold the reader names after the call to SCardListReaders
                int readerCount = 0;        // Total length of the reader names
                string readerNames = "";    // Will hold the reader names after converting from byte array to a single string.
                string[] readerList = null; // String array of the Reader Names
                int idx = 0;
    
                log.Info("Established using ScardEstablishedContext()");
                // Established using ScardEstablishedContext()
                retcode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, ref hContext);
                if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                {
                    log.Error(string.Format("SCardEstablishContext error {0}" , retcode));
                    LastOperationStatus = string.Format("SCardEstablishContext error {0}", retcode);
                    return 1;
                }
    
                // List PC/SC card readers installed in the system
    
                log.Info("List PC/SC card readers installed in the system");
                log.Info("Call SCardListReaders to get the total length of the reader names");
                // Call SCardListReaders to get the total length of the reader names
                retcode = ModWinsCard.SCardListReaders(hContext, null, null, ref readerCount);
                if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                {
                    log.Error(string.Format("SCardListReaders error {0}", retcode));
                    LastOperationStatus = string.Format("SCardListReaders error {0}", retcode);
                    return 2;
                }
                else
                {
                    returnData = new byte[readerCount];
    
                    log.Info("Call SCardListReaders this time passing the array to hold the return data.");
                    // Call SCardListReaders this time passing the array to hold the return data.
                    retcode = ModWinsCard.SCardListReaders(hContext, null, returnData, ref readerCount);
                    if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                    {
                        log.Error(string.Format("SCardListReaders2 error {0}", retcode));
                        LastOperationStatus = string.Format("SCardListReaders2 error {0}", retcode);
                        return 2;
                    }
                    else
                    {
                        log.Info("SCardListReaders...OK");
    
                        // Convert the return data to a string
                        readerNames = Encoding.ASCII.GetString(returnData);
    
                        // Parse the string and split the reader names. Delimited by \0.
                        readerList = readerNames.Split('\0');
    
                        //// For each string in the array, add them to the combo list
                        for (idx = 0; idx < readerList.Length; idx++)
                        {
                            log.Info(readerList[idx]);
                        }

                        if (connActive)
                        {
                            log.Info("Active Connection SCardDisconnect");
                            retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
                        }
    
                        if (readerList.Length <= 1)
                        {
                            log.Error(string.Format("Invalid Readers List"));
                            LastOperationStatus = string.Format("Invalid Readers List");
                            return 3;
                        }
    
                        SelectedReader = readerList[1];

                        while (true)
                        {
                            log.Info("Connect to the reader using hContext handle and obtain hCard handle");
                            // Connect to the reader using hContext handle and obtain hCard handle  
                            retcode = ModWinsCard.SCardConnect(hContext, readerList[1], ModWinsCard.SCARD_SHARE_EXCLUSIVE, 0 | 1, ref hCard, ref ActiveProtocol);
                            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                            {
                                if (retcode == -2146435061)
                                {
                                    log.Info("Connection OK");
                                    LastOperationStatus = "Connection OK";
                                    connActive = true;
                                    retcode = 0;

                                    break;
                                }
                                else
                                {
                                    if (retryCount <= 3)
                                    {
                                        retryCount++;
                                    }
                                    else
                                    {
                                        log.Error(string.Format("SCardConnect error {0}", retcode));
                                        LastOperationStatus = string.Format("SCardConnect error {0}", retcode);
                                        retryCount = 0;
                                        return 4;
                                    }
                                }

                                Thread.Sleep(1000);

                            }
                            else
                            {
                                log.Info("Connection OK");
                                LastOperationStatus = "Connection OK";
                                connActive = true;
                                break;
                            }

                            
                        }
                    }
                }

                retcode = 0;

            }
            catch(Exception ex)
            {
                log.Error(ex.ToString());

                return 5;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apdu"></param>
        /// <param name="resvLen"></param>
        protected void PerformTransmitAPDUGen(ref ModWinsCard.APDURec apdu, int resvLen)
        {
            try
            {
                log.Info("PerformTransmitAPDUGen called");

                ModWinsCard.SCARD_IO_REQUEST SendRequest;
                ModWinsCard.SCARD_IO_REQUEST RecvRequest;

                SendBuff = new byte[262];
                RecvBuff = new byte[262];
                array = new byte[256];
                SendBuffLen = 0;
                RecvBuffLen = 0;

                SendBuff[0] = apdu.bCLA;
                SendBuff[1] = apdu.bINS;
                SendBuff[2] = apdu.bP1;
                SendBuff[3] = apdu.bP2;
                SendBuff[4] = apdu.bP3;

                if (apdu.IsSend)
                {
                    for (indx = 0; indx < apdu.bP3; indx++)
                    {
                        SendBuff[5 + indx] = apdu.Data[indx];
                    }

                    SendBuffLen = 5 + apdu.bP3;
                    RecvBuffLen = resvLen;
                }
                else
                {
                    SendBuffLen = 5;
                    RecvBuffLen = 2 + apdu.bP3;
                }

                SendRequest.dwProtocol = Aprotocol;
                SendRequest.cbPciLength = 8;

                RecvRequest.dwProtocol = Aprotocol;
                RecvRequest.cbPciLength = 8;


                retcode = ModWinsCard.SCardTransmit(hCard, ref SendRequest, ref SendBuff[0], SendBuffLen, ref SendRequest, ref RecvBuff[0], ref RecvBuffLen);
                if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                {
                    log.Error(string.Format("SCardTransmit error {0}", retcode));
                    LastOperationStatus = string.Format("SCardTransmit error {0}", retcode);

                    //if (retryCount<=10)
                    //{
                    //    retryCount++;

                    int retvalue = Connect2Card();

                        if (retvalue == 0)
                        {
                            PerformTransmitAPDUGen(ref apdu, resvLen);
                        }

                    //}
                    //else
                    //{
                    //    if (retryCount > 10)
                    //    {
                    //        retryCount = 0;
                    //    }
                    //}

                    return;
                }

                sTemp = "";
                // do loop for sendbuffLen
                for (indx = 0; indx < SendBuffLen; indx++)
                {
                    sTemp = sTemp + " " + string.Format("{0:X2}", SendBuff[indx]);
                }

                log.Info(sTemp);

                returnValue = "";

                // do loop for RecvbuffLen
                for (indx = 0; indx < RecvBuffLen; indx++)
                {
                    returnValue = returnValue + string.Format("{0:X2}", RecvBuff[indx]);
                }

                log.Info(returnValue);

                if (!returnValue.Substring(returnValue.Length - 4 , 4).Equals("9000"))
                {
                    var codeValue = returnValue.Substring(returnValue.Length - 4, 4);

                    specRetCode = codeValue;
                    
                    retcode = -19999;

                    return;
                }

                if (apdu.IsSend == false)
                {
                    for (indx = 0; indx < apdu.bP3 + 2; indx++)
                    {
                        apdu.Data[indx] = RecvBuff[indx];
                    }
                }

                retcode = 0;
                retryCount = 0;
            }
            catch(Exception ex)
            {
                log.Error(ex.ToString());
            }
        }

        /// <summary>
        ///  This command allows the selecting of the SC-PKI applet 
        ///  which must be executed before issuing other APDU commands. 
        /// </summary>
        protected int SelectFile()
        {
            log.Info("SelectFile");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0xA4;          // INS
            apdu.bP1 = 0x04;           // P1
            apdu.bP2 = 0x00;           // P2
            apdu.bP3 = 0x07;           // P3
            apdu.Data[0] = 0x73;       // 
            apdu.Data[1] = 0x63;       // 
            apdu.Data[2] = 0x70;       // 
            apdu.Data[3] = 0x6B;       // 
            apdu.Data[4] = 0x69;       // 
            apdu.Data[5] = 0x01;       // 
            apdu.Data[6] = 0x00;       // 
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 2);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                LastOperationStatus = string.Format("SelectFile error {0}", retcode);
                log.Error(string.Format("SelectFile error {0}", retcode));
                return 5;
            }

            return 0;
        }

        /// <summary>
        /// This command queries the smart card for the 20 byte UUID which is derived by 
        /// using SHA256 to derive a 32 byte hash from the RSA Public Modulus of the card 
        /// and then extracting the first 20 bytes of the hashed result as the UUID for the smart card. 
        /// </summary>
        protected int getUUID()
        { 
            log.Info("getUUID");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x03;          // INS
            apdu.bP1 = 0x00;           // P1
            apdu.bP2 = 0x06;           // P2
            apdu.bP3 = 0x00;           // P3
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 22);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                log.Error(string.Format("getUUID error {0}", retcode));
                return 6;
            }

            byte[] destination = new byte[20];

            Array.Copy(RecvBuff, destination, 20);

            if (!string.IsNullOrEmpty(returnValue))
            {
                CardUUID = Convert.ToBase64String(destination);
            }

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
            log.Info("userLogin");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x01;          // INS
            apdu.bP1 = 0x00;           // P1
            apdu.bP2 = 0x01;           // P2
            apdu.bP3 = 0x06;           // P3

            for (int i = 0; i < 6; i++)
            {
                apdu.Data[i] = pinCode[i];
            }
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 2);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6984"))
                    { 
                        string code = "Invalid login data due to User PIN being locked due to too many bad retries or simply wrong PIN code";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 7;
                    }
                }
                else
                {
                    log.Error(string.Format("userLogin error {0}", retcode));
                    LastOperationStatus = string.Format("userLogin error {0}", retcode);
                }
                
                return 8;
            }

            LastOperationStatus = "Login Success!";

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
            log.Info("adminLogin");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x01;          // INS
            apdu.bP1 = 0x00;           // P1
            apdu.bP2 = 0x02;           // P2
            apdu.bP3 = 0x08;           // P3

            for (int i = 0; i < 8; i++)
            {
                apdu.Data[i] = pukCode[i];
            }
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 2);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6984"))
                    {
                        string code = "Invalid login data due to User PIN being locked due to too many bad retries or simply wrong PIN code";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 9;
                    }
                }
                else
                {
                    log.Error(string.Format("adminLogin error {0}", retcode));
                    LastOperationStatus = string.Format("adminLogin error {0}", retcode);
                }

                return 10;
            }

            LastOperationStatus = "Login Success!";

            return 0;
        }

        /// <summary>
        /// This command is for a user to issue for the changing of the User PIN. 
        /// The user must already be logged in to proceed to issue this command to change User PIN. 
        /// </summary>
        /// <param name="newPinCode"></param>
        protected int userChangePin(byte[] newPinCode)
        {
            log.Info("userChangePin");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x02;          // INS
            apdu.bP1 = 0x01;           // P1
            apdu.bP2 = 0x01;           // P2
            apdu.bP3 = 0x06;           // P3

            for (int i = 0; i < 6; i++)
            {
                apdu.Data[i] = newPinCode[i];
            }
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 2);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6700"))
                    {
                        string code = "Wrong length of PIN. PIN must be 4 to 16 bytes long";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 11;
                    }
                    else if (specRetCode.Equals("6982"))
                    {
                        string code = "Login is required for this command to execute";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 12;
                    }
                }
                else
                {
                    log.Error(string.Format("userChangePin error {0}", retcode));
                    LastOperationStatus = string.Format("userChangePin error {0}", retcode);
                }

                return 13;
            }

            LastOperationStatus = "Pin Change Success!";

            return 0;
        }

        /// <summary>
        /// This command is for am administrator to issue for the changing of the PUK. 
        /// The administrator must already be logged in to proceed to issue this command to change the PUK 
        /// </summary>
        /// <param name="newPinCode"></param>
        protected int adminChangePuk(byte[] newPukCode)
        {
            log.Info("userChangePin");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x02;          // INS
            apdu.bP1 = 0x01;           // P1
            apdu.bP2 = 0x02;           // P2
            apdu.bP3 = 0x08;           // P3

            for (int i = 0; i < 8; i++)
            {
                apdu.Data[i] = newPukCode[i];
            }
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 2);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6700"))
                    {
                        string code = "Wrong length of PIN. PIN must be 4 to 16 bytes long";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 14;
                    }
                    else if (specRetCode.Equals("6982"))
                    {
                        string code = "Login is required for this command to execute";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 15;
                    }
                }
                else
                {
                    log.Error(string.Format("adminChangePuk error {0}", retcode));
                    LastOperationStatus = string.Format("adminChangePuk error {0}", retcode);
                }

                return 16;
            }

            LastOperationStatus = "Puk Change Success!";

            return 0;
        }


        /// <summary>
        /// This command is for the administrator to reset and unblock the User PIN to the default User PIN (123456). 
        /// The administrator must already be logged in to proceed to issue this command.
        /// </summary>
        protected int adminResetUserPin()
        {
            log.Info("adminResetUserPin");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x02;          // INS
            apdu.bP1 = 0x02;           // P1
            apdu.bP2 = 0x01;           // P2
            apdu.bP3 = 0x00;           // P3

            PerformTransmitAPDUGen(ref apdu, 2);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6982"))
                    {
                        string code = "Login is required for this command to execute";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 17;
                    }
                }
                else
                {
                    log.Error(string.Format("adminResetUserPin error {0}", retcode));
                    LastOperationStatus = string.Format("adminResetUserPin error {0}", retcode);
                }

                return 18;
            }

            LastOperationStatus = "User Pin reset Success!";

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
            log.Info("readPublicKeyModulus");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x03;          // INS
            apdu.bP1 = 0x00;           // P1
            apdu.bP2 = 0x03;           // P2
            apdu.bP3 = 0x00;           // P3
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 258);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6A83"))
                    {
                        string code = "Public Key Modulus cannot be found due to possible fault in the card. Reinstallation of card applet is necessary";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 19;
                    }
                }
                else
                {
                    log.Error(string.Format("readPublicKeyModulus error {0}", retcode));
                    LastOperationStatus = string.Format("readPublicKeyModulus error {0}", retcode);
                }

                return 20;
            }

            byte[] destination = new byte[256];

            Array.Copy(RecvBuff, destination, 256);

            if (!string.IsNullOrEmpty(returnValue))
            {
                PublicKey = Convert.ToBase64String(destination);
            }

            LastOperationStatus = "Public Key Success!";

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
            log.Info("signToken");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x04;          // INS
            apdu.bP1 = 0x00;           // P1
            apdu.bP2 = 0x00;           // P2
            apdu.bP3 = 0x40;           // P3

            for (int i = 0; i < 0x40; i++) //0x40
            {
                apdu.Data[i] = token[i];
            }
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 258);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6982"))
                    {
                        string code = "Login is required for this command to execute";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 21;
                    }
                }
                else
                {
                    log.Error(string.Format("signToken error {0}", retcode));
                    LastOperationStatus = string.Format("signToken error {0}", retcode);
                }

                return 22;
            }

            byte[] destination = new byte[256];

            Array.Copy(RecvBuff, destination, 256);

            if (!string.IsNullOrEmpty(returnValue))
            {
                SignedToken = Convert.ToBase64String(destination);
            }

            LastOperationStatus = "Sign Token Success!";

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
                uploadCAPart(CACurrentPart);
                if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                {
                    if (retcode == -19999)
                    {
                        if (specRetCode.Equals("6982"))
                        {
                            string code = "Admin login is required for this command to execute";
                            LastOperationStatus = code;
                            log.Error(code);

                            return 23;
                        }

                        if (specRetCode.Equals("6700"))
                        {
                            string code = "Wrong length of data have been supplied";
                            LastOperationStatus = code;
                            log.Error(code);

                            return 24;
                        }

                        if (specRetCode.Equals("6985"))
                        {
                            string code = "An internal error in the ‘Upload Certificate’ state has been detected. Re-installing the card applet is required";
                            LastOperationStatus = code;
                            log.Error(code);

                            return 25;
                        }
                    }
                    else
                    {
                        log.Error(string.Format("signToken error {0}", retcode));
                        LastOperationStatus = string.Format("signToken error {0}", retcode);
                    }

                    return 26;
                }
            }

            for (int i = 0; i < (CA.Length % 255); i++)
            {
                CAFinalizePart[i] = CA[i + CAPartsCount * 255];
            }


            finalizeCA(CAFinalizePart);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6982"))
                    {
                        string code = "Admin login is required for this command to execute";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 23;
                    }

                    if (specRetCode.Equals("6700"))
                    {
                        string code = "Wrong length of data have been supplied";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 24;
                    }

                    if (specRetCode.Equals("6985"))
                    {
                        string code = "An internal error in the ‘Upload Certificate’ state has been detected. Re-installing the card applet is required";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 25;
                    }
                }
                else
                {
                    log.Error(string.Format("signToken error {0}", retcode));
                    LastOperationStatus = string.Format("signToken error {0}", retcode);
                }

                return 26;
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
            log.Info("uploadCAPart");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x05;          // INS
            apdu.bP1 = 0x00;           // P1
            apdu.bP2 = 0x04;           // P2
            apdu.bP3 = (byte)CA.Length;           // P3

            for (int i = 0; i < CA.Length; i++)
            {
                apdu.Data[i] = CA[i];
            }
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 2);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6700"))
                    {
                        string code = "Wrong length of data have been supplied";
                        LastOperationStatus = code;
                        log.Error(code);
                    }
                    else if (specRetCode.Equals("6982"))
                    {
                        string code = "Admin login is required for this command to execute";
                        LastOperationStatus = code;
                        log.Error(code);
                    }
                    else if (specRetCode.Equals("6985"))
                    {
                        string code = "An internal error in the ‘Upload Certificate’ state has been detected. Re-installing the card applet is required";
                        LastOperationStatus = code;
                        log.Error(code);
                    }
                }
                else
                {
                    log.Error(string.Format("uploadCAPart error {0}", retcode));
                    LastOperationStatus = string.Format("uploadCAPart error {0}", retcode);
                }

                return 1;
            }

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
            log.Info("finalizeCA");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x05;          // INS
            apdu.bP1 = 0x03;           // P1
            apdu.bP2 = 0x04;           // P2
            apdu.bP3 = (byte)CA.Length;           // P3

            for (int i = 0; i < CA.Length; i++)
            {
                apdu.Data[i] = CA[i];
            }
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 2);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6700"))
                    {
                        string code = "Wrong length of data have been supplied";
                        LastOperationStatus = code;
                        log.Error(code);
                    }
                    else if (specRetCode.Equals("6982"))
                    {
                        string code = "Admin login is required for this command to execute";
                        LastOperationStatus = code;
                        log.Error(code);
                    }
                    else if (specRetCode.Equals("6985"))
                    {
                        string code = "An internal error in the ‘Upload Certificate’ state has been detected. Re-installing the card applet is required";
                        LastOperationStatus = code;
                        log.Error(code);
                    }
                }
                else
                {
                    log.Error(string.Format("finalizeCA error {0}", retcode));
                    LastOperationStatus = string.Format("finalizeCA error {0}", retcode);
                }

                return 1;
            }

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
            readCASize(ref CASize);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6A83"))
                    {
                        string code = "A CA Signed Certificate upload process have not been completed successfully and thus the certificate size could not be determined. Finalizing the CA Signed Certificate upload is required to be executed before being able to read the certificate size.";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 27;
                    }
                }
                else
                {
                    log.Error(string.Format("uploadCAPart error {0}", retcode));
                    LastOperationStatus = string.Format("uploadCAPart error {0}", retcode);
                }

                return 28;
            }

            CertificateSize = CASize;

            for (int i = 0; i < CASize / 256; i++)
            {
                apdu.Data = array;
                apdu.bCLA = 0x00;          // CLA
                apdu.bINS = 0x03;          // INS
                apdu.bP1 = 0x00;           // P1
                apdu.bP2 = 0x04;           // P2
                apdu.bP3 = 0x04;           // P3
                
                apdu.Data[0] = (byte)i;
                apdu.Data[1] = 0x00;
                apdu.Data[2] = (byte)(i + 1);
                apdu.Data[3] = 0x00;
                apdu.IsSend = true;

                PerformTransmitAPDUGen(ref apdu, 258);
                if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                {
                    if (retcode == -19999)
                    {
                        if (specRetCode.Equals("6A83"))
                        {
                            string code = "A CA Signed Certificate upload process have not been completed successfully and thus the certificate data could not be safely read. Finalizing the CA Signed Certificate upload is required to be executed before being able to read the certificate data fragments.";
                            LastOperationStatus = code;
                            log.Error(code);

                            return 29;
                        }
                        else if (specRetCode.Equals("6984"))
                        {
                            string code = "An invalid certificate read range have been issued";
                            LastOperationStatus = code;
                            log.Error(code);

                            return 30;
                        }
                        else if (specRetCode.Equals("6F00"))
                        {
                            string code = "An invalid certificate read range with negative reading values have been issued";
                            LastOperationStatus = code;
                            log.Error(code);

                            return 31;
                        }
                    }
                    else
                    {
                        log.Error(string.Format("uploadCAPart error {0}", retcode));
                        LastOperationStatus = string.Format("uploadCAPart error {0}", retcode);
                    }

                    return 32;
                }

                var certificatePart = Encoding.UTF8.GetString(RecvBuff);

                for (int j = 0; j < 256; j++)
                {
                    CA[i * 256 + j] = RecvBuff[j];
                }
            }

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x03;          // INS
            apdu.bP1 = 0x00;           // P1
            apdu.bP2 = 0x04;           // P2
            apdu.bP3 = 0x04;           // P3


            apdu.Data[0] = (byte)(CASize / 256);
            apdu.Data[1] = 0x00;
            apdu.Data[2] = (byte)(CASize / 256);
            apdu.Data[3] = (byte)(CASize % 256);
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 257);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6A83"))
                    {
                        string code = "A CA Signed Certificate upload process have not been completed successfully and thus the certificate data could not be safely read. Finalizing the CA Signed Certificate upload is required to be executed before being able to read the certificate data fragments.";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 29;
                    }
                    else if (specRetCode.Equals("6984"))
                    {
                        string code = "An invalid certificate read range have been issued";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 30;
                    }
                    else if (specRetCode.Equals("6F00"))
                    {
                        string code = "An invalid certificate read range with negative reading values have been issued";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 31;
                    }
                }
                else
                {
                    log.Error(string.Format("uploadCAPart error {0}", retcode));
                    LastOperationStatus = string.Format("uploadCAPart error {0}", retcode);
                }

                return 32;
            }

            for (int j = 0; j < (CASize % 256); j++)
            {
                CA[(CASize / 256) * 256 + j] = RecvBuff[j];
            }

            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6A83"))
                    {
                        string code = "A CA Signed Certificate upload process have not been completed successfully and thus the certificate data could not be safely read. Finalizing the CA Signed Certificate upload is required to be executed before being able to read the certificate data fragments.";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 29;
                    }
                    else if (specRetCode.Equals("6984"))
                    {
                        string code = "An invalid certificate read range have been issued";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 30;
                    }
                    else if (specRetCode.Equals("6F00"))
                    {
                        string code = "An invalid certificate read range with negative reading values have been issued";
                        LastOperationStatus = code;
                        log.Error(code);

                        return 31;
                    }
                }
                else
                {
                    log.Error(string.Format("uploadCAPart error {0}", retcode));
                    LastOperationStatus = string.Format("uploadCAPart error {0}", retcode);
                }

                return 32;
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
            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x03;          // INS
            apdu.bP1 = 0x00;           // P1
            apdu.bP2 = 0x05;           // P2
            apdu.bP3 = 0x00;           // P3
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 4);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            {
                if (retcode == -19999)
                {
                    if (specRetCode.Equals("6A83"))
                    {
                        string code = "A CA Signed Certificate upload process have not been completed successfully and thus the certificate size could not be determined. Finalizing the CA Signed Certificate upload is required to be executed before being able to read the certificate size.";
                        LastOperationStatus = code;
                        log.Error(code);
                    }
                }
                else
                {
                    log.Error(string.Format("uploadCAPart error {0}", retcode));
                    LastOperationStatus = string.Format("uploadCAPart error {0}", retcode);
                }

                return 1;
            }

            CAsize = (int)RecvBuff[0] * 256 + (int)RecvBuff[1];

            return 0;
        }
    }
}
