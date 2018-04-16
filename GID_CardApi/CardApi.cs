using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GemCard;
using System.Threading;
using Iso18013Lib;
using CardAPILib.CardAPI;
using SmartCardApi.MRZ;
using SmartCardApi.SmartCard;
using System.Globalization;
using CardAPILib.InterfaceCL;

namespace GID_CardApi
{
    public class CardApi
    {
        private static APDUResponse apduResp;

        private static CardNative iCard;
        private static CardApiController _controller = new CardApiController(true); 
        private const ushort SC_OK = 0x9000;
        private const byte SC_PENDING = 0x9F;

        private static int Connect2Card()
        {
            try
            {

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

                throw new ApduCommandException("Uneble to connect to Card");
            }

            return -1;
        }

        private static Task<int> Save2CardDriver(string content)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                int result = 0;

                DrivingLicense dl = new DrivingLicense(content);

                if (dl.ParseInputJson() == 0)
                {
                    var mrzInfo = new MRZInfo(
                        dl._DL._license_number,
                        DateTime.ParseExact(dl._DL._driver._date_of_birth, "yyyyMMdd", CultureInfo.InvariantCulture),
                        DateTime.ParseExact(dl._DL._expire_date, "yyyyMMdd", CultureInfo.InvariantCulture)
                    );

                    var str = string.Format("{0}{1}", "1",mrzInfo.ToString()).Substring(0, 16);

                    var butes = Encoding.ASCII.GetBytes(str);

                    var hexMRZ = ByteArrayToString(butes);

                    var kenc = BAC_Calculate.calculateKENC(butes); //Encoding.ASCII.GetBytes("123456")

                    var kMac = BAC_Calculate.calculateKMAC(butes); //Encoding.ASCII.GetBytes("123456")

                    List<byte> kencLL = new List<byte>();
                    kencLL.Add(0x83); kencLL.Add(0x02); kencLL.Add(0x20); kencLL.Add(0x01); kencLL.Add(0x8F);
                    kencLL.Add(0x10); kencLL.AddRange(kenc);

                    List<byte> kMacLL = new List<byte>();
                    kMacLL.Add(0x83); kMacLL.Add(0x02); kMacLL.Add(0x20); kMacLL.Add(0x03); kMacLL.Add(0x8F);
                    kMacLL.Add(0x10); kMacLL.AddRange(kMac);

                    //First Step 3
                    result = _controller.SaveIDL2Card6(dl.GetDG1x(), dl.GetDG2x(), dl.GetDG3x(), dl.GetDG4xx(), dl.GetDG5xx(), dl.GetCommon(), kencLL.ToArray(), kMacLL.ToArray(), butes);

                }

                return result;
            });

            return resultTask;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public static async Task<int> WriteDriverInfo(string jsonFData)
        {
            if (iCard ==null)
                iCard = new CardNative();

            //var result = Connect2Card();

            //if (result != 0)
            //{
            //    return result;
            //}

            if (string.IsNullOrEmpty(jsonFData))
            {
                return -2;
            }

            var res = await Save2CardDriver(jsonFData);

            return res;
        }

        private static Task<int> Save2CardVehicleRegistration(string content)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                int result = 0;

                VehicleRegistration dR = new VehicleRegistration(content);

                if (dR.ParseInputJson() == 0)
                {
                    var Vehicle1_reg_number = dR._VR._vehicle._reg_number; //*******

                    var Issue_date = dR._VR._issue_date;  //************

                    var Vehicle1__license_number = dR._VR._license_number; //************

                    var str = string.Format("{0}{1}{2}{3}", "1", Vehicle1_reg_number, Issue_date, Vehicle1__license_number).Substring(0, 16);

                    var butes = Encoding.ASCII.GetBytes(str);

                    var hexMRZ = ByteArrayToString(butes);

                    var kenc = BAC_Calculate.calculateKENC(butes); //Encoding.ASCII.GetBytes("123456")

                    var kMac = BAC_Calculate.calculateKMAC(butes); //Encoding.ASCII.GetBytes("123456")

                    List<byte> kencLL = new List<byte>();
                    kencLL.Add(0x83); kencLL.Add(0x02); kencLL.Add(0x20); kencLL.Add(0x01); kencLL.Add(0x8F);
                    kencLL.Add(0x10); kencLL.AddRange(kenc);

                    List<byte> kMacLL = new List<byte>();
                    kMacLL.Add(0x83); kMacLL.Add(0x02); kMacLL.Add(0x20); kMacLL.Add(0x03); kMacLL.Add(0x8F);
                    kMacLL.Add(0x10); kMacLL.AddRange(kMac);

                    result = _controller.SaveVL2Card3(dR.GetVehicleData(), kencLL.ToArray(), kMacLL.ToArray(), butes);
                }

                return result;
            });

            return resultTask;
        }

        public static async Task<int> WriteVehilcleInfo(string jsonFData)
        {
            if (iCard == null)
                iCard = new CardNative();

            //var result = Connect2Card();

            //if (result != 0)
            //{
            //    return result;
            //}

            if (string.IsNullOrEmpty(jsonFData))
            {
                return -2;
            }

            var res = await Save2CardVehicleRegistration(jsonFData);

            return res;
        }

        private static Task<string> ReadUUid()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                int result = 0;

                byte[] uuid = new byte[4];

                result = _controller.ReadUniqueId(ref uuid);

                StringBuilder hex1 = new StringBuilder((4) * 2);
                foreach (byte b in uuid)
                        hex1.AppendFormat("{0:X2}", b);
                var uid_temp = hex1.ToString();
                uid_temp = uid_temp.Substring(0, ((int)(4)) * 2);

                var MyUIDofCard = uid_temp;

                return MyUIDofCard;
            });

            return resultTask;
        }

        public static async Task<string> ReadUUidAsync()
        {
            if (iCard == null)
                iCard = new CardNative();

            //var result = Connect2Card();

            //if (result != 0)
            //{
            //    return result;
            //}


            var res = await ReadUUid();

            return res;
        }
    }
}
