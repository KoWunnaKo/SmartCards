using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.InfocomService;
using SmartCardDesc.Model;
using SmartCardDesc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcDeleteCardViewModel : ViewModelBase
    {
        private EpiService service;

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand GetToken { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand LoadResults { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public RelayCommand ClearResults { get; private set; }


        public UcDeleteCardViewModel()
        {
            GetToken = new RelayCommand(_ => fGetToken());

            LoadResults = new RelayCommand(_ => fLoadResults());

            ClearResults = new RelayCommand(_ => fClearResults());

            service = new EpiService();

            if (!string.IsNullOrEmpty(ViewModelBase.CurrentSelectedLogin))
            {
                try
                {
                    using (var context = new SmartCardDBEntities())
                    {
                        UsersList = context.USERS.ToList();
                    }

                }
                catch (Exception)
                {

                }

                var defUser = _usersList.FirstOrDefault(c => c.LOGIN == ViewModelBase.CurrentSelectedLogin);

                if (defUser != null)
                {
                    SelectedIndex = _usersList.IndexOf(defUser);

                    SelectedUser = defUser;
                }
            }
        }

        private void fGetToken()
        {
            Token = CryptoFuncs.GetMD5(UserId);
        }

        private async void fLoadResults()
        {
            IsIntermadiate = true;

            StatusText = "Загрузка...";

            fGetToken();

            Model = await service.DeleteCardInfo(UserId, Token);

            await AuditModel.InsertAuditAsync("DELETE_CARD",
                string.Format("user = {0} ", userId));

            if ((Model != null) && (Model.result != null))
            {
                using (var context = new SmartCardDBEntities())
                {
                    var Card = context.CARD_INFO.ToList().FirstOrDefault(x => x.OWNER_USER == SelectedUser.REC_ID &&
                    x.IS_ACTIVE.Value);

                    if (Card != null)
                    {
                        Card.IS_ACTIVE = false;
                    }

                    context.SaveChanges();
                }
            }

            IsIntermadiate = false;

            StatusText = string.Empty;
        }

        private void fClearResults()
        {
            UserId = string.Empty;
            Token = string.Empty;
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

                ViewModelBase.CurrentSelectedLogin = UserId;

                OnPropertyChanged("SelectedUser");
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }

            set
            {
                _selectedIndex = value;

                OnPropertyChanged("SelectedIndex");
            }
        }

        private List<USER> _usersList;

        public List<USER> UsersList
        {
            get
            {
                try
                {
                    if (_usersList != null)
                    {
                        return _usersList;
                    }

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
            set
            {
                _usersList = value;

                OnPropertyChanged("UsersList");
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
    }
}
