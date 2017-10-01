using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] key = new byte[16];

            #region Keys
            key[0] = 0x40;
            key[1] = 0x41;
            key[2] = 0x42;
            key[3] = 0x43;
            key[4] = 0x44;
            key[5] = 0x45;
            key[6] = 0x46;
            key[7] = 0x47;
            key[8] = 0x48;
            key[9] = 0x49;
            key[10] = 0x4A;
            key[11] = 0x4B;
            key[12] = 0x4C;
            key[13] = 0x4D;
            key[14] = 0x4E;
            key[15] = 0x4F;

            #endregion

            byte[] GET_DATA_BYTE = new byte[] { 0x45, 0x01, 0x0D, 0x02, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            byte[] CHIP_NUMBER = new byte[] { 0x45, 0x01, 0x0D, 0x02, 0x08 };

            byte[] Card_spes_Key = encryptAES(key, CHIP_NUMBER);

            var RES1 = ByteArrayToString(Card_spes_Key);

            //67 EC E1 E9 FE 2F 2F ED
            //C9 6A 36 CA D6 97 F3 B7
            //BB EF A0 64 88 22 DA C5
            //EC 9C 3E 69 E2 AE F6 FC
            byte[] GET_CHALLANGE = new byte[] { 0xEC, 0x9C, 0x3E, 0x69, 0xE2, 0xAE, 0xF6, 0xFC };

            byte[] GET_TERMINAL_RANDOM = SecureRandom.GetBytes(8);

            List<byte> concat = new List<byte>();

            concat.AddRange(GET_TERMINAL_RANDOM);
            concat.AddRange(GET_CHALLANGE);
            concat.AddRange(CHIP_NUMBER);

            //byte[] Pad = new byte[] { 0x00,0x00,0x00};

            //concat.AddRange(Pad);

            byte[] XIFD = encryptAES(Card_spes_Key, concat.ToArray());
            
            var RES2 = ByteArrayToString(Card_spes_Key);

            List<byte> concatTT = new List<byte>();

            concatTT.AddRange(XIFD);

            concatTT.AddRange(Card_spes_Key);

            var RES3 = ByteArrayToString(concatTT.ToArray());

        }

        private static TripleDESCryptoServiceProvider e1 = new TripleDESCryptoServiceProvider();
        private static DESCryptoServiceProvider d1 = new DESCryptoServiceProvider();
        private static AesCryptoServiceProvider a1 = new AesCryptoServiceProvider();

        public static byte[] encryptDES(byte[] key, byte[] data)
        {
            d1.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            if (key.Length != 0x08 &&
                key.Length != 0x10 &&
                key.Length != 0x18)
                throw new Exception("Invalid key length");
            d1.Padding = PaddingMode.None;
            d1.Mode = CipherMode.CBC;
            d1.Key = key;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, d1.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                return ms.ToArray();
            }

        }

        public static byte[] encryptAES(byte[] key, byte[] data)
        {
            a1.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            if (key.Length != 0x08 &&
                key.Length != 0x10 &&
                key.Length != 0x18)
                throw new Exception("Invalid key length");
            a1.Padding = PaddingMode.Zeros;
            a1.Mode = CipherMode.CBC;
            a1.Key = key;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, a1.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                return ms.ToArray();
            }

        }

        public static byte[] encrypt3DES(byte[] key, byte[] data)
        {
            e1.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            if (key.Length != 0x08 &&
                key.Length != 0x10 &&
                key.Length != 0x18)
                throw new Exception("Invalid key length");
            e1.Padding = PaddingMode.None;
            e1.Mode = CipherMode.CBC;
            e1.Key = key;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, e1.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                return ms.ToArray();
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public static byte[] calculateSHA1(byte[] temp)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(temp, 0, 16);
                return hash;
            }
        }
    }

    internal static class SecureRandom
    {
        public static byte[] GetBytes(int length)
        {
            var data = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }

            return data;
        }
    }
}
