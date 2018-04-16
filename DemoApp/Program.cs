using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using SmartCardApi.MRZ;
using SmartCardApi.SmartCard;
using SmartCardApi.Infrastructure;
using SmartCardApi.SmartCardReader;


namespace DemoApp
{
   
    class Program
    {
        public static void Main()
        {
            //var mrzInfo = new MRZInfo("15IC69034", new DateTime(1996,11,26), new DateTime(2026, 06, 11)); //"496112612606118" Bagdavadze
            //var mrzInfo = "13ID37063295110732402055";     // + Shako
            //var mrzInfo = "13IB90080296040761709252";   // + guka 
            //var mrzInfo = "13ID40308689022472402103";     // + Giorgio
            //"12IB34415792061602210089" K

            var mrzInfo = new MRZInfo(
                                "A290654395164273X",
                                new DateTime(1970, 03, 01),
                                new DateTime(2007, 09, 30)
                          );

            var mrzInfo2 = new Symbols("1A123XZ20170917A");

            //var cardContext = ContextFactory.Instance.Establish(SCardScope.System);

            //var reader = new ConnectedReader()

            //var reader = new SecuredReader(mrzInfo2,)





            //var mrzInfo = new MRZInfo(
            //    "10BB53550",
            //    new DateTime(1983, 05, 14),
            //    new DateTime(2021, 11, 24)
            //);
            //var dgsContent = new SmartCardContent(mrzInfo2)
            //                    .Content()
            //                    .Result;

            SecuredReaderTest dd = new SecuredReaderTest();

            //1

            dd.IDL_ReaderDG1("123456");
            dd.IDL_ReaderDG2("123456");

            dd.IDL_ReaderDG4("123456");
            dd.IDL_ReaderDG5("123456");

            //Console.WriteLine(
            //            dgsContent
            //                .Dg1Content
            //                    .MRZ
            //                    .DocumentNumber
            //        );

            //Console.WriteLine(
            //dgsContent
            //    .Dg1Content
            //        .MRZ
            //        .NameOfHolder
            //    );


            //Console.WriteLine(
            //dgsContent
            //    .Dg1Content
            //        .MRZ
            //        .Optionaldata
            //);

            //Console.WriteLine(
            //dgsContent
            //.Dg1Content
            //.MRZ
            //.Nationality
            //);

            //Console.WriteLine(
            //dgsContent
            //.Dg2Content.FaceImageData
            //);

            Console.ReadKey();
        }
    }
}
