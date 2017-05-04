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
        private string SuccessIcon { get; set; }

        private string FailIcon { get; set; }

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

            SuccessIcon = "/SmartCardDesc;component/Resources/1493374034_Tick_Mark_Dark.png";

            FailIcon = "/SmartCardDesc;component/Resources/1493374024_Close_Icon_Dark.png";
        }

        private async void fStartOp()
        {
            UserInfoResult = string.Empty;
            UserInfoResImage = string.Empty;
            CardInfoResult = string.Empty;
            CardInfoResImage = string.Empty;
            GenInfoResult = string.Empty;
            GenInfoResImage = string.Empty;
            CerInfoResult = string.Empty;
            CerInfoResImage = string.Empty;

            await fGetUserInfo();

            //if (!inProcessFlg)
            //{
            //    return;
            //}
                        
            await fLoadResults();

            await GenKeyFunc();

            //if (!string.IsNullOrEmpty(Modules))
            //{
                await CertCreate();
            //}
        }

        private async Task GenKeyFunc()
        {
            rsaViewModel.UserId = UserId;

            _genInfoTaskStatus = 0;
            GenInfoProssVis = true;
            GenInfoProssInter = true;
            GenInfoResultVis = false;
            GenInfoResImage = string.Empty;

            await rsaViewModel.fGenRsax();

            if (rsaViewModel.StatusText.Equals("1"))
            {
                _genInfoTaskStatus = 1;
                GenInfoProssVis = false;
                GenInfoProssInter = false;
                GenInfoResult = rsaViewModel.StatusText;
                GenInfoResultVis = true;
                GenInfoResImage = SuccessIcon;
            }
            else
            {
                _genInfoTaskStatus = 2;
                GenInfoProssVis = false;
                GenInfoProssInter = false;
                GenInfoResult = rsaViewModel.StatusText;
                GenInfoResultVis = true;
                GenInfoResImage = FailIcon;
            }

            StatusText = "Key Generation: " + rsaViewModel.StatusText;

            Modules = rsaViewModel.Modules;
            Exponental = rsaViewModel.Exponental;
        }

        private async Task CertCreate()
        {
            certViewModel.UserId = UserId;
            _cerInfoTaskStatus = 0;
            CerInfoProssVis = true;
            CerInfoProssInter = true;
            CerInfoResultVis = false;
            CerInfoResImage = string.Empty;

            await certViewModel.fGetCertificatex();

            if (certViewModel.StatusText.Equals("1"))
            {
                _cerInfoTaskStatus = 1;
                CerInfoProssVis = false;
                CerInfoProssInter = false;
                CerInfoResult = rsaViewModel.StatusText;
                CerInfoResultVis = true;
                CerInfoResImage = SuccessIcon;
            }
            else
            {
                _cerInfoTaskStatus = 2;
                CerInfoProssVis = false;
                CerInfoProssInter = false;
                CerInfoResult = rsaViewModel.StatusText;
                CerInfoResultVis = true;
                CerInfoResImage = FailIcon;
            }

            StatusText = "Certificate Gen " + certViewModel.StatusText;

            Certificate = certViewModel.Certificate;
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
                _userInfoTaskStatus = 0;
                UserInfoProssVis = true;
                UserInfoProssInter = true;
                UserInfoResultVis = false;
                UserInfoResImage = string.Empty;

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

                        _userInfoTaskStatus = 1;
                        UserInfoProssVis = false;
                        UserInfoProssInter = false;
                        UserInfoResult = "Загрузка прошла удачно...";
                        UserInfoResultVis = true;
                        UserInfoResImage = SuccessIcon;
                    }
                    else
                    {
                        StatusText = "UserInfo: " + UserInfo.result;
                        _userInfoTaskStatus = 2;
                        UserInfoProssVis = false;
                        UserInfoProssInter = false;
                        UserInfoResult = UserInfo.result;
                        UserInfoResultVis = true;
                        UserInfoResImage = FailIcon;
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
                IssueDate = DateTime.Now.Date;
                ExpireDate = DateTime.Now.Date.AddYears(10);

                IsIntermadiate = true;
                inProcessFlg = false;
                StatusText = "Card Insert Proccess...";
                _cardInfoTaskStatus = 0;
                CardInfoProssVis = true;
                CardInfoProssInter = true;
                CardInfoResultVis = false;
                CardInfoResImage = string.Empty;

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
                        if (Model.result.ToLower().Equals("success"))
                        {
                            Model.PinNumber = PinNumber;

                            FileInfo file = new FileInfo(imagePhotoPath);

                            var destFilePath = Path.Combine(Properties.Settings.Default.CardPhotoPath,
                                string.Format("{0}.{1}", Model.card_num, file.Extension));

                            file.CopyTo(destFilePath, true);

                            Model.picturePath = destFilePath;

                            Model.InsertCardInfoEntx();

                            _cardInfoTaskStatus = 1;
                            CardInfoProssVis = false;
                            CardInfoProssInter = false;
                            CardInfoResult = "Загрузка прошла удачно...";
                            CardInfoResultVis = true;
                            CardInfoResImage = SuccessIcon;
                        }
                        else
                        {
                            StatusText = "UserInfo: " + Model.result;
                            _cardInfoTaskStatus = 2;
                            CardInfoProssVis = false;
                            CardInfoProssInter = false;
                            CardInfoResult = Model.result;
                            CardInfoResultVis = true;
                            CardInfoResImage = FailIcon;
                        }


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

        #region UserProperties

        private byte _userInfoTaskStatus;

        private string _userInfoStatePath;

        public string UserInfoResImage
        {
            get
            {
                string image = string.Empty;

                if (_userInfoTaskStatus == 1)
                {
                    image = SuccessIcon;
                }
                else if (_userInfoTaskStatus == 2)
                {
                    image = FailIcon;
                }

                return image;
            }
            set
            {
                _userInfoStatePath = value;

                OnPropertyChanged("UserInfoResImage");
            }
        }

        private bool _userInfoProssVis;

        public bool UserInfoProssVis
        {
            get
            {
                return _userInfoProssVis;
            }

            set
            {
                _userInfoProssVis = value;

                OnPropertyChanged("UserInfoProssVis");
            }
        }

        private bool _userInfoProssInter;

        public bool UserInfoProssInter
        {
            get
            {
                return _userInfoProssInter;
            }

            set
            {
                _userInfoProssInter = value;

                OnPropertyChanged("UserInfoProssInter");
            }
        }

        private string _userInfoResult;

        public string UserInfoResult
        {
            get
            {
                return _userInfoResult;
            }

            set
            {
                _userInfoResult = value;

                OnPropertyChanged("UserInfoResult");
            }
        }

        private bool _userInfoResultVis;

        public bool UserInfoResultVis
        {
            get
            {
                return _userInfoResultVis;
            }

            set
            {
                _userInfoResultVis = value;

                OnPropertyChanged("UserInfoResultVis");
            }
        }

        #endregion

        #region CardProperties

        private byte _cardInfoTaskStatus;

        private string _cardInfoStatePath;

        public string CardInfoResImage
        {
            get
            {
                string image = string.Empty;

                if (_cardInfoTaskStatus == 1)
                {
                    image = SuccessIcon;
                }
                else if (_cardInfoTaskStatus == 2)
                {
                    image = FailIcon;
                }

                return image;
            }
            set
            {
                _cardInfoStatePath = value;

                OnPropertyChanged("CardInfoResImage");
            }
        }

        private bool _cardInfoProssVis;

        public bool CardInfoProssVis
        {
            get
            {
                return _cardInfoProssVis;
            }

            set
            {
                _cardInfoProssVis = value;

                OnPropertyChanged("CardInfoProssVis");
            }
        }

        private bool _cardInfoProssInter;

        public bool CardInfoProssInter
        {
            get
            {
                return _cardInfoProssInter;
            }

            set
            {
                _cardInfoProssInter = value;

                OnPropertyChanged("CardInfoProssInter");
            }
        }

        private string _cardInfoResult;

        public string CardInfoResult
        {
            get
            {
                return _cardInfoResult;
            }

            set
            {
                _cardInfoResult = value;

                OnPropertyChanged("CardInfoResult");
            }
        }

        private bool _cardInfoResultVis;

        public bool CardInfoResultVis
        {
            get
            {
                return _cardInfoResultVis;
            }

            set
            {
                _cardInfoResultVis = value;

                OnPropertyChanged("CardInfoResultVis");
            }
        }

        #endregion

        #region GenProperties

        private byte _genInfoTaskStatus;

        private string _genInfoStatePath;

        public string GenInfoResImage
        {
            get
            {
                string image = string.Empty;

                if (_genInfoTaskStatus == 1)
                {
                    image = SuccessIcon;
                }
                else if (_genInfoTaskStatus == 2)
                {
                    image = FailIcon;
                }

                return image;
            }
            set
            {
                _genInfoStatePath = value;

                OnPropertyChanged("GenInfoResImage");
            }
        }

        private bool _genInfoProssVis;

        public bool GenInfoProssVis
        {
            get
            {
                return _genInfoProssVis;
            }

            set
            {
                _genInfoProssVis = value;

                OnPropertyChanged("GenInfoProssVis");
            }
        }

        private bool _genInfoProssInter;

        public bool GenInfoProssInter
        {
            get
            {
                return _genInfoProssInter;
            }

            set
            {
                _genInfoProssInter = value;

                OnPropertyChanged("GenInfoProssInter");
            }
        }

        private string _genInfoResult;

        public string GenInfoResult
        {
            get
            {
                return _genInfoResult;
            }

            set
            {
                _genInfoResult = value;

                OnPropertyChanged("GenInfoResult");
            }
        }

        private bool _genInfoResultVis;

        public bool GenInfoResultVis
        {
            get
            {
                return _genInfoResultVis;
            }

            set
            {
                _genInfoResultVis = value;

                OnPropertyChanged("GenInfoResultVis");
            }
        }

        #endregion

        #region CerProperties

        private byte _cerInfoTaskStatus;

        private string _cerInfoStatePath;

        public string CerInfoResImage
        {
            get
            {
                string image = string.Empty;

                if (_cerInfoTaskStatus == 1)
                {
                    image = SuccessIcon;
                }
                else if (_cerInfoTaskStatus == 2)
                {
                    image = FailIcon;
                }

                return image;
            }
            set
            {
                _cerInfoStatePath = value;

                OnPropertyChanged("CerInfoResImage");
            }
        }

        private bool _cerInfoProssVis;

        public bool CerInfoProssVis
        {
            get
            {
                return _cerInfoProssVis;
            }

            set
            {
                _cerInfoProssVis = value;

                OnPropertyChanged("CerInfoProssVis");
            }
        }

        private bool _cerInfoProssInter;

        public bool CerInfoProssInter
        {
            get
            {
                return _cerInfoProssInter;
            }

            set
            {
                _cerInfoProssInter = value;

                OnPropertyChanged("CerInfoProssInter");
            }
        }

        private string _cerInfoResult;

        public string CerInfoResult
        {
            get
            {
                return _cerInfoResult;
            }

            set
            {
                _cerInfoResult = value;

                OnPropertyChanged("CerInfoResult");
            }
        }

        private bool _cerInfoResultVis;

        public bool CerInfoResultVis
        {
            get
            {
                return _cerInfoResultVis;
            }

            set
            {
                _cerInfoResultVis = value;

                OnPropertyChanged("CerInfoResultVis");
            }
        }

        #endregion

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
