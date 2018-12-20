using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kadastr.CoreLib;
using Kadastr.CoreLib.KadastrApi;

namespace Kadastr.CoreLibTest
{
    [TestClass]
    public class ParsersTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void Parse()
        {

        }

        [TestMethod]
        public void Convert()
        {
            var ReturnObject = OutputObj();

            var result = Kadastr2MetaDataMapper.Map(ReturnObject);

            var parseProc = new ParseDataV1();

            var resStr = parseProc.Convert(null);

            Assert.IsTrue(string.IsNullOrEmpty(resStr));

            var resStr2 = parseProc.Convert(result);

            Assert.IsFalse(string.IsNullOrEmpty(resStr2));

            parseProc.TestParse();
        }

        private OutputKadastrInfo OutputObj()
        {
            var test = new OutputKadastrInfo();
            test.area = 12.2F;
            test.area_id = "sdfsdfsdf";
            test.b_cert_s_n = "asdasdasd";
            test.b_dolya = "werwerwer";
            test.b_kad_no = "234234234";
            test.b_own_type = "wersdsdfsdf";
            test.b_reg_d_n = "sdfsdfsdfsd";
            test.b_restric = true;
            test.cert_s_n = "qweqweqweqweqwe";
            test.est_price = 123321;
            test.fio_grajdan = "qweqweqweqweqwe";
            test.id_req = "sdfsdfsdfsdf";
            test.inn_yur_liso = "qweqweqweqwe";
            test.kadastr_number = "werwerwerwer";
            test.kad_no = "werwerwerwerwe";
            test.land_purp = "sdfsdfsdfsdfsdf";
            test.location = "werwerwerwerwerwer";
            test.name_yur_liso = "sdfsdfsdfsdfsdf";
            test.own_type = "sfdsdfsdfsdf";
            test.passport_seria_nomer = "sdfsdfsdfsdfsdf";
            test.pnfl_fizliso = "werwerwerwer";
            test.region_id = "asdasdasdasdasd";
            test.region_name = "sdfsdfsdfsdf";
            test.registr_subj = "sdfsdfsdfsdf";
            test.reg_d_n = "werwerwerwer";
            test.servitudes = "sdfsdfsdfsdf";
            test.tree_quant = 88;

            return test;
        }
    }
}
