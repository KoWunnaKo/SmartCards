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

namespace GID_Client.ViewModel
{
    internal class VoditelPravaViewModel : ViewModelBase
    {
        private readonly ILogService _logService;

        private CardApiController _controller;

        public RelayCommand ReadCard { get; private set; }

        public RelayCommand SaveCard { get; private set; }

        private string readJson { get; set; }

        public VoditelPravaViewModel()
        {
            _logService = new FileLogService(typeof(InitCardViewModel));

            ReadCard = new RelayCommand(_ => fReadCard());

            SaveCard = new RelayCommand(_ => fSaveCard());

            _controller = new CardApiController(true);

            CheckedA = false;
            CheckedB = false;
            CheckedC = false;
            CheckedD = false;
            CheckedE = false;

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

        private Task<int> PfReadCard()
        {
            var resultTask = Task.Factory.StartNew(() =>
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
                        var result = _controller.ReadIDLCardNext(ref DG1, ref DG2, ref DG3, ref DG4, ref DG5, ref DGCommon);

                        if (result != 0)
                        {
                            StatusText = "Ошибка при прочтении карты";

                            _logService.Error(string.Format("{0} {1}", "Ошибка при прочтении карты", result));

                            return -1;
                        }

                        DrivingLicense DrL = new DrivingLicense("");

                        var dl = DrL.ParseReadMaterial(DG1, DG2, DG3, DG4, DG5, DGCommon);

                        dl._card_number = GetUUid();

                        LastName = dl._driver._last_name;

                        FirstName = dl._driver._first_name;

                        MiddleName = dl._driver._middle_name;

                        BirthDate = DateTime.ParseExact(dl._driver._date_of_birth, "yyyyMMdd", CultureInfo.InvariantCulture);

                        IssueDate = DateTime.ParseExact(dl._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);

                        ExpireDate = DateTime.ParseExact(dl._expire_date, "yyyyMMdd", CultureInfo.InvariantCulture);

                        GivenPlace = dl._issue_region_name;

                        LicenseNumber = dl._license_number;

                        PNFL = dl._driver._pinfl;

                        FullAdress = string.Format("{0} {1} {2}", dl._driver._address._address, dl._driver._address._rayon_name, dl._driver._address._region_name);

                        foreach (Category cat in dl._categories)
                        {
                            if (cat._name.Contains("A"))
                            {
                                CheckedA = true;
                                IssueA = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);
                            }

                            if (cat._name.Contains("B"))
                            {
                                CheckedB = true;
                                IssueB = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);
                            }

                            if (cat._name.Contains("C"))
                            {
                                CheckedC = true;
                                IssueC = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);
                            }

                            if (cat._name.Contains("D"))
                            {
                                CheckedD = true;
                                IssueD = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);
                            }

                            if (cat._name.Contains("E"))
                            {
                                CheckedE = true;
                                IssueE = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);
                            }
                        }

                        BirthPlace = dl._driver._region_name_birth;

                        Base64ImageData = dl._driver._Photo;

                        Base64ImageData2 = dl._driver._Signature;

                        readJson = GetStrFromDrivingLicense(dl);

                        var activationInfo = ServerApiController.SendBackEndActivation(true, readJson);

                        StatusText = activationInfo._data._message;
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
            });

            return resultTask;
        }

        private async void fReadCard()
        {
            MondatoryWindow mon = new MondatoryWindow();

            mon.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            var result1 = mon.ShowDialog();

            if (!result1.Value)
            {
                StatusText = "Введенные данные не верны";

                _logService.Error(string.Format("{0} ", "Введенные данные не верны"));

                return;
            }

            IsIntermadiate = true;

            StatusText = "Считка...";

            var res = await PfReadCard();

            if (res == 0)
            {
                StatusText = "Удачная Считка!!!";
            }
            else
            {
                StatusText = "Error...";
            }

            IsIntermadiate = false;

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


        private DateTime _birthDate;

        public DateTime BirthDate
        {
            get { return _birthDate; }

            set
            {
                _birthDate = value;

                OnPropertyChanged("BirthDate");
            }
        }

        private DateTime _issueDate;

        public DateTime IssueDate
        {
            get { return _issueDate; }

            set
            {
                _issueDate = value;

                OnPropertyChanged("IssueDate");
            }
        }

        private DateTime _expireDate;

        public DateTime ExpireDate
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

        private DateTime _issuedA;
        private DateTime _issuedB;
        private DateTime _issuedC;
        private DateTime _issuedD;
        private DateTime _issuedE;

        public DateTime IssueA
        {
            get { return _issuedA; }

            set
            {
                _issuedA = value;

                OnPropertyChanged("IssueA");
            }
        }


        public DateTime IssueB
        {
            get { return _issuedB; }

            set
            {
                _issuedB = value;

                OnPropertyChanged("IssueB");
            }
        }

        public DateTime IssueC
        {
            get { return _issuedC; }

            set
            {
                _issuedC = value;

                OnPropertyChanged("IssueC");
            }
        }

        public DateTime IssueD
        {
            get { return _issuedD; }

            set
            {
                _issuedD = value;

                OnPropertyChanged("IssueD");
            }
        }


        public DateTime IssueE
        {
            get { return _issuedE; }

            set
            {
                _issuedE = value;

                OnPropertyChanged("IssueE");
            }
        }

    }
}
