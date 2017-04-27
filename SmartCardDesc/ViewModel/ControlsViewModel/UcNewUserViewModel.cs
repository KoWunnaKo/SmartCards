using Microsoft.Win32;
using SmartCardDesc.InfocomService;
using SmartCardDesc.Model;
using SmartCardDesc.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcNewUserViewModel : ViewModelBase
    {
        public RelayCommand StartOp { get; private set; }

        public RelayCommand Clear { get; private set; }

        public RelayCommand SetPhoto { get; private set; }

        private CardRSAGenViewModel rsaViewModel;

        private UcGetCertificateViewModel certViewModel;


        private EpiService service;

        private bool inProcessFlg;

        public UcNewUserViewModel()
        {
            StartOp = new RelayCommand(_ => fStartOp());

            Clear = new RelayCommand(_ => fClear());

            SetPhoto = new RelayCommand(_ => fSetPhoto());

            rsaViewModel = new CardRSAGenViewModel();

            certViewModel = new UcGetCertificateViewModel();

            service = new EpiService();
        }

        private async void fStartOp()
        {
            await fGetUserInfo();

            if (!inProcessFlg)
            {
                return;
            }

            IssueDate = DateTime.Now.Date;
            ExpireDate = DateTime.Now.Date.AddYears(10);
            
            await fLoadResults();

            rsaViewModel.UserId = UserId;

            await rsaViewModel.fGenRsax();

            StatusText ="Key Generation: " +  rsaViewModel.StatusText;

            Modules = rsaViewModel.Modules;
            Exponental = rsaViewModel.Exponental;

            if (!string.IsNullOrEmpty(Modules))
            {
                certViewModel.UserId = UserId;

                await certViewModel.fGetCertificatex();

                StatusText = "Certificate Gen " + certViewModel.StatusText;

                Certificate = certViewModel.Certificate;
            }
        }

        private void fGetNumber()
        {
            Number = CardTools.GenerateCardNumberx();
        }

        private void fGetToken()
        {
            Token = CryptoFuncs.GetMD5(UserId);
        }

        private void fGetTokenInsert()
        {
            string value = string.Format("{0}.{1}.{2}.{3}", UserId, Number, IssueDate.ToString("yyyy-MM-dd"), ExpireDate.ToString("yyyy-MM-dd"));

            Token = CryptoFuncs.GetMD5(value);
        }

        public Task fGetUserInfo()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                IsIntermadiate = true;
                inProcessFlg = false;
                StatusText = "Загрузка...";

                fGetToken();

                UserInfo =  service.GetUserIdx(UserId, Token);

                StatusText = "UserInfo Proccess...";

                try
                {
                    if ((UserInfo != null) && (UserInfo.userId != null))
                    {
                         UserInfo.InsertUserInfoEntx();

                        StatusText = "UserInfo: Загрузка прошла удачно...";

                        inProcessFlg = true;
                    }
                    else
                    {
                        StatusText = "UserInfo: " + UserInfo.result;
                    }

                }
                catch (Exception ex)
                {
                    StatusText = "UserInfo: " + ex.Message;
                }
                finally
                {
                    IsIntermadiate = false;
                }
            });

            return resultTask;
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

        private Task fLoadResults()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                IsIntermadiate = true;
                inProcessFlg = false;
                StatusText = "Card Insert Proccess...";

                fGetNumber();

                fGetTokenInsert();

                Model = service.InsertCardInfox(UserId,
                                               Token,
                                               Number,
                                               IssueDate.ToString("yyyy-MM-dd"),
                                               ExpireDate.ToString("yyyy-MM-dd"));

                AuditModel.InsertAudit("INSERT_CARD",
                    string.Format("user = {0} card_number = {1}", userId, number));

                try
                {
                    if ((Model != null) && (Model.user_id != null))
                    {
                        Model.PinNumber = PinNumber;

                        FileInfo file = new FileInfo(imagePhotoPath);

                        var destFilePath = Path.Combine(Properties.Settings.Default.CardPhotoPath,
                            string.Format("{0}.{1}", Model.card_num, file.Extension));

                        file.CopyTo(destFilePath, true);

                        Model.picturePath = destFilePath;

                        Model.InsertCardInfoEntx();

                        StatusText = "Card Insert: " + Model.result;
                    }
                    else
                    {
                        //For Test

                    }

                }
                catch (Exception ex)
                {
                    StatusText = "Card Insert: " + ex.Message;
                }
                finally
                {
                    IsIntermadiate = false;
                }

                IsIntermadiate = false;
            });

            return resultTask;
            //StatusText = string.Empty;
        }

        private void fClear()
        {
            UserInfo = null;
            IssueDate = DateTime.MinValue;
            ExpireDate = DateTime.MinValue;
            Number = string.Empty;
            Exponental = string.Empty;
            Modules = string.Empty;
            Certificate = string.Empty;
        }

        private Uri _image;

        public Uri ImageSource
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;

                OnPropertyChanged("ImageSource");
            }
        }

        private string imagePhotoPath;

        private void fSetPhoto()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                     FileInfo file = new FileInfo(openFileDialog.FileName);

                    imagePhotoPath = openFileDialog.FileName;

                    ImageSource = new Uri(openFileDialog.FileName);
                     //var destFilePath = Path.Combine(Properties.Settings.Default.CardPhotoPath,
                     //       string.Format("{0}.{1}", "", file.Extension));

                    //file.CopyTo(destFilePath, true);

                }
            }
            catch (Exception ex)
            {
                StatusText = ex.Message;
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

        private string token;
        private string userId;


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

        private UserInfoModel userInfo;

        public UserInfoModel UserInfo
        {
            get
            {
                return userInfo;
            }
            set
            {
                userInfo = value;

                OnPropertyChanged("UserInfo");
            }
        }

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

        private string number;

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

        private string _pinNumber;

        public string PinNumber
        {
            get
            {
                return _pinNumber;
            }
            set
            {
                _pinNumber = value;

                OnPropertyChanged("PinNumber");
            }
        }
    }
}
