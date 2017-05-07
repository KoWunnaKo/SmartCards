using System;
using System.Text;
using System.Threading;
//using System.Security.Cryptography.Pkcs;
//using System.Security.Cryptography.X509Certificates;
using log4net;

namespace DesktopListener.Socket
{
    public class SocketClient
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(SocketClient));

        private Thread _Thread;
        private System.Net.Sockets.Socket _s;
        private string _errorText;

        public SocketClient(System.Net.Sockets.Socket s)
        {
            _s = s;
            _Thread = new Thread(new ThreadStart(Execute));
            _Thread.Start();

        }

        private void Execute()
        {
            try
            {
                try
                {
                    byte[] x = new byte[1024];
                    int i;

                    i = _s.Receive(x);

                    string data = Encoding.UTF8.GetString(x);

                    log.Info(data);

                    byte[] msg = Encoding.UTF8.GetBytes(string.Format("Listenet Enqripted and returned {0}", data));

                    i = _s.Send(msg);
                }
                catch (Exception ex)
                {
                    log.Error("Ошибка обработки ", ex);
                    return;
                }
            }
            finally
            {
                _s.Close();
            }
        }

    }
}
