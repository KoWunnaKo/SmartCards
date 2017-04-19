using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.InfocomService;
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
        }

        private void fGetToken()
        {
            Token = CryptoFuncs.GetMD5(UserId);
        }

        private void fLoadResults()
        {

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
