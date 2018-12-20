using GID_CardApi;
using Kadastr.CoreLib.Parsers;
using SmartCardApi.SmartCardReader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadDataV1 : IReader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> ReadData()
        {
            //var resultTask = Task.Factory.StartNew(() =>
            //{
            //    IDictionary<string, string> resDict = null; 
            //    try
            //    {
            //        byte[] Vr = null;

            //        var reader = new SecuredReaderTest();

            //        Vr = reader.VR_Reader(Settings.KeyForEncription);

            //        resDict = Utils.ParseVR(Vr);
            //    }
            //    catch 
            //    {
            //        return null;
            //    }

            //    return resDict;
            //});

            //return resultTask;
            IDictionary<string, string> resDict = null;
            byte[] Vr = null;

            var reader = new SecuredReaderTest();

            Vr = reader.VR_Reader(Settings.KeyForEncription);

            resDict = Utils.ParseVR(Vr);

            return resDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void Reset(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException();
            }

            if (!System.IO.File.Exists(fileName))
            {
                throw new System.IO.FileNotFoundException();
            }

            Process.Start(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task Write2Kart(byte[] content)
        {
            var result = await CardApi.WriteKadastrInfo(content, Settings.KeyForEncription);
        }
    }
}
