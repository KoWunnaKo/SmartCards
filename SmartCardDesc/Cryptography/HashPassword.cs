﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.Cryptography
{
    /// <summary>
    /// Crypto Hash Class
    /// </summary>
    public class HashPassword
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_password"></param>
        /// <returns></returns>
        public static byte[] HashPasswordWithSalt(string a_password)
        {
            string defaultPassword = "1111";

            if (a_password == null)
            {
                a_password = defaultPassword;
            }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u_password"></param>
        /// <param name="e_password"></param>
        /// <returns></returns>
        public static bool Validate(byte[] u_password, string e_password )
        {
            byte[] hashedEntered;

            byte[] password = Encoding.UTF8.GetBytes(e_password);

            using (var sha256 = SHA256.Create())
            {
                hashedEntered = sha256.ComputeHash(Combine(password, GenerateSalt()));
            }

            return Compare(u_password, hashedEntered);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        private static bool Compare(byte[] array1, byte[] array2)
        {
            var result = array1.Length == array2.Length;

            for (var i = 0; i < array1.Length && i < array2.Length; ++i)
            {
                result &= array1[i] == array2[i];
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length); Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static byte[] GenerateSalt()
        {
            string salt = "baxa0095247&^%$#@!";

            return Encoding.UTF8.GetBytes(salt);

        }
    }
}
