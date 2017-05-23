using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.Sockets;
using WebSockets.Server.WebSocket;
using WebSockets.Common;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using ListenerDecktop.Controllers;

namespace ListenerDecktop.WebSoket
{
    internal class ChatWebSocketService : WebSocketService
    {
        private readonly IWebSocketLogger _logger;

        public ChatWebSocketService(Stream stream, TcpClient tcpClient, string header, IWebSocketLogger logger)
            : base(stream, tcpClient, header, true, logger)
        {
            _logger = logger;
        }

        protected override void OnTextFrame(string text)
        {
            string response = text;

            var request = ParseInputRequest(text);

            if (request.FuncName.Equals("getCertificate"))
            {

                string rcv = "FNNM10";

                var Message = MessageProcessor.ProccIncomingMessage(rcv);


                var responceCl = new getCertificateResponce();
                responceCl.FuncName = request.FuncName;
                responceCl.Status = 1;
                responceCl.certificate = Message.Substring(16, Message.Length - 16);

                response = GetStrFromgetCertificateResponce(responceCl);
            }
            else if (request.FuncName.Equals("putToken"))
            {
                string rcv = string.Format("{0}{1}", "FNNM11INPT000064", request.InputParm.PadLeft(64, ' '));

                var Message = MessageProcessor.ProccIncomingMessage(rcv);

                if (string.IsNullOrEmpty(Message))
                {
                    Message = "RETR10EROR00000205";
                }

                var responceCl = new getCertificateResponce();
                responceCl.FuncName = request.FuncName;
                responceCl.Status = 1;
                responceCl.signedToken = Message.Substring(16, Message.Length - 16);

                response = GetStrFromgetCertificateResponce(responceCl);
            }

            base.Send(response);
            _logger.Information(this.GetType(), response);
        }

        private getRequest ParseInputRequest(string message)
        {
            getRequest request = null;

            try
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(message)))
                {
                    var ser = new DataContractJsonSerializer(typeof(getRequest));
                    request = (getRequest)ser.ReadObject(stream);
                }
            }
            catch(Exception ex)
            {

            }

            return request;
        }

        private string GetStrFromgetCertificateResponce(getCertificateResponce responce)
        {
            string resultString = string.Empty;

            try
            {
                using (var stream1 = new MemoryStream())
                {
                    var ser = new DataContractJsonSerializer(typeof(getCertificateResponce));
                    ser.WriteObject(stream1, responce);
                    stream1.Position = 0;

                    using (var sr = new StreamReader(stream1))
                    {
                        resultString = sr.ReadToEnd();
                    }
                }
            }
            catch(Exception ex)
            {
                //
            }

            return resultString;
        }
    }

    #region JSON_CL

    [DataContract]
    public class getRequest
    {
        [DataMember(Name = "funcName")]
        public string FuncName { get; set; }

        [DataMember(Name = "inputParm")]
        public string InputParm { get; set; }
    }

    [DataContract]
    public class getCertificateResponce
    {
        [DataMember(Name = "funcName")]
        public string FuncName { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "certificate")]
        public string certificate { get; set; }

        [DataMember(Name = "signToken")]
        public string signedToken { get; set; }
    }

    #endregion


}
