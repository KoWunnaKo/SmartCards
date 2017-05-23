using ListenerDecktop.SocketInAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SystemTrayApp;

namespace ListenerDecktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        public static BaseAcceptor _connector;
        public static CardAPILib.CardAPI.CardApiController controller;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool isInstance;
            var mutex = new System.Threading.Mutex(true, "ListenerDecktop", out isInstance);

            if (!isInstance)
            {
                Application.Exit();
            }

            _connector = new WebSocketConnectionAceptor();

            using (ProcessIcon pi = new ProcessIcon())
            {
                pi.Display();

                controller = new CardAPILib.CardAPI.CardApiController(true);
                // Make sure the application runs!
                Application.Run();
            }

            //Application.Run(new MainForm());
        }
    }
}
