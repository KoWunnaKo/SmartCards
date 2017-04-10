using SmartCardDesc.InfocomService;
using SmartCardDesc.Model;
using SmartCardDesc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcUserInfoViewModel : ViewModelBase
    {
        private EpiService service;

        public RelayCommand GetToken { get; private set; }

        public RelayCommand GetUserInfo { get; private set; }

        public RelayCommand ClearParams { get; private set; }

        public RelayCommand SaveUser { get; private set; }

        public UcUserInfoViewModel()
        {
            GetToken = new RelayCommand(_ => fGetToken());

            GetUserInfo = new RelayCommand(_ => fGetUserInfo());

            ClearParams = new RelayCommand(_ => FClearParams());

            SaveUser = new RelayCommand(_ => fSaveUserInfo());

            service = new EpiService();
        }

        private async void fSaveUserInfo()
        {
            try
            {
                IsIntermadiate = true;

                if (UserInfo != null)
                {
                    await UserInfo.SaveUserInfo();
                }
            }
            catch(Exception ex)
            {
                UserInfo.result = ex.Message;
                StatusText = ex.Message;
            }
            finally
            {
                IsIntermadiate = false;
            }
        }

        private void FClearParams()
        {
            Token = string.Empty;

            UserId = string.Empty;
        }

        private void fGetToken()
        {
            Token = CryptoFuncs.GetMD5(UserId);
        }

        private string _statusText;

        public string StatusText
        {
            get
            {
                return _statusText;
            }

            set
            {
                _statusText = value;

                OnPropertyChanged("StatusText");
            }
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

        private async void fGetUserInfo()
        {
            IsIntermadiate = true;
            StatusText = "Загрузка...";

            UserInfo = await service.GetUserId(UserId, Token);

            //UserInfo = new UserInfoModel();
            //UserInfo.userId = "ulugbek";
            //UserInfo.reg_dttm = "2017-10-01";
            //UserInfo.pport_no = "AA0012321";
            //UserInfo.Department = 1;

            IsIntermadiate = false;

            StatusText = string.Empty;

            try
            {
                if (UserInfo != null)
                {
                    UserInfo.InsertUserInfo();
                }
            }
            catch(Exception ex)
            {
                UserInfo.result = ex.Message;
            }

        }

        private string token;
        private string userId;

        public string Token
        {
            get 
            {
                return token;
            }

            set
            {
                token = value;

                OnPropertyChanged("Token");
            }
        }

        public string UserId
        {
            get 
            {
                return userId;
            }
            set 
            {
                userId = value;

                OnPropertyChanged("UserId");
            }
        }

        private UserInfoModel userInfo;

        public UserInfoModel UserInfo
        {
            get
            {
                return userInfo;
            }
            set
            {
                userInfo = value;

                OnPropertyChanged("UserInfo");
            }
        }
    }
}
