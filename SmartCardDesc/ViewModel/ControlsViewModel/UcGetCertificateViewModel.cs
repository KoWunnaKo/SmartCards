using SC_CertificateCALib;
using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.Model;
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

        private Task tGetCertificate()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                try
                {
                    if (SelectedUser == null)
                        return;

                    //Generate Subject
                    string fio = string.Format("{0} {1} {2}", SelectedUser.SURNAME_NAME, SelectedUser.FIRST_NAME, SelectedUser.MIDDLE_NAME);
                    string subjectTxt = string.Format(@"CN = {0}, OU = IT, O = UZINFOCOM, L = Tashkent, 
                    S = Tashkent, C = UZ , INN = {1} , PINFL = {2}", fio, SelectedUser.TIN, SelectedUser.PIN);


                    //Validation Server Address and Template Name
                    string serverAddress = Properties.Settings.Default.CAServerIpAndName;
                    string template = Properties.Settings.Default.CA_TemplateName;

                    if (string.IsNullOrEmpty(serverAddress) || string.IsNullOrEmpty(template))
                        return;

                    //Create Request
                    var request = CertInterOpApi.CreateCertRequestMessage(subjectTxt, template);

                    //Send Request
                    var id = CertInterOpApi.SendCertificateRequest(request, serverAddress);

                    //Download Certificate
                    var cert = CertInterOpApi.DownloadCert(id, serverAddress);

                    Certificate = cert;

                    //Save Certificate in DB
                    using (var context = new SmartCardDBEntities())
                    {
                        var Card = context.CARD_INFO.ToList().FirstOrDefault(x => x.OWNER_USER == SelectedUser.REC_ID &&
                        x.IS_ACTIVE.Value);

                        if (Card != null)
                        {
                            //Card.CERTIFICATE_FILE 
                            Card.CERTIFICATE_BIN = Encoding.UTF8.GetBytes(cert);
                        }

                        var user = context.USERS.ToList().FirstOrDefault(x => x.REC_ID == SelectedUser.REC_ID);

                        user.KEY_FLG = true;

                        context.SaveChanges();
                    }

                    StatusText = string.Empty;
                }
                catch (Exception ex)
                {
                    StatusText = ex.Message;
                }
            });

            return resultTask;
            ///////////////////
        }

        private async void fGetCertificate()
        {
            IsIntermadiate= true;

            StatusText = "Получение сертификата...";

            await tGetCertificate();

            await AuditModel.InsertAuditAsync("CERT_CARD",
                string.Format("user = {0} ", userId));

            IsIntermadiate = false;
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

                Exponental = string.Empty;
                Modules = string.Empty;

                using (var context = new SmartCardDBEntities())
                {
                    var Card = context.CARD_INFO.ToList().FirstOrDefault(x => x.OWNER_USER == SelectedUser.REC_ID &&
                    x.IS_ACTIVE.Value);

                    if (Card != null)
                    {
                        if (Card.EXPONENT != null)
                        {
                            Exponental = Encoding.UTF8.GetString(Card.EXPONENT);
                        }
                        
                        if (Card.MODULUS != null)
                        {
                            Modules = Encoding.UTF8.GetString(Card.MODULUS);
                        }
                        
                    }
                }
            });

            return resultTask;
        }


    }
}
