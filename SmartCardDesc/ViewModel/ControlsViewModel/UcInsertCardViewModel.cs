using SmartCardDesc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcInsertCardViewModel : ViewModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand GetToken { get; private set; }

        public UcInsertCardViewModel()
        {
            GetToken = new RelayCommand(_ => fGetToken());
        }

        private void fGetToken()
        {
            Token = CryptoFuncs.GetMD5(UserId);
        }

        private string token;
        private string userId;

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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
