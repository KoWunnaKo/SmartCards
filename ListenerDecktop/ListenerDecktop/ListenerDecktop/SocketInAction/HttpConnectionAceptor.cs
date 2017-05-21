using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ListenerDecktop.SocketInAction
{
    public class HttpConnectionAceptor : BaseAcceptor
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(HttpConnectionAceptor));
        private Thread _Thread;
        private HttpListener list;
        private bool _state;

        public HttpConnectionAceptor()
        {
            _state = false;
            int port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["AppPort"]);
            string uriPrefix = string.Format("http://localhost:{0}/smartcard/", port);
            list = new HttpListener();
            list.Prefixes.Add(uriPrefix);
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
                    HttpListenerContext context = list.GetContext();
                    HttpClient cl = new HttpClient(context);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }
    }
}
