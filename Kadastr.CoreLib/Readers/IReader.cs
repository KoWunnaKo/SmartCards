using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib
{
    public interface IReader
    {
        IDictionary<string, string> ReadData();

        void Reset(string fileName);

        Task Write2Kart(byte[] content);
    }
}
