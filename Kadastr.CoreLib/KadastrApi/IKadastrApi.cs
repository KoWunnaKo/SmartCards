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
    public interface IKadastrApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string GetKadastrInfo(InputKadastrInfo param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        OutputKadastrInfo ConvertInputToObj(string rawData);
    }
}
