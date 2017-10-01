using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExperiment2
{
    class Program
    {
        private static bool bacEstablished = false;
        
        private static byte[] kenc = null;

        private static byte[] kmac = null;

        private static byte[] ksenc = null;

        private static byte[] ksmac = null;
        
        private static byte[] ssc = new byte[8];
        
        private static byte[] rndicc = null;

        private static byte[] rndifd = null;
        
        private static byte[] kicc = null;

        private static byte[] kifd = null;

        
        /**

         * Constants that help in determining whether or not a byte array is parity

         * adjusted.

         */

        private static byte[] PARITY = { 8, 1, 0, 8, 0, 8, 8, 0, 0, 8, 8, 0,

            8, 0, 2, 8, 0, 8, 8, 0, 8, 0, 0, 8, 8, 0, 0, 8, 0, 8, 8, 3, 0, 8,

            8, 0, 8, 0, 0, 8, 8, 0, 0, 8, 0, 8, 8, 0, 8, 0, 0, 8, 0, 8, 8, 0,

            0, 8, 8, 0, 8, 0, 0, 8, 0, 8, 8, 0, 8, 0, 0, 8, 8, 0, 0, 8, 0, 8,

            8, 0, 8, 0, 0, 8, 0, 8, 8, 0, 0, 8, 8, 0, 8, 0, 0, 8, 8, 0, 0, 8,

            0, 8, 8, 0, 0, 8, 8, 0, 8, 0, 0, 8, 0, 8, 8, 0, 8, 0, 0, 8, 8, 0,

            0, 8, 0, 8, 8, 0, 0, 8, 8, 0, 8, 0, 0, 8, 8, 0, 0, 8, 0, 8, 8, 0,

            8, 0, 0, 8, 0, 8, 8, 0, 0, 8, 8, 0, 8, 0, 0, 8, 8, 0, 0, 8, 0, 8,

            8, 0, 0, 8, 8, 0, 8, 0, 0, 8, 0, 8, 8, 0, 8, 0, 0, 8, 8, 0, 0, 8,

            0, 8, 8, 0, 8, 0, 0, 8, 0, 8, 8, 0, 0, 8, 8, 0, 8, 0, 0, 8, 0, 8,

            8, 0, 8, 0, 0, 8, 8, 0, 0, 8, 0, 8, 8, 0, 4, 8, 8, 0, 8, 0, 0, 8,

            8, 0, 0, 8, 0, 8, 8, 0, 8, 5, 0, 8, 0, 8, 8, 0, 0, 8, 8, 0, 8, 0,

            6, 8 };

        static void Main(string[] args)
        {
            byte[] key = new byte[24];

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

            byte[] GET_DATA_BYTE = new byte[] { 0x45, 0x01, 0x0D, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            byte[] GET_CHALLANGE = new byte[] { 0xEC, 0x9C, 0x3E, 0x69, 0xE2, 0xAE, 0xF6, 0xFC };

            rndicc = GET_CHALLANGE;

            //byte[] mrzInfo = calculateMrzInfo(GET_DATA_BYTE);

            kenc = calculateKENC(GET_DATA_BYTE);

            kmac = calculateKMAC(GET_DATA_BYTE);

            byte[] Mutual = getMutualAuthenticationCommand();


        }

        public static byte[] getMutualAuthenticationCommand()
        {

            // 2. Generate an 8 byte random and a 16 byte random.

            rndifd = new byte[8];

            kifd = new byte[8];

            Random rand = new Random();

            rand.NextBytes(rndifd); // fill rndifd with random bytes

            rand.NextBytes(kifd); // fill kifd with random bytes

            // 3. Concatenate RND.IFD, RND.ICC and KIFD:

            byte[] s = new byte[24];

            Array.Copy(rndifd, 0, s, 0, rndifd.Length);

            Array.Copy(rndicc, 0, s, 8, rndicc.Length);

            Array.Copy(kifd, 0, s, 16, kifd.Length);

            
            // 4. Encrypt S with TDES key Kenc:

            byte[] eifd = encryptAES(kenc, s);

            var RES3 = ByteArrayToString(eifd);

            return eifd;
        }


 //       public static byte[] computeMAC(byte[] key, byte[] plaintext)
 //        {

 //       Cipher des;

 //       byte[] ka = new byte[8];

 //       byte[] kb = new byte[8];

 //       System.arraycopy(key, 0, ka, 0, 8);

	//	System.arraycopy(key, 8, kb, 0, 8);



	//	SecretKeySpec skeya = new SecretKeySpec(ka, "DES");

 //       SecretKeySpec skeyb = new SecretKeySpec(kb, "DES");

 //       byte[] current = new byte[8];

 //       byte[] mac = new byte[] { (byte) 0, (byte) 0, (byte) 0, (byte) 0,

 //               (byte) 0, (byte) 0, (byte) 0, (byte) 0 };



 //       plaintext = padByteArray(plaintext);



	//	for (int i = 0; i<plaintext.length; i += 8) {

	//		System.arraycopy(plaintext, i, current, 0, 8);

	//		mac = xorArray(current, mac);

 //       des = Cipher.getInstance("DES/ECB/NoPadding");

	//		des.init(Cipher.ENCRYPT_MODE, skeya);

	//		mac = des.update(mac);

	//	}

 //   des = Cipher.getInstance("DES/ECB/NoPadding");

	//	des.init(Cipher.DECRYPT_MODE, skeyb);

	//	mac = des.update(mac);



	//	des.init(Cipher.ENCRYPT_MODE, skeya);

	//	mac = des.doFinal(mac);

	//	return mac;

	//}


        public static byte[] calculateSHA1(byte[] temp)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(temp,0,16);
                return hash;
            }
        }

        private static AesCryptoServiceProvider a1 = new AesCryptoServiceProvider();

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


        private static byte[] calculateKSeed(byte[] mrzInfoBytes)
        {
            byte[] hash = calculateSHA1(mrzInfoBytes);

            byte[] kseed = new byte[16];

            for (int i = 0; i < 16; i++)

                kseed[i] = hash[i];

            return kseed;
        }

        private static void adjustParity(byte[] key, int offset)
        {
            for (int i = offset; i < 8; i++)
            {
                key[i] ^= (PARITY[key[i] & 0xff] == 8) ? (byte)1 : (byte)0;
            }
        }

        private static byte[] computeKey(byte[] kseed, byte[] c)
        {

            byte[] d = new byte[20];

            Array.Copy(kseed, 0, d, 0, kseed.Length);

            Array.Copy(c, 0, d, 16, c.Length);

            byte[] hd = calculateSHA1(d);

            byte[] ka = new byte[8];

            byte[] kb = new byte[8];

            Array.Copy(hd, 0, ka, 0, ka.Length);

            Array.Copy(hd, 8, kb, 0, kb.Length);
            
            // Adjust Parity-Bits

            adjustParity(ka, 0);

            adjustParity(kb, 0);
            
            byte[] key = new byte[24];

            Array.Copy(ka, 0, key, 0, 8);

            Array.Copy(kb, 0, key, 8, 8);

            Array.Copy(ka, 0, key, 16, 8);

            return key;

        }

        private static TripleDESCryptoServiceProvider e1 = new TripleDESCryptoServiceProvider();
        private static DESCryptoServiceProvider d1 = new DESCryptoServiceProvider();

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

        private static string calculateMrzInfo(string mrz)
        {
            string documentNr = mrz.Substring(0, 8); // +1 checkdiget

            string dateOfBirth = mrz.Substring(8, 8); // +1 checkdiget

            string dateOfExpiry = mrz.Substring(16, 8); // +1 checkdiget

            string mrzInfo = documentNr + dateOfBirth + dateOfExpiry;

            return mrzInfo;
        }
        
        private static byte[] calculateKMAC(byte[] mrzInfo)
        {

            //byte[] mrzinfobytes = Encoding.UTF8.GetBytes(mrzInfo);

            byte[] kseed = calculateKSeed(mrzInfo);

            return computeKey(kseed, new byte[] { 0, 0, 0, 2 });

        }

        public static byte[] exclusiveOR(byte[] key, byte[] PAN)
        {
            if (key.Length == PAN.Length)
            {
                byte[] result = new byte[key.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    result[i] = (byte)(key[i] ^ PAN[i]);
                }
                string hex = BitConverter.ToString(result).Replace("-", "");
                return result;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private static void calculateSessionKeys(byte[] kifd, byte[] kicc)
        {

            byte[] kseed = exclusiveOR(kicc, kifd);

            // 18.Calculate Session Keys (KS_ENC and KS_MAC):

            ksenc = computeKey(kseed, new byte[] { 0, 0, 0, 1 });

            ksmac = computeKey(kseed, new byte[] { 0, 0, 0, 2 });

        }

        private static byte[] calculateKENC(byte[] mrzInfo)
        {

            //byte[] mrzInfoBytes = Encoding.UTF8.GetBytes(mrzInfo);

            byte[] kseed = calculateKSeed(mrzInfo);

            return computeKey(kseed, new byte[] { 0, 0, 0, 1 });

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
