using System;
using System.Net;
using Epigov.Log;
using System.Xml;
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

                        if (model == null)
                            model = new UserInfoModel();

                        model.result = ex.Message;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
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

            SoapEnvelop.LoadXml(resultSoap);

            return SoapEnvelop;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pUserId"></param>
        /// <param name="pToken"></param>
        /// <returns></returns>
        public Task<CardModel> GetUserCardInfo(string pUserId, string pToken)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {

                CardModel model = null;

                try
                {
                    var xml = getUserCard(pUserId, pToken);

                    var result = CallWebService("getUserCard", xml);

                    model = ParseGetUserCardInfoMethod(result);
                }
                catch (Exception ex)
                {
                    _logService.Error(ex.ToString());

                    if (model == null)
                        model = new CardModel();

                    model.result = ex.Message;
                }

                return model;
            });

            return resultTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
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

            SoapEnvelop.LoadXml(resultSoap);

            return SoapEnvelop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pResult"></param>
        /// <returns></returns>
        private CardModel ParseGetUserCardInfoMethod(string pResult)
        {
            string ResultInnerXml = string.Empty;
            string OrigResultInnerXml = string.Empty;

            var CardInfo = new CardModel();

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
                        CardInfo.globalTransactionID = attribute.Value;
                    }

                    if (attribute.Name.Equals("localTransactionID"))
                    {
                        CardInfo.localTransactionID = attribute.Value;
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

            list = resultXml.GetElementsByTagName("x:resp"); //

            foreach (XmlNode obj in list)
            {
                foreach (XmlNode child in obj.ChildNodes)
                {
                    if (child.Name.Equals("result"))
                    {
                        CardInfo.result = child.InnerText;
                    }

                    if (child.Name.Equals("card_stat"))
                    {
                        CardInfo.card_stat = child.InnerText;
                    }

                    if (child.Name.Equals("issue_date"))
                    {
                        CardInfo.issue_date = child.InnerText;
                    }

                    if (child.Name.Equals("card_num"))
                    {
                        CardInfo.card_num = child.InnerText;
                    }

                    if (child.Name.Equals("user_id"))
                    {
                        CardInfo.user_id = child.InnerText;
                    }

                    if (child.Name.Equals("expiry_date"))
                    {
                        CardInfo.expiry_date = child.InnerText;
                    }
                }
            }

            #endregion

            return CardInfo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="cardNumber"></param>
        /// <param name="issue_date"></param>
        /// <param name="exp_date"></param>
        /// <returns></returns>
        public Task<CardModel> InsertCardInfo(string userId,
                                            string token,
                                            string cardNumber,
                                            string issue_date,
                                            string exp_date)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {

                CardModel model = null;

                try
                {
                    var xml = insertCardInfo(userId,
                                             token,
                                             cardNumber,
                                             issue_date,
                                             exp_date);

                    var result = CallWebService("insertUserCard", xml);

                    model = ParseInsertCardInfoMethod(result);
                }
                catch (Exception ex)
                {
                    _logService.Error(ex.ToString());

                    if (model == null)
                        model = new CardModel();

                    model.result = ex.Message;
                }

                return model;
            });

            return resultTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="cardNumber"></param>
        /// <param name="issue_date"></param>
        /// <param name="exp_date"></param>
        /// <returns></returns>
        private XmlDocument insertCardInfo( string userId, 
                                            string token, 
                                            string cardNumber,
                                            string issue_date,
                                            string exp_date)
        {
            var SoapEnvelop = new XmlDocument();

            string resultSoap = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                                                <x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ins=""urn:megaware:/mediate/ips/testIdUz/insertUserCard/insertUserCard.wsdl"">
                                                      <x:Header/>
                                                        <x:Body>
                                                            <ins:req>
                                                                <ins:token>{0}</ins:token>
                                                                <ins:user_id>{1}</ins:user_id>
                                                                <ins:card_num>{2}</ins:card_num>
                                                                <ins:issue_date>{3}</ins:issue_date>
                                                                <ins:exp_date>{4}</ins:exp_date>
                                                            </ins:req>
                                                        </x:Body>
                                                    </x:Envelope>", token , userId, cardNumber, issue_date, exp_date);

            SoapEnvelop.LoadXml(resultSoap);

            return SoapEnvelop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pResult"></param>
        /// <returns></returns>
        private CardModel ParseInsertCardInfoMethod(string pResult)
        {
            string ResultInnerXml = string.Empty;
            string OrigResultInnerXml = string.Empty;

            var CardInfo = new CardModel();

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
                        CardInfo.globalTransactionID = attribute.Value;
                    }

                    if (attribute.Name.Equals("localTransactionID"))
                    {
                        CardInfo.localTransactionID = attribute.Value;
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

            list = resultXml.GetElementsByTagName("x:resp"); //

            foreach (XmlNode obj in list)
            {
                foreach (XmlNode child in obj.ChildNodes)
                {
                    if (child.Name.Equals("result"))
                    {
                        CardInfo.result = child.InnerText;
                    }
                }
            }

            #endregion

            return CardInfo;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="card_stat"></param>
        /// <param name="issue_date"></param>
        /// <param name="exp_date"></param>
        /// <returns></returns>
        public Task<CardModel> UpdateCardInfo(string userId,
                                    string token,
                                    string card_stat,
                                    string issue_date,
                                    string exp_date)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {

                CardModel model = null;

                try
                {
                    var xml = updateCardInfo(userId,
                                             token,
                                             card_stat,
                                             issue_date,
                                             exp_date);

                    var result = CallWebService("updateUserCard", xml);

                    model = ParseInsertCardInfoMethod(result);
                }
                catch (Exception ex)
                {
                    _logService.Error(ex.ToString());

                    if (model == null)
                        model = new CardModel();

                    model.result = ex.Message;
                }

                return model;
            });

            return resultTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="card_stat"></param>
        /// <param name="issue_date"></param>
        /// <param name="exp_date"></param>
        /// <returns></returns>
        private XmlDocument updateCardInfo(string userId,
                                            string token,
                                            string card_stat,
                                            string issue_date,
                                            string exp_date)
        {
            var SoapEnvelop = new XmlDocument();

            string resultSoap = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?>
                                                <x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:upd=""urn:megaware:/mediate/ips/testIdUz/updateUserCard/updateUserCard.wsdl"">
                                                        <x:Header/>
                                                        <x:Body>
                                                            <upd:req>
                                                                <upd:token>{0}</upd:token>
                                                                <upd:user_id>{1}</upd:user_id>
                                                                <upd:card_stat>{2}</upd:card_stat>
                                                                <upd:issue_date>{3}</upd:issue_date>
                                                                <upd:exp_date>{4}</upd:exp_date>
                                                            </upd:req>
                                                        </x:Body>
                                                    </x:Envelope>", token, userId, card_stat, issue_date, exp_date);

            SoapEnvelop.LoadXml(resultSoap);

            return SoapEnvelop;
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
