using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GID_Client.ServerApi
{
    public class ServerApiController
    {

        public static getResponceCardInsert GetCertificate(string CardNumber)
        {
            var _url = string.Format(@"http://192.168.0.68:8888/api/v1/cards/{0}/csr", CardNumber);

            var client = new RestClient();
            client.EndPoint = _url;
            client.Method = HttpVerb.GET;

            //putCardRequest obj = new putCardRequest();

            //obj.CardNumber = CardNumber;

            //var cardJson = GetStrFromCardNumber(obj);

            //client.PostData = cardJson;
            var xmlresult = client.MakeRequest("");

            return ParseInputRequest(xmlresult);
        }

        public static getResponceCardInsert RegisterCard(string CardNumber)
        {
            var _url = @"http://192.168.0.68:8888/api/v1/cards";

            var client = new RestClient();
            client.EndPoint = _url;
            client.Method = HttpVerb.POST;

            putCardRequest obj = new putCardRequest();

            obj.CardNumber = CardNumber;

            var cardJson = GetStrFromCardNumber(obj);

            client.PostData = cardJson;
            var xmlresult = client.MakeRequest("");

            return ParseInputRequest(xmlresult);
        }

        private static string GetStrFromCardNumber(putCardRequest responce)
        {
            string resultString = string.Empty;

            try
            {
                using (var stream1 = new MemoryStream())
                {
                    var ser = new DataContractJsonSerializer(typeof(putCardRequest));
                    ser.WriteObject(stream1, responce);
                    stream1.Position = 0;

                    using (var sr = new StreamReader(stream1))
                    {
                        resultString = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }

            return resultString;
        }

        private static getResponceCardInsert ParseInputRequest(string message)
        {
            getResponceCardInsert request = null;

            try
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(message)))
                {
                    var ser = new DataContractJsonSerializer(typeof(getResponceCardInsert));
                    request = (getResponceCardInsert)ser.ReadObject(stream);
                }
            }
            catch (Exception ex)
            {

            }

            return request;
        }
    }

    [DataContract]
    public class putCardRequest
    {
        [DataMember(Name = "number")]
        public string CardNumber { get; set; }
    }

    [DataContract]
    public class getResponceCardData
    {
        [DataMember(Name = "private_key")]
        public string PrivateKey { get; set; }

        [DataMember(Name = "public_key")]
        public string PublicKey { get; set; }

        [DataMember(Name = "message")]
        public string _message { get; set; }

        [DataMember(Name = "certificate")]
        public string _certificate { get; set; }

    }


    [DataContract]
    public class getResponceCardInsert
    {
        [DataMember(Name = "data")]
        public getResponceCardData _data { get; set; }

        [DataMember(Name = "status")]
        public string _status { get; set; }

        [DataMember(Name = "error_code")]
        public string _errorCode { get; set; }
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
            ContentType = "application/json";
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
            ContentType = "application/json";
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
            ContentType = "application/json";
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
            ContentType = "application/json";
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

            var request = (HttpWebRequest)WebRequest.Create(EndPoint ); //+ parameters

            request.Method = Method.ToString();
            request.ContentLength = 0;
            request.ContentType = ContentType;
            //request.Headers.Add("SOAPAction", "/mediate/ips/OneId/" + parameters);

            String username = "user";
            String password = "dluz-backend";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + encoded);

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
