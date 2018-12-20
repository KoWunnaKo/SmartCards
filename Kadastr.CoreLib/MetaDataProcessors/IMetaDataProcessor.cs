using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib
{
    public interface IMetaDataProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string ReadMetaData(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonRes"></param>
        /// <returns></returns>
        IEnumerable<MetaDataContainer> ParsMetaData(string jsonRes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mList"></param>
        /// <returns></returns>
        bool ValidateMetaData(IEnumerable<MetaDataContainer> mList);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metaList"></param>
        /// <param name="inputDataList"></param>
        /// <returns></returns>
        bool ValidateDataByMetadata(IEnumerable<MetaDataContainer> metaList, 
            IDictionary<string, string> inputDataList);


    }
}
