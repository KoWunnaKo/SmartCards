using SmartCardDesc.EntityModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcGetCertificateViewModel : ViewModelBase
    {
        public RelayCommand GetCertificate { get; private set; }

        public UcGetCertificateViewModel()
        {
            GetCertificate = new RelayCommand(_ => fGetCertificate());
        }

        private void fGetCertificate()
        {

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

        public string _exponental;

        public string Exponental
        {
            get
            {
                return _exponental;
            }
            set
            {
                _exponental = value;

                OnPropertyChanged("Exponental");
            }
        }

        public string _modules;

        public string Modules
        {
            get
            {
                return _modules;
            }
            set
            {
                _modules = value;

                OnPropertyChanged("Modules");
            }
        }

        private Task GetCardPublicKeyInfo()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                if (SelectedUser == null)
                    return;

                Exponental = string.Empty;
                Modules = string.Empty;

                using (var context = new SmartCardDBEntities())
                {
                    var Card = context.CARD_INFO.ToList().FirstOrDefault(x => x.OWNER_USER == SelectedUser.REC_ID &&
                    x.IS_ACTIVE.Value);

                    if (Card != null)
                    {
                        Exponental = Encoding.UTF8.GetString(Card.EXPONENT);

                        Modules = Encoding.UTF8.GetString(Card.MODULUS);
                    }
                }
            });

            return resultTask;
        }
    }
}
