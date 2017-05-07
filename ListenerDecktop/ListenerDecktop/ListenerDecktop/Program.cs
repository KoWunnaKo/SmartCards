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

        public static ConnectionAcepter _connector;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _connector = new ConnectionAcepter();

            using (ProcessIcon pi = new ProcessIcon())
            {
                pi.Display();

                // Make sure the application runs!
                Application.Run();
            }

            //Application.Run(new MainForm());
        }
    }
}
