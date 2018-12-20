using Kadastr.CoreLib.Parsers;
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
    public class KadastrApiV1 : IKadastrApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public OutputKadastrInfo ConvertInputToObj(string rawData)
        {
            #region Test
            //var test = new OutputKadastrInfo();
            //test.area = 12.2F;
            //test.area_id = "sdfsdfsdf";
            //test.b_cert_s_n = "asdasdasd";
            //test.b_dolya = "werwerwer";
            //test.b_kad_no = "234234234";
            //test.b_own_type = "wersdsdfsdf";
            //test.b_reg_d_n = "sdfsdfsdfsd";
            //test.b_restric = true;
            //test.cert_s_n = "qweqweqweqweqwe";
            //test.est_price = 123321;
            //test.fio_grajdan = "qweqweqweqweqwe";
            //test.id_req = "sdfsdfsdfsdf";
            //test.inn_yur_liso = "qweqweqweqwe";
            //test.kadastr_number = "werwerwerwer";
            //test.kad_no = "werwerwerwerwe";
            //test.land_purp = "sdfsdfsdfsdfsdf";
            //test.location = "werwerwerwerwerwer";
            //test.name_yur_liso = "sdfsdfsdfsdfsdf";
            //test.own_type = "sfdsdfsdfsdf";
            //test.passport_seria_nomer = "sdfsdfsdfsdfsdf";
            //test.pnfl_fizliso = "werwerwerwer";
            //test.region_id = "asdasdasdasdasd";
            //test.region_name = "sdfsdfsdfsdf";
            //test.registr_subj = "sdfsdfsdfsdf";
            //test.reg_d_n = "werwerwerwer";
            //test.servitudes = "sdfsdfsdfsdf";
            //test.tree_quant = 88;

            //var result = Utils.SerializeObject(test);
            #endregion

            return string.IsNullOrEmpty(rawData) ? null : Utils.Deserialize<OutputKadastrInfo>(rawData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetKadastrInfo(InputKadastrInfo param)
        {
            if (param == null)
            {
                return null;
            }

            if ((param.id == 0) || (string.IsNullOrEmpty(param.lastUpdateDate)))
            {
                return null;
            }

            //Serialize object to Xml
            var requestXml = ConvertInput2Xml(param);

            //Make call to external API return responce Xml
            var response = CallKadastrApi(requestXml);

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestXml"></param>
        /// <returns></returns>
        public string CallKadastrApi(string requestXml)
        {
            if (string.IsNullOrEmpty(requestXml)) return null;

            return "<?xml version=\"1.0\" encoding=\"utf - 16\"?>"
            + "<ResponseFromKadastr xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">"
            +"  <ID>0</ID>"
            + "  <ID_REQ>1231321321</ID_REQ>"
            + "  <KADASTR_NUMBER>17:01:01:01:234</KADASTR_NUMBER>"
            + "  <REGION_NAME>TOSHKENT</REGION_NAME>"
            + "  <REGION_ID>124</REGION_ID>"
            + "  <AREA_ID>55</AREA_ID>"
            + "  <NAME_FIO>RADJABOV BAKHODIR</NAME_FIO>"
            + "  <NAME_FIO_2>RADJABOV BAKHODIR</NAME_FIO_2>"
            + "  <NAME_FIO_3>RADJABOV BAKHODIR</NAME_FIO_3>"
            + "  <NAME_FIO_4>RADJABOV BAKHODIR</NAME_FIO_4>"
            + "  <NAME_FIO_5>RADJABOV BAKHODIR</NAME_FIO_5>"
            + "  <KAD_NO>17:01:01:01:234</KAD_NO>"
            + "  <AREA>12.2</AREA>"
            + "  <LOCATION>Xumoyun 24-30</LOCATION>"
            + "  <OWN_TYPE>mulkdor</OWN_TYPE>"
            + "  <LAND_DOC>0</LAND_DOC>"
            + "  <LAND_PURP>Yashash</LAND_PURP>"
            + "  <LAND_RESTR>false</LAND_RESTR>"
            + "  <EST_PRICE>1251000</EST_PRICE>"
            + "  <REG_D_N>TOSHKENT</REG_D_N>"
            + "  <CERT_S_N>TOSHKENT</CERT_S_N>"
            + "  <SERVITUDES>TOSHKENT</SERVITUDES>"
            + "  <B_KAD_NO>17:01:01:01:234</B_KAD_NO>"
            + "  <B_AREA_TOTAL>0</B_AREA_TOTAL>"
            + "  <B_AREA_OBSH>0</B_AREA_OBSH>"
            + "  <B_AREA_PROIZV>0</B_AREA_PROIZV>"
            + "  <B_AREA_JILAYA>0</B_AREA_JILAYA>"
            + "  <B_OWN_TYPE>Mulkdor</B_OWN_TYPE>"
            + "  <B_LAND_DOC>0</B_LAND_DOC>"
            + "  <B_DOLYA>100</B_DOLYA>"
            + "  <B_RESTRIC>true</B_RESTRIC>"
            + "  <B_INV_STOIM>0</B_INV_STOIM>"
            + "  <B_REG_D_N>TOSHKENT</B_REG_D_N>"
            + "  <B_CERT_S_N>TOSHKENT</B_CERT_S_N>"
            + "  <REGISTR_SUBJ>TOSHKENT</REGISTR_SUBJ>"
            + "  <TREE_QUANT>88</TREE_QUANT>"
            + "</ResponseFromKadastr>";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string ConvertInput2Xml(InputKadastrInfo param)
        {
            return param == null ? null : Utils.SerializeObject(param);
        }
    }
}
