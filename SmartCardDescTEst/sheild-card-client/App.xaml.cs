using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GID_Client.ViewModel;
using SmartCardDesc.Secirity;

namespace GID_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            if (GID_Client.Properties.Settings.Default.BackEndMode.Equals("2"))
            {
                MainWindow MainWindow = new MainWindow();
                MainWindow.SetAccessRules();
                MainWindow.Show(); //10.10.2017
            }
            else
            {
                Window LoginWindow = new LoginWindow();
                LoginWindow.Show();
                ((LoginViewModel)LoginWindow.DataContext).LoginCompleted += (s, ev) =>
                {
                    MainWindow MainWindow = new MainWindow();
                    MainWindow.SetAccessRules();
                    LoginWindow.Hide();
                    LoginWindow.Close();
                    MainWindow.Show();
                }; //06.10.2017
            }
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("MyHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Ошибка: " + e.Exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }


}
