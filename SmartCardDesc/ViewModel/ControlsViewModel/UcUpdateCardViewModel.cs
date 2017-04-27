using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.InfocomService;
using SmartCardDesc.Model;
using SmartCardDesc.Utils;
using System;
using System.Linq;
using System.Collections.Generic;

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

            fGetToken();

            Model = await service.UpdateCardInfo(UserId,
                                           Token,
                                           CardStat,
                                           IssueDate.ToString("yyyy-MM-dd"),
                                           ExpireDate.ToString("yyyy-MM-dd"));

            await AuditModel.InsertAuditAsync("UPDATE_CARD",
                string.Format("user = {0} card_number = {1} card stat = {2}", userId, number, CardStat));

            if ((Model != null) && (Model.result != null))
            {
                using (var context = new SmartCardDBEntities())
                {
                    Card = context.CARD_INFO.ToList().FirstOrDefault(x => x.OWNER_USER == SelectedUser.REC_ID &&
                    x.IS_ACTIVE.Value);

                    if (Card != null)
                    {
                        Card.IS_ACTIVE = CardStat.Equals("Y") ? true : false;
                    }

                    context.SaveChanges();
                }
            }

            IsIntermadiate = false;

            StatusText = string.Empty;
        }

        private void fGetToken()
        {
            string value = string.Format("{0}.{1}.{2}.{3}", UserId, CardStat, IssueDate.ToString("yyyy-MM-dd"), ExpireDate.ToString("yyyy-MM-dd"));

            Token = CryptoFuncs.GetMD5(value);
        }

        private void fGetNumber()
        {
            //Number = CardTools.GenerateCardNumber();
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

        private USER _selectedUser;

        public USER SelectedUser
        {
            get
            {
                return _selectedUser;
            }

            set
            {
                _selectedUser = value;

                UserId = _selectedUser.LOGIN;

                LoadCardInfo();

                OnPropertyChanged("SelectedUser");
            }
        }

        private List<USER> _usersList;

        public List<USER> UsersList
        {
            get
            {
                try
                {
                    using (var context = new SmartCardDBEntities())
                    {
                        _usersList = context.USERS.ToList();
                    }

                }
                catch (Exception)
                {

                }

                if (_usersList == null)
                {
                    _usersList = new List<USER>();
                }

                return _usersList;
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

        private string _cardStat;

        public string CardStat
        {
            get
            {
                return _cardStat;
            }

            set
            {
                _cardStat = value;

                OnPropertyChanged("CardStat");
            }
        }

        private bool _activateChecked;

        public bool ActivateChecked
        {
            get
            {
                return _activateChecked;
            }

            set
            {
                _activateChecked = value;

                if (_activateChecked)
                {
                    CardStat = "Y";
                }

                OnPropertyChanged("ActivateChecked");
            }
        }

        private bool _deactivateChecked;

        public bool DeactivateChecked
        {
            get
            {
                return _deactivateChecked;
            }

            set
            {
                _deactivateChecked = value;

                if (_deactivateChecked)
                {
                    CardStat = "N";
                }

                OnPropertyChanged("DeactivateChecked");
            }
        }

        private CARD_INFO _card;

        public CARD_INFO Card
        {
            get
            {
                return _card;
            }

            set
            {
                _card = value;

                OnPropertyChanged("Card");
            }
        }

        private void LoadCardInfo()
        {
            using (var context = new SmartCardDBEntities())
            {
                Card = context.CARD_INFO.
                    ToList().OrderBy(x=>x.REC_ID).
                    LastOrDefault(x=>x.OWNER_USER == SelectedUser.REC_ID && 
                x.IS_ACTIVE.Value);

                if (Card != null)
                {
                    if (Card.ISSUE_DATE != null)
                        IssueDate = Card.ISSUE_DATE.Value;

                    if (Card.EXPIRE_DATE != null)
                        ExpireDate = Card.EXPIRE_DATE.Value;
                }
            }
        }
    }
}
