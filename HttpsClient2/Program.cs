using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace HttpsClient2
{
    class Program
    {
        private const string URL = @"https://ips.gov.uz:443/mediate/ips/OneId/getUserInfoById";


        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/xml"));//ACCEPT header

            string resultSoap = @"<?xml version=""1.0"" encoding=""utf-8""?><soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><soap:Body><request xmlns=""megaware:/mediate/ips/OneId/getUserInfoById/getUserInfoById.wsdl""><user_id xmlns=""urn:megaware:/mediate/ips/OneId/getUserInfoById/getUserInfoById.wsdl"">ulugbek</user_id><token xmlns=""urn:megaware:/mediate/ips/OneId/getUserInfoById/getUserInfoById.wsdl"">5127189a315ad39b21bc4eab6b602cb6</token></request></soap:Body></soap:Envelope>";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URL);
            request.Content = new StringContent(resultSoap,
                                                Encoding.UTF8,
                                                "application/xml");//CONTENT-TYPE header
            
            client.SendAsync(request)
                  .ContinueWith(responseTask =>
                  {
                      Console.WriteLine("Response: {0}", responseTask.Result);
                  });
        }
    }
}
