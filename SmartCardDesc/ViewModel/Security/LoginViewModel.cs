using SmartCardDesc.Cryptography;
using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            catch(Exception ex)
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

                    if (!HashPassword.Validate(user.PASSWORD, Password))
                    {
                        StatusText = "Неверный пароль";
                        return false;
                    }

                    LoginModel.currentUser = user;
                }

                AuditModel.InsertAudit("LOGIN", "User Logged In");

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
