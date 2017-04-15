using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.Cryptography
{
    public class HashPassword
    {
        public static byte[] HashPasswordWithSalt(string a_password)
        {
            string defaultPassword = "1111";

            if (string.IsNullOrEmpty(a_password.Trim()))
            {
                a_password = defaultPassword;
            }

            byte[] password = Encoding.UTF8.GetBytes(a_password);

            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Combine(password, GenerateSalt()));
            }
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length); Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }

        private static byte[] GenerateSalt()
        {
            string salt = "baxa0095247&^%$#@!";

            return Encoding.UTF8.GetBytes(salt);

        }
    }
}
