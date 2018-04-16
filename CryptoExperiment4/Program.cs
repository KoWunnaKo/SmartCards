using CardAPILib.InterfaceCL;
using GID_CardApi;
using Iso18013Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SmartCardApi.MRZ;
using SmartCardApi.SmartCard;
using System.Globalization;

namespace CryptoExperiment4
{
    class Program 
    {

        static void Main(string[] args)
        {
            RunBurun();

            //var key = "404142434445464748494A4B4C4D4E4F";

            //var keyByte = Enumerable.Range(0, key.Length)
            //         .Where(x => x % 2 == 0)
            //         .Select(x => Convert.ToByte(key.Substring(x, 2), 16))
            //         .ToArray();

            //Console.ReadKey();

        }

        static  void RunBurun()
        {
            //InstallCardIDL();

            //var res = CardApi.WriteVehilcleInfo(LoadJsonVR_Private()).Result;

            var res = CardApi.WriteDriverInfo(LoadJsonIDL()).Result;

            Console.WriteLine(res);



           // var res = CardApi.WriteDriverInfo(LoadJsonIDL()).Result;

        }

        #region Comment

        ////CardProcIDL();
        //var res = CardApi.WriteDriverInfo(LoadJsonIDL()).Result;

        //WriteCerfificate();
        //OpenCard();
        //var mrzInfo = new MRZInfo(
        //        "123456789",
        //        new DateTime(1975, 01, 19),
        //        new DateTime(2020, 06, 30)
        //  );

        //var strKK = mrzInfo.ToString();

        //var hexMRZ = ByteArrayToString(Encoding.ASCII.GetBytes(strKK));



        //var kenc = BAC_Calculate.calculateKENC(Encoding.ASCII.GetBytes("123456"));

        //var hexMRZkenc = ByteArrayToString(kenc);

        //var kMac = BAC_Calculate.calculateKMAC(Encoding.ASCII.GetBytes("123456"));

        //var hexMRZkMac = ByteArrayToString(kMac);

        ////ReadCardData();

        ////var dgsContent = new SmartCardContent(mrzInfo)
        ////        .Content()
        ////        .Result;

        ////var strKK = "123456";

        ////SetSecureMessage("AA000003997050212709145");
        //if (false)
        //{

        //    //GetUUID();
        //    //InstallCardIDL();

        //    ////InstallCardVR();

        //    //CardProcIDL();

        //    InstallCardIDL();

        //    InstallCardIDLFull();

        //    CardProcIDL();
        //}
        //else
        //{

        //    InstallCardVL();

        //    InstallCardVLFull();
        //    //InstallCardVR();

        //    CardProcVR();
        //}

        ////InstallCardVR();
        #endregion

        public static string LoadJsonIDL()
        {
            string json = string.Empty;

            using (StreamReader r = new StreamReader(@"C:\RADSOFT\LOG\json vl111.json"))
            {
                json = r.ReadToEnd();
                
            }

            return json;
        }


        public static string LoadCertificate()
        {
            string json = string.Empty;

            using (StreamReader r = new StreamReader(@"C:\RADSOFT\LOG\Certificate.txt"))
            {
                json = r.ReadToEnd();

            }

            return json;
        }

        private static void WriteCerfificate()
        {
            CardApiMessages cc = new CardApiMessages();

            cc.SaveCertificate(LoadCertificate());
        }

        public static async void GetUUID()
        {
            var uuid = await CardApi.ReadUUidAsync();

        }

        public static string LoadJsonVR_Company()
        {
            string json = string.Empty;

            using (StreamReader r = new StreamReader(@"C:\RADSOFT\LOG\json_vl.json"))
            {
                json = r.ReadToEnd();

            }

            return json;
        }

        public static string LoadJsonVR_Private()
        {
            string json = string.Empty;

            using (StreamReader r = new StreamReader(@"C:\RADSOFT\LOG\vl private.json"))
            {
                json = r.ReadToEnd();

            }

            return json;
        }

        private static void CardProcIDL()
        {
            string Json = LoadJsonIDL();

            DrivingLicense dR = new DrivingLicense(Json);

            dR.ParseInputJson();

            var DG1 = dR.GetDG1x();//dR.GetDG1Test2();

            var DG2 = dR.GetDG2x();//dR.GetDG2Test();

            var DG3 = dR.GetDG3x();

            var DG4 = dR.GetDG4xx(false);

            var DG4HexStr = ByteArrayToString(DG4);

            var DG5 = dR.GetDG5xx(false);

            var DG5HexStr = ByteArrayToString(DG5);

            var DGCommon = dR.GetCommon();

            CardApiMessages cc = new CardApiMessages();

                    var mrzInfo = new MRZInfo(
                dR._DL._license_number,
                DateTime.ParseExact(dR._DL._driver._date_of_birth, "yyyyMMdd", CultureInfo.InvariantCulture),
                DateTime.ParseExact(dR._DL._expire_date, "yyyyMMdd", CultureInfo.InvariantCulture)
            );

            var str = mrzInfo.ToString();

            var butes = Encoding.ASCII.GetBytes(str);

            var hexMRZ = ByteArrayToString(butes);

            var kenc = BAC_Calculate.calculateKENC(Encoding.ASCII.GetBytes("123456"));

            var kMac = BAC_Calculate.calculateKMAC(Encoding.ASCII.GetBytes("123456"));

            //83 | 02 < 2001 > 8F | 10 < EE38DDC8F7995FBB6EFFEA11F27EB052 >
            //83 | 02 < 2003 > 8F | 10 < 5F39443B9A9CA70DE39F889FA7E164F6 >

            List<byte> kencLL = new List<byte>();
            kencLL.Add(0x83); kencLL.Add(0x02); kencLL.Add(0x20); kencLL.Add(0x01); kencLL.Add(0x8F);
            kencLL.Add(0x10); kencLL.AddRange(kenc);

            List<byte> kMacLL = new List<byte>();
            kMacLL.Add(0x83); kMacLL.Add(0x02); kMacLL.Add(0x20); kMacLL.Add(0x03); kMacLL.Add(0x8F);
            kMacLL.Add(0x10); kMacLL.AddRange(kMac);

            int res = cc.SaveIDL2Card3(DG1,
                DG2, DG3, DG4, DG5, DGCommon, kencLL.ToArray(), kMacLL.ToArray());

            int res2 = cc.SaveIDL2Card4(DG1,
                DG2, DG3, DG4, DG5, DGCommon, kencLL.ToArray(), kMacLL.ToArray());
        }

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }


        private static void OpenCard()
        {
            CardApiMessages cc = new CardApiMessages();

            cc.OpenCardCommands(CardFactoryMode.DrivingLicence);
        }


        private static void CardProcVR()
        {
            string Json = LoadJsonVR_Company();

            VehicleRegistration dR = new VehicleRegistration(Json);

            dR.ParseInputJson();


            CardApiMessages cc = new CardApiMessages();

            var ret2= cc.SaveeVr2Card(dR.GetVehicleData());

            //var ret3 =  cc.SaveeVr2Card2(dR.GetVehicleData());
        }

        private static void InstallCardIDL()
        {
            SecureMessaging sc = new SecureMessaging();

            sc.InstallAppletV3();
        }

        private static void InstallCardIDLFull()
        {
            SecureMessaging sc = new SecureMessaging();

            sc.InstallAppletV1();
        }



        private static void InstallCardVR()
        {
            SecureMessaging sc = new SecureMessaging();

            sc.InstallAppletV2();
        }

        private static void ReadCardData()
        {
            CardApiMessages cc = new CardApiMessages();

            byte[] DG1 = null;

            byte[] DG2 = null;

            byte[] DG3 = null;

            byte[] DG4 = null;

            byte[] DG5 = null;

            byte[] DGCommon = null;

            cc.ReadIDLCardNext(ref DG1, ref DG2, ref DG3, ref DG4, ref DG5, ref DGCommon);

        }

        private static void TestCalc()
        {
            byte[] key = new byte[6] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36 };

            byte[] rndIcc = new byte[8] { 0x49, 0x7C, 0x38, 0xE1, 0x7A, 0x27, 0xFD, 0x90 };

            byte[] rndIfd = new byte[8] { 0x6A, 0xc8, 0x42, 0xc8, 0x4b, 0xb0, 0x22, 0x96 };
            
            byte[] KIFD = new byte[16] { 0x6a, 0xc8, 0x42, 0xc8, 0x4b, 0xb0, 0x22, 0x96, 0xcc, 0xa8, 0x0f, 0x96, 0xbb, 0x50, 0xf2, 0x9a };

            var res = BAC_CalculateTest.CalcBAC_Res(key, rndIcc, rndIfd, KIFD);

            Console.Write(res);

            Console.ReadKey();
        }

        private static void TextProc()
        {
            string input = "0000000C6A5020200D0A870A00000014667479706A703220000000006A7032200000002D6A703268000000166968647200000049000000DE0004070700000000000F636F6C7201000000000010000014B46A703263FF4FFF5100320000000000DE000000490000000000000000000000DE0000004900000000000000000004070101070101070101070101FF52000C00000001000504040001FF5C00134040484850484850484850484850484850FF90000A0000000014510001FF53000901000504040001FF5D0014014040484850484850484850484850484850FF53000902000504040001FF5D0014024040484850484850484850484850484850FF53000903000504040001FF5D0014034040484850484850484850484850484850FF93DF78C002E8CD480A84381DD62E777725858FFE349F6D0F2829F68EDF78C002E8CD480A84381DD62E777725858FFE349F6D0F2829F68EDF78C002E8CD480A84381DD62E777725858FFE349F6D0F2829F68EDF60B00598D5480A84382EC779510E432929830EAE3638F5E9C7C63E7D2363E61A1295EEA89BB207FD02CEA5C7183263235C6240DCC525A296CA41B5CB13D3BDAB065EE26B8F51C9ABD6C7C63E7D2363E61A1295EEA89BB207FD02CEA5C7183263235C6240DCC525A296CA41B5CB13D3BDAB065EE26B8F51C9ABD6C7C63E7D2363E61A1295EEA89BB207FD02CEA5C7183263235C6240DCC525A296CA41B5CB13D3BDAB065EE26B8F51C9ABD6C7C63E7CC2E3E61A11E165A18E909575034A9E4DF866C1225BBC81B98A4B454CF96514A9BFAB4197B89A9AFE7ADA3ECF9D35F4AE9F32702D2B558487D0AE07EE57D56DCD54B15244B47682ABE4194CCFD4DA26B6C2B7FB5BA9D0718CDF28BE519E0BB2410FCCAEC7DDE6594AF8DC28715EC376F86FAAC3A1A9BD92648A08D6A6E7F6794BED3D06939553081E6D7B11E2F40325E768C8DB4C3F5B499187A88CCEE86100691E1446A316AD5CF30CCF3EB23405CF9D35F4AE9F32702D2B558487D0AE07EE57D56DCD54B15244B47682ABE4194CCFD4DA26B6C2B7FB5BA9D0718CDF28BE519E0BB2410FCCAEC7DDE6594AF8DC28715EC376F86FAAC3A1A9BD92648A08D6A6E7F6794BED3D06939553081E6D7B11E2F40325E768C8DB4C3F5B499187A88CCEE86100691E1446A316AD5CF30CCF3EB23405CF9D35F4AE9F32702D2B558487D0AE07EE57D56DCD54B15244B47682ABE4194CCFD4DA26B6C2B7FB5BA9D0718CDF28BE519E0BB2410FCCAEC7DDE6594AF8DC28715EC376F86FAAC3A1A9BD92648A08D6A6E7F6794BED3D06939553081E6D7B11E2F40325E768C8DB4C3F5B499187A88CCEE86100691E1446A316AD5CF30CCF3EB23405CF9935F4B09F32702DD7578487D0AE082A9DDBE9769EEDB5";
            string output = "";

            for(int i = 0; i < input.Length; i+=2)
            {
                output += string.Format(" , 0x{0}{1}", input[i], input[i+1]);
            }

            var result = output;
        }

        private static void SetSecureMessage(string mrz)
        {
            SecureMessaging sc = new SecureMessaging();

            sc.EstablishSecureChannelV2(Encoding.UTF8.GetBytes(mrz));
        }


        private static void InstallCardVL()
        {
            SecureMessaging sc = new SecureMessaging();

            sc.InstallAppletV2();
        }

        private static void InstallCardVLFull()
        {
            SecureMessaging sc = new SecureMessaging();

            sc.InstallAppletV4();
        }

    }

}
