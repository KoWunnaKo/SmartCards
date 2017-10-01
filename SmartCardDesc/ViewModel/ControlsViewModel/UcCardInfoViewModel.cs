using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.InfocomService;
using SmartCardDesc.Model;
using SmartCardDesc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    return;
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

        private async void fGetCardInfo()
        {
            IsIntermadiate = true;

            StatusText = "Загрузка...";

            fGetToken();

            CardInfo = await service.GetUserCardInfo(UserId, Token);

            try
            {
                if ((CardInfo != null) && (CardInfo.user_id != null))
                {
                    await CardInfo.InsertCardInfoEnt();

                    StatusText = "Загрузка прошла удачно...";
                }
                else
                {
                    //For Test
                    
                }

            }
            catch (Exception ex)
            {
                StatusText = ex.Message;
            }
            finally
            {
                IsIntermadiate = false;
            }

            StatusText = string.Empty;
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
