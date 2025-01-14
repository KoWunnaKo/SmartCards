﻿using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.InfocomService;
using SmartCardDesc.Model;
using SmartCardDesc.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using CardAPILib.InterfaceCL;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcInsertCardViewModel : ViewModelBase
    {
        private CardApiMessages cardApiObj;

        private EpiService service;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand GetToken { get; private set; }

        public RelayCommand GetNumber { get; private set; }

        public RelayCommand LoadResults { get; private set; }

        public RelayCommand ClearResults { get; private set; }

        public RelayCommand GetRfId { get; private set; }

        public RelayCommand ContextChanged { get; private set; }


        public UcInsertCardViewModel()
        {
            GetToken = new RelayCommand(_ => fGetToken());

            GetNumber = new RelayCommand(_ => fGetNumber());

            LoadResults = new RelayCommand(_ => fLoadResults());

            ClearResults = new RelayCommand(_ => fClearResults());

            GetRfId = new RelayCommand(_ => fGetRfId());

            ContextChanged = new RelayCommand(_ => fContextChanged());

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

            IssueDate = DateTime.Now.Date;
            ExpireDate = DateTime.Now.Date.AddYears(10);

            cardApiObj = new CardApiMessages();

            fGetNumber();

            fGetRfId();

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

        private void fContextChanged()
        {
            StatusText = "Загрузка...";
        }

        private async void fLoadResults()
        {
            IsIntermadiate = true;

            StatusText = "Загрузка...";

            fGetToken();

            Model = await service.InsertCardInfo(UserId,
                                           Token,
                                           Number,
                                           IssueDate.ToString("yyyy-MM-dd"),
                                           ExpireDate.ToString("yyyy-MM-dd"),
                                           Rfid);

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
            string value = string.Format("{0}.{1}.{2}.{3}", UserId, Number, IssueDate.ToString("yyyy-MM-dd"), ExpireDate.ToString("yyyy-MM-dd"));

            Token = CryptoFuncs.GetMD5(value);
        }

        private void fGetRfId()
        {
            StatusText = string.Empty;

            if (cardApiObj.Connect2Card() != 0)
            {
                StatusText = "Невазможно присоединиться к Карте";

                return;
            }

            Rfid = cardApiObj.GetRfId().ToString();
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

        private string _rfid;

        public string Rfid
        {
            get 
            { 
                return _rfid; 
            }
            set 
            { 
                _rfid = value;

                OnPropertyChanged("Rfid");
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
