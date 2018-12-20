using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kadastr.CoreLib;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml;

namespace Kadastr.CoreLibTest
{
    [TestClass]
    public class KadastrApiTest
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void Process()
        {
            var kadastr = new KadastrApiV1();

            //input null parametr
            var resJson2 = kadastr.GetKadastrInfo(null);
            //should be null
            Assert.IsTrue(string.IsNullOrEmpty(resJson2));

            //Input not null but not filled 
            var resJson = kadastr.GetKadastrInfo(new InputKadastrInfo());
            //should be null if properties not initialized
            Assert.IsTrue(string.IsNullOrEmpty(resJson));

            var input = new InputKadastrInfo();
            input.id = 123465789;
            input.lastUpdateDate = DateTime.Now.ToString("dd.MM.yyyy");
            var resJsonF = kadastr.GetKadastrInfo(input);
            Assert.IsTrue(!string.IsNullOrEmpty(resJsonF));


            //Check Json result for validity
            //Delete one of this tests
            //Assert.IsTrue(IsValidJson(resJson));
            //Delete one of this tests
            Assert.IsTrue(IsValidXml(resJsonF));

            //Empty parametr 
            var resObj2 = kadastr.ConvertInputToObj(string.Empty);
            //Should return null
            Assert.IsNull(resObj2);

            //Send valid Json
            var resObj = kadastr.ConvertInputToObj(resJsonF);
            //Should return not null Object
            Assert.IsNotNull(resObj);

            //Return object should be OutputKadastrInfo type 
            Assert.IsTrue(resObj is OutputKadastrInfo);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private bool IsValidXml(string xml)
        {
            try
            {
                new XmlDocument().LoadXml(xml);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
