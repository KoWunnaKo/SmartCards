using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib
{
    public interface IPrinterService
    {
        void SendToPrint(IDictionary<string, string> info);
    }
}
