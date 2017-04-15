using SmartCardDesc.ViewModel.Security;
using System.Windows;
using SmartCardDesc.Secirity;

namespace SmartCardDesc
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
                Window MainWindow = new RibbonWindow();
                LoginWindow.Hide();
                LoginWindow.Close();
                MainWindow.Show();
            };
        }
    }
}
