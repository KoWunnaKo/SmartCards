using System;
using System.Threading;
using System.Net.Sockets;

namespace DesktopListener.Socket
{
    public class ConnectionAcepter
    {
        private Thread _Thread;
        private TcpListener list;
        private bool _state;

        public ConnectionAcepter()
        {
            _state = false;
            int port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["AppPort"]);
            list = new TcpListener(port);
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
                    System.Net.Sockets.Socket s = list.AcceptSocket();
                    SocketClient cl = new SocketClient(s);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
