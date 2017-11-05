using Epigov.Log;
using CardAPILib.CardAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iso18013Lib;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using GID_Client.ServerApi;
using SmartCardApi.SmartCardReader;
using GID_Client.DB;
using GemCard;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using CardAPILib.InterfaceCL;

namespace GID_Client.ViewModel
{
    internal class VoditelPravaViewModel : ViewModelBase
    {
        private readonly ILogService _logService;

        private CardNative _card;

        private CardApiController _controller;

        public RelayCommand ReadCard { get; private set; }

        public RelayCommand SaveCard { get; private set; }

        public RelayCommand StopProcess { get; private set; }

        private string readJson { get; set; }

        private SQLiteDataCommands dbDataSet { get; set; }

        public VoditelPravaViewModel()
        {
            _logService = new FileLogService(typeof(InitCardViewModel));

            ReadCard = new RelayCommand(_ => fReadCard());

            SaveCard = new RelayCommand(_ => fSaveCard());

            StopProcess = new RelayCommand(_ => fStopProcess());

            try
            {
                _controller = new CardApiController(false);
                dbDataSet = new SQLiteDataCommands();
                _card = new CardNative();

                _card.OnCardInserted += _card_OnCardInserted;
                _card.OnCardRemoved += _card_OnCardRemoved;

                string[] readers;
                string[] SpecReaders;


                readers = _card.ListReaders();

                SpecReaders = (from reader in readers
                               where reader.Contains("CK") || reader.Contains("CL")
                               select reader).ToArray();

                if (SpecReaders.Length == 0)
                {
                    MessageBox.Show("Отсуствует ридер или не установленны драйвера!!!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new ApplicationException("Отсуствует ридер или не установленны драйвера!!! Проблемы с устройством");
                }

                _card_OnCardRemoved(SpecReaders[0]);
                _card.StartCardEvents(SpecReaders[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Отсуствует ридер или не установленны драйвера!!!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new ApplicationException("Отсуствует ридер или не установленны драйвера!!! Проблемы с устройством " + ex.Message);
            }

            
            CheckedA = false;
            CheckedB = false;
            CheckedC = false;
            CheckedD = false;
            CheckedE = false;
        }

        private void fStopProcess()
        {
            try
            {
                worker.CancelAsync();
            }
            catch
            {
                //
            }

            IsIntermadiate = false;
            StopReadingProc = false;

        }

        public void Release()
        {
            _card.OnCardInserted -= _card_OnCardInserted;
            _card.OnCardRemoved -= _card_OnCardRemoved;
        }

        private void _card_OnCardInserted(string reader)
        {
            
        }

        private void ClearFields()
        {
            LastName = string.Empty;

            FirstName = string.Empty;

            MiddleName = string.Empty;

            BirthDate = string.Empty;

            IssueDate = string.Empty;

            ExpireDate = string.Empty;

            GivenPlace = string.Empty;

            LicenseNumber = string.Empty;

            PNFL = string.Empty;

            FullAdress = string.Empty;

            CheckedA = false;
            IssueA = string.Empty;
            ExpireA = string.Empty;

            CheckedB = false;
            IssueB = string.Empty;
            ExpireB = string.Empty;

            CheckedC = false;
            IssueC = string.Empty;
            ExpireC = string.Empty;

            CheckedD = false;
            IssueD = string.Empty;
            ExpireD = string.Empty;

            CheckedE = false;
            IssueE = string.Empty;
            ExpireE = string.Empty;

            BirthPlace = string.Empty;

            Base64ImageData = null;

            Base64ImageData2 = null;
        }

        private void _card_OnCardRemoved(string reader)
        {
            ClearFields();

            fReadCard();
        }

        private string GetUUid()
        {
            byte[] uuid = new byte[4];

            var result = _controller.ReadUniqueId(ref uuid);

            StringBuilder hex1 = new StringBuilder((4) * 2);
            foreach (byte b in uuid)
                hex1.AppendFormat("{0:X2}", b);
            var uid_temp = hex1.ToString();
            uid_temp = uid_temp.Substring(0, ((int)(4)) * 2);

            var MyUIDofCard = uid_temp;

            return MyUIDofCard;
        }

        private string GetStrFromDrivingLicense(DrivingLicenseExample responce)
        {
            string resultString = string.Empty;

            try
            {
                using (var stream1 = new MemoryStream())
                {
                    var ser = new DataContractJsonSerializer(typeof(DrivingLicenseExample));
                    ser.WriteObject(stream1, responce);
                    stream1.Position = 0;

                    using (var sr = new StreamReader(stream1))
                    {
                        resultString = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }

            return resultString;
        }

        private int PfReadCard()
        {

                try
                {
                    byte[] DG1 = null;

                    byte[] DG2 = null;

                    byte[] DG3 = null;

                    byte[] DG4 = null;

                    byte[] DG5 = null;

                    byte[] DGCommon = null;

                    try
                    {

                        SecuredReaderTest dd = new SecuredReaderTest();

                        DG1 = dd.IDL_ReaderDG1(InputString);
                        DG2 = dd.IDL_ReaderDG2(InputString);

                        DG4 = dd.IDL_ReaderDG4(InputString);
                        DG5 = dd.IDL_ReaderDG5(InputString);

                        DrivingLicense DrL = new DrivingLicense("");

                        var dl = DrL.ParseReadMaterial(DG1, DG2, DG3, DG4, DG5, DGCommon);

                        dl._card_number = GetUUid();

                        LastName = dl._driver._last_name;

                        FirstName = dl._driver._first_name;

                        MiddleName = dl._driver._middle_name;

                        BirthDate = DateTime.ParseExact(dl._driver._date_of_birth, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");

                        IssueDate = DateTime.ParseExact(dl._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");

                    ExpireDate = DateTime.ParseExact(dl._expire_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");

                    GivenPlace = dl._issue_region_name;

                        LicenseNumber = dl._license_number;

                        PNFL = dl._driver._pinfl;

                        FullAdress = string.Format("{0} {1} {2}", dl._driver._address._address, dl._driver._address._rayon_name, dl._driver._address._region_name);

                        dl._driver._address._address = string.Empty;

                        dl._driver._address._rayon_name = string.Empty;

                        dl._driver._address._region_name = string.Empty;

                        foreach (Category cat in dl._categories)
                        {
                            if (cat._name.Contains("A"))
                            {
                                CheckedA = true;
                                
                                var IssueAx = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);

                                IssueA = IssueAx.ToString("dd.MM.yyyy");

                                if (string.IsNullOrEmpty(cat._expiry_date))
                                {
                                    ExpireA = IssueAx.AddYears(10).ToString("dd.MM.yyyy");
                            }
                                else
                                {
                                    ExpireA = DateTime.ParseExact(cat._expiry_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
                            }
                                
                            }
                            if (cat._name.Contains("B"))
                            {
                                CheckedB = true;

                                var IssueBx = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);

                                IssueB = IssueBx.ToString("dd.MM.yyyy");


                                if (string.IsNullOrEmpty(cat._expiry_date))
                                {
                                    ExpireB = IssueBx.AddYears(10).ToString("dd.MM.yyyy");
                            }
                                else
                                {
                                    ExpireB = DateTime.ParseExact(cat._expiry_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
                            }
                            }
                            if (cat._name.Contains("C"))
                            {
                                CheckedC = true;

                                var IssueCx = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);

                                IssueC = IssueCx.ToString("dd.MM.yyyy");

                                if (string.IsNullOrEmpty(cat._expiry_date))
                                {
                                    ExpireC = IssueCx.AddYears(10).ToString("dd.MM.yyyy");
                            }
                                else
                                {
                                    ExpireC = DateTime.ParseExact(cat._expiry_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
                            }
                            }
                            if (cat._name.Contains("D"))
                            {
                                CheckedD = true;

                                var IssueDx = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);

                                IssueD = IssueDx.ToString("dd.MM.yyyy");
                                
                                if (string.IsNullOrEmpty(cat._expiry_date))
                                {
                                    ExpireD = IssueDx.AddYears(10).ToString("dd.MM.yyyy");
                            }
                                else
                                {
                                    ExpireD = DateTime.ParseExact(cat._expiry_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
                            }
                            }
                            if (cat._name.Contains("E"))
                            {
                                CheckedE = true;

                                var IssueEx = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);

                                IssueE = IssueEx.ToString("dd.MM.yyyy");

                                if (string.IsNullOrEmpty(cat._expiry_date))
                                {
                                    ExpireE = IssueEx.AddYears(10).ToString("dd.MM.yyyy");
                            }
                                else
                                {
                                    ExpireE = DateTime.ParseExact(cat._expiry_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
                            }
                            }
                        }

                        BirthPlace = dl._driver._region_name_birth;

                        Base64ImageData = dl._driver._Photo;

                        Base64ImageData2 = dl._driver._Signature;

                        readJson = GetStrFromDrivingLicense(dl);

                        if (Properties.Settings.Default.BackEndMode.Equals("1"))
                        {
                            var activationInfo = ServerApiController.SendBackEndActivation(true, readJson);//06.10.2017

                            StatusText = activationInfo._data._message;//06.10.2017
                        }

                }
                catch (Exception ex)
                    {
                        StatusText = "Ошибка при прочтении карты. Попробуйте еще раз";

                        _logService.Error(string.Format("{0} {1}", "Ошибка при прочтении карты", ex.Message));

                        return -1;
                    }

                    StatusText = StatusText + " -  " + "Карта удачно прочитанно";
                }
                catch (Exception ex)
                {
                    StatusText = ex.Message;
                }

                return 0;

        }

        private string InputString = string.Empty;
        private BackgroundWorker worker = new BackgroundWorker();

        private async void fReadCard()
        {
            StatusText = string.Empty;

            MondatoryWindow mon = new MondatoryWindow();

            //mon.Owner = App.Current.MainWindow;
            mon.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //mon.Owner = App.Current.MainWindow;

            var result1 = mon.ShowDialog();

            if (!result1.Value)
            {
                StatusText = "Ma'lumot to'liq kiritilmagan";

                _logService.Error(string.Format("{0} ", "Ma'lumot to'liq kiritilmagan"));

                var result = MessageBox.Show("Ma'lumot to'liq kiritilmagan. Ya harakat qilib ko'rasizmi?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    fReadCard();
                }

                return;
            }

            InputString = mon.InpetString;

            var sc = new SecureMessaging();

            bool dataCheck = false;

            for(int i = 0; i <= 3; i++)
            {
                int res = sc.CheckValidityOfKey(Encoding.UTF8.GetBytes(InputString));

                if (res == 0)
                {
                    dataCheck = true;
                    break;
                }
                else if (res == -99)
                {
                    dataCheck = false;
                    break;
                }
            }

            if (!dataCheck)
            {
                StatusText = "Ma'lumot xato kiritilgan";

                _logService.Error(string.Format("{0} ", "Введенные данные не верны"));

                var result = MessageBox.Show("Ma'lumot xato kiritilgan. Ya harakat qilib ko'rasizmi?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    fReadCard();
                }

                return;
            }



            StatusText = "O'qimoqda...";

            worker.DoWork += (o, ea) =>
            {
                //no direct interaction with the UI is allowed from this method
                //Mouse.OverrideCursor = Cursors.Wait;

                var res = PfReadCard();

                if (res == 0)
                {
                    StatusText = "O'qish yakunlandi";
                }
                else
                {
                    StatusText = "Xatolik...";
                }
            };
            worker.RunWorkerCompleted += (o, ea) =>
            {
                //work has completed. you can now interact with the UI
                IsIntermadiate = false;
                StopReadingProc = false;
            };
            //set the IsBusy before you start the thread
            IsIntermadiate = true;
            StopReadingProc = true;
            worker.RunWorkerAsync();

            //Mouse.OverrideCursor = null;
        }

        private void fSaveCard()
        {
            try
            {
                var activationInfo = ServerApiController.SendBackEndActivation(true, readJson);

                StatusText = activationInfo._data._message;
            }
            catch (Exception ex)
            {
                StatusText = "Ошибка при сохранении карты. Попробуйте еще раз";

                _logService.Error(string.Format("{0} {1}", "Ошибка при сохранении карты", ex.Message));

                return;
            }

            StatusText = "Карта удачно сохранена";
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }

            set
            {
                _lastName = value;

                OnPropertyChanged("LastName");
            }
        }

        private string _firstName;


        public string FirstName
        {
            get { return _firstName; }

            set
            {
                _firstName = value;

                OnPropertyChanged("FirstName");
            }
        }

        private string _middleName;

        private string _pnfl;

        public string PNFL
        {
            get { return _pnfl; }

            set
            {
                _pnfl = value;

                OnPropertyChanged("PNFL");
            }
        }

        private string _fullAdress;

        public string FullAdress
        {
            get { return _fullAdress; }

            set
            {
                _fullAdress = value;

                OnPropertyChanged("FullAdress");
            }
        }

        public string MiddleName
        {
            get { return _middleName; }

            set
            {
                _middleName = value;

                OnPropertyChanged("MiddleName");
            }
        }


        private string _base64ImageData;

        public string Base64ImageData
        {
            get { return _base64ImageData; }

            set
            {
                _base64ImageData = value;

                OnPropertyChanged("Base64ImageData");
            }
        }

        private string _base64ImageData2;

        public string Base64ImageData2
        {
            get { return _base64ImageData2; }

            set
            {
                _base64ImageData2 = value;

                OnPropertyChanged("Base64ImageData2");
            }
        }


        private string _birthPlace;


        public string BirthPlace
        {
            get { return _birthPlace; }

            set
            {
                _birthPlace = value;

                OnPropertyChanged("BirthPlace");
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


        private string _birthDate;

        public string BirthDate
        {
            get { return _birthDate; }

            set
            {
                _birthDate = value;

                OnPropertyChanged("BirthDate");
            }
        }

        private string _issueDate;

        public string IssueDate
        {
            get { return _issueDate; }

            set
            {
                _issueDate = value;

                OnPropertyChanged("IssueDate");
            }
        }

        private string _expireDate;

        public string ExpireDate
        {
            get { return _expireDate; }

            set
            {
                _expireDate = value;

                OnPropertyChanged("ExpireDate");
            }
        }

        private string _givenPlace;

        public string GivenPlace
        {
            get { return _givenPlace; }

            set
            {
                _givenPlace = value;

                OnPropertyChanged("GivenPlace");
            }
        }

        private string _licenseNumber;

        public string LicenseNumber
        {
            get { return _licenseNumber; }

            set
            {
                _licenseNumber = value;

                OnPropertyChanged("LicenseNumber");
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

        private bool _checkedA;
        private bool _checkedB;
        private bool _checkedC;
        private bool _checkedD;
        private bool _checkedE;

        public bool CheckedA
        {
            get
            {
                return _checkedA;
            }

            set
            {
                _checkedA = value;

                OnPropertyChanged("CheckedA");
            }
        }

        public bool CheckedB
        {
            get
            {
                return _checkedB;
            }

            set
            {
                _checkedB = value;

                OnPropertyChanged("CheckedB");
            }
        }

        public bool CheckedC
        {
            get
            {
                return _checkedC;
            }

            set
            {
                _checkedC = value;

                OnPropertyChanged("CheckedC");
            }
        }

        public bool CheckedD
        {
            get
            {
                return _checkedD;
            }

            set
            {
                _checkedD = value;

                OnPropertyChanged("CheckedD");
            }
        }

        public bool CheckedE
        {
            get
            {
                return _checkedE;
            }

            set
            {
                _checkedE = value;

                OnPropertyChanged("CheckedE");
            }
        }

        private string _issuedA;
        private string _issuedB;
        private string _issuedC;
        private string _issuedD;
        private string _issuedE;

        private string _expireA;
        private string _expireB;
        private string _expireC;
        private string _expireD;
        private string _expireE;

        public string IssueA
        {
            get { return _issuedA; }

            set
            {
                _issuedA = value;

                OnPropertyChanged("IssueA");
            }
        }


        public string IssueB
        {
            get { return _issuedB; }

            set
            {
                _issuedB = value;

                OnPropertyChanged("IssueB");
            }
        }

        public string IssueC
        {
            get { return _issuedC; }

            set
            {
                _issuedC = value;

                OnPropertyChanged("IssueC");
            }
        }

        public string IssueD
        {
            get { return _issuedD; }

            set
            {
                _issuedD = value;

                OnPropertyChanged("IssueD");
            }
        }


        public string IssueE
        {
            get { return _issuedE; }

            set
            {
                _issuedE = value;

                OnPropertyChanged("IssueE");
            }
        }


        public string ExpireA
        {
            get { return _expireA; }

            set
            {
                _expireA = value;

                OnPropertyChanged("ExpireA");
            }
        }


        public string ExpireB
        {
            get { return _expireB; }

            set
            {
                _expireB = value;

                OnPropertyChanged("ExpireB");
            }
        }

        public string ExpireC
        {
            get { return _expireC; }

            set
            {
                _expireC = value;

                OnPropertyChanged("ExpireC");
            }
        }

        public string ExpireD
        {
            get { return _expireD; }

            set
            {
                _expireD = value;

                OnPropertyChanged("ExpireD");
            }
        }


        public string ExpireE
        {
            get { return _expireE; }

            set
            {
                _expireE = value;

                OnPropertyChanged("ExpireE");
            }
        }

        private bool _stopReadingProc;

        public bool StopReadingProc
        {
            get { return _stopReadingProc; }

            set
            {
                _stopReadingProc = value;

                OnPropertyChanged("StopReadingProc");
            }
        }

    }
}
