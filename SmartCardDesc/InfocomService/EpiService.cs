using System;
using System.Net;
using Epigov.Log;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using SmartCardDesc.Properties;
using SmartCardDesc.Model;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
                        var xml = getUserByIdPr(pUserId, pToken);

                        var result = CallWebService("getUserInfoById", xml);

                        model = ParseGetUserIdMethod(result);

                        model.userId = pUserId;

                        model.token = pToken;
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
        /// <param name="pUserId"></param>
        /// <param name="pToken"></param>
        /// <returns></returns>
        public UserInfoModel GetUserIdx(string pUserId, string pToken)
        {

                UserInfoModel model = null;

                try
                {
                    var xml = getUserByIdPr(pUserId, pToken);

                    var result = CallWebService("getUserInfoById", xml);

                    model = ParseGetUserIdMethod(result);

                    model.userId = pUserId;

                    model.token = pToken;
                }
                catch (Exception ex)
                {
                    _logService.Error(ex.ToString());

                    if (model == null)
                        model = new UserInfoModel();

                    model.result = ex.Message;
                }

                return model;
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

            list = resultXml.GetElementsByTagName("n1:respond"); //

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
                        userInfo.PerAdr = child.InnerText;
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

            string resultSoap = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
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
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private string getUserByIdPr(string userId, string token)
        {
            string resultSoap = string.Format(@"<x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:get=""urn:megaware:/mediate/ips/OneId/getUserInfoById/getUserInfoById.wsdl"">
                                <x:Header/>
                                    <x:Body>
                                        <get:request>
                                            <get:user_id>{0}</get:user_id>
                                            <get:token>{1}</get:token>
                                        </get:request>
                                    </x:Body>
                                </x:Envelope>", userId, token); 

            return resultSoap;
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

                    model.user_id = pUserId;

                    model.token = pToken;
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
        //private XmlDocument getUserCard(string userId, string token)
        //{
        //    var SoapEnvelop = new XmlDocument();

        //    string resultSoap = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?><x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:get=""urn:megaware:/mediate/ips/testIdUz/getUserCard/getUserCard.wsdl""><x:Header/><x:Body><get:req><get:user_id>{0}</get:user_id><get:token>{1}</get:token></get:req></x:Body></x:Envelope>", userId, token);

        //    SoapEnvelop.LoadXml(resultSoap);

        //    return SoapEnvelop;
        //}

        
        private string getUserCard(string userId, string token)
        {
            //string resultSoap = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?><x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:get=""urn:megaware:/mediate/ips/testIdUz/getUserCard/getUserCard.wsdl""><x:Header/><x:Body><get:req><get:user_id>{0}</get:user_id><get:token>{1}</get:token></get:req></x:Body></x:Envelope>", userId, token);

            string resultSoap = string.Format(@"<x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:get=""urn:megaware:/mediate/ips/OneId/getUserCard/getUserCard.wsdl"">
                                                    <x:Header/>
                                                    <x:Body>
                                                        <get:request>
                                                            <get:user_id>{0}</get:user_id>
                                                            <get:token>{1}</get:token>
                                                        </get:request>
                                                    </x:Body>
                                                </x:Envelope>", userId, token);


            return resultSoap;
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

            list = resultXml.GetElementsByTagName("n1:respond"); //

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
                                            string exp_date,
                                            string rfid)
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
                                             exp_date,
                                             rfid);

                    var result = CallWebService("insertUserCardInfo", xml);

                    model = ParseInsertCardInfoMethod(result);

                }
                catch (Exception ex)
                {
                    _logService.Error(ex.ToString());

                    if (model == null)
                        model = new CardModel();

                    model.result = ex.Message;
                }
                finally
                {
                    model.user_id = userId;
                    model.token = token;
                    model.card_num = cardNumber;
                    model.issue_date = issue_date;
                    model.expiry_date = exp_date;
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
        /// <param name="rfid"></param>
        /// <returns></returns>
        public CardModel InsertCardInfox(string userId,
                                    string token,
                                    string cardNumber,
                                    string issue_date,
                                    string exp_date,
                                    string rfid)
        {


                CardModel model = null;

                try
                {
                    var xml = insertCardInfo(userId,
                                             token,
                                             cardNumber,
                                             issue_date,
                                             exp_date,
                                             rfid);

                    var result = CallWebService("insertUserCardInfo", xml);

                    model = ParseInsertCardInfoMethod(result);

                }
                catch (Exception ex)
                {
                    _logService.Error(ex.ToString());

                    if (model == null)
                        model = new CardModel();

                    model.result = ex.Message;
                }
                finally
                {
                    model.user_id = userId;
                    model.token = token;
                    model.card_num = cardNumber;
                    model.issue_date = issue_date;
                    model.expiry_date = exp_date;
                }

                return model;

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
        private string insertCardInfo( string userId, 
                                            string token, 
                                            string cardNumber,
                                            string issue_date,
                                            string exp_date,
                                            string rfId)
        {
            //var SoapEnvelop = new XmlDocument();

            string resultSoap = string.Format(@"<x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ins=""urn:megaware:/mediate/ips/OneId/insertUserCardInfo/insertUserCardInfo.wsdl"">
    <x:Header/>
    <x:Body>
        <ins:request>
            <ins:token>{0}</ins:token>
            <ins:user_id>{1}</ins:user_id>
            <ins:card_num>{2}</ins:card_num>
            <ins:issue_date>{3}</ins:issue_date>
            <ins:exp_date>{4}</ins:exp_date>
            <ins:rfid>{5}</ins:rfid>
        </ins:request>
    </x:Body>
</x:Envelope>", token, userId, cardNumber, issue_date, exp_date, rfId);

            //SoapEnvelop.LoadXml(resultSoap);

            return resultSoap;
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

            list = resultXml.GetElementsByTagName("n1:response"); //

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
                                    string exp_date,
                                    string rfid)
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
                                             exp_date,
                                             rfid);

                    var result = CallWebService("updateUserCard", xml);

                    model = ParseUpdateCardInfoMethod(result);
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
        private string updateCardInfo(string userId,
                                            string token,
                                            string card_stat,
                                            string issue_date,
                                            string exp_date,
                                            string rfid)
        {
            string resultSoap = string.Format(@"<x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:upd=""urn:megaware:/mediate/ips/testIdUz/updateUserCard/updateUserCard.wsdl"">
                                                        <x:Header/>
                                                        <x:Body>
                                                            <upd:request>
                                                                <upd:token>{0}</upd:token>
                                                                <upd:user_id>{1}</upd:user_id>
                                                                <upd:card_stat>{2}</upd:card_stat>
                                                                <upd:issue_date>{3}</upd:issue_date>
                                                                <upd:exp_date>{4}</upd:exp_date>
                                                                <upd:rfid>{5}</upd:rfid>
                                                            </upd:request>
                                                        </x:Body>
                                                    </x:Envelope>", token, userId, card_stat, issue_date, exp_date, rfid);


            return resultSoap;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="pResult"></param>
        /// <returns></returns>
        private CardModel ParseUpdateCardInfoMethod(string pResult)
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

            list = resultXml.GetElementsByTagName("n1:respond"); //

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
        /// <returns></returns>
        public Task<CardModel> DeleteCardInfo(string userId,
                                    string token)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {

                CardModel model = null;

                try
                {
                    var xml = deleteCardInfo(userId,
                                             token);

                    var result = CallWebService("deleteCardByUserId", xml);

                    model = ParseDeleteCardInfoMethod(result);
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
        private string deleteCardInfo(string userId,
                                            string token)
        {
            string resultSoap = string.Format(@"<x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:del=""urn:megaware:/mediate/ips/testIdUz/deleteCardByUserId/deleteCardByUserId.wsdl"">
                                                    <x:Header/>
                                                        <x:Body>
                                                            <del:request>
                                                              <del:user_id>{0}</del:user_id>
                                                              <del:token>{1}</del:token >
                                                               </del:request>
                                                         </x:Body>
                                                </x:Envelope>", userId, token);

            return resultSoap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pResult"></param>
        /// <returns></returns>
        private CardModel ParseDeleteCardInfoMethod(string pResult)
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

            list = resultXml.GetElementsByTagName("n1:response"); //

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
        /// <param name="action"></param>
        /// <param name="SoapXml"></param>
        /// <returns></returns>
        public static string CallWebService(string action , XmlDocument SoapXml)
        {
            var _url = Settings.Default.ServiceUrl;
            var _action = string.Format("/mediate/ips/OneId/{0}", action);
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

            //webRequest.Credentials = CredentialCache.DefaultCredentials;

            
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            //ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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
        /// <param name="action"></param>
        /// <param name="SoapXml"></param>
        /// <returns></returns>
        public static string CallWebService(string action,string SoapXml)
        {
            var _url = Settings.Default.ServiceUrl;
            //var _action = string.Format("/mediate/ips/OneId/{0}", action);
            //_url = _url + _action;

            var client = new RestClient();
            client.EndPoint = _url + "/mediate/ips/OneId/";
            client.Method = HttpVerb.POST;

            client.PostData = SoapXml;
            var xmlresult = client.MakeRequest(action);

            return xmlresult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
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
            //webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; MS Web Services Client Protocol 4.0.30319.42000)";
            webRequest.ContentType = "text/xml;charset=utf-8";
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.KeepAlive = true;
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
            string resultSoap = @"<x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:get=""urn:megaware:/mediate/ips/OneId/getUserInfoById/getUserInfoById.wsdl"">
    <x:Header/>
    <x:Body>
        <get:request>
            <get:user_id>ulugbek</get:user_id>
            <get:token>5127189a315ad39b21bc4eab6b602cb6</get:token>
        </get:request>
    </x:Body>
</x:Envelope>";

            string PostData = string.Empty;
            //using (var stringWriter = new StringWriter())
            //using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            //{
            //    soapEnvelopeXml.WriteTo(xmlTextWriter);
            //    xmlTextWriter.Flush();
            //    PostData = stringWriter.GetStringBuilder().ToString();
            //}

            PostData = resultSoap;

            var encoding = new UTF8Encoding();
            var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
            webRequest.ContentLength = bytes.Length;

            using (var writeStream = webRequest.GetRequestStream())
            {
                writeStream.Write(bytes, 0, bytes.Length);
            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    /// <summary>
    /// 
    /// </summary>
    public class RestClient
    {
        /// <summary>
        /// 
        /// </summary>
        public string EndPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HttpVerb Method { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PostData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RestClient()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "text/xml;charset=utf-8";
            PostData = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        public RestClient(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "text/xml;charset=utf-8";
            PostData = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="method"></param>
        public RestClient(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml;charset=utf-8";
            PostData = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="method"></param>
        /// <param name="postData"></param>
        public RestClient(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml;charset=utf-8";
            PostData = postData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string MakeRequest()
        {
            return MakeRequest("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string MakeRequest(string parameters)
        {
            //ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);

            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;
            request.Headers.Add("SOAPAction", "/mediate/ips/OneId/" + parameters);

            if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.POST)
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;
            }
        }

    } // class
}
