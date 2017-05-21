using ListenerDecktop.Controllers;
using log4net;
using System;
using System.Net.Sockets;
using System.Threading;

namespace ListenerDecktop.SocketInAction
{
    public class SocketClient
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(SocketClient));

        private Thread _Thread;
        private Socket _s;
        //private string _errorText;
        public SocketClient(Socket s)
        {
            _s = s;
            _Thread = new Thread(new ThreadStart(Execute));
            _Thread.Start();

        }

        private void Execute()
        {
            try
            {
                byte[] rcvLenBytes = new byte[4];
                _s.Receive(rcvLenBytes);
                int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
                byte[] rcvBytes = new byte[rcvLen];
                _s.Receive(rcvBytes);
                String rcv = System.Text.Encoding.UTF8.GetString(rcvBytes);
    
                Console.WriteLine("Client received: " + rcv);
                log.Info("Client received: " + rcv);
    
                var Message = MessageProcessor.ProccIncomingMessage(rcv);

                if (string.IsNullOrEmpty(Message))
                {
                    Message = "RETR10EROR00000236";

                    log.Error("Empty Out Message. Contact IT");
                }
    
                log.Info("Reply: " + Message);
    
                int toSendLen = System.Text.Encoding.UTF8.GetByteCount(Message);
                byte[] toSendBytes = System.Text.Encoding.UTF8.GetBytes(Message);
                byte[] toSendLenBytes = System.BitConverter.GetBytes(toSendLen);
                _s.Send(toSendLenBytes);
                _s.Send(toSendBytes);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
    }
}
