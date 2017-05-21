using ListenerDecktop.WebSoket;
using log4net;
using System;
using System.Threading;
using WebSockets;
using WebSockets.Common;

namespace ListenerDecktop.SocketInAction
{
    public class WebSocketConnectionAceptor : BaseAcceptor
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(WebSocketConnectionAceptor));
        private Thread _Thread;
        //private HttpListener list;
        private bool _state;
        private static IWebSocketLogger _logger;
        private ServiceFactory serviceFactory;
        private WebServer server;
        private int port;

        public WebSocketConnectionAceptor()
        {
            _state = false;
            _logger = new WebSocketLogger();
            port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["AppPort"]); ;
            string webRoot = AppDomain.CurrentDomain.BaseDirectory;

            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            _logger.Warning(typeof(Program), "Webroot folder {0} not found. Using application base directory: {1}", webRoot, baseFolder);
             webRoot = baseFolder;

            // used to decide what to do with incoming connections
            serviceFactory = new ServiceFactory(webRoot, _logger);

            _Thread = new Thread(new ThreadStart(Execute));

        }

        public void Start()
        {
            _Thread.Start();
        }

        public void Stop()
        {
            _state = false;
            
        }

        private void Execute()
        {
            try
            {
                _state = true;
                while (_state)
                {
                    using (WebServer server = new WebServer(serviceFactory, _logger))
                    {
                         server.Listen(port);

                        Thread.Sleep(600000);
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
        }
    }
}
