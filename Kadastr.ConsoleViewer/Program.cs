using Kadastr.CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.ConsoleViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            var processor = new ProcessControllerV1();

            processor.MockGetKadastrInfo(null);

            Console.WriteLine("{0}", processor.objKadastrRes.ToString());

            var Path = @"C:\RADSOFT\Projects\SmartCardDesc\SmartCardDesc\Kadastr.CoreLib\bin\Debug\";
            var FileName = "MetaData.json";
            var FullPath = System.IO.Path.Combine(Path, FileName);

            processor.ProcessMetaData(null, FullPath);

            //processor.Write2Cart(null, null, null);

            var resDicts = processor.ReadData(null);

            foreach (var obj in resDicts)
            {
                Console.WriteLine("{0} -- {1}", obj.Key, obj.Value);
            }

            Console.ReadKey();
        }

    }
}
