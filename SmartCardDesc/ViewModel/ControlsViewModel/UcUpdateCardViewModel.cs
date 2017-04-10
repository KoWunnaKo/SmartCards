using SmartCardDesc.InfocomService;
using SmartCardDesc.Model;
using SmartCardDesc.Utils;
using System;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcUpdateCardViewModel : ViewModelBase
    {
        private EpiService service;

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand GetToken { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand GetNumber { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand LoadResults { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand ClearResults { get; private set; }


        public UcUpdateCardViewModel()
        {
            GetToken = new RelayCommand(_ => fGetToken());

            GetNumber = new RelayCommand(_ => fGetNumber());

            LoadResults = new RelayCommand(_ => fLoadResults());

            ClearResults = new RelayCommand(_ => fClearResults());

            IssueDate = DateTime.Now;
            ExpireDate = DateTime.Now;

            service = new EpiService();
        }

        private void fClearResults()
        {
            UserId = string.Empty;
            Token = string.Empty;
            Number = string.Empty;
            IssueDate = DateTime.MinValue;
            ExpireDate = DateTime.MinValue;
        }

        private async void fLoadResults()
        {
            IsIntermadiate = true;

            StatusText = "Загрузка...";

            Model = await service.UpdateCardInfo(UserId,
                                           Token,
                                           Number,
                                           IssueDate.ToString("yyyy-MM-dd"),
                                           ExpireDate.ToString("yyyy-MM-dd"));

            IsIntermadiate = false;

            StatusText = string.Empty;
        }

        private void fGetToken()
        {
            Token = CryptoFuncs.GetMD5(UserId);
        }

        private void fGetNumber()
        {
            Number = CardTools.GenerateCardNumber();
        }

        private string token;
        private string userId;
        private string number;
        private DateTime issueDate;
        private DateTime expireDate;

        public DateTime IssueDate
        {
            get
            {
                return issueDate;
            }

            set
            {
                issueDate = value;

                OnPropertyChanged("IssueDate");
            }
        }

        public DateTime ExpireDate
        {
            get
            {
                return expireDate;
            }

            set
            {
                expireDate = value;

                OnPropertyChanged("ExpireDate");
            }
        }


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

        public string Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;

                OnPropertyChanged("Number");
            }
        }

        private CardModel _model;

        public CardModel Model
        {
            get
            {
                return _model;
            }

            set
            {
                _model = value;

                OnPropertyChanged("Model");
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

    }
}
