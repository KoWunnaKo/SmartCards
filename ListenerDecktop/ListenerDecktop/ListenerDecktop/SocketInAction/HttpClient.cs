using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Threading;
using System.Net;
using System.IO;
using ListenerDecktop.Controllers;

namespace ListenerDecktop.SocketInAction
{
    public class HttpClient
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(HttpClient));
        private Thread _Thread;
        HttpListenerContext context;

        public HttpClient(HttpListenerContext s)
        {
            context = s;
            _Thread = new Thread(new ThreadStart(Execute));
            _Thread.Start();

        }

        private void Execute()
        {
            HttpListenerRequest request = context.Request;


            Console.WriteLine("Client received: " + request.Url.OriginalString);
            log.Info("Client received: " + request.Url.LocalPath);

            string rcv = "";

            if (request.Url.LocalPath.ToLower().Contains("isvalid"))
            {
                rcv = "FNNM10";
            }
            else if (request.Url.LocalPath.ToLower().Contains("puttoken"))
            {
                rcv = "FNNM11INPT0000641234567891234567891234657891234567891234567891234657891234567891";
            }
            else
            {
                throw new ApplicationException("Invalid Query");
            }

            var Message = MessageProcessor.ProccIncomingMessage(rcv);

            if (string.IsNullOrEmpty(Message))
            {
                Message = "RETR10EROR00000236";

                log.Error("Empty Out Message. Contact IT");
            }

            log.Info("Reply: " + Message);

            // получаем объект ответа
            HttpListenerResponse response = context.Response;
            // создаем ответ в виде кода html
            string responseStr = string.Format("<html><head><meta charset='utf8'></head><body>Сертификат: {0}</body></html>", Message.Substring(16, Message.Length - 16));
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);
            // получаем поток ответа и пишем в него ответ
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // закрываем поток
            output.Close();
        }

    }
}
