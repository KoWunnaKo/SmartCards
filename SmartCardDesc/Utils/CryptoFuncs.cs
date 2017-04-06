using SmartCardDesc.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SmartCardDesc.Utils
{
    public class CryptoFuncs
    {
        public static string GetMD5(string userId)
        {
            byte[] hash = Encoding.ASCII.GetBytes(string.Format("{0}.{1}", userId, Settings.Default.KeyMD5));
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
            {
                result += b.ToString("x2");
            }
            return result;
        }
    }
}
