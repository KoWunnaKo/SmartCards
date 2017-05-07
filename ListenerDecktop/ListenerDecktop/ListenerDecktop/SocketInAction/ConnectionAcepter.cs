using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using log4net;

namespace ListenerDecktop.SocketInAction
{
    public class ConnectionAcepter
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(ConnectionAcepter));
        private Thread _Thread;
        private TcpListener list;
        private bool _state;

        public ConnectionAcepter()
        {
            _state = false;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            int port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["AppPort"]);
            list = new TcpListener(localAddr, port);
            _Thread = new Thread(new ThreadStart(Execute));
        }

        public void Start()
        {
            list.Start();
            _Thread.Start();
        }

        public void Stop()
        {
            list.Stop();
            _state = false;
        }

        private void Execute()
        {
            try
            {
                _state = true;
                while (_state)
                {
                    Socket s = list.AcceptSocket();
                    SocketClient cl = new SocketClient(s);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }
    }
}
