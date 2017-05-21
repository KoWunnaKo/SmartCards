using Advanced_Device_Programming;
using CardAPILib.CardAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApiLibTest
{
    class Program
    {
        static int hContext;       //card reader context handle
        static int hCard;          //card connection handle
        static int ActiveProtocol, retcode;
        static int Aprotocol;
        static byte[] rdrlist = new byte[100];
        static byte[] array = new byte[256];
        static byte[] SendBuff = new byte[262];
        static byte[] RecvBuff = new byte[262];
        static byte[] tmpArray = new byte[56];
        static ModWinsCard.APDURec apdu = new ModWinsCard.APDURec();
        static int indx, SendBuffLen, RecvBuffLen;
        static string sTemp;
        static bool connActive = false;

        static void Main(string[] args)
        {
            CardApiController cp = new CardApiController();

            //string uuid = string.Empty;

            //cp.getUUId(out uuid);

            //Console.WriteLine(uuid);

            string signedToken = string.Empty;

            //cp.userPinCodeLogin("123456");

            cp.adminPukCodeLogin("12345678");

            //cp.signToken("1234567891234567891234657891234567891234567891234657891234567891", out signedToken);

            //cp.adminRestoreUserPin();

            string certificate = "MIIFvzCCBKegAwIBAgITVQAAACB6RwNlWwaulQAAAAAAIDANBgkqhkiG9w0BAQ0F" +
                                 "ADBHMRUwEwYKCZImiZPyLGQBGRYFbG9jYWwxFDASBgoJkiaJk/IsZAEZFgRha2hv" +
                                 "MRgwFgYDVQQDEw9ha2hvLUNBMDFTUlYtQ0EwHhcNMTcwNTEzMDcyMDU5WhcNMTgw" +
                                 "NTEzMDcyMDU5WjBfMRUwEwYKCZImiZPyLGQBGRYFbG9jYWwxFDASBgoJkiaJk/Is" +
                                 "ZAEZFgRha2hvMQ0wCwYDVQQLEwRBa2hvMQ4wDAYDVQQLEwVVc2VyczERMA8GA1UE" +
                                 "AxMIaGVscGRlc2swgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBAK/AjJ3JtVSx" +
                                 "1Vun2UYbgj3xZfJy63VqTZRIarJYGJ+QubzW1uFhsOg9LrXfby5a8vpcaHXO9zpO" +
                                 "SAxp/8iDBG/asq1X4SrEfaqj1nZwg+vzJfR4gjJbN5231fFBe3nTve+pwSfQyQCS" +
                                 "MAjlINM7CMiyn7sfXwcS4l3LlNwxHX3NAgMBAAGjggMOMIIDCjAOBgNVHQ8BAf8E" +
                                 "BAMCBaAwKQYDVR0lBCIwIAYIKwYBBQUHAwQGCCsGAQUFBwMCBgorBgEEAYI3FAIC" +
                                 "MCkGCSsGAQQBgjcUAgQcHhoAUwBtAGEAcgB0AGMAYQByAGQAVQBzAGUAcjAdBgNV" +
                                 "HQ4EFgQUdfh6mguFl5EqWomL5sAcLtuB9E0wHwYDVR0jBBgwFoAU9c9TkANJF6/C" +
                                 "GD4bbXNJrhY0WIMwgcwGA1UdHwSBxDCBwTCBvqCBu6CBuIaBtWxkYXA6Ly8vQ049" +
                                 "YWtoby1DQTAxU1JWLUNBLENOPWNhMDFzcnYsQ049Q0RQLENOPVB1YmxpYyUyMEtl" +
                                 "eSUyMFNlcnZpY2VzLENOPVNlcnZpY2VzLENOPUNvbmZpZ3VyYXRpb24sREM9YWto" +
                                 "byxEQz1sb2NhbD9jZXJ0aWZpY2F0ZVJldm9jYXRpb25MaXN0P2Jhc2U/b2JqZWN0" +
                                 "Q2xhc3M9Y1JMRGlzdHJpYnV0aW9uUG9pbnQwggEbBggrBgEFBQcBAQSCAQ0wggEJ" +
                                 "MIGtBggrBgEFBQcwAoaBoGxkYXA6Ly8vQ049YWtoby1DQTAxU1JWLUNBLENOPUFJ" +
                                 "QSxDTj1QdWJsaWMlMjBLZXklMjBTZXJ2aWNlcyxDTj1TZXJ2aWNlcyxDTj1Db25m" +
                                 "aWd1cmF0aW9uLERDPWFraG8sREM9bG9jYWw/Y0FDZXJ0aWZpY2F0ZT9iYXNlP29i" +
                                 "amVjdENsYXNzPWNlcnRpZmljYXRpb25BdXRob3JpdHkwVwYIKwYBBQUHMAGGS2h0" +
                                 "dHA6Ly9jYTAxc3J2LmFraG8ubG9jYWwvQ2VydEVucm9sbC9jYTAxc3J2LmFraG8u" +
                                 "bG9jYWxfYWtoby1DQTAxU1JWLUNBLmNydDAuBgNVHREEJzAloCMGCisGAQQBgjcU" +
                                 "AgOgFQwTaGVscGRlc2tAYWtoby5sb2NhbDBEBgkqhkiG9w0BCQ8ENzA1MA4GCCqG" +
                                 "SIb3DQMCAgIAgDAOBggqhkiG9w0DBAICAIAwBwYFKw4DAgcwCgYIKoZIhvcNAwcw" +
                                 "DQYJKoZIhvcNAQENBQADggEBAA5kcFCc/8TI4L1mqqmNkYqXVhQp7J1gggcrYbCv" +
                                 "ZR4KALxJZtSjuKH0vyXp/IHntUj3gd7RN8sGfP8SoEapCAOfPAlNVEVduTLOxuAU" +
                                 "HlD4qovUvnIxLlG0ehLQcXyks0zD0TxqZ8TEwD9Ehos81EM1Kt5YCGl6h9Fd3Ic/" +
                                 "aK4i8slrejAxvm39BCqdpqpR6CWiDtV9yG3ld5xS848Uueeg7Q3ViCBAmCEmIYkp" +
                                 "dtIU5K44yDZwpsXblvok/Gjf7hPcQPJK0p0jBA2my2QCwudAes7mfq8hjVrf/+44" +
                                 "JqsYeaFV6e1v4TEVmZAadT8SPNhVmIzjBl8ls9NQNjtVMTU=";

            cp.WriteCert(certificate);

            string certFormCard = string.Empty;

            cp.LoadCert(out certFormCard);

            Console.WriteLine(cp.LastOperationStatus);
            Console.WriteLine(certFormCard);
            

            if (certificate.Equals(certFormCard))
            {
                Console.WriteLine("Equals");
            }

            Console.ReadKey();

            //byte[] returnData = null;   // Will hold the reader names after the call to SCardListReaders
            //int readerCount = 0;        // Total length of the reader names
            //string readerNames = "";    // Will hold the reader names after converting from byte array to a single string.
            //string[] readerList = null; // String array of the Reader Names
            //int idx = 0;

            //// Established using ScardEstablishedContext()
            //retcode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, ref hContext);
            //if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            //{
            //    //displayOut(1, retcode, "");
            //    return;
            //}

            //// List PC/SC card readers installed in the system

            //// Call SCardListReaders to get the total length of the reader names
            //retcode = ModWinsCard.SCardListReaders(hContext, null, null, ref readerCount);
            //if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            //{
            //    //displayOut(1, retcode, "");
            //    return;
            //}
            //else
            //{
            //    returnData = new byte[readerCount];

            //    // Call SCardListReaders this time passing the array to hold the return data.
            //    retcode = ModWinsCard.SCardListReaders(hContext, null, returnData, ref readerCount);
            //    if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            //    {
            //        //displayOut(1, retcode, "");
            //        return;
            //    }
            //    else
            //    {
            //        //displayOut(0, 0, "SCardListReaders...OK");

            //        // Convert the return data to a string
            //        readerNames = System.Text.ASCIIEncoding.ASCII.GetString(returnData);

            //        // Parse the string and split the reader names. Delimited by \0.
            //        readerList = readerNames.Split('\0');

            //        //// Clear the combo list of all items.
            //        //cbReader.Items.Clear();

            //        //// For each string in the array, add them to the combo list
            //        for (idx = 0; idx < readerList.Length; idx++)
            //        {
            //            //cbReader.Items.Add(readerList[idx]);

            //            Console.WriteLine(readerList[idx]);
            //        }

            //        if (connActive)
            //            retcode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            //        // Connect to the reader using hContext handle and obtain hCard handle  
            //        retcode = ModWinsCard.SCardConnect(hContext, readerList[1], ModWinsCard.SCARD_SHARE_EXCLUSIVE, 0 | 1, ref hCard, ref ActiveProtocol);
            //        if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            //        {
            //            //displayOut(1, retcode, "");
            //            Console.WriteLine("Not Connected!!!");
            //            return;
            //        }
            //        else
            //        {
            //            //displayOut(0, 0, "Connection OK");
            //            Console.WriteLine("Connected!!!");
            //            connActive = true;
            //        }

            //        #region SelectApplet

            //        //Console.WriteLine("Select Applet");
            //        //apdu.Data = array;

            //        //apdu.bCLA = 0x00;          // CLA
            //        //apdu.bINS = 0xA4;          // INS
            //        //apdu.bP1 = 0x04;           // P1
            //        //apdu.bP2 = 0x00;           // P2
            //        //apdu.bP3 = 0x07;           // P3
            //        //apdu.Data[0] = 0x73;       // A
            //        //apdu.Data[1] = 0x63;       // C
            //        //apdu.Data[2] = 0x70;       // O
            //        //apdu.Data[3] = 0x6B;       // S
            //        //apdu.Data[4] = 0x69;       // T
            //        //apdu.Data[5] = 0x01;       // E
            //        //apdu.Data[6] = 0x00;       // S
            //        //apdu.IsSend = true;

            //        //PerformTransmitAPDUGen(ref apdu);
            //        //if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            //        //    return;

            //        //Console.WriteLine("End Select Applet");
            //        //#endregion

            //        //#region UUID
            //        //if (false)
            //        //{
            //        //    apdu = new ModWinsCard.APDURec();

            //        //    apdu.Data = array;

            //        //    apdu.bCLA = 0x00;          // CLA
            //        //    apdu.bINS = 0x03;          // INS
            //        //    apdu.bP1 = 0x00;           // P1
            //        //    apdu.bP2 = 0x06;           // P2
            //        //    apdu.bP3 = 0x00;           // P3

            //        //    PerformTransmitAPDUUUID(ref apdu);
            //        //    if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            //        //        return;
            //        //}

            //        //#endregion

            //        //#region User Pin Login
            //        //Console.WriteLine("User Pin Login");
            //        //apdu.Data = array;

            //        //apdu.bCLA = 0x00;          // CLA
            //        //apdu.bINS = 0x01;          // INS
            //        //apdu.bP1 = 0x00;           // P1
            //        //apdu.bP2 = 0x01;           // P2
            //        //apdu.bP3 = 0x06;           // P3
            //        //apdu.Data[0] = 0x31;       // A
            //        //apdu.Data[1] = 0x32;       // C
            //        //apdu.Data[2] = 0x33;       // O
            //        //apdu.Data[3] = 0x34;       // S
            //        //apdu.Data[4] = 0x35;       // T
            //        //apdu.Data[5] = 0x36;       // E
            //        //apdu.IsSend = true;

            //        //PerformTransmitAPDUGen(ref apdu);
            //        //if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            //        //    return;

            //        //Console.WriteLine("End User Pin Login");

            //        //#endregion

            //        //#region Admin Pin Login
            //        //Console.WriteLine("Admin Pin Login");
            //        //apdu.Data = array;

            //        //apdu.bCLA    = 0x00;       // CLA
            //        //apdu.bINS    = 0x01;       // INS
            //        //apdu.bP1     = 0x00;       // P1
            //        //apdu.bP2     = 0x02;       // P2
            //        //apdu.bP3     = 0x08;       // P3
            //        //apdu.Data[0] = 0x31;       // A
            //        //apdu.Data[1] = 0x32;       // C
            //        //apdu.Data[2] = 0x33;       // O
            //        //apdu.Data[3] = 0x34;       // S
            //        //apdu.Data[4] = 0x35;       // T
            //        //apdu.Data[5] = 0x36;       // E
            //        //apdu.Data[6] = 0x37;       // E
            //        //apdu.Data[7] = 0x38;       // E
            //        //apdu.IsSend = true;

            //        //PerformTransmitAPDUGen(ref apdu);
            //        //if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            //        //    return;

            //        //Console.WriteLine("End Admin Pin Login");

            //        //#endregion

            //        //#region User Pin Login
            //        //Console.WriteLine("User Pin Login");
            //        //apdu.Data = array;

            //        //apdu.bCLA = 0x00;          // CLA
            //        //apdu.bINS = 0x01;          // INS
            //        //apdu.bP1 = 0x00;           // P1
            //        //apdu.bP2 = 0x01;           // P2
            //        //apdu.bP3 = 0x06;           // P3
            //        //apdu.Data[0] = 0x31;       // A
            //        //apdu.Data[1] = 0x32;       // C
            //        //apdu.Data[2] = 0x33;       // O
            //        //apdu.Data[3] = 0x34;       // S
            //        //apdu.Data[4] = 0x35;       // T
            //        //apdu.Data[5] = 0x36;       // E
            //        //apdu.IsSend = true;

            //        //PerformTransmitAPDUGen(ref apdu);
            //        //if (retcode != ModWinsCard.SCARD_S_SUCCESS)
            //        //    return;

            //        //Console.WriteLine("End User Pin Login");

            //        #endregion

            //        SelectFile();



            //        //getUUID();

            //        string pinCode = "123456";

            //        userLogin(Encoding.UTF8.GetBytes(pinCode));

            //        //pinCode = "654321";

            //        //userChangePin(Encoding.UTF8.GetBytes(pinCode));

            //        //string pukCode = "12345678";

            //        //adminLogin(Encoding.UTF8.GetBytes(pukCode));

            //        //adminResetUserPin();

            //        readPublicKeyModulus();

            //        Console.ReadKey();
            //    }
            //}

        }

        /// <summary>
        /// 
        /// </summary>
        static private void SelectFile()
        {
            Console.WriteLine("Select Applet");

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
                Console.WriteLine("Error HAppened");
                return;
            }

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        static private void getUUID()
        {
            Console.WriteLine("getUUID");

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
                Console.WriteLine("Error HAppened");
                return;
            }

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pinCode"></param>
        static private void userLogin(byte[] pinCode)
        {
            Console.WriteLine("userLogin");

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
                Console.WriteLine("Error HAppened");
                return;
            }

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pukCode"></param>
        static private void adminLogin(byte[] pukCode)
        {
            Console.WriteLine("adminLogin");

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
                return;

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPinCode"></param>
        static private void userChangePin(byte[] newPinCode)
        {
            Console.WriteLine("userChangePin");

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
                return;

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        static private void adminResetUserPin()
        {
            Console.WriteLine("adminResetUserPin");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x02;          // INS
            apdu.bP1 = 0x02;           // P1
            apdu.bP2 = 0x01;           // P2
            apdu.bP3 = 0x00;           // P3

            PerformTransmitAPDUGen(ref apdu, 2);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                return;

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        static private void readPublicKeyModulus()
        {
            Console.WriteLine("readPublicKeyModulus");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x03;          // INS
            apdu.bP1 = 0x00;           // P1
            apdu.bP2 = 0x03;           // P2
            apdu.bP3 = 0x00;           // P3
            apdu.IsSend = true;

            //Ибрагим Алланазаров, [12.05.17 11:47]
            PerformTransmitAPDUGen(ref apdu, 258);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                return;

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        static private void signToken(byte[] token)
        {
            Console.WriteLine("signToken");

            apdu.Data = array;
            apdu.bCLA = 0x00;          // CLA
            apdu.bINS = 0x04;          // INS
            apdu.bP1 = 0x00;           // P1
            apdu.bP2 = 0x00;           // P2
            apdu.bP3 = 0x40;           // P3

            for (int i = 0; i < 0x40; i++)
            {
                apdu.Data[i] = token[i];
            }
            apdu.IsSend = true;

            PerformTransmitAPDUGen(ref apdu, 256);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                return;

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CA"></param>
        static private void uploadCA(byte[] CA)
        {
            Console.WriteLine("uploadCA");

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
                    return;
            }
            for (int i = 0; i < (CA.Length % 255); i++)
            {
                CAFinalizePart[i] = CA[i + CAPartsCount * 255];
            }
            finalizeCA(CAFinalizePart);
            if (retcode != ModWinsCard.SCARD_S_SUCCESS)
                return;

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CA"></param>
        static private void uploadCAPart(byte[] CA)
        {
            Console.WriteLine("uploadCAPart");

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
                return;

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CA"></param>
        static private void finalizeCA(byte[] CA)
        {
            Console.WriteLine("finalizeCA");

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
                return;

            Console.WriteLine("=================================");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apdu"></param>
        /// <param name="resvLen"></param>
        static private void PerformTransmitAPDUGen(ref ModWinsCard.APDURec apdu, int resvLen)
        {
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
                //displayOut(1, retcode, "");
                return;
            }

            sTemp = "";
            // do loop for sendbuffLen
            for (indx = 0; indx < SendBuffLen; indx++)
            {
                sTemp = sTemp + " " + string.Format("{0:X2}", SendBuff[indx]);
            }

            // Display Send Buffer Value
            //displayOut(2, 0, sTemp);

            sTemp = "";

            // do loop for RecvbuffLen
            for (indx = 0; indx < RecvBuffLen; indx++)
            {
                sTemp = sTemp + string.Format("{0:X2}", RecvBuff[indx]);
            }
            Console.WriteLine(sTemp);

            //if (!sTemp.Equals("9000"))
            //{
            //    retcode = int.Parse(sTemp);

            //    return;
            //}

            // Display Receive Buffer Value
            //displayOut(3, 0, sTemp);

            if (apdu.IsSend == false)
            {
                for (indx = 0; indx < apdu.bP3 + 2; indx++)
                {
                    apdu.Data[indx] = RecvBuff[indx];
                }
            }
        }
    }
}
