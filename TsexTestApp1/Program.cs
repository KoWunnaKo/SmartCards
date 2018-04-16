using CardAPILib.InterfaceCL;
using GemCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TsexClient.TsexLogic;

namespace TsexTestApp1
{
    class Program
    {
        private static CardProcessController cdCt { get; set; }

        static void Main(string[] args)
        {
            cdCt = new CardProcessController();

            cdCt.StartPros();
        }
    }
}
