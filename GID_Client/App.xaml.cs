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
            Window LoginWindow = new LoginWindow();
            LoginWindow.Show();
            ((LoginViewModel)LoginWindow.DataContext).LoginCompleted += (s, ev) =>
            {
                Window MainWindow = new MainWindow();
                LoginWindow.Hide();
                LoginWindow.Close();
                MainWindow.Show();
            };
        }
    }


}
