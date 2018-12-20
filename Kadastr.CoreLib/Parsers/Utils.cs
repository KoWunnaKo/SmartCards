using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Kadastr.CoreLib.Parsers
{
    public static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toSerialize"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toDeserialize"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this string toDeserialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader textReader = new StringReader(toDeserialize))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static byte ConvertMetaInfoCode2Byte(string code)
        {
            byte retbyte = 0x00;

            if (string.IsNullOrEmpty(code)) return retbyte;

            switch (code)
            {
                case "A1":
                    retbyte = 0xA1;
                    break;
                case "A2":
                    retbyte = 0xA2;
                    break;
                case "A3":
                    retbyte = 0xA3;
                    break;
                case "A4":
                    retbyte = 0xA4;
                    break;
                case "A5":
                    retbyte = 0xA5;
                    break;
                case "A6":
                    retbyte = 0xA6;
                    break;
                case "A7":
                    retbyte = 0xA7;
                    break;
                case "A8":
                    retbyte = 0xA8;
                    break;
                case "A9":
                    retbyte = 0xA9;
                    break;

                ////////////////////////////////////////////////////////
                case "B1":
                    retbyte = 0xB1;
                    break;
                case "B2":
                    retbyte = 0xB2;
                    break;
                case "B3":
                    retbyte = 0xB3;
                    break;
                case "B4":
                    retbyte = 0xB4;
                    break;
                case "B5":
                    retbyte = 0xB5;
                    break;
                case "B6":
                    retbyte = 0xB6;
                    break;
                case "B7":
                    retbyte = 0xB7;
                    break;
                case "B8":
                    retbyte = 0xB8;
                    break;
                case "B9":
                    retbyte = 0xB9;
                    break;

                ////////////////////////////////////////////////////////
                case "C1":
                    retbyte = 0xC1;
                    break;
                case "C2":
                    retbyte = 0xC2;
                    break;
                case "C3":
                    retbyte = 0xC3;
                    break;
                case "C4":
                    retbyte = 0xC4;
                    break;
                case "C5":
                    retbyte = 0xC5;
                    break;
                case "C6":
                    retbyte = 0xC6;
                    break;
                case "C7":
                    retbyte = 0xC7;
                    break;
                case "C8":
                    retbyte = 0xC8;
                    break;
                case "C9":
                    retbyte = 0xC9;
                    break;

                ////////////////////////////////////////////////////////
                case "D1":
                    retbyte = 0xD1;
                    break;
                case "D2":
                    retbyte = 0xD2;
                    break;
                case "D3":
                    retbyte = 0xD3;
                    break;
                case "D4":
                    retbyte = 0xD4;
                    break;
                case "D5":
                    retbyte = 0xD5;
                    break;
                case "D6":
                    retbyte = 0xD6;
                    break;
                case "D7":
                    retbyte = 0xD7;
                    break;
                case "D8":
                    retbyte = 0xD8;
                    break;
                case "D9":
                    retbyte = 0xD9;
                    break;

                ////////////////////////////////////////////////////////
                case "E1":
                    retbyte = 0xE1;
                    break;
                case "E2":
                    retbyte = 0xE2;
                    break;
                case "E3":
                    retbyte = 0xE3;
                    break;
                case "E4":
                    retbyte = 0xE4;
                    break;
                case "E5":
                    retbyte = 0xE5;
                    break;
                case "E6":
                    retbyte = 0xE6;
                    break;
                case "E7":
                    retbyte = 0xE7;
                    break;
                case "E8":
                    retbyte = 0xE8;
                    break;
                case "E9":
                    retbyte = 0xE9;
                    break;

                ////////////////////////////////////////////////////////
                case "F1":
                    retbyte = 0xF1;
                    break;
                case "F2":
                    retbyte = 0xF2;
                    break;
                case "F3":
                    retbyte = 0xF3;
                    break;
                case "F4":
                    retbyte = 0xF4;
                    break;
                case "F5":
                    retbyte = 0xF5;
                    break;
                case "F6":
                    retbyte = 0xF6;
                    break;
                case "F7":
                    retbyte = 0xF7;
                    break;
                case "F8":
                    retbyte = 0xF8;
                    break;
                case "F9":
                    retbyte = 0xF9;
                    break;
            }

            return retbyte;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ConvertByte2MetaInfoCode(byte code)
        {
            string retbyte = string.Empty;

            switch (code)
            {
                case 0xA1:
                    retbyte ="A1" ;
                    break;
                case 0xA2:
                    retbyte = "A2";
                    break;
                case 0xA3:
                    retbyte = "A3" ;
                    break;
                case 0xA4:
                    retbyte = "A4";
                    break;
                case 0xA5:
                    retbyte = "A5";
                    break;
                case 0xA6:
                    retbyte = "A6";
                    break;
                case 0xA7:
                    retbyte = "A7";
                    break;
                case 0xA8:
                    retbyte = "A8";
                    break;
                case 0xA9:
                    retbyte = "A9";
                    break;

                ////////////////////////////////////////////////////////
                case 0xB1:
                    retbyte = "B1";
                    break;
                case 0xB2:
                    retbyte = "B2";
                    break;
                case 0xB3:
                    retbyte = "B3";
                    break;
                case 0xB4:
                    retbyte = "B4";
                    break;
                case 0xB5:
                    retbyte = "B5";
                    break;
                case 0xB6:
                    retbyte = "B6";
                    break;
                case 0xB7:
                    retbyte = "B7";
                    break;
                case 0xB8:
                    retbyte = "B8";
                    break;
                case 0xB9:
                    retbyte = "B9";
                    break;

                ////////////////////////////////////////////////////////
                case 0xC1:
                    retbyte = "C1";
                    break;
                case 0xC2:
                    retbyte = "C2";
                    break;
                case 0xC3:
                    retbyte = "C3";
                    break;
                case 0xC4:
                    retbyte = "C4";
                    break;
                case 0xC5:
                    retbyte = "C5";
                    break;
                case 0xC6:
                    retbyte = "C6";
                    break;
                case 0xC7:
                    retbyte = "C7";
                    break;
                case 0xC8:
                    retbyte = "C8";
                    break;
                case 0xC9:
                    retbyte = "C9";
                    break;

                ////////////////////////////////////////////////////////
                case 0xD1:
                    retbyte = "D1";
                    break;
                case 0xD2:
                    retbyte = "D2";
                    break;
                case 0xD3:
                    retbyte = "D3";
                    break;
                case 0xD4:
                    retbyte = "D4";
                    break;
                case 0xD5:
                    retbyte = "D5";
                    break;
                case 0xD6:
                    retbyte = "D6";
                    break;
                case 0xD7:
                    retbyte = "D7";
                    break;
                case 0xD8:
                    retbyte = "D8";
                    break;
                case 0xD9:
                    retbyte = "D9";
                    break;

                ////////////////////////////////////////////////////////
                case 0xE1:
                    retbyte = "E1";
                    break;
                case 0xE2:
                    retbyte = "E2";
                    break;
                case 0xE3:
                    retbyte = "E3";
                    break;
                case 0xE4:
                    retbyte = "E4";
                    break;
                case 0xE5:
                    retbyte = "E5";
                    break;
                case 0xE6:
                    retbyte = "E6";
                    break;
                case 0xE7:
                    retbyte = "E7";
                    break;
                case 0xE8:
                    retbyte = "E8";
                    break;
                case 0xE9:
                    retbyte = "E9";
                    break;

                ////////////////////////////////////////////////////////
                case 0xF1:
                    retbyte = "F1";
                    break;
                case 0xF2:
                    retbyte = "F2";
                    break;
                case 0xF3:
                    retbyte = "F3";
                    break;
                case 0xF4:
                    retbyte = "F4";
                    break;
                case 0xF5:
                    retbyte = "F5";
                    break;
                case 0xF6:
                    retbyte = "F6";
                    break;
                case 0xF7:
                    retbyte = "F7";
                    break;
                case 0xF8:
                    retbyte = "F8";
                    break;
                case 0xF9:
                    retbyte = "F9";
                    break;
            }

            return retbyte;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vr"></param>
        public static IDictionary<string, string> ParseVR(byte[] Vr)
        {
            if (Vr.Length == 0) return null;

            var resDict = new Dictionary<string, string>();

            var TrimmedVr = Decode(Vr);

            var ResultinStr = ByteArrayToString(TrimmedVr);

            int StartReadNumber = 4;

            if (TrimmedVr[1] == 0x81)
            {
                StartReadNumber = 3;
            }
            else if (TrimmedVr[1] == 0x82)
            {
                StartReadNumber = 4;
            }
            else
            {
                StartReadNumber = 2;
            }

            bool valueCountingBegin = false;
            bool tagGetting = true;
            bool lengthGetting = false;
            bool valueCountingFinished = false;

            byte tag = 0x00;
            byte[] value = null;
            int length = 0;
            int endOfValue = 0;

            for (int j = 0; j < TrimmedVr.Length - StartReadNumber; j++)
            {
                //Action
                if (tagGetting)
                {
                    tag = TrimmedVr[j + StartReadNumber];
                }
                else if (lengthGetting)
                {
                    byte[] lenArray = new byte[4];

                    lenArray[0] = TrimmedVr[j + StartReadNumber];

                    length = BitConverter.ToInt32(lenArray, 0);

                    if (length == 0)
                    {
                        lengthGetting = false;
                        tagGetting = false;
                        valueCountingBegin = false;
                        valueCountingFinished = true;
                    }

                }
                else if (valueCountingBegin)
                {

                    value = new byte[length];

                    if (((j + StartReadNumber) + length) > TrimmedVr.Length)
                    {
                        //
                        length = TrimmedVr.Length - (j + StartReadNumber);

                        Array.Copy(TrimmedVr, j + StartReadNumber, value, 0, length);
                    }
                    else
                    {
                        Array.Copy(TrimmedVr, j + StartReadNumber, value, 0, length);
                    }

                    //SetData(tag, value);
                    var tagStr = ConvertByte2MetaInfoCode(tag);
                    var valueStr = Encoding.UTF8.GetString(value);
                    resDict.Add(tagStr, valueStr);

                    endOfValue = j + StartReadNumber + length - 1;

                    if ((j + StartReadNumber) >= endOfValue)
                    {
                        lengthGetting = false;
                        tagGetting = true;
                        valueCountingBegin = false;
                        valueCountingFinished = false;

                        tag = 0x00;
                        value = null;
                        length = 0;
                        endOfValue = 0;

                        continue;
                    }

                }
                else if (valueCountingFinished)
                {
                    if ((j + StartReadNumber) >= endOfValue)
                    {
                        tag = 0x00;
                        value = null;
                        length = 0;
                        endOfValue = 0;
                    }
                    else
                    {
                        continue;
                    }
                }


                //Setting
                if (tagGetting)
                {
                    lengthGetting = true;
                    tagGetting = false;
                    valueCountingBegin = false;
                    valueCountingFinished = false;
                }
                else if (lengthGetting)
                {
                    lengthGetting = false;
                    tagGetting = false;
                    valueCountingBegin = true;
                    valueCountingFinished = false;
                }
                else if (valueCountingBegin)
                {
                    lengthGetting = false;
                    tagGetting = false;
                    valueCountingBegin = false;
                    valueCountingFinished = true;
                }
                else if (valueCountingFinished)
                {
                    lengthGetting = false;
                    tagGetting = true;
                    valueCountingBegin = false;
                    valueCountingFinished = false;
                }
            }

            return resDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        private static byte[] Decode(byte[] packet)
        {
            var i = packet.Length - 1;
            while (packet[i] == 0)
            {
                --i;
            }
            var temp = new byte[i + 1];
            Array.Copy(packet, temp, i + 1);
            return temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
    }
}
