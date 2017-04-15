using System;

namespace SmartCardDesc.ViewModel.Security
{
    internal class LoginViewModel : ViewModelBase
    {

        public event EventHandler LoginCompleted;

        public void RaiseLoginCompleted()
        {
            if (LoginCompleted != null)
            {
                LoginCompleted(this, EventArgs.Empty);
            }
        }

        private string login;
        private string password;

        public string Login
        {
            get { return login; }
            set { login = value; OnPropertyChanged("Login"); }
        }

        public string Password
        {
            private get { return password; }
            set { password = value; OnPropertyChanged("Password"); }
        }

        public void LoginOp(object o)
        {
            //# Validation logic
            RaiseLoginCompleted();
        }

        public void CloseOp(object o)
        {

        }

        public RelayCommand LoginCommand { get; set; }

        public RelayCommand CloseCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(_ => LoginOp(1));
            CloseCommand = new RelayCommand(_ => CloseOp(1));

            OnPropertyChanged("LoginOp");
        }
    }
}
