using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib
{
    public interface IParser
    {
        IDictionary<string, string> Parse(string rowData);

        string Convert(IDictionary<string, string> input);

        byte[] ConvertInput2ByteArray(IDictionary<string, string> input);
    }
}
