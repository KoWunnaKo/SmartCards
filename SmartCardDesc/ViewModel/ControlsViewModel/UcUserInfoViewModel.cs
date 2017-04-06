using SmartCardDesc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcUserInfoViewModel : ViewModelBase
    {
        public RelayCommand GetToken { get; private set; }

        public UcUserInfoViewModel()
        {
            GetToken = new RelayCommand(_ => fGetToken());
        }

        private void fGetToken()
        {
            Token = CryptoFuncs.GetMD5(UserId);
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

    }
}
