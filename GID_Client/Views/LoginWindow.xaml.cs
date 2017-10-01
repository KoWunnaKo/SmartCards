using GID_Client.ViewModel;
using System.Windows;

namespace SmartCardDesc.Secirity
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private LoginViewModel model;

        public LoginWindow()
        {
            InitializeComponent();

            model = new LoginViewModel();

            DataContext = model;

            txbLogin.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            model.Password = txbPassword.Password;
        }
    }
}
