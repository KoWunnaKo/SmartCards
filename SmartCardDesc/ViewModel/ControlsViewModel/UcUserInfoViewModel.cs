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

        public UcUserInfoViewModel()
        {
            GetToken = new RelayCommand(_ => fGetToken());

            GetUserInfo = new RelayCommand(_ => fGetUserInfo());

            ClearParams = new RelayCommand(_ => fClearParams());

            service = new EpiService();
        }

        private void fClearParams()
        {
            Token = string.Empty;

            UserId = string.Empty;
        }

        private void fGetToken()
        {
            Token = CryptoFuncs.GetMD5(UserId);
        }

        private async void fGetUserInfo()
        {
            UserInfo = await service.GetUserId(UserId, Token);
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
