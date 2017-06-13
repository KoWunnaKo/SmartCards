using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class BitEndianConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(bool value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(char value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(double value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(float value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(int value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(long value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(short value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(uint value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(ulong value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="littleEndian"></param>
        /// <returns></returns>
        public static byte[] GetBytes(ushort value, bool littleEndian)
        {
            return ReverseAsNeeded(BitConverter.GetBytes(value), littleEndian);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="wantsLittleEndian"></param>
        /// <returns></returns>
        private static byte[] ReverseAsNeeded(byte[] bytes, bool wantsLittleEndian)
        {
            if (wantsLittleEndian == BitConverter.IsLittleEndian)
                return bytes;
            else
                return (byte[])bytes.Reverse().ToArray();
        }
    }
}
