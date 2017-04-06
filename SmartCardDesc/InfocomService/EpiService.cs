using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Epigov.Log;
using System.Xml;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SmartCardDesc.Properties;
using SmartCardDesc.Model;

namespace SmartCardDesc.InfocomService
{
    /// <summary>
    /// EpiGov Network Class
    /// </summary>
    public class EpiService
    {
        private readonly ILogService _logService;

        /// <summary>
        /// 
        /// </summary>
        public EpiService()
        {
            _logService = new FileLogService(typeof(EpiService));
            _logService.Info("EpiGov Service");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUserId"></param>
        /// <param name="pToken"></param>
        /// <returns></returns>
        public Task<UserInfoModel> GetUserId(string pUserId, string pToken)
        {
            var resultTask = Task.Factory.StartNew(()=>
            {
        
                    UserInfoModel model = null;

                    try
                    {
                        var xml = getUserById(pUserId, pToken);

                        var result = CallWebService("getUserById", xml);

                        model = ParseGetUserIdMethod(result);
                    }
                    catch(Exception ex)
                    {
                        _logService.Error(ex.ToString());
                    }

                    return model;
            }); 

            return resultTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pResult"></param>
        /// <returns></returns>
        private UserInfoModel  ParseGetUserIdMethod(string pResult)
        {
            string ResultInnerXml = string.Empty;
            string OrigResultInnerXml = string.Empty;

            var userInfo = new UserInfoModel();

            XmlDocument resultXml = new XmlDocument();

            ResultInnerXml = WebUtility.HtmlDecode(pResult);

            OrigResultInnerXml = ResultInnerXml;

            resultXml.LoadXml(ResultInnerXml);

            #region Header
            var list = resultXml.GetElementsByTagName("env:Header"); //env:Header

            foreach (XmlNode obj in list)
            {
                ResultInnerXml = obj.InnerXml;
            }

            resultXml.LoadXml(ResultInnerXml);

            list = resultXml.GetElementsByTagName("wsc:CoordinationContext"); //env:Header

            foreach (XmlNode obj in list)
            {
                foreach (XmlAttribute attribute in obj.Attributes)
                {
                    if (attribute.Name.Equals("globalTransactionID"))
                    {
                        userInfo.globalTransactionID = attribute.Value;
                    }

                    if (attribute.Name.Equals("localTransactionID"))
                    {
                        userInfo.localTransactionID = attribute.Value;
                    }
                }
            }

            #endregion

            #region Body

            resultXml.LoadXml(OrigResultInnerXml);

            list = resultXml.GetElementsByTagName("env:Body"); //

            foreach (XmlNode obj in list)
            {
                ResultInnerXml = obj.InnerXml;
            }

            resultXml.LoadXml(ResultInnerXml);

            list = resultXml.GetElementsByTagName("x:res"); //

            foreach (XmlNode obj in list)
            {
                foreach(XmlNode child in obj.ChildNodes)
                {
                    if (child.Name.Equals("reg_dttm"))
                    {
                        userInfo.reg_dttm = child.InnerText;
                    }

                    if (child.Name.Equals("first_name"))
                    {
                        userInfo.first_name = child.InnerText;
                    }

                    if (child.Name.Equals("result"))
                    {
                        userInfo.result = child.InnerText;
                    }

                    if (child.Name.Equals("mid_name"))
                    {
                        userInfo.mid_name = child.InnerText;
                    }

                    if (child.Name.Equals("pin"))
                    {
                        userInfo.pin = child.InnerText;
                    }

                    if (child.Name.Equals("dob"))
                    {
                        userInfo.dob = child.InnerText;
                    }

                    if (child.Name.Equals("gd"))
                    {
                        userInfo.gd = child.InnerText;
                    }


                    if (child.Name.Equals("surname"))
                    {
                        userInfo.surname = child.InnerText;
                    }

                    if (child.Name.Equals("per_adr"))
                    {
                        userInfo.per_adr = child.InnerText;
                    }

                    if (child.Name.Equals("tin"))
                    {
                        userInfo.tin = child.InnerText;
                    }

                    if (child.Name.Equals("pport_no"))
                    {
                        userInfo.pport_no = child.InnerText;
                    }

                }
            }

            #endregion

            return userInfo;
        }

        public void TestGetUserCard()
        {
            var xml = getUserCard("ulugbek", "5127189a315ad39b21bc4eab6b602cb6");

            CallWebService("getUserCard", xml);
        }

        private XmlDocument getUserById(string userId, string token)
        {
            var SoapEnvelop = new XmlDocument();

            string resultSoap = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                                                <x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:get=""urn:megaware:/mediate/ips/testIdUz/getUserById/getUserById.wsdl"">
                                                    <x:Header/>
                                                    <x:Body>
                                                        <get:req>
                                                            <get:user_id>{0}</get:user_id>
                                                            <get:token>{1}</get:token>
                                                        </get:req>
                                                    </x:Body>
                                                </x:Envelope>", userId, token);

            //ulugbek
            //5127189a315ad39b21bc4eab6b602cb6

            SoapEnvelop.LoadXml(resultSoap);

            return SoapEnvelop;

            /*
             * <env:Envelope xmlns:env="http://schemas.xmlsoap.org/soap/envelope/">
    <env:Header>
        <wsc:CoordinationContext xmlns:wsc="http://docs.oasis-open.org/ws-tx/wscoor/2006/06" globalTransactionID="27e61be4-152e-11e7-af71-00ff85f06e57" localTransactionID="27e61be3-152e-11e7-af71-00ff85f06e57"/>
    </env:Header>
    <env:Body>
        <x:res xmlns:x="urn:megaware:/mediate/ips/testIdUz/getUserById/getUserById.wsdl">
            <reg_dttm>22/02/2017</reg_dttm>
            <first_name>ULUG‘BEK</first_name>
            <result>success</result>
            <mid_name>ABDUVOIT O‘G‘LI</mid_name>
            <pin>32512920201717</pin>
            <dob>25/12/1992</dob>
            <gd>M</gd>
            <surname>KO‘CHAROV</surname>
            <per_adr>ГОРОД ТАШКЕНТ МИРАБАДСКИЙ РАЙОН НУКУС 1- ТУПИК 4-3</per_adr>
            <tin>498975465</tin>
            <pport_no>AA1011149</pport_no>
        </x:res>
    </env:Body>
</env:Envelope>
             */
        }

        private XmlDocument getUserCard(string userId, string token)
        {
            var SoapEnvelop = new XmlDocument();

            string resultSoap = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                                                <x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:get=""urn:megaware:/mediate/ips/testIdUz/getUserCard/getUserCard.wsdl"">
                                                    <x:Header/>
                                                    <x:Body>
                                                        <get:req>
                                                            <get:user_id>{0}</get:user_id>
                                                            <get:token>{1}</get:token>
                                                        </get:req>
                                                    </x:Body>
                                                </x:Envelope>", userId, token);

            //ulugbek
            //5127189a315ad39b21bc4eab6b602cb6

            SoapEnvelop.LoadXml(resultSoap);

            return SoapEnvelop;

            /*
             <env:Envelope xmlns:env="http://schemas.xmlsoap.org/soap/envelope/">
    <env:Header>
        <wsc:CoordinationContext xmlns:wsc="http://docs.oasis-open.org/ws-tx/wscoor/2006/06" globalTransactionID="cafbb0b4-153d-11e7-af71-00ff85f06e57" localTransactionID="cafbb0b3-153d-11e7-af71-00ff85f06e57"/>
    </env:Header>
    <env:Body>
        <x:resp xmlns:x="urn:megaware:/mediate/ips/testIdUz/getUserCard/getUserCard.wsdl">
            <result>Success</result>
            <card_stat>Y</card_stat>
            <issue_date>2018-12-10</issue_date>
            <card_num>CARD001</card_num>
            <user_id>ulugbek</user_id>
            <expiry_date>2019-12-10</expiry_date>
        </x:resp>
    </env:Body>
</env:Envelope>
             */
        }

        private XmlDocument insertCardInfo()
        {
            /* Enter XML
             * <x:Envelope xmlns:x="http://schemas.xmlsoap.org/soap/envelope/" xmlns:ins="urn:megaware:/mediate/ips/testIdUz/insertUserCard/insertUserCard.wsdl">
    <x:Header/>
    <x:Body>
        <ins:req>
            <ins:token>5127189a315ad39b21bc4eab6b602cb6</ins:token>
            <ins:user_id>ulugbek</ins:user_id>
            <ins:card_num>CARD002</ins:card_num>
            <ins:issue_date>2018-12-10</ins:issue_date>
            <ins:exp_date>2019-12-10</ins:exp_date>
        </ins:req>
    </x:Body>
</x:Envelope>*
             */


            /*
             * <env:Envelope xmlns:env="http://schemas.xmlsoap.org/soap/envelope/">
    <env:Header>
        <wsc:CoordinationContext xmlns:wsc="http://docs.oasis-open.org/ws-tx/wscoor/2006/06" globalTransactionID="88d0089f-153e-11e7-af71-00ff85f06e57" localTransactionID="88d0089e-153e-11e7-af71-00ff85f06e57"/>
    </env:Header>
    <env:Body>
        <x:resp xmlns:x="urn:megaware:/mediate/ips/testIdUz/insertUserCard/insertUserCard.wsdl">
            <result>Denied</result>
        </x:resp>
    </env:Body>
</env:Envelope>
             */
            return null;
        }

        private XmlDocument updateCardInfo()
        {
            /* Enter XML
<x:Envelope xmlns:x="http://schemas.xmlsoap.org/soap/envelope/" xmlns:upd="urn:megaware:/mediate/ips/testIdUz/updateUserCard/updateUserCard.wsdl">
    <x:Header/>
    <x:Body>
        <upd:req>
            <upd:token>5127189a315ad39b21bc4eab6b602cb6</upd:token>
            <upd:user_id>ulugbek</upd:user_id>
            <upd:card_stat>1</upd:card_stat>
            <upd:issue_date>2018-12-10</upd:issue_date>
            <upd:exp_date>2019-12-10</upd:exp_date>
        </upd:req>
    </x:Body>
</x:Envelope>
             */


            /*
<env:Envelope xmlns:env="http://schemas.xmlsoap.org/soap/envelope/">
    <env:Header>
        <wsc:CoordinationContext xmlns:wsc="http://docs.oasis-open.org/ws-tx/wscoor/2006/06" globalTransactionID="db698217-153e-11e7-af71-00ff85f06e57" localTransactionID="db698216-153e-11e7-af71-00ff85f06e57"/>
    </env:Header>
    <env:Body>
        <x:res xmlns:x="urn:megaware:/mediate/ips/testIdUz/updateUserCard/updateUserCard.wsdl">
            <result>Denied</result>
        </x:res>
    </env:Body>
</env:Envelope>
             */
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="SoapXml"></param>
        /// <returns></returns>
        public static string CallWebService(string action , XmlDocument SoapXml)
        {
            var _url = Settings.Default.ServiceUrl;
            var _action = string.Format("/mediate/ips/testIdUz/{0}", action);
            _url = _url + _action;

            XmlDocument soapEnvelopeXml = null;

            if (SoapXml != null)
            {
                soapEnvelopeXml = SoapXml;
            }
            else
            {
                throw new ApplicationException("SoapXml is Empty. Contact IT");
            }
            
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);

            //Credentials
            //byte[] credentialBuffer = new UTF8Encoding().GetBytes(Settings.Default.ServiceLogin + ":" + Settings.Default.ServicePassword);
            //webRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialBuffer);

            //IWebProxy theProxy = webRequest.Proxy;

            //if (theProxy != null)
            //{
            //    theProxy.Credentials = CredentialCache.DefaultCredentials;
            //}

            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult = string.Empty;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                //Console.Write(soapResult);
            }

            return soapResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="soapEnvelopeXml"></param>
        /// <param name="webRequest"></param>
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}
