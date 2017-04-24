using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcWriteCertificate : ViewModelBase
    {
        public RelayCommand SetCertificate { get; private set; }

        public UcWriteCertificate()
        {
            SetCertificate = new RelayCommand(_ => fSetCertificate());
        }

        private async void fSetCertificate()
        {
            IsIntermadiate = true;

            StatusText = "Идет запись...";

            Thread.Sleep(1000);

            await AuditModel.InsertAuditAsync("DELETE_CARD",
            string.Format("user = {0} ", userId));

            IsIntermadiate = false;

            StatusText = "Запись успешно осуществлена.";
        }

        private string userId;

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

                IsIntermadiate = true;

                GetCardPublicKeyInfo();

                IsIntermadiate = false;

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

        private string _certificate;

        public string Certificate
        {
            get
            {
                return _certificate;
            }

            set
            {
                _certificate = value;

                OnPropertyChanged("Certificate");
            }
        }

        private Task GetCardPublicKeyInfo()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                if (SelectedUser == null)
                    return;

                Certificate = string.Empty;

                using (var context = new SmartCardDBEntities())
                {
                    var Card = context.CARD_INFO.ToList().FirstOrDefault(x => x.OWNER_USER == SelectedUser.REC_ID &&
                    x.IS_ACTIVE.Value);

                    if (Card != null)
                    {
                        if (Card.CERTIFICATE_BIN != null)
                        {
                            Certificate = Encoding.UTF8.GetString(Card.CERTIFICATE_BIN);
                        }
                    }
                }
            });

            return resultTask;
        }
    }
}
