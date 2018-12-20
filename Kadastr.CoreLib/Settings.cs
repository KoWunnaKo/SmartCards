using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib
{
    /// <summary>
    /// 
    /// </summary>
    public static class Settings
    {
        public static string KeyForEncription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static string MetaDataFilePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static IKadastrApi kadastrapi_real { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public static IKadastrApi kadastrapi_mock { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static IMetaDataProcessor metadataprocessor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static IParser parser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static IPrinterService printerservice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static IReader reader { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string Reset_Tool_Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static void MakeInterfaceSetting()
        {
            KeyForEncription = "GID_KADASTR_KEY1";
            Reset_Tool_Path = @"C:\RADSOFT\Projects\SmartCardDesc\SmartCardDesc\Kadastr.WinClient\bin\Debug\Card_Reset_Tool\Card_Reset_Tool.exe";
            kadastrapi_real     = new KadastrApiV1();
            kadastrapi_mock     = new MockDataProvider();
            metadataprocessor   = new MetaDataProcessorV1();
            parser              = new ParseDataV1();
            printerservice      = new PrintServiceV1();
            reader              = new ReadDataV1();
        }
    }
}
