using SmartCardDesc.Cryptography;
using SmartCardDesc.EntityModel.EntityModel;
using System;
using System.Linq;

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
        private string _status;

        public string StatusText
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;

                OnPropertyChanged("StatusText");
            }
        }

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
            if (CheckAuthorithation())
            {
                //# Validation logic
                RaiseLoginCompleted();
            }
        }

        private bool CheckAuthorithation()
        {
            if ((string.IsNullOrEmpty(Login)) || (string.IsNullOrEmpty(Password)))
            {
                StatusText = "Заполните все поля";
                return false;
            }

            using (var context = new SmartCardDBEntities())
            {
                var count = (from userx in context.USERS
                             where userx.LOGIN == Login
                             select userx.LOGIN).Count();

                if (count == 0)
                {
                    StatusText = "Не существующий юзер";
                    return false;
                }

                var user = context.USERS.First(x => x.LOGIN == Login);

                LoginModel.currentUser = user;

                 if (!HashPassword.Validate(user.PASSWORD, Password))
                {
                    StatusText = "Неверный пароль";
                    return false;
                }

                return true;
            }
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
