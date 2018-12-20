using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kadastr.CoreLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Kadastr.CoreLib.KadastrApi;

namespace Kadastr.CoreLibTest
{
    [TestClass]
    public class MetaDataProcessorsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void Process()
        {
            var metaProc = new MetaDataProcessorV1();

            //Validation check
            var jsonRes1 = metaProc.ReadMetaData(null);

            Assert.IsNull(jsonRes1);

            var jsonRes2 = metaProc.ReadMetaData("asdasdasdasdasdasdsdfsdfsdfsdf");

            Assert.IsNull(jsonRes2);
            //validation check end

            //Functional check with correct path
            var Path = @"C:\RADSOFT\Projects\SmartCardDesc\SmartCardDesc\Kadastr.CoreLib\bin\Debug\";
            var FileName = "MetaData.json";
            var FullPath = System.IO.Path.Combine(Path, FileName);

            var jsonRes = metaProc.ReadMetaData(FullPath);

            //Result should contain json string
            Assert.IsFalse(string.IsNullOrEmpty(jsonRes));
            //Result should contain valid json
            Assert.IsTrue(IsValidJson(jsonRes));

            //Second stage 
            //Validation begin
            var res1 = metaProc.ParsMetaData(null);

            Assert.IsNull(res1);

            var res2 = metaProc.ParsMetaData(jsonRes);

            //Type should be List<MetaDataContainer>
            Assert.IsTrue(res2 is List<MetaDataContainer>);

            //Count of collection should be greater then 0
            Assert.IsTrue(((List<MetaDataContainer>)res2).Count > 0);

            //Validation should return false when input parametr is null
            Assert.IsFalse(metaProc.ValidateMetaData(null));

            //Validation should return true  when metada List is valid
            Assert.IsTrue(metaProc.ValidateMetaData(res2));

            //Empty Metadata List and Valid Metadata List Validation results should not be equal
            Assert.AreNotEqual(metaProc.ValidateMetaData(res2), metaProc.ValidateMetaData(new List<MetaDataContainer>()));

            //Validation data by metadata
            //If input data is empty should return false
            Assert.IsFalse(metaProc.ValidateDataByMetadata(res2, null));

            //If input data is valid and metadata also valid 
            var inputDataFake = FillInputData();
            Assert.IsTrue(metaProc.ValidateDataByMetadata(res2, inputDataFake));

        }

        [TestMethod]
        public void Kadastr2MetaMapperTest()
        {
            var ReturnObject = OutputObj();
            
            var result = Kadastr2MetaDataMapper.Map(ReturnObject);

            Assert.IsTrue(result is Dictionary<string, string>);

            Assert.IsTrue(result != null);

            Assert.IsTrue(result.Count > 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> FillInputData()
        {
            Dictionary<string, string> inputData = new Dictionary<string, string>();
            inputData.Add("A1", "Yakka tartibdagi turar joy");
            inputData.Add("A2", "17:01:01:01:01:234");
            inputData.Add("A3", "17:01:01:01:01:234");
            inputData.Add("A4", "Andijon viloyati, Oltinko'l tumani, Yakkabog' MFY, Bobur ko'chasi 10 uy");
            inputData.Add("A5", "145 m2");
            inputData.Add("A6", "27456000");
            inputData.Add("A7", "AA 00822/23");
            inputData.Add("A8", "Mavjud emas");
            inputData.Add("A9", "Abdullaev Abdulla Azizovich");
            inputData.Add("B1", "AA1238830");
            inputData.Add("B2", "231838830");
            inputData.Add("B3", "12.01.2011 yildagi N 123456/22 sonli shartnomasi");
            inputData.Add("B4", "100 %");

            return inputData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        private bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
