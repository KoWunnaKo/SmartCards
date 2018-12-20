using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Iso18013Lib.DGs;
using System.Text.RegularExpressions;

namespace Iso18013Lib.Communication
{
    public class DXX_DataTransformer
    {
        private string _Json { get; set; }

        public DXX_LicenseExample _DL { get; set; }

        private bool IsJsonParsed = false;

        public DXX_DataTransformer(string Json)
        {
            _Json = Json;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ParseInputJson()
        {
            try
            {
                if (string.IsNullOrEmpty(_Json))
                {
                    return -2;
                }

                _DL = ParseInputRequest(_Json);

                IsJsonParsed = true;
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Json"></param>
        /// <returns></returns>
        private DXX_LicenseExample ParseInputRequest(string Json)
        {
            DXX_LicenseExample request = null;

            try
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(Json)))
                {
                    var ser = new DataContractJsonSerializer(typeof(DXX_LicenseExample));
                    request = (DXX_LicenseExample)ser.ReadObject(stream);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("JSON Parse Error {0}", ex.Message));
            }

            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetDG1x()
        {
            List<byte> TotalList = new List<byte>();

            if (!Regex.IsMatch(_DL._first_name, @"\P{IsCyrillic}"))
            {
                if ((_DL._first_name != null) && (!string.IsNullOrEmpty(_DL._first_name)))
                {
                    TotalList.Add(0xA1);
                    TotalList.Add((byte)(_DL._first_name.Length * 2));
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._first_name));
                }
            }
            else
            {
                if ((_DL._first_name != null) && (!string.IsNullOrEmpty(_DL._first_name)))
                {
                    TotalList.Add(0xA1);
                    TotalList.Add((byte)_DL._first_name.Length);
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._first_name));
                }
            }

            if (!Regex.IsMatch(_DL._last_name, @"\P{IsCyrillic}"))
            {
                if ((_DL._last_name != null) && (!string.IsNullOrEmpty(_DL._last_name)))
                {
                    TotalList.Add(0xA2);
                    TotalList.Add((byte)(_DL._last_name.Length * 2));
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._last_name));
                }
            }
            else
            {
                if ((_DL._last_name != null) && (!string.IsNullOrEmpty(_DL._last_name)))
                {
                    TotalList.Add(0xA2);
                    TotalList.Add((byte)_DL._last_name.Length);
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._last_name));
                }
            }

            if (!Regex.IsMatch(_DL._middle_name, @"\P{IsCyrillic}"))
            {
                if ((_DL._middle_name != null) && (!string.IsNullOrEmpty(_DL._middle_name)))
                {
                    TotalList.Add(0xA3);
                    TotalList.Add((byte)(_DL._middle_name.Length * 2));
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._middle_name));
                }
            }
            else
            {
                if ((_DL._middle_name != null) && (!string.IsNullOrEmpty(_DL._middle_name)))
                {
                    TotalList.Add(0xA3);
                    TotalList.Add((byte)_DL._middle_name.Length);
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._middle_name));
                }
            }



            if ((_DL._issue_date != null) && (!string.IsNullOrEmpty(_DL._issue_date)))
            {
                TotalList.Add(0xA4);
                TotalList.Add((byte)_DL._issue_date.Length);
                TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._issue_date));
            }
            else
            {
                _DL._issue_date = DateTime.Now.ToString("yyyyMMdd");
                TotalList.Add(0xA4);
                TotalList.Add((byte)_DL._issue_date.Length);
                TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._issue_date));

            }

            if ((_DL._expire_date != null) && (!string.IsNullOrEmpty(_DL._expire_date)))
            {
                TotalList.Add(0xA5);
                TotalList.Add((byte)_DL._expire_date.Length);
                TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._expire_date));
            }
            else
            {
                _DL._expire_date = DateTime.Now.AddYears(10).ToString("yyyyMMdd");
                TotalList.Add(0xA5);
                TotalList.Add((byte)_DL._expire_date.Length);
                TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._expire_date));

            }

            if ((_DL._pinfl != null) && (!string.IsNullOrEmpty(_DL._pinfl)))
            {
                TotalList.Add(0xA6);
                TotalList.Add((byte)_DL._pinfl.Length);
                TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._pinfl));
            }

            if (!Regex.IsMatch(_DL._position, @"\P{IsCyrillic}"))
            {
                if ((_DL._position != null) && (!string.IsNullOrEmpty(_DL._position)))
                {
                    TotalList.Add(0xA7);
                    TotalList.Add((byte)(_DL._position.Length * 2));
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._position));
                }
            }
            else
            {
                if ((_DL._position != null) && (!string.IsNullOrEmpty(_DL._position)))
                {
                    TotalList.Add(0xA7);
                    TotalList.Add((byte)_DL._position.Length);
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._position));
                }
            }



            if ((_DL._badge_number != null) && (!string.IsNullOrEmpty(_DL._badge_number)))
            {
                TotalList.Add(0xA8);
                TotalList.Add((byte)_DL._badge_number.Length);
                TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._badge_number));
            }

            if (!Regex.IsMatch(_DL._rank, @"\P{IsCyrillic}"))
            {
                if ((_DL._rank != null) && (!string.IsNullOrEmpty(_DL._rank)))
                {
                    TotalList.Add(0xA9);
                    TotalList.Add((byte)(_DL._rank.Length * 2));
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._rank));
                }
            }
            else
            {
                if ((_DL._rank != null) && (!string.IsNullOrEmpty(_DL._rank)))
                {
                    TotalList.Add(0xA9);
                    TotalList.Add((byte)_DL._rank.Length);
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_DL._rank));
                }
            }



            if (TotalList.Count < 128)
            {
                TotalList.Insert(0, (byte)TotalList.Count);
            }
            else if (TotalList.Count > 127 && TotalList.Count < 256)
            {
                TotalList.Insert(0, (byte)TotalList.Count);
                TotalList.Insert(0, 0x81);

            }
            else if ((TotalList.Count > 255 && TotalList.Count < 65536))
            {
                TotalList.InsertRange(0, Int2ByteArray(TotalList.Count));
            }

            var ResultinStr02 = ByteArrayToString(Int2ByteArray(TotalList.Count));
            TotalList.Insert(0, 0x65);

            if (TotalList.Count < 128)
            {
                TotalList.Insert(0, (byte)TotalList.Count);
            }
            else if (TotalList.Count > 127 && TotalList.Count < 256)
            {
                TotalList.Insert(0, (byte)TotalList.Count);
                TotalList.Insert(0, 0x81);

            }
            else if ((TotalList.Count > 255 && TotalList.Count < 65536))
            {
                TotalList.InsertRange(0, Int2ByteArray(TotalList.Count));
            }

            var ResultinStr03 = ByteArrayToString(Int2ByteArray(TotalList.Count));
            TotalList.Insert(0, 0x53);
            TotalList.Insert(0, 0x00);
            TotalList.Insert(0, 0x01);
            TotalList.Insert(0, 0x54);

            var ResultinStr = ByteArrayToString(TotalList.ToArray());

            return TotalList.ToArray();
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
        /// <returns></returns>
        public byte[] GetDG2x()
        {
            return new byte[12] { 0x54, 0x01, 0x00, 0x53, 0x07, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37 };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetDG3x()
        {
            return new byte[12] { 0x54, 0x01, 0x00, 0x53, 0x07, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37 };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public byte[] GetDG4xx(bool test = false)
        {
            try
            {
                string _total = string.Empty;

                List<byte> ImageTimestamp = new List<byte>();
                List<byte> TypeOfImage = new List<byte>();
                List<byte> JPEG = new List<byte>();

                List<byte> totalList = new List<byte>();

                DateTime _t = DateTime.Now;

                ImageTimestamp.Add(0x20);
                ImageTimestamp.Add(0x17);
                ImageTimestamp.Add(0x09);
                ImageTimestamp.Add(0x21);
                ImageTimestamp.Add(0x12);
                ImageTimestamp.Add(0x01);
                ImageTimestamp.Add(0x01);

                ImageTimestamp.Insert(0, (byte)ImageTimestamp.Count);
                ImageTimestamp.Insert(0, 0x88);

                TypeOfImage.Add(0x89);
                TypeOfImage.Add(0x01);
                TypeOfImage.Add(0x03);

                if (test)
                {
                    JPEG.AddRange(new byte[] { 0xFF, 0xD8, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A, 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x6A, 0x70, 0x32, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6A, 0x70, 0x32, 0x20, 0x00, 0x00, 0x00, 0x47, 0x6A, 0x70, 0x32, 0x68, 0x00, 0x00, 0x00, 0x16, 0x69, 0x68, 0x64, 0x72, 0x00, 0x00, 0x02, 0x58, 0x00, 0x00, 0x01, 0xD5, 0x00, 0x03, 0x07, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x63, 0x6F, 0x6C, 0x72, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x1A, 0x72, 0x65, 0x73, 0x20, 0x00, 0x00, 0x00, 0x12, 0x72, 0x65, 0x73, 0x63, 0x00, 0x60, 0x00, 0xFE, 0x00, 0x60, 0x00, 0xFE, 0x04, 0x04, 0x00, 0x00, 0x00, 0x00, 0x6A, 0x70, 0x32, 0x63, 0xFF, 0x4F, 0xFF, 0x51, 0x00, 0x2F, 0x00, 0x00, 0x00, 0x00, 0x01, 0xD5, 0x00, 0x00, 0x02, 0x58, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0xD5, 0x00, 0x00, 0x02, 0x58, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x07, 0x01, 0x01, 0x07, 0x01, 0x01, 0x07, 0x01, 0x01, 0xFF, 0x5C, 0x00, 0x1D, 0x42, 0x6F, 0x1E, 0x5D, 0x30, 0x5D, 0x30, 0x5D, 0x16, 0x55, 0x74, 0x55, 0x74, 0x55, 0x89, 0x29, 0xB0, 0x29, 0xB0, 0x2A, 0x00, 0x21, 0x91, 0x21, 0x91, 0x21, 0x4C, 0xFF, 0x52, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x01, 0x01, 0x04, 0x04, 0x04, 0x00, 0x00, 0xFF, 0x64, 0x00, 0x0F, 0x00, 0x01, 0x4C, 0x57, 0x46, 0x5F, 0x4A, 0x50, 0x32, 0x5F, 0x32, 0x31, 0x31, 0xFF, 0x90, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x2D, 0xFA, 0x00, 0x01, 0xFF, 0x93, 0xCF, 0xDF, 0xD4, 0x44, 0x58, 0x06, 0xF9, 0x52, 0x07, 0xB6, 0x89, 0x75, 0xE9, 0x2A, 0x2D, 0xD9, 0x37, 0xC9, 0x3C, 0xCE, 0x9D, 0xF4, 0xA0, 0xD5, 0x50, 0x98, 0xEF, 0xA2, 0x41, 0x40, 0xC4, 0x29, 0xE5, 0x34, 0x78, 0x93, 0xA9, 0x66, 0x3D, 0xE9, 0x71, 0x1E, 0x5C, 0x55, 0xD3, 0xFA, 0xBA, 0x96, 0xF3, 0x37, 0xFF, 0x2F, 0x4A, 0x7E, 0x76, 0x0A, 0x4C, 0x46, 0xC2, 0xF9, 0x62, 0xC6, 0x8D, 0xB8, 0x52, 0xBE, 0x1C, 0x48, 0xC9, 0xF0, 0x6B, 0x3C, 0xD8, 0xF2, 0x93, 0xC0, 0x3C, 0x0D, 0xA9, 0x1D, 0x33, 0x71, 0xE0, 0xCF, 0x68, 0x3B, 0x1E, 0x0A, 0x23, 0xCC, 0xB4, 0x5C, 0xFD, 0xDE, 0xAD, 0x92, 0x21, 0xAC, 0x4F, 0x92, 0xF9, 0x80, 0x43, 0x5E, 0x1E, 0xA4, 0xCA, 0xD8, 0xE5, 0xC3, 0xA9, 0x0B, 0xC2, 0x11, 0x38, 0xDE, 0x21, 0x00, 0x65, 0x76, 0xE7, 0xE4, 0x02, 0xD1, 0x87, 0x64, 0xD8, 0x89, 0x72, 0x52, 0x9A, 0xAB, 0xF9, 0x3A, 0xDC, 0x46, 0xD6, 0x34, 0x15, 0x50, 0x08, 0xE7, 0x7C, 0xB2, 0xF7, 0x6F, 0xD3, 0x17, 0x11, 0xB1, 0x95, 0x8C, 0x71, 0xA5, 0x67, 0x2D, 0x91, 0x3F, 0xB1, 0x1E, 0xC2, 0xD7, 0xFA, 0xB3, 0x71, 0xF5, 0x1D, 0x24, 0x59, 0x62, 0x36, 0x1E, 0xE7, 0x94, 0x10, 0x06, 0xED, 0xF1, 0xDF, 0xC2, 0x66, 0x6E, 0xFE, 0x7F, 0x63, 0xEF, 0xD1, 0xE1, 0x61, 0xD1, 0x33, 0xB3, 0xD3, 0x28, 0x95, 0xF5, 0x52, 0xA4, 0xB0, 0xDA, 0x5D, 0x7F, 0xC5, 0xE9, 0x16, 0x0B, 0x4A, 0xD6, 0x94, 0xD5, 0x8B, 0x41, 0xC9, 0x56, 0x7C, 0x2E, 0xC1, 0x5C, 0xB7, 0x2C, 0x94, 0x03, 0x1C, 0x0A, 0xE4, 0xD2, 0x98, 0x2D, 0x37, 0x33, 0x04, 0x18, 0x11, 0xAC, 0x97, 0x84, 0xFE, 0xF8, 0xCF, 0xA8, 0xC7, 0x95, 0x39, 0x7E, 0xB6, 0x78, 0xDD, 0x98, 0x7F, 0xF9, 0xE0, 0x5C, 0x9B, 0x1B, 0x9A, 0x74, 0x3D, 0x26, 0xED, 0x5C, 0x8E, 0x04, 0xFE, 0x32, 0x55, 0xD0, 0x58, 0xE6, 0x5B, 0x13, 0xB5, 0x0F, 0x51, 0x67, 0xFB, 0x5C, 0x8D, 0xD3, 0x8F, 0x80, 0x8A, 0x11, 0x86, 0x9C, 0x44, 0x02, 0x40, 0x79, 0x35, 0xD3, 0x32, 0x1B, 0xDD, 0xB4, 0x7D, 0x22, 0xAA, 0xA7, 0xD4, 0xD9, 0x69, 0xFF, 0x50, 0x42, 0x43, 0x6F, 0x70, 0x80, 0x72, 0x54, 0x82, 0xCC, 0x34, 0x52, 0x03, 0xC8, 0xC5, 0x91, 0xBF, 0x14, 0x7F, 0x22, 0x85, 0xD8, 0x56, 0x36, 0x2C, 0xD5, 0xE4, 0x80, 0x9C, 0x4D, 0x83, 0xA3, 0xFE, 0x5D, 0x85, 0xA9, 0x23, 0xA1, 0x67, 0x50, 0xBF, 0x80, 0x64, 0x43, 0x62, 0x1D, 0xA9, 0xC2, 0xD6, 0x8B, 0x89, 0x27, 0x61, 0x3D, 0x78, 0x1A, 0x74, 0xFE, 0xAD, 0x07, 0xB8, 0xA0, 0x54, 0x16, 0x14, 0x84, 0x0C, 0x70, 0x44, 0x04, 0x82, 0x74, 0xC7, 0x4E, 0x49, 0xC0, 0x61, 0xDE, 0x51, 0xA0, 0xE5, 0xEB, 0x95, 0x01, 0x8F, 0x8D, 0x79, 0x51, 0xA1, 0xA8, 0x71, 0x3E, 0x69, 0x0D, 0xA7, 0x08, 0x6C, 0x5F, 0x7E, 0x70, 0x9A, 0x32, 0x34, 0x82, 0xD2, 0xA3, 0x99, 0xCC, 0x79, 0x16, 0xA6, 0x38, 0xAB, 0xAE, 0x98, 0x12, 0xE4, 0x95, 0x6F, 0x10, 0x18, 0xD0, 0xB8, 0xC2, 0x32, 0x72, 0xBB, 0xB5, 0x0C, 0x2B, 0xA1, 0x27, 0xD2, 0x33, 0xDF, 0x16, 0x6D, 0x8D, 0x79, 0xC4, 0x1C, 0xBE, 0x6F, 0x3C, 0x30, 0x2C, 0x51, 0xB1, 0xA6, 0x9C, 0x33, 0xA3, 0x73, 0x29, 0xE4, 0xEC, 0x25, 0xD9, 0x71, 0xCA, 0xA3, 0x49, 0xCD, 0x2C, 0xBC, 0x09, 0xAA, 0x30, 0x2A, 0x0A, 0xEB, 0x6A, 0xFD, 0xF7, 0xE4, 0xFB, 0x27, 0xAF, 0x46, 0x75, 0xBA, 0xB4, 0x63, 0x9D, 0xA1, 0x74, 0x68, 0x10, 0x89, 0xD6, 0x95, 0x2A, 0xD0, 0xD4, 0x91, 0xE7, 0xE2, 0x26, 0x8F, 0x22, 0x64, 0x70, 0xF3, 0xAF, 0x73, 0xB1, 0x07, 0x68, 0x21, 0x2A, 0x6F, 0xDC, 0x6F, 0x7C, 0xC3, 0xA8, 0x1B, 0xBB, 0x27, 0x2D, 0xB8, 0x58, 0x02, 0xEC, 0x11, 0xC6, 0x5B, 0x5C, 0x79, 0xD7, 0x34, 0x6D, 0x86, 0x93, 0x63, 0xB1, 0x90, 0x6F, 0x1B, 0xA3, 0x11, 0x91, 0xF0, 0x35, 0xB1, 0xFA, 0xE6, 0xBE, 0x8C, 0x61, 0xEE, 0xE8, 0x5C, 0xBD, 0x9C, 0x62, 0xAE, 0x47, 0x2A, 0x10, 0xAB, 0x03, 0xEC, 0x60, 0x8F, 0xB6, 0x43, 0xD3, 0x04, 0x25, 0x08, 0x36, 0xEE, 0x15, 0x48, 0x08, 0x08, 0xE3, 0x7F, 0xD8, 0x67, 0x18, 0x58, 0xE2, 0x58, 0x1A, 0x4D, 0x26, 0x43, 0xC0, 0x27, 0x3C, 0x47, 0xA6, 0x81, 0x9C, 0xD7, 0x46, 0xB9, 0x61, 0x07, 0x5C, 0xE9, 0x79, 0xE7, 0xF4, 0xBC, 0xFC, 0xA6, 0xBD, 0xD3, 0xA3, 0xAC, 0x0F, 0x4F, 0xFF, 0x80, 0x1A, 0xA5, 0x75, 0x72, 0x05, 0xE5, 0x47, 0xD6, 0x7A, 0xC7, 0x19, 0x54, 0x65, 0xCC, 0x22, 0x41, 0x42, 0xA3, 0x07, 0x54, 0x4B, 0xDF, 0x0F, 0x09, 0x60, 0x20, 0x22, 0xCD, 0x2C, 0xFD, 0xBD, 0xEF, 0x8E, 0x14, 0x25, 0x4A, 0x61, 0xBE, 0x3C, 0x30, 0x8A, 0xE2, 0x10, 0x71, 0x99, 0x99, 0x1C, 0x0C, 0x0F, 0xBD, 0xE3, 0xE5, 0xD1, 0xD1, 0xB9, 0xE2, 0xCF, 0xC4, 0x5D, 0xDA, 0x3F, 0xA0, 0xC7, 0x35, 0x29, 0x1B, 0x38, 0x20, 0x4F, 0x0D, 0x22, 0x81, 0x79, 0x40, 0x38, 0x66, 0x72, 0x92, 0xF2, 0x4A, 0x75, 0x34, 0xA1, 0x53, 0x83, 0xF1, 0x95, 0x73, 0xA5, 0x66, 0xE2, 0x24, 0x40, 0xAD, 0x43, 0x05, 0x3B, 0x61, 0xC1, 0x4E, 0xAC, 0xF6, 0xC6, 0x2F, 0x57, 0x74, 0x0A, 0xB5, 0x34, 0x1E, 0x83, 0x9B, 0x8B, 0xDD, 0xEC, 0xE7, 0x46, 0xDB, 0xC3, 0xF0, 0xE4, 0xD9, 0x4A, 0x07, 0x8C, 0x7B, 0xB7, 0x1F, 0x3B, 0xA2, 0x0E }); //_DL._Photo
                }
                else
                {
                    byte[] bytesPhoto = Convert.FromBase64String(_DL._Photo);

                    //JPEG.Add(0xFF);
                    //JPEG.Add(0xD8);
                    JPEG.AddRange(bytesPhoto);
                }

                JPEG.InsertRange(0, Int2ByteArray(JPEG.Count));

                var ResultinStr01 = ByteArrayToString(Int2ByteArray(JPEG.Count));
                JPEG.Insert(0, 0x40);
                JPEG.Insert(0, 0x5F);

                totalList.AddRange(ImageTimestamp);
                totalList.AddRange(TypeOfImage);
                totalList.AddRange(JPEG);

                totalList.InsertRange(0, Int2ByteArray(totalList.Count));
                totalList.Insert(0, 0xA2);

                totalList.Insert(0, 0x01);
                totalList.Insert(0, 0x01);
                totalList.Insert(0, 0x02);

                totalList.InsertRange(0, Int2ByteArray(totalList.Count));

                var ResultinStr02 = ByteArrayToString(Int2ByteArray(totalList.Count));
                totalList.Insert(0, 0x65);

                var ResultinStr = ByteArrayToString(totalList.ToArray());

                return totalList.ToArray();

            }
            catch (Exception ex)
            {
                return null;
            }
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
        /// <param name="test"></param>
        /// <returns></returns>
        public byte[] GetDG5xx(bool test = false)
        {
            try
            {
                string _total = string.Empty;

                List<byte> TypeOfImage = new List<byte>();
                List<byte> JPEG = new List<byte>();

                List<byte> totalList = new List<byte>();

                DateTime _t = DateTime.Now;

                TypeOfImage.Add(0x89);
                TypeOfImage.Add(0x01);
                TypeOfImage.Add(0x03);

                if (test)
                {
                    JPEG.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20, 0x0D, 0x0A, 0x87, 0x0A, 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x6A, 0x70, 0x32, 0x20, 0x00, 0x00, 0x00, 0x00, 0x6A, 0x70, 0x32, 0x20, 0x00, 0x00, 0x00, 0x2D, 0x6A, 0x70, 0x32, 0x68, 0x00, 0x00, 0x00, 0x16, 0x69, 0x68, 0x64, 0x72, 0x00, 0x00, 0x00, 0x49, 0x00, 0x00, 0x00, 0xDE, 0x00, 0x04, 0x07, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x63, 0x6F, 0x6C, 0x72, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x14, 0xB4, 0x6A, 0x70, 0x32, 0x63, 0xFF, 0x4F, 0xFF, 0x51, 0x00, 0x32, 0x00, 0x00, 0x00, 0x00, 0x00, 0xDE, 0x00, 0x00, 0x00, 0x49, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xDE, 0x00, 0x00, 0x00, 0x49, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x07, 0x01, 0x01, 0x07, 0x01, 0x01, 0x07, 0x01, 0x01, 0x07, 0x01, 0x01, 0xFF, 0x52, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x01, 0x00, 0x05, 0x04, 0x04, 0x00, 0x01, 0xFF, 0x5C, 0x00, 0x13, 0x40, 0x40, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0xFF, 0x90, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x14, 0x51, 0x00, 0x01, 0xFF, 0x53, 0x00, 0x09, 0x01, 0x00, 0x05, 0x04, 0x04, 0x00, 0x01, 0xFF, 0x5D, 0x00, 0x14, 0x01, 0x40, 0x40, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0xFF, 0x53, 0x00, 0x09, 0x02, 0x00, 0x05, 0x04, 0x04, 0x00, 0x01, 0xFF, 0x5D, 0x00, 0x14, 0x02, 0x40, 0x40, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0xFF, 0x53, 0x00, 0x09, 0x03, 0x00, 0x05, 0x04, 0x04, 0x00, 0x01, 0xFF, 0x5D, 0x00, 0x14, 0x03, 0x40, 0x40, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0x48, 0x48, 0x50, 0xFF, 0x93, 0xDF, 0x78, 0xC0, 0x02, 0xE8, 0xCD, 0x48, 0x0A, 0x84, 0x38, 0x1D, 0xD6, 0x2E, 0x77, 0x77, 0x25, 0x85, 0x8F, 0xFE, 0x34, 0x9F, 0x6D, 0x0F, 0x28, 0x29, 0xF6, 0x8E, 0xDF, 0x78, 0xC0, 0x02, 0xE8, 0xCD, 0x48, 0x0A, 0x84, 0x38, 0x1D, 0xD6, 0x2E, 0x77, 0x77, 0x25, 0x85, 0x8F, 0xFE, 0x34, 0x9F, 0x6D, 0x0F, 0x28, 0x29, 0xF6, 0x8E, 0xDF, 0x78, 0xC0, 0x02, 0xE8, 0xCD, 0x48, 0x0A, 0x84, 0x38, 0x1D, 0xD6, 0x2E, 0x77, 0x77, 0x25, 0x85, 0x8F, 0xFE, 0x34, 0x9F, 0x6D, 0x0F, 0x28, 0x29, 0xF6, 0x8E, 0xDF, 0x60, 0xB0, 0x05, 0x98, 0xD5, 0x48, 0x0A, 0x84, 0x38, 0x2E, 0xC7, 0x79, 0x51, 0x0E, 0x43, 0x29, 0x29, 0x83, 0x0E, 0xAE, 0x36, 0x38, 0xF5, 0xE9, 0xC7, 0xC6, 0x3E, 0x7D, 0x23, 0x63, 0xE6, 0x1A, 0x12, 0x95, 0xEE, 0xA8, 0x9B, 0xB2, 0x07, 0xFD, 0x02, 0xCE, 0xA5, 0xC7, 0x18, 0x32, 0x63, 0x23, 0x5C, 0x62, 0x40, 0xDC, 0xC5, 0x25, 0xA2, 0x96, 0xCA, 0x41, 0xB5, 0xCB, 0x13, 0xD3, 0xBD, 0xAB, 0x06, 0x5E, 0xE2, 0x6B, 0x8F, 0x51, 0xC9, 0xAB, 0xD6, 0xC7, 0xC6, 0x3E, 0x7D, 0x23, 0x63, 0xE6, 0x1A, 0x12, 0x95, 0xEE, 0xA8, 0x9B, 0xB2, 0x07, 0xFD, 0x02, 0xCE, 0xA5, 0xC7, 0x18, 0x32, 0x63, 0x23, 0x5C, 0x62, 0x40, 0xDC, 0xC5, 0x25, 0xA2, 0x96, 0xCA, 0x41, 0xB5, 0xCB, 0x13, 0xD3, 0xBD, 0xAB, 0x06, 0x5E, 0xE2, 0x6B, 0x8F, 0x51, 0xC9, 0xAB, 0xD6, 0xC7, 0xC6, 0x3E, 0x7D, 0x23, 0x63, 0xE6, 0x1A, 0x12, 0x95, 0xEE, 0xA8, 0x9B, 0xB2, 0x07, 0xFD, 0x02, 0xCE, 0xA5, 0xC7, 0x18, 0x32, 0x63, 0x23, 0x5C, 0x62, 0x40, 0xDC, 0xC5, 0x25, 0xA2, 0x96, 0xCA, 0x41, 0xB5, 0xCB, 0x13, 0xD3, 0xBD, 0xAB, 0x06, 0x5E, 0xE2, 0x6B, 0x8F, 0x51, 0xC9, 0xAB, 0xD6, 0xC7, 0xC6, 0x3E, 0x7C, 0xC2, 0xE3, 0xE6, 0x1A, 0x11, 0xE1, 0x65, 0xA1, 0x8E, 0x90, 0x95, 0x75, 0x03, 0x4A, 0x9E, 0x4D, 0xF8, 0x66, 0xC1, 0x22, 0x5B, 0xBC, 0x81, 0xB9, 0x8A, 0x4B, 0x45, 0x4C, 0xF9, 0x65, 0x14, 0xA9, 0xBF, 0xAB, 0x41, 0x97, 0xB8, 0x9A, 0x9A, 0xFE, 0x7A, 0xDA, 0x3E, 0xCF, 0x9D, 0x35, 0xF4, 0xAE, 0x9F, 0x32, 0x70, 0x2D, 0x2B, 0x55, 0x84, 0x87, 0xD0, 0xAE, 0x07, 0xEE, 0x57, 0xD5, 0x6D, 0xCD, 0x54, 0xB1, 0x52, 0x44, 0xB4, 0x76, 0x82, 0xAB, 0xE4, 0x19, 0x4C, 0xCF, 0xD4, 0xDA, 0x26, 0xB6, 0xC2, 0xB7, 0xFB, 0x5B, 0xA9, 0xD0, 0x71, 0x8C, 0xDF, 0x28, 0xBE, 0x51, 0x9E, 0x0B, 0xB2, 0x41, 0x0F, 0xCC, 0xAE, 0xC7, 0xDD, 0xE6, 0x59, 0x4A, 0xF8, 0xDC, 0x28, 0x71, 0x5E, 0xC3, 0x76, 0xF8, 0x6F, 0xAA, 0xC3, 0xA1, 0xA9, 0xBD, 0x92, 0x64, 0x8A, 0x08, 0xD6, 0xA6, 0xE7, 0xF6, 0x79, 0x4B, 0xED, 0x3D, 0x06, 0x93, 0x95, 0x53, 0x08, 0x1E, 0x6D, 0x7B, 0x11, 0xE2, 0xF4, 0x03, 0x25, 0xE7, 0x68, 0xC8, 0xDB, 0x4C, 0x3F, 0x5B, 0x49, 0x91, 0x87, 0xA8, 0x8C, 0xCE, 0xE8, 0x61, 0x00, 0x69, 0x1E, 0x14, 0x46, 0xA3, 0x16, 0xAD, 0x5C, 0xF3, 0x0C, 0xCF, 0x3E, 0xB2, 0x34, 0x05, 0xCF, 0x9D, 0x35, 0xF4, 0xAE, 0x9F, 0x32, 0x70, 0x2D, 0x2B, 0x55, 0x84, 0x87, 0xD0, 0xAE, 0x07, 0xEE, 0x57, 0xD5, 0x6D, 0xCD, 0x54, 0xB1, 0x52, 0x44, 0xB4, 0x76, 0x82, 0xAB, 0xE4, 0x19, 0x4C, 0xCF, 0xD4, 0xDA, 0x26, 0xB6, 0xC2, 0xB7, 0xFB, 0x5B, 0xA9, 0xD0, 0x71, 0x8C, 0xDF, 0x28, 0xBE, 0x51, 0x9E, 0x0B, 0xB2, 0x41, 0x0F, 0xCC, 0xAE, 0xC7, 0xDD, 0xE6, 0x59, 0x4A, 0xF8, 0xDC, 0x28, 0x71, 0x5E, 0xC3, 0x76, 0xF8, 0x6F, 0xAA, 0xC3, 0xA1, 0xA9, 0xBD, 0x92, 0x64, 0x8A, 0x08, 0xD6, 0xA6, 0xE7, 0xF6, 0x79, 0x4B, 0xED, 0x3D, 0x06, 0x93, 0x95, 0x53, 0x08, 0x1E, 0x6D, 0x7B, 0x11, 0xE2, 0xF4, 0x03, 0x25, 0xE7, 0x68, 0xC8, 0xDB, 0x4C, 0x3F, 0x5B, 0x49, 0x91, 0x87, 0xA8, 0x8C, 0xCE, 0xE8, 0x61, 0x00, 0x69, 0x1E, 0x14, 0x46, 0xA3, 0x16, 0xAD, 0x5C, 0xF3, 0x0C, 0xCF, 0x3E, 0xB2, 0x34, 0x05, 0xCF, 0x9D, 0x35, 0xF4, 0xAE, 0x9F, 0x32, 0x70, 0x2D, 0x2B, 0x55, 0x84, 0x87, 0xD0, 0xAE, 0x07, 0xEE, 0x57, 0xD5, 0x6D, 0xCD, 0x54, 0xB1, 0x52, 0x44, 0xB4, 0x76, 0x82, 0xAB, 0xE4, 0x19, 0x4C, 0xCF, 0xD4, 0xDA, 0x26, 0xB6, 0xC2, 0xB7, 0xFB, 0x5B, 0xA9, 0xD0, 0x71, 0x8C, 0xDF, 0x28, 0xBE, 0x51, 0x9E, 0x0B, 0xB2, 0x41, 0x0F, 0xCC, 0xAE, 0xC7, 0xDD, 0xE6, 0x59, 0x4A, 0xF8, 0xDC, 0x28, 0x71, 0x5E, 0xC3, 0x76, 0xF8, 0x6F, 0xAA, 0xC3, 0xA1, 0xA9, 0xBD, 0x92, 0x64, 0x8A, 0x08, 0xD6, 0xA6, 0xE7, 0xF6, 0x79, 0x4B, 0xED, 0x3D, 0x06, 0x93, 0x95, 0x53, 0x08, 0x1E, 0x6D, 0x7B, 0x11, 0xE2, 0xF4, 0x03, 0x25, 0xE7, 0x68, 0xC8, 0xDB, 0x4C, 0x3F, 0x5B, 0x49, 0x91, 0x87, 0xA8, 0x8C, 0xCE, 0xE8, 0x61, 0x00, 0x69, 0x1E, 0x14, 0x46, 0xA3, 0x16, 0xAD, 0x5C, 0xF3, 0x0C, 0xCF, 0x3E, 0xB2, 0x34, 0x05, 0xCF, 0x99, 0x35, 0xF4, 0xB0, 0x9F, 0x32, 0x70, 0x2D, 0xD7, 0x57, 0x84, 0x87, 0xD0, 0xAE, 0x08, 0x2A, 0x9D, 0xDB, 0xE9, 0x76, 0x9E, 0xED, 0xB5 }); //_DL._Photo
                }
                else
                {
                    byte[] bytesPhoto = Convert.FromBase64String(_DL._Signature);

                    if (bytesPhoto.Length > 5120)
                    {
                        byte[] bytesPhotoM = new byte[5120];

                        Array.Copy(bytesPhoto, 0, bytesPhotoM, 0, 5120);

                        JPEG.AddRange(bytesPhotoM);
                    }
                    else
                    {
                        JPEG.AddRange(bytesPhoto);
                    }
                }

                JPEG.InsertRange(0, Int2ByteArray(JPEG.Count));

                var ResultinStr01 = ByteArrayToString(Int2ByteArray(JPEG.Count));
                JPEG.Insert(0, 0x43);
                JPEG.Insert(0, 0x5F);

                totalList.AddRange(TypeOfImage);
                totalList.AddRange(JPEG);

                totalList.InsertRange(0, Int2ByteArray(totalList.Count));

                var ResultinStr02 = ByteArrayToString(Int2ByteArray(totalList.Count));
                totalList.Insert(0, 0x67);

                var ResultinStr = ByteArrayToString(totalList.ToArray());

                return totalList.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public byte[] GetCommon()
        {
            /*
             * ‘60’ ’0C’ ‘5F01’ ’02’ 0100 ‘5C’ ’05’ '61' '6B' '6C' '65' '67' 
             */
            List<byte> Common = new List<byte>();

            Common.AddRange(new byte[] { 0x54, 0x01, 0x00, 0x53, 0x5E, 0x60, 0x5C, 0x5F, 0x01, 0x02, 0x01, 0x00, 0x5C, 0x07, 0x61, 0x6B, 0x6C, 0x65, 0x67, 0x63, 0x6E, 0x86, 0x4C, 0x31, 0x4A, 0x30, 0x48, 0x30, 0x41, 0x06, 0x07, 0x28, 0x81, 0x8C, 0x5D, 0x03, 0x02, 0x02, 0x30, 0x36, 0x02, 0x01, 0x00, 0x02, 0x01, 0x0E, 0x06, 0x08, 0x28, 0x81, 0x8C, 0x5D, 0x03, 0x02, 0x01, 0x01, 0x04, 0x11, 0x0F, 0x41, 0x54, 0x43, 0x56, 0x43, 0x41, 0x5F, 0x4E, 0x58, 0x50, 0x30, 0x30, 0x30, 0x30, 0x31, 0x00, 0x04, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x31, 0x03, 0x02, 0x01, 0x03 });

            return Common.ToArray();
        }

        public DrivingLicenseExample _driverLicense = new DrivingLicenseExample();

        public DXX_LicenseExample _dxxLicense = new DXX_LicenseExample();

        public DrivingLicenseExample ParseReadMaterial(byte[] DG1, byte[] DG2, byte[] DG3, byte[] DG4, byte[] DG5, byte[] DGCommon)
        {

            ParseDG1(DG1);

            ParseDG2(DG2);

            //ParseDG3(DG3);

            ParseDG4(DG4);

            ParseDG5(DG5);

            return _driverLicense;
        }


        public DXX_LicenseExample ParseReadDXX(byte[] DG1, byte[] DG2, byte[] DG3, byte[] DG4, byte[] DG5, byte[] DGCommon)
        {
            ParseDXX(DG1);

            ParseDG4DXX(DG4);

            ParseDG5DXX(DG5);

            return _dxxLicense;
        }

        private void ParseDG1(byte[] DG1)
        {
            string Hex2Str = ByteArrayToString(DG1);

            int n = 0, m = 0, k = 0;

            if (Hex2Str.Substring(2, 2).Equals("81"))
            {
                n = 6;
                m = 7;
                k = 8;
            }
            else if (Hex2Str.Substring(2, 2).Equals("82"))
            {
                n = 7;
                m = 8;
                k = 9;
            }
            else
            {
                n = 5;
                m = 6;
                k = 7;
            }

            byte[] demografLenth = new byte[4];

            demografLenth[0] = DG1[n]; //6 in Correct mode

            int idemografLenth = BitConverter.ToInt32(demografLenth, 0);

            byte[] NameLenth = new byte[4];

            NameLenth[0] = DG1[m]; //7 in Correct mode


            int iNameLenth = BitConverter.ToInt32(NameLenth, 0);

            byte[] Name = new byte[iNameLenth];

            Array.Copy(DG1, k, Name, 0, iNameLenth); // k in correct mode

            string resultLastName = Encoding.UTF8.GetString(Name);

            _driverLicense._driver._last_name = resultLastName;

            byte[] FullNameLength = new byte[4];

            FullNameLength[0] = DG1[k + iNameLenth];

            int iFullLenth = BitConverter.ToInt32(FullNameLength, 0);

            byte[] FullName = new byte[iFullLenth];

            Array.Copy(DG1, k + iNameLenth + 1, FullName, 0, iFullLenth);

            string resultFullName = Encoding.UTF8.GetString(FullName);

            var splitted = resultFullName.Split('_');

            if (splitted.Length >= 3)
            {
                _driverLicense._driver._first_name = splitted[1];
                _driverLicense._driver._middle_name = splitted[2];
            }
            else if (splitted.Length == 2)
            {
                _driverLicense._driver._first_name = splitted[1];
            }

            int currectPos = k + iNameLenth + 1 + iFullLenth;

            byte[] dateOfBirth = new byte[4];

            dateOfBirth[0] = DG1[currectPos + 0];
            dateOfBirth[1] = DG1[currectPos + 1];
            dateOfBirth[2] = DG1[currectPos + 2];
            dateOfBirth[3] = DG1[currectPos + 3];

            var dateOfBirthStr = ByteArrayToString(dateOfBirth);


            _driverLicense._driver._date_of_birth = dateOfBirthStr;

            byte[] dateOfIssue = new byte[4];

            dateOfIssue[0] = DG1[currectPos + 4];
            dateOfIssue[1] = DG1[currectPos + 5];
            dateOfIssue[2] = DG1[currectPos + 6];
            dateOfIssue[3] = DG1[currectPos + 7];

            var dateOfIssueStr = ByteArrayToString(dateOfIssue);

            _driverLicense._issue_date = dateOfIssueStr;

            byte[] dateOfExpire = new byte[4];

            dateOfExpire[0] = DG1[currectPos + 8];
            dateOfExpire[1] = DG1[currectPos + 9];
            dateOfExpire[2] = DG1[currectPos + 10];
            dateOfExpire[3] = DG1[currectPos + 11];

            var dateOfExpireStr = ByteArrayToString(dateOfExpire);

            _driverLicense._expire_date = dateOfExpireStr;

            currectPos += 11;

            byte[] Country = new byte[3];

            Country[0] = DG1[currectPos + 1];
            Country[1] = DG1[currectPos + 2];
            Country[2] = DG1[currectPos + 3];

            string CountryName = Encoding.UTF8.GetString(Country);

            //IA get Data

            byte[] IALenth = new byte[4];

            IALenth[0] = DG1[currectPos + 4];

            int IALenthLenthL = BitConverter.ToInt32(IALenth, 0);

            currectPos += 4;

            byte[] IAFullName = new byte[IALenthLenthL];

            Array.Copy(DG1, currectPos + 1, IAFullName, 0, IALenthLenthL);

            string IAFullName1 = Encoding.UTF8.GetString(IAFullName);

            _driverLicense._issue_region_name = IAFullName1;

            //Licesnse Number Get
            currectPos += IALenthLenthL;

            byte[] LicenseNumberLenth = new byte[4];

            LicenseNumberLenth[0] = DG1[currectPos + 1];

            int LicenseNumLL = BitConverter.ToInt32(LicenseNumberLenth, 0);

            byte[] LicenseNumName = new byte[LicenseNumLL];

            Array.Copy(DG1, currectPos + 2, LicenseNumName, 0, LicenseNumLL);

            string LicenseN = Encoding.UTF8.GetString(LicenseNumName);

            _driverLicense._license_number = LicenseN;

            currectPos += 2;
            currectPos += LicenseNumLL;

            ///////////////////////////////////////////////////////////
            /// Category
            /// ///////////////////////////////////////////////////////

            byte[] CategorySiklLenth = new byte[4];

            CategorySiklLenth[0] = DG1[currectPos + 5];

            int CategorySiklLenthLL = BitConverter.ToInt32(CategorySiklLenth, 0);

            currectPos += 5;

            _driverLicense._categories = new Category[CategorySiklLenthLL];

            for (int j = 0; j < CategorySiklLenthLL; j++)
            {
                currectPos += 2;

                byte[] FirstCategoryLen = new byte[4];

                FirstCategoryLen[0] = DG1[currectPos];

                int FirstCategoryLenhLL = BitConverter.ToInt32(FirstCategoryLen, 0);

                byte[] FirstCategoryData = new byte[FirstCategoryLenhLL];

                Array.Copy(DG1, currectPos + 1, FirstCategoryData, 0, FirstCategoryLenhLL);

                string FirstCategoryDataStr = Encoding.UTF8.GetString(FirstCategoryData);

                var FirstCategoryItems = FirstCategoryDataStr.Split(';');

                if (FirstCategoryItems.Length == 6)
                {
                    _driverLicense._categories[j] = new Category();

                    _driverLicense._categories[j]._name = FirstCategoryItems[0];
                    _driverLicense._categories[j]._issue_date = FirstCategoryItems[1];
                    _driverLicense._categories[j]._expiry_date = FirstCategoryItems[2];
                    _driverLicense._categories[j]._additional_information = FirstCategoryItems[3];

                }

                currectPos += FirstCategoryLenhLL;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void ParseDXX(byte[] Vr)
        {
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

                    SetData(tag, value);

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

        }


        private void SetData(byte tag, byte[] value)
        {
            switch (tag)
            {
                case 0xA1:
                    _dxxLicense._first_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xA2:
                    _dxxLicense._last_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xA3:
                    _dxxLicense._middle_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xA4:
                    _dxxLicense._issue_date = Encoding.UTF8.GetString(value);
                    break;
                case 0xA5:
                    _dxxLicense._expire_date = Encoding.UTF8.GetString(value);
                    break;
                case 0xA6:
                    _dxxLicense._pinfl = Encoding.UTF8.GetString(value);
                    break;
                case 0xA7:
                    _dxxLicense._position = Encoding.UTF8.GetString(value);
                    break;
                case 0xA8:
                    _dxxLicense._badge_number = Encoding.UTF8.GetString(value);
                    break;
                case 0xA9:
                    _dxxLicense._rank = Encoding.UTF8.GetString(value);
                    break;
             }
        }

        public string ParseDG1Expired(byte[] DG1)
        {
            string Hex2Str = ByteArrayToString(DG1);

            int n = 0, m = 0, k = 0;

            if (Hex2Str.Substring(2, 2).Equals("81"))
            {
                n = 6;
                m = 7;
                k = 8;
            }
            else if (Hex2Str.Substring(2, 2).Equals("82"))
            {
                n = 7;
                m = 8;
                k = 9;
            }
            else
            {
                n = 5;
                m = 6;
                k = 7;
            }

            byte[] demografLenth = new byte[4];

            demografLenth[0] = DG1[n]; //6 in Correct mode

            int idemografLenth = BitConverter.ToInt32(demografLenth, 0);

            byte[] NameLenth = new byte[4];

            NameLenth[0] = DG1[m]; //7 in Correct mode


            int iNameLenth = BitConverter.ToInt32(NameLenth, 0);

            byte[] Name = new byte[iNameLenth];

            Array.Copy(DG1, k, Name, 0, iNameLenth); // k in correct mode

            string resultLastName = Encoding.UTF8.GetString(Name);

            _driverLicense._driver._last_name = resultLastName;

            byte[] FullNameLength = new byte[4];

            FullNameLength[0] = DG1[k + iNameLenth];

            int iFullLenth = BitConverter.ToInt32(FullNameLength, 0);

            byte[] FullName = new byte[iFullLenth];

            Array.Copy(DG1, k + iNameLenth + 1, FullName, 0, iFullLenth);

            string resultFullName = Encoding.UTF8.GetString(FullName);

            var splitted = resultFullName.Split('_');

            if (splitted.Length >= 3)
            {
                _driverLicense._driver._first_name = splitted[1];
                _driverLicense._driver._middle_name = splitted[2];
            }
            else if (splitted.Length == 2)
            {
                _driverLicense._driver._first_name = splitted[1];
            }

            int currectPos = k + iNameLenth + 1 + iFullLenth;

            byte[] dateOfBirth = new byte[4];

            dateOfBirth[0] = DG1[currectPos + 0];
            dateOfBirth[1] = DG1[currectPos + 1];
            dateOfBirth[2] = DG1[currectPos + 2];
            dateOfBirth[3] = DG1[currectPos + 3];

            var dateOfBirthStr = ByteArrayToString(dateOfBirth);


            _driverLicense._driver._date_of_birth = dateOfBirthStr;

            byte[] dateOfIssue = new byte[4];

            dateOfIssue[0] = DG1[currectPos + 4];
            dateOfIssue[1] = DG1[currectPos + 5];
            dateOfIssue[2] = DG1[currectPos + 6];
            dateOfIssue[3] = DG1[currectPos + 7];

            var dateOfIssueStr = ByteArrayToString(dateOfIssue);

            _driverLicense._issue_date = dateOfIssueStr;

            byte[] dateOfExpire = new byte[4];

            dateOfExpire[0] = DG1[currectPos + 8];
            dateOfExpire[1] = DG1[currectPos + 9];
            dateOfExpire[2] = DG1[currectPos + 10];
            dateOfExpire[3] = DG1[currectPos + 11];

            var dateOfExpireStr = ByteArrayToString(dateOfExpire);

            _driverLicense._expire_date = dateOfExpireStr;

            currectPos += 11;

            byte[] Country = new byte[3];

            Country[0] = DG1[currectPos + 1];
            Country[1] = DG1[currectPos + 2];
            Country[2] = DG1[currectPos + 3];

            string CountryName = Encoding.UTF8.GetString(Country);

            //IA get Data

            byte[] IALenth = new byte[4];

            IALenth[0] = DG1[currectPos + 4];

            int IALenthLenthL = BitConverter.ToInt32(IALenth, 0);

            currectPos += 4;

            byte[] IAFullName = new byte[IALenthLenthL];

            Array.Copy(DG1, currectPos + 1, IAFullName, 0, IALenthLenthL);

            string IAFullName1 = Encoding.UTF8.GetString(IAFullName);

            _driverLicense._issue_region_name = IAFullName1;

            //Licesnse Number Get
            currectPos += IALenthLenthL;

            byte[] LicenseNumberLenth = new byte[4];

            LicenseNumberLenth[0] = DG1[currectPos + 1];

            int LicenseNumLL = BitConverter.ToInt32(LicenseNumberLenth, 0);

            byte[] LicenseNumName = new byte[LicenseNumLL];

            Array.Copy(DG1, currectPos + 2, LicenseNumName, 0, LicenseNumLL);

            string LicenseN = Encoding.UTF8.GetString(LicenseNumName);

            _driverLicense._license_number = LicenseN;

            currectPos += 2;
            currectPos += LicenseNumLL;

            ///////////////////////////////////////////////////////////
            /// Category
            /// ///////////////////////////////////////////////////////

            byte[] CategorySiklLenth = new byte[4];

            CategorySiklLenth[0] = DG1[currectPos + 5];

            int CategorySiklLenthLL = BitConverter.ToInt32(CategorySiklLenth, 0);

            currectPos += 5;

            _driverLicense._categories = new Category[CategorySiklLenthLL];

            for (int j = 0; j < CategorySiklLenthLL; j++)
            {
                currectPos += 2;

                byte[] FirstCategoryLen = new byte[4];

                FirstCategoryLen[0] = DG1[currectPos];

                int FirstCategoryLenhLL = BitConverter.ToInt32(FirstCategoryLen, 0);

                byte[] FirstCategoryData = new byte[FirstCategoryLenhLL];

                Array.Copy(DG1, currectPos + 1, FirstCategoryData, 0, FirstCategoryLenhLL);

                string FirstCategoryDataStr = Encoding.UTF8.GetString(FirstCategoryData);

                var FirstCategoryItems = FirstCategoryDataStr.Split(';');

                if (FirstCategoryItems.Length == 6)
                {
                    _driverLicense._categories[j] = new Category();

                    _driverLicense._categories[j]._name = FirstCategoryItems[0];
                    _driverLicense._categories[j]._issue_date = FirstCategoryItems[1];
                    _driverLicense._categories[j]._expiry_date = FirstCategoryItems[2];
                    _driverLicense._categories[j]._additional_information = FirstCategoryItems[3];

                }

                currectPos += FirstCategoryLenhLL;
            }

            return _driverLicense._expire_date;

        }

        private void ParseDG2(byte[] DG2)
        {
            string Hex2Str = ByteArrayToString(DG2);

            byte[] brthPlsLenth = new byte[4];

            brthPlsLenth[0] = DG2[47];

            int birthPlaceLenth = BitConverter.ToInt32(brthPlsLenth, 0);

            byte[] Name = new byte[birthPlaceLenth];

            Array.Copy(DG2, 48, Name, 0, birthPlaceLenth);

            string resultLastName = Encoding.UTF8.GetString(Name);

            var splitted = resultLastName.Split(';');

            _driverLicense._driver._region_name_birth = splitted[0];

            _driverLicense._driver._pinfl = splitted[1];

            int currentPos = 48 + birthPlaceLenth + 2;

            byte[] resPlsLenth = new byte[4];

            resPlsLenth[0] = DG2[currentPos];

            int resPlaceLenth = BitConverter.ToInt32(resPlsLenth, 0);

            byte[] ResName = new byte[resPlaceLenth];

            Array.Copy(DG2, currentPos + 1, ResName, 0, resPlaceLenth);

            string resultResLastName = Encoding.UTF8.GetString(ResName);

            var splittedRes = resultResLastName.Split(';');

            _driverLicense._driver._address._address = splittedRes[0];

            if (splittedRes.Count() > 1)
                _driverLicense._driver._address._region_name = splittedRes[2];

            if (splittedRes.Count() > 2)
                _driverLicense._driver._address._rayon_name = splittedRes[3];


        }

        private void ParseDG3(byte[] DG3)
        {

        }

        public byte[] Decode(byte[] packet)
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

        private void ParseDG4(byte[] DG4)
        {
            string Hex2Str = ByteArrayToString(DG4);

            var Trimmed = Decode(DG4);

            byte[] ImageBytes = new byte[Trimmed.Length - 28];

            Array.Copy(Trimmed, 28, ImageBytes, 0, Trimmed.Length - 28);


            string Hex2Str2 = ByteArrayToString(ImageBytes);

            _driverLicense._driver._Photo = Convert.ToBase64String(ImageBytes);
        }

        private void ParseDG4DXX(byte[] DG4)
        {
            string Hex2Str = ByteArrayToString(DG4);

            var Trimmed = Decode(DG4);

            byte[] ImageBytes = new byte[Trimmed.Length - 28];

            Array.Copy(Trimmed, 28, ImageBytes, 0, Trimmed.Length - 28);


            string Hex2Str2 = ByteArrayToString(ImageBytes);

            _dxxLicense._Photo = Convert.ToBase64String(ImageBytes);
        }

        private void ParseDG5(byte[] DG5)
        {
            string Hex2Str = ByteArrayToString(DG5);

            var Trimmed = Decode(DG5);

            byte[] ImageBytes = new byte[Trimmed.Length - 12];

            Array.Copy(Trimmed, 12, ImageBytes, 0, Trimmed.Length - 12);


            _driverLicense._driver._Signature = Convert.ToBase64String(ImageBytes);
        }

        private void ParseDG5DXX(byte[] DG5)
        {
            string Hex2Str = ByteArrayToString(DG5);

            var Trimmed = Decode(DG5);

            byte[] ImageBytes = new byte[Trimmed.Length - 12];

            Array.Copy(Trimmed, 12, ImageBytes, 0, Trimmed.Length - 12);


            _dxxLicense._Signature = Convert.ToBase64String(ImageBytes);
        }
    }

    [DataContract]
    public class DXX_LicenseExample
    {
        [DataMember(Name = "first_name")] //DG1
        public string _first_name { get; set; }

        [DataMember(Name = "last_name")] //DG1
        public string _last_name { get; set; }

        [DataMember(Name = "middle_name")] //DG1
        public string _middle_name { get; set; }

        [DataMember(Name = "issue_date")] //DG1
        public string _issue_date { get; set; }

        [DataMember(Name = "expire_date")] //DG1
        public string _expire_date { get; set; }

        [DataMember(Name = "pinfl")] //DG1
        public string _pinfl { get; set; }

        [DataMember(Name = "position")] //DG1
        public string _position { get; set; }

        [DataMember(Name = "badge_number")] //DG1
        public string _badge_number { get; set; }

        [DataMember(Name = "rank")] //DG1
        public string _rank { get; set; }

        [DataMember(Name = "photo")]
        public string _Photo { get; set; }

        [DataMember(Name = "signature")]
        public string _Signature { get; set; }
    }
}

