using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CardAPILib.InterfaceCL
{
    /// <summary>
    /// 
    /// </summary>
    public static class BAC_Calculate
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

        private static ILog log = LogManager.GetLogger(typeof(BAC_Calculate));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_rndIcc"></param>
        /// <returns></returns>
        public static byte[] CalcBAC_Res(byte[] _rndIcc, byte[] key)
        {
            if (key == null)
            {
                key = Encoding.UTF8.GetBytes("A290654395164273X970030110709303");
            }

            log.Info(string.Format("key = {0}", ByteArrayToString(key)));

            rndicc = _rndIcc;

            log.Info(string.Format("rndicc = {0}", ByteArrayToString(rndicc)));

            kenc = calculateKENC(key);

            log.Info(string.Format("kenc = {0}", ByteArrayToString(kenc)));

            var RES_ENC = ByteArrayToString(kenc);

            kmac = calculateKMAC(key);

            log.Info(string.Format("kmac = {0}", ByteArrayToString(kmac)));

            var RES_MAC = ByteArrayToString(kmac);

            byte[] Mutual = getMutualAuthenticationCommand();

            log.Info(string.Format("Mutual Data = {0}", ByteArrayToString(Mutual)));

            return Mutual;
        }

        private static byte[] getMutualAuthenticationCommand()
        {

            // 2. Generate an 8 byte random and a 16 byte random.

            rndifd = new byte[8];

            kifd = new byte[16];

            Random rand = new Random();

            rand.NextBytes(rndifd); // fill rndifd with random bytes

            log.Info(string.Format("rndifd = {0}", ByteArrayToString(rndifd)));

            rand.NextBytes(kifd); // fill kifd with random bytes

            log.Info(string.Format("kifd = {0}", ByteArrayToString(kifd)));

            // 3. Concatenate RND.IFD, RND.ICC and KIFD:

            byte[] s = new byte[32];

            Array.Copy(rndifd, 0, s, 0, rndifd.Length);

            Array.Copy(rndicc, 0, s, 8, rndicc.Length);

            Array.Copy(kifd, 0, s, 16, kifd.Length);

            var s_enc = ByteArrayToString(s);

            // 4. Encrypt S with TDES key Kenc:
            var K_enc = ByteArrayToString(kenc);

            byte[] eifd = encrypt3DES(kenc, s);

            var RES3 = ByteArrayToString(eifd);

            var K_mac = ByteArrayToString(kmac);

            // 5. Compute MAC over eifd with TDES key Kmac:
            byte[] mifd = getCC_MACNbytes(kmac, eifd);

            var Mifd = ByteArrayToString(mifd);

            log.Info(string.Format("mifd = {0}", ByteArrayToString(mifd)));

            // 6. Construct command data for MUTUAL AUTHENTICATE and send command
            // APDU to the MRTD's chip:
            byte[] mu_data = new byte[eifd.Length + mifd.Length];
            Array.Copy(eifd, 0, mu_data, 0, eifd.Length);
            Array.Copy(mifd, 0, mu_data, eifd.Length, mifd.Length);

            var RES_Final = ByteArrayToString(mu_data);

            return mu_data;
        }

        private static byte[] getCC_MACNbytes(byte[] Key_MAC, byte[] eIFD)
        {
            byte[] Kmac = Key_MAC;

            log.Info(string.Format("Mac Calculation Key_MAC = {0}", ByteArrayToString(Key_MAC)));

            // Split the 16 byte MAC key into two keys
            byte[] key1 = new byte[8];
            Array.Copy(Kmac, 0, key1, 0, 8);

            log.Info(string.Format("Mac Calculation key1 = {0}", ByteArrayToString(key1)));

            byte[] key2 = new byte[8];
            Array.Copy(Kmac, 8, key2, 0, 8);

            log.Info(string.Format("Mac Calculation key2 = {0}", ByteArrayToString(key2)));

            DES des1 = DES.Create();
            des1.BlockSize = 64;
            des1.Key = key1;
            des1.Mode = CipherMode.CBC;
            des1.Padding = PaddingMode.None;
            des1.IV = new byte[8];

            log.Info(string.Format("Mac Calculation des1 CipherMode.CBC/PaddingMode.None/IV = 0"));

            DES des2 = DES.Create();
            des2.BlockSize = 64;
            des2.Key = key2;
            des2.Mode = CipherMode.CBC;
            des2.Padding = PaddingMode.None;
            des2.IV = new byte[8];

            log.Info(string.Format("Mac Calculation des2 CipherMode.CBC/PaddingMode.None/IV = 0"));

            // Padd the data with Padding Method 2 (Bit Padding)
            System.IO.MemoryStream out_Renamed = new System.IO.MemoryStream();
            out_Renamed.Write(eIFD, 0, eIFD.Length);
            out_Renamed.WriteByte((byte)(0x80));

            log.Info(string.Format("Mac Calculation eIFD = {0}", ByteArrayToString(eIFD)));

            while (out_Renamed.Length % 8 != 0)
            {
                out_Renamed.WriteByte((byte)0x00);
            }
            byte[] eIfd_padded = out_Renamed.ToArray();
            int N_bytes = eIfd_padded.Length / 8;  // Number of Bytes 

            log.Info(string.Format("Mac Calculation eIfd_padded = {0}", ByteArrayToString(eIfd_padded)));

            byte[] d1 = new byte[8];
            byte[] dN = new byte[8];
            byte[] hN = new byte[8];
            byte[] intN = new byte[8];

            // MAC Algorithm 3
            // Initial Transformation 1
            Array.Copy(eIfd_padded, 0, d1, 0, 8);
            hN = des1.CreateEncryptor().TransformFinalBlock(d1, 0, 8);

            log.Info(string.Format("Mac Calculation hN = {0}", ByteArrayToString(hN)));

            // Split the blocks
            // Iteration on the rest of blocks
            for (int j = 1; j < N_bytes; j++)
            {
                Array.Copy(eIfd_padded, (8 * j), dN, 0, 8);
                // XOR
                for (int i = 0; i < 8; i++)
                    intN[i] = (byte)(hN[i] ^ dN[i]);

                // Encrypt
                hN = des1.CreateEncryptor().TransformFinalBlock(intN, 0, 8);
            }

            log.Info(string.Format("Mac Calculation Split the blocks Iteration on the rest of blocks hN = {0}", ByteArrayToString(hN)));

            // Output Transformation 3
            byte[] hNdecrypt = des2.CreateDecryptor().TransformFinalBlock(hN, 0, 8);

            log.Info(string.Format("Mac Calculation hNdecrypt = {0}", ByteArrayToString(hNdecrypt)));

            byte[] mIfd = des1.CreateEncryptor().TransformFinalBlock(hNdecrypt, 0, 8);

            log.Info(string.Format("Mac Calculation mIfd = {0}", ByteArrayToString(mIfd)));

            //  Get check Sum CC
            return mIfd;
        }

        private static byte[] calculateSHA1(byte[] temp)
        {
            byte[] result = null;

            try
            {
                using (SHA1Managed sha1 = new SHA1Managed())
                {
                    var hash = sha1.ComputeHash(temp);

                    result = hash;
                }
            }
            catch (Exception)
            {

            }

            return result;
        }

        private static AesCryptoServiceProvider a1 = new AesCryptoServiceProvider();

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

            //adjustParity(ka, 0);

            //adjustParity(kb, 0);

            byte[] key = new byte[16];

            Array.Copy(ka, 0, key, 0, 8);

            Array.Copy(kb, 0, key, 8, 8);

            //Array.Copy(ka, 0, key, 16, 8);

            return key;

        }

        private static TripleDESCryptoServiceProvider e1 = new TripleDESCryptoServiceProvider();
        private static DESCryptoServiceProvider d1 = new DESCryptoServiceProvider();

        private static byte[] encryptDES(byte[] key, byte[] data)
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

        private static byte[] encrypt3DES(byte[] key, byte[] data)
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

        private static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        public static byte[] calculateKMAC(byte[] mrzInfo)
        {

            //byte[] mrzinfobytes = Encoding.UTF8.GetBytes(mrzInfo);

            byte[] kseed = calculateKSeed(mrzInfo);

            return computeKey(kseed, new byte[] { 0, 0, 0, 2 });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mrzInfo"></param>
        /// <returns></returns>
        public static byte[] calculateKENC(byte[] mrzInfo)
        {

            //byte[] mrzInfoBytes = Encoding.UTF8.GetBytes(mrzInfo);

            byte[] kseed = calculateKSeed(mrzInfo);

            return computeKey(kseed, new byte[] { 0, 0, 0, 1 });

        }

        #region NotUsedUtils

        private static byte[] padByteArray(byte[] data)
        {

            int i = 0;
            byte[] tempdata = new byte[data.Length + 8];

            for (i = 0; i < data.Length; i++)
            {
                tempdata[i] = data[i];
            }

            tempdata[i] = (byte)0x80;

            for (i = i + 1; ((i) % 8) != 0; i++)
            {
                tempdata[i] = (byte)0;
            }

            byte[] filledArray = new byte[i];

            Array.Copy(tempdata, 0, filledArray, 0, i);

            return filledArray;
        }

        private static byte[] removePadding(byte[] b)
        {
            byte[] rd = null;
            int i = b.Length - 1;
            do
            {
                i--;
            } while (b[i] == (byte)0x00);

            if (b[i] == (byte)0x80)
            {
                rd = new byte[i];
                Array.Copy(b, 0, rd, 0, rd.Length);
                return rd;
            }
            return b;
        }

        private static byte[] exclusiveOR(byte[] key, byte[] PAN)
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

        private static byte[] xorArray(byte[] a, byte[] b)
        {
            if (b.Length < a.Length)
                throw new ArgumentException(
                        "length of byte [] b must be >= byte [] a");

            byte[] c = new byte[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                c[i] = (byte)(a[i] ^ b[i]);
            }
            return c;
        }

        private static void calculateSessionKeys(byte[] kifd, byte[] kicc)
        {

            byte[] kseed = xorArray(kicc, kifd);

            // 18.Calculate Session Keys (KS_ENC and KS_MAC):

            ksenc = computeKey(kseed, new byte[] { 0, 0, 0, 1 });

            ksmac = computeKey(kseed, new byte[] { 0, 0, 0, 2 });

        }

        private static byte[] decrypt3DES(byte[] key, byte[] data)
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
                using (var cs = new CryptoStream(ms, e1.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                }

                return ms.ToArray();
            }
        }

        private static byte[] encryptAES(byte[] key, byte[] data)
        {
            a1.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            if (key.Length != 0x08 &&
                key.Length != 0x10 &&
                key.Length != 0x18)
                throw new Exception("Invalid key length");

            a1.Padding = PaddingMode.None;

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

        private static byte[] Algorithm3(byte[] key, byte[] data)
        {
            if (data.Length % 8 != 0)
            {
                throw new ArgumentException("Data must be padded to 8-byte blocks.");
            }

            //if (key.Length != 16)
            //{
            //    throw new ArgumentException("key.Length must be 16 bytes");
            //}

            int numBlocks = data.Length / 8;

            byte[] iv = new byte[8];

            if (numBlocks > 1)
            {
                byte[] firstBlocks = data.Take((numBlocks - 1) * 8).ToArray();

                byte[] encFirstBlocks = encryptDES(key.Take(8).ToArray(), firstBlocks);

                Array.Copy(encFirstBlocks, encFirstBlocks.Length - 8, iv, 0, 8);
                //iv = encFirstBlocks.TakeLast(8).ToArray();
            }
            else
            {
                //iv = icv;
            }

            byte[] lastBlock = new byte[8];
            Array.Copy(data, data.Length - 8, lastBlock, 0, 8);


            byte[] encLastBlock = encrypt3DES(key, lastBlock);
            byte[] mac = new byte[8];
            Array.Copy(encLastBlock, encLastBlock.Length - 8, mac, 0, 8);

            return mac;
        }

        private static byte[] CalcDesMac(byte[] key, byte[] data)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            des.Key = key;

            des.IV = new byte[8];

            des.Padding = PaddingMode.Zeros;

            MemoryStream ms = new MemoryStream();

            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
            }
            byte[] encryption = ms.ToArray();

            byte[] mac = new byte[8];

            Array.Copy(encryption, encryption.Length - 8, mac, 0, 8);

            //PrintByteArray(encryption);

            return mac;
        }

        #endregion
    }
}
