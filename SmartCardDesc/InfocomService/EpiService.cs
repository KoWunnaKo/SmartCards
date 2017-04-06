using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using Epigov.Properties;
using Epigov.Log;
using System.Xml;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SmartCardDesc.Properties;

namespace SmartCardDesc.InfocomService
{
    /// <summary>
    /// EpiGov Return Codes
    /// </summary>
    public enum ReturnCodes
    {
        Success = 1200,
        Unauthorized = 1401,
        AccessNotAllowed = 1403,
        NotFound = 1404
    }

    /// <summary>
    /// EpiGov Message States
    /// </summary>
    public enum ApplicationStatus
    {
        New = 1,
        InProcess = 2,
        Accepted = 3,
        Rejected = 4
    }

    /// <summary>
    /// AIS Objects
    /// </summary>
    public enum AisServices
    {
        Patent = 52,
        Util = 53,
        Db = 54,
        Soft = 55,
        Zoo = 56,
        Plant = 57,
        Design = 58,
        TradeMark = 59,
        Micro = 270,
        Origin = 271
    }

    /// <summary>
    /// EpiGov Network Class
    /// </summary>
    public class EpiService
    {
        private readonly AisServices _currentService;

        private readonly Dictionary<AisServices, string> _serviceAuthList;

        private string _authprocId;

        private readonly ILogService _logService;

        //public EpiTasksReturn TasksReturn;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentService"></param>
        public EpiService(AisServices currentService)
        {
            _logService = new FileLogService(typeof(EpiService));
            _logService.Info("EpiGov Service");

            _serviceAuthList = new Dictionary<AisServices, string>();

            _currentService = currentService;

            SetServiceAuthCollection();
        }

        /// <summary>
        /// 
        /// </summary>
        public EpiService()
        {
            _logService = new FileLogService(typeof(EpiService));
            _logService.Info("EpiGov Service");

            _serviceAuthList = new Dictionary<AisServices, string>();

            _currentService = AisServices.TradeMark;

            SetServiceAuthCollection();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetServiceAuthCollection()
        {
            //_serviceAuthList.Add(AisServices.Patent, Settings.Default.authproc_id_Plant);
            //_serviceAuthList.Add(AisServices.Util, Settings.Default.authproc_id_Util);
            //_serviceAuthList.Add(AisServices.Db, Settings.Default.authproc_id_Db);
            //_serviceAuthList.Add(AisServices.Soft, Settings.Default.authproc_id_Soft);
            //_serviceAuthList.Add(AisServices.Zoo, Settings.Default.authproc_id_Zoo);
            //_serviceAuthList.Add(AisServices.Plant, Settings.Default.authproc_id_Plant);
            //_serviceAuthList.Add(AisServices.Design, Settings.Default.authproc_id_Design);
            //_serviceAuthList.Add(AisServices.TradeMark, Settings.Default.authproc_id_TradeMark);
            //_serviceAuthList.Add(AisServices.Micro, Settings.Default.authproc_id_Micro);
            //_serviceAuthList.Add(AisServices.Origin, Settings.Default.authproc_id_TovarPlace);

            foreach (var obj in _serviceAuthList.Where(obj => obj.Key == _currentService))
            {
                _authprocId = obj.Value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public Task<List<TasksCL>> GetTasks()
        //{
        //    var tresult = Task.Factory.StartNew(() =>
        //    {
        //        _logService.Info("=============GetTasks=================");

        //        string result = string.Empty;

        //        CLGetTasks method = new CLGetTasks(_authprocId);

        //        method.ResultSoap = CallWebService(method.methodName, method.SoapEnvelop);

        //        method.ParseXml();

        //        result = method.ResultInnerXml;

        //        return method.listTask;
        //    });

        //    return tresult;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appl"></param>
        /// <returns></returns>
        //public Task<CLGetExactTask> GetExactTasks(string appl , AisServices ois)
        //{
        //    var tresult = Task.Factory.StartNew(() =>
        //    {
        //        _logService.Info("=============GetTasks=================");
            
        //        string result = string.Empty;

        //        CLGetExactTask method = new CLGetExactTask(appl, ois);
            
        //        method.ResultSoap = CallWebService(method.methodName, method.SoapEnvelop);
            
        //        method.ParseXml();
            
        //        result = method.ResultInnerXml;
            
        //        _logService.Info(result);
            
        //        return method;
        //    });

        //    return tresult;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="incomeMessage"></param>
        /// <returns></returns>
        //public Task SetTaskStatus(CLSetTasksStatus incomeMessage)
        //{
        //    var tresult = Task.Factory.StartNew(() =>
        //    {
        //        _logService.Info("=============SetTaskStatus=================");


        //        incomeMessage.ResultSoap = CallWebService(incomeMessage.methodName, incomeMessage.SoapEnvelop);

        //        incomeMessage.ParseXml();

        //        string result = incomeMessage.ResultInnerXml;

        //        _logService.Info(result);

        //    });

        //    return tresult;
        //}

        public void TestGetUserId()
        {
            var xml = getUserById("ulugbek", "5127189a315ad39b21bc4eab6b602cb6");

            CallWebService("getUserById", xml);
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
