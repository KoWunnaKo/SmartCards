using SC_CertificateCALib;
using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardAPILib.CardAPI;
using Epigov.Log;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcGetCertificateViewModel : ViewModelBase
    {
        private readonly ILogService _logService;

        private CardApiController cardApiObj;

        public RelayCommand GetCertificate { get; private set; }

        public RelayCommand GetRsa { get; private set; }

        public RelayCommand SetCertificate { get; private set; }

        public RelayCommand GetFromKartCertificate { get; private set; }


        public UcGetCertificateViewModel()
        {
            _logService = new FileLogService(typeof(UcGetCertificateViewModel));
            _logService.Info("UcGetCertificateViewModel");

            GetCertificate = new RelayCommand(_ => fGetCertificate());

            GetRsa =  new RelayCommand(_ => fGetRsa());

            SetCertificate = new RelayCommand(_ => fSetCertificate());

            GetFromKartCertificate = new RelayCommand(_ => fGetFromKartCertificate());

            cardApiObj = new CardApiController();

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

        private Task tGetCertificate()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                try
                {
                    if (SelectedUser == null)
                    {
                        StatusText = "Не выбран юзер...";

                        return;
                    }

                    _logService.Info("Certificate Create");

                    //Generate Subject
                    string fio = string.Format("{0} {1} {2}", SelectedUser.SURNAME_NAME, SelectedUser.FIRST_NAME, SelectedUser.MIDDLE_NAME);
                    string subjectTxt = string.Format(@"CN = {0}, OU = IT, O = UZINFOCOM, L = Tashkent, S = Tashkent, C = UZ ", fio);
                    //, INN = {1} , PINFL = {2} , SelectedUser.TIN, SelectedUser.PIN

                    _logService.Info(subjectTxt);

                    //Validation Server Address and Template Name
                    string serverAddress = Properties.Settings.Default.CAServerIpAndName;

                    _logService.Info(serverAddress);

                    string template = Properties.Settings.Default.CA_TemplateName;

                    _logService.Info(template);

                    if (string.IsNullOrEmpty(serverAddress) || string.IsNullOrEmpty(template))
                    {
                        StatusText = "Настройки сервера не верны...";

                        return;
                    }
                        

                    //Create Request
                    var request = SmartCardLogonCertApi.CreateCertRequestMessage(subjectTxt, template);

                    _logService.Info(request);

                    //Send Request
                    var id = SmartCardLogonCertApi.SendCertificateRequest(request, serverAddress);

                    _logService.Info(string.Format("request Id = {0}", id));

                    //Download Certificate
                    var cert = SmartCardLogonCertApi.DownloadCert(id, serverAddress);

                    Certificate = string.Empty;

                    Certificate = cert;

                    _logService.Info(string.Format("certificate = length {0} {1} ", cert.Length, cert));

                    if (cert.Length > 8192)
                    {
                        
                        _logService.Warn("Certificate Length more than 8192");

                        StatusText = "Certificate Length more than 8192";

                        return;

                    }

                    //Save Certificate in DB
                    using (var context = new SmartCardDBEntities())
                    {
                        var Card = context.CARD_INFO.ToList().FirstOrDefault(x => x.OWNER_USER == SelectedUser.REC_ID &&
                        x.IS_ACTIVE.Value);

                        if (Card != null)
                        {
                            //Card.CERTIFICATE_FILE 

                            byte[] certOfByte = Encoding.UTF8.GetBytes(cert);

                            if (certOfByte.Length < 4000 )
                            {
                                Card.CERTIFICATE_BIN = certOfByte;
                            }
                        }

                        var user = context.USERS.ToList().FirstOrDefault(x => x.REC_ID == SelectedUser.REC_ID);

                        user.KEY_FLG = true;
                        user.CERT_CRT_FLG = true;
                        user.CERT_WRT_FLG = true;

                        context.SaveChanges();
                    }

                    StatusText = string.Empty;
                }
                catch (Exception ex)
                {
                    _logService.Info(ex.ToString());
                    StatusText = ex.Message;
                }
            });

            return resultTask;
            ///////////////////
        }

        private void tGetCertificatex()
        {
                try
                {
                    if (SelectedUser == null)
                    {
                        StatusText = "Не выбран юзер...";

                        return;
                    }

                //Generate Subject
                string fio = string.Format("{0} {1} {2}", SelectedUser.SURNAME_NAME, SelectedUser.FIRST_NAME, SelectedUser.MIDDLE_NAME);
                    string subjectTxt = string.Format(@"CN = {0}, OU = IT, O = UZINFOCOM, L = Tashkent, S = Tashkent, C = UZ ", fio);
                //, INN = {1} , PINFL = {2} , SelectedUser.TIN, SelectedUser.PIN

                //Validation Server Address and Template Name
                string serverAddress = Properties.Settings.Default.CAServerIpAndName;
                    string template = Properties.Settings.Default.CA_TemplateName;

                    if (string.IsNullOrEmpty(serverAddress) || string.IsNullOrEmpty(template))
                    {
                        StatusText = "Настройки сервера не верны...";

                        return;
                    }

                //Create Request
                var request = SmartCardLogonCertApi.CreateCertRequestMessage(subjectTxt, template);

                    //Send Request
                    var id = SmartCardLogonCertApi.SendCertificateRequest(request, serverAddress);

                    //Download Certificate
                    var cert = SmartCardLogonCertApi.DownloadCert(id, serverAddress);

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
                        user.CERT_CRT_FLG = true;
                        user.CERT_WRT_FLG = true;

                        context.SaveChanges();
                    }

                    fSetCertificate();

                    fGetFromKartCertificate();

                    StatusText = string.Empty;
                }
                catch (Exception ex)
                {
                    StatusText = ex.Message;
                }
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

        private void fSetCertificate()
        {
            try
            {
                if (cardApiObj.Connect2Card() != 0)
                {
                    StatusText = "Невазможно присоединиться к Карте";
                }

                if ((!string.IsNullOrEmpty(Certificate)) && (Certificate.Length <= 8192))
                {
                    cardApiObj.adminPukCodeLogin(Properties.Settings.Default.AdminPINLogin);

                    cardApiObj.WriteCert(Certificate);

                    StatusText = "Запись сертификата завершена";
                }
                else
                {
                    StatusText = "Пустой сертификат или неверная длина";
                }
            }
            catch(Exception ex)
            {
                StatusText = ex.Message;
            }
        }

        private void fGetFromKartCertificate()
        {
            try
            {
                if (cardApiObj.Connect2Card() != 0)
                {
                    StatusText = "Невазможно присоединиться к Карте";
                }

                cardApiObj.adminPukCodeLogin(Properties.Settings.Default.AdminPINLogin);

                string certOfCard = "Невалидный сертификат";

                cardApiObj.LoadCert(out certOfCard);

                if (string.IsNullOrEmpty(certOfCard))
                {
                    CertificateOnCard = "Невалидный сертификат";
                }
                else
                {
                    CertificateOnCard = "Сертификат валидный";
                }

                CertificateOnCard = certOfCard;

                StatusText = "Прочнение сертификата удачно законченно";

            }
            catch (Exception ex)
            {
                StatusText = ex.Message;
            }
        }

        private void fGetRsa()
        {
            string publicKey = string.Empty;

            try
            {
                if (cardApiObj.Connect2Card() != 0)
                {
                    StatusText = "Невазможно присоединиться к Карте";
                }
                
                cardApiObj.getPubKeyModule(out publicKey);

                StatusText = "Оькрытый ключ удачно прочтен";

            }
            catch(Exception ex)
            {
                StatusText = ex.Message;
            }
            finally
            {
                Modules = publicKey;
            }
        }

        public Task fGetCertificatex()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                IsIntermadiate = true;

                StatusText = "1";

                tGetCertificatex();

                AuditModel.InsertAudit("CERT_CARD",
                    string.Format("user = {0} ", userId));

                IsIntermadiate = false;
            });

            return resultTask;
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

                //GetCardPublicKeyInfo();

                ViewModelBase.CurrentSelectedLogin = UserId;

                IsIntermadiate = false;

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


        private string _certificateOnCard;

        public string CertificateOnCard
        {
            get
            {
                return _certificateOnCard;
            }

            set
            {
                _certificateOnCard = value;

                OnPropertyChanged("CertificateOnCard");
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
