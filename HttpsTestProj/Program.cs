using HttpsTestProj.uz.gov.ips;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HttpsTestProj
{
    class Program
    {
        private static XmlDocument getUserCard(string userId, string token)
        {
            var SoapEnvelop = new XmlDocument();

            string resultSoap = string.Format(@"<?xml version=""1.0"" encoding=""UTF-8""?><x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:get=""urn:megaware:/mediate/ips/testIdUz/getUserCard/getUserCard.wsdl""><x:Header/><x:Body><get:req><get:user_id>{0}</get:user_id><get:token>{1}</get:token></get:req></x:Body>
                                                </x:Envelope>", userId, token);

            SoapEnvelop.LoadXml(resultSoap);

            return SoapEnvelop;
        }

        public static string CallWebService(string action, XmlDocument SoapXml)
        {
            var _url = "https://ips.gov.uz:443";
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

            webRequest.Credentials = CredentialCache.DefaultCredentials;


            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=utf-8";
            webRequest.Accept = "text/xml";
            webRequest.KeepAlive = true;
            webRequest.Method = "POST";
            webRequest.UnsafeAuthenticatedConnectionSharing = true;

            //webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            //webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,**;q=0.8";
            //webRequest.UnsafeAuthenticatedConnectionSharing = true;
            //webRequest.Method = "POST";
            //webRequest.KeepAlive = true;
            //webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            //webRequest.AllowAutoRedirect = true;
            //webRequest.Host = "ips.gov.uz:443";

            return webRequest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="soapEnvelopeXml"></param>
        /// <param name="webRequest"></param>
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(soapEnvelopeXml.ToString());

            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                while(true)
                {
                    getUserInfoByIdService serv = new getUserInfoByIdService();

                    request req = new request();
                    req.user_id = "ulugbek";
                    req.token = "5127189a315ad39b21bc4eab6b602cb6";

                    var resp = serv.rEQ(req);

                    var client = new RestClient();
                    client.EndPoint = @"https://ips.gov.uz:443/mediate/ips/OneId/getUserInfoById"; ;
                    client.Method = HttpVerb.POST;

                    //string resultSoap = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:get=""urn:megaware:/mediate/ips/testIdUz/getUserCard/getUserCard.wsdl""><soap:Header/><soap:Body><get:req><get:user_id>{0}</get:user_id><get:token>{1}</get:token></get:req></soap:Body></soap:Envelope>", "ulugbek", "5127189a315ad39b21bc4eab6b602cb6");

                    //string resultSoap = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Body><request xmlns=""megaware:/mediate/ips/OneId/getUserInfoById/getUserInfoById.wsdl""><user_id xmlns=""urn:megaware:/mediate/ips/OneId/getUserInfoById/getUserInfoById.wsdl"">ulugbek</user_id><token xmlns=""urn:megaware:/mediate/ips/OneId/getUserInfoById/getUserInfoById.wsdl"">5127189a315ad39b21bc4eab6b602cb6</token></request></soap:Body></soap:Envelope>";

                    string resultSoap = @"<x:Envelope xmlns:x=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:get=""urn:megaware:/mediate/ips/OneId/getUserInfoById/getUserInfoById.wsdl"">
    <x:Header/>
    <x:Body>
        <get:request>
            <get:user_id>ulugbek</get:user_id>
            <get:token>5127189a315ad39b21bc4eab6b602cb6</get:token>
        </get:request>
    </x:Body>
</x:Envelope>";


                    client.PostData = resultSoap;
                    var json = client.MakeRequest();
                }

                    
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }

    public  enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public class RestClient
  {
    public string EndPoint { get; set; }
    public HttpVerb Method { get; set; }
    public string ContentType { get; set; }
    public string PostData { get; set; }

    public RestClient()
    {
      EndPoint = "";
      Method = HttpVerb.GET;
      ContentType = "text/xml;charset=utf-8";
      PostData = "";
    }
    public RestClient(string endpoint)
    {
      EndPoint = endpoint;
      Method = HttpVerb.GET;
      ContentType = "text/xml;charset=utf-8";
      PostData = "";
    }
    public RestClient(string endpoint, HttpVerb method)
    {
      EndPoint = endpoint;
      Method = method;
      ContentType = "text/xml;charset=utf-8";
      PostData = "";
    }

    public RestClient(string endpoint, HttpVerb method, string postData)
    {
      EndPoint = endpoint;
      Method = method;
      ContentType = "text/xml;charset=utf-8";
      PostData = postData;
    }


    public string MakeRequest()
    {
      return MakeRequest("");
    }

     private  bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
     {
            return true;
     }

        public string MakeRequest(string parameters)
    {
       //ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);

      var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

      request.Method = Method.ToString();
      request.ContentLength = 0;
      request.ContentType = ContentType;
      request.Headers.Add("SOAPAction", "/mediate/ips/OneId/getUserInfoById");

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
