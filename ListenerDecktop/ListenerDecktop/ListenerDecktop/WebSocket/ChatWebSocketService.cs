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

            if (response.Contains("getCertificate"))
            {
                response = "{ funcName: \"getCertificate\",  status: \"0\", certificate: \"certificate64\" }";
            }

            base.Send(response);
            _logger.Information(this.GetType(), response);
        }
    }

    #region JSON_CL

    [DataContract]
    public class getCertificateRequest
    {
        [DataMember(Name = "funcName")]
        public string FuncName { get; set; }
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
    }

    #endregion
}
