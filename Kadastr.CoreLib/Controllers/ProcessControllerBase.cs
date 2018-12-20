using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib
{
    public abstract class ProcessControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract Task GetKadastrInfo(IKadastrApi api, InputKadastrInfo inputData);

        /// <summary>
        /// 
        /// </summary>
        public abstract Task SendToPrint(IPrinterService printer, IMetaDataProcessor metaData);

        /// <summary>
        /// 
        /// </summary>
        public abstract IDictionary<string,string> ReadData(IReader reader);

        /// <summary>
        /// 
        /// </summary>
        public abstract void MockGetKadastrInfo(IKadastrApi api);

        /// <summary>
        /// 
        /// </summary>
        public abstract void ResetCard(IReader reader, string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public abstract Task Write2Cart(IReader reader, IMetaDataProcessor metaData, IParser parser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="path"></param>
        public abstract void ProcessMetaData(IMetaDataProcessor metaData, string path);


        public abstract Task Write2Cart2(IReader reader, IMetaDataProcessor metaData, IParser parser, IDictionary<string, string> pair);
    }
}
