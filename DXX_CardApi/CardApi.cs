using CardAPILib.CardAPI;
using CardAPILib.InterfaceCL;
using GemCard;
using Iso18013Lib.Communication;
using SmartCardApi.MRZ;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXX_CardApi
{
    public class CardApi
    {
        private static APDUResponse apduResp;

        private static CardNative iCard;
        private static CardApiController _controller = new CardApiController(true);
        private const ushort SC_OK = 0x9000;
        private const byte SC_PENDING = 0x9F;

        private static Task<int> Save2CardDXX(string content)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                int result = 0;

                var dl = new DXX_DataTransformer(content);

                if (dl.ParseInputJson() == 0)
                {
                    string encKey = "DXX_Encription_Key";

                    var butes = Encoding.ASCII.GetBytes(encKey.Substring(0,16));

                    var hexMRZ = ByteArrayToString(butes);

                    var kenc = BAC_Calculate.calculateKENC(butes);

                    var kMac = BAC_Calculate.calculateKMAC(butes);

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

        private static Task<int> Save2CardDXX_Rewrite(string content)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                int result = 0;

                var dl = new DXX_DataTransformer(content);

                if (dl.ParseInputJson() == 0)
                {
                    string encKey = "DXX_Encription_Key";

                    var butes = Encoding.ASCII.GetBytes(encKey.Substring(0, 16));

                    var hexMRZ = ByteArrayToString(butes);

                    var kenc = BAC_Calculate.calculateKENC(butes);

                    var kMac = BAC_Calculate.calculateKMAC(butes);

                    List<byte> kencLL = new List<byte>();
                    kencLL.Add(0x83); kencLL.Add(0x02); kencLL.Add(0x20); kencLL.Add(0x01); kencLL.Add(0x8F);
                    kencLL.Add(0x10); kencLL.AddRange(kenc);

                    List<byte> kMacLL = new List<byte>();
                    kMacLL.Add(0x83); kMacLL.Add(0x02); kMacLL.Add(0x20); kMacLL.Add(0x03); kMacLL.Add(0x8F);
                    kMacLL.Add(0x10); kMacLL.AddRange(kMac);

                    //First Step 3
                    result = _controller.SaveIDL2Card7(dl.GetDG1x(), dl.GetDG2x(), dl.GetDG3x(), dl.GetDG4xx(), dl.GetDG5xx(), dl.GetCommon(), kencLL.ToArray(), kMacLL.ToArray(), butes);

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

        public static async Task<int> WriteDXXInfo(string jsonFData)
        {
            if (iCard == null)
                iCard = new CardNative();

            if (string.IsNullOrEmpty(jsonFData))
            {
                return -2;
            }

            var res = await Save2CardDXX(jsonFData);

            return res;
        }

        public static async Task<int> ReWriteDXXInfo(string jsonFData)
        {
            if (iCard == null)
                iCard = new CardNative();

            if (string.IsNullOrEmpty(jsonFData))
            {
                return -2;
            }

            var res = await Save2CardDXX_Rewrite(jsonFData);

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
