using SmartCardDesc.InfocomService;
using SmartCardDesc.Model;
using SmartCardDesc.Utils;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcCardInfoViewModel : ViewModelBase
    {
        private EpiService service;

        public RelayCommand GetToken { get; private set; }

        public RelayCommand ClearParams { get; private set; }

        public RelayCommand GetCardInfo { get; private set; }

        public UcCardInfoViewModel()
        {
            GetToken = new RelayCommand(_ => fGetToken());

            ClearParams = new RelayCommand(_ => fClearParams());

            GetCardInfo = new RelayCommand(_ => fGetCardInfo());

            service = new EpiService();
        }

        private void fGetToken()
        {
            Token = CryptoFuncs.GetMD5(UserId);
        }

        private async void fGetCardInfo()
        {
            CardInfo = await service.GetUserCardInfo(UserId, Token);
        }

        private void fClearParams()
        {
            Token = string.Empty;

            UserId = string.Empty;
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

        private CardModel cardInfo;

        /// <summary>
        /// 
        /// </summary>
        public CardModel CardInfo
        {
            get
            {
                return cardInfo;
            }
            set
            {
                cardInfo = value;

                OnPropertyChanged("CardInfo");
            }
        }
    }
}
