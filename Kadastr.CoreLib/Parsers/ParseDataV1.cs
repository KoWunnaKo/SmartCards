using Kadastr.CoreLib.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib
{
    public class ParseDataV1 : IParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        //For printer use
        public string Convert(IDictionary<string, string> inputs)
        {
            if (inputs == null)
            {
                return null;
            }

            foreach(var obj in inputs)
            {

            }

            return "sadasdasd";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        //To Reader directly
        public byte[] ConvertInput2ByteArray(IDictionary<string, string> inputs)
        {
            if (inputs == null)
            {
                return null;
            }

            var resCollection = new List<byte>();

            foreach(var obj in inputs)
            {
                if (!string.IsNullOrEmpty(obj.Key))
                {
                    byte tag = Utils.ConvertMetaInfoCode2Byte(obj.Key);
                    resCollection.Add(tag);
                    
                    if (!string.IsNullOrEmpty(obj.Value.Trim()))
                    {
                        if (obj.Value.Trim().Length > 255)
                        {
                            var properStr = obj.Value.Trim().Substring(0, 255);
                            resCollection.Add((byte)properStr.Length);
                        }
                        else
                        {
                            resCollection.Add((byte)obj.Value.Trim().Length);
                        }
                    }

                    resCollection.AddRange(Encoding.UTF8.GetBytes(obj.Value.Trim()));
                }
            }

            if (resCollection.Count < 128)
            {
                resCollection.Insert(0, (byte)resCollection.Count);
            }
            else if (resCollection.Count > 127 && resCollection.Count < 256)
            {
                resCollection.Insert(0, (byte)resCollection.Count);
                resCollection.Insert(0, 0x81);

            }
            else if ((resCollection.Count > 255 && resCollection.Count < 65536))
            {
                resCollection.InsertRange(0, Int2ByteArray(resCollection.Count));
            }

            var ResultinStr02 = ByteArrayToString(Int2ByteArray(resCollection.Count));
            resCollection.Insert(0, 0x65);

            if (resCollection.Count < 128)
            {
                resCollection.Insert(0, (byte)resCollection.Count);
            }
            else if (resCollection.Count > 127 && resCollection.Count < 256)
            {
                resCollection.Insert(0, (byte)resCollection.Count);
                resCollection.Insert(0, 0x81);

            }
            else if ((resCollection.Count > 255 && resCollection.Count < 65536))
            {
                resCollection.InsertRange(0, Int2ByteArray(resCollection.Count));
            }

            var ResultinStr03 = ByteArrayToString(Int2ByteArray(resCollection.Count));
            resCollection.Insert(0, 0x53);
            resCollection.Insert(0, 0x00);
            resCollection.Insert(0, 0x01);
            resCollection.Insert(0, 0x54);

            var ResultinStr = ByteArrayToString(resCollection.ToArray());

            return resCollection.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        private string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private byte[] Int2ByteArray(int val)
        {
            int intValue = val;
            byte[] intBytes = BitConverter.GetBytes(intValue);
            Array.Reverse(intBytes);
            byte[] result = intBytes;

            List<byte> ll = new List<byte>();

            ll.Add(0x82);
            //ll.Add(result[1]);
            ll.Add(result[2]);
            ll.Add(result[3]);

            return ll.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public void TestParse()
        {
            var resBytes = Encoding.UTF8.GetBytes("ы");
            var resBytes2 = Encoding.UTF8.GetBytes("s");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowData">TLV structure </param>
        /// <returns></returns>
        public IDictionary<string, string> Parse(string rowData)
        {
            throw new NotImplementedException();
        }
    }
}
