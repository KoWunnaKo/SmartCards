using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.InfocomService;
using SmartCardDesc.Model;
using SmartCardDesc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcInsertCardViewModel : ViewModelBase
    {

        private EpiService service;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand GetToken { get; private set; }

        public RelayCommand GetNumber { get; private set; }

        public RelayCommand LoadResults { get; private set; }

        public RelayCommand ClearResults { get; private set; }


        public UcInsertCardViewModel()
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

            Model = await service.InsertCardInfo(UserId,
                                           Token,
                                           Number,
                                           IssueDate.ToString("yyyy-MM-dd"),
                                           ExpireDate.ToString("yyyy-MM-dd"));

            await AuditModel.InsertAuditAsync("INSERT_CARD",
                string.Format("user = {0} card_number = {1}", userId, number));

            try
            {
                if ((Model != null) && (Model.user_id != null))
                {
                    await Model.InsertCardInfoEnt();

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

            IsIntermadiate = false;

            StatusText = string.Empty;
        }

        private void fGetToken()
        {
            Token = CryptoFuncs.GetMD5(UserId);
        }

        private async void fGetNumber()
        {
            Number = await CardTools.GenerateCardNumber();
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
