using GID_Client.ServerApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GID_Client.ViewModel
{
    class LoginViewModel : ViewModelBase
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

        public async void LoginOp(object o)
        {
            IsIntermadiate = true;

            StatusText = string.Empty;

            try
            {
                if (await CheckAuthorithation())
                {
                    //# Validation logic
                    RaiseLoginCompleted();
                }
            }
            catch (Exception)
            {
                StatusText = "Невозможно подключится к БД";
            }

            IsIntermadiate = false;
        }

        private bool _isIntermadiate;

        public bool IsIntermadiate
        {
            get
            {
                return _isIntermadiate;
            }

            set
            {
                _isIntermadiate = value;

                OnPropertyChanged("IsIntermadiate");
            }
        }

        private Task<bool> CheckAuthorithation()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                if ((string.IsNullOrEmpty(Login)) || (string.IsNullOrEmpty(Password)))
                {
                    StatusText = "Заполните все поля";
                    return false;
                }

                LoginResponce responce = null;

                try
                {
                    responce = ServerApiController.LoginReqRes(Login, Password);
                }
                catch(Exception ex)
                {
                    if (ex.Message.Contains("401"))
                    {
                        StatusText = "Неверный юзер или пароль";
                        return false;
                    }

                    StatusText = ex.Message;
                    return false;
                }

                if (!responce.Status.Equals("success"))
                {
                    StatusText = "Неверный юзер или пароль";
                    return false;
                }

                ServerApiController.token = responce.data.Token;

                return true;
            });

            return resultTask;
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
