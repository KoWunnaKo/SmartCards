﻿using Epigov.Log;
using System;
using System.Linq;
using System.Text;
using Iso18013Lib;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using GID_Client.ServerApi;
using SmartCardApi.SmartCardReader;
using GID_Client.DB;
using GemCard;
using System.Windows;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Configuration;

namespace GID_Client.ViewModel
{
    internal class VoditelPravaViewModel : ViewModelBase
    {
        private readonly ILogService _logService;

        private CardNative _card;

        public RelayCommand ReadCard { get; private set; }

        public RelayCommand SaveCard { get; private set; }

        public RelayCommand StopProcess { get; private set; }

        private string readJson { get; set; }

        private SQLiteDataCommands dbDataSet { get; set; }

        public VoditelPravaViewModel()
        {
            _logService = new FileLogService(typeof(VoditelPravaViewModel));

            ReadCard = new RelayCommand(_ => fReadCard());

            SaveCard = new RelayCommand(_ => fSaveCard());

            StopProcess = new RelayCommand(_ => fStopProcess());

            try
            {
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
                    MessageBox.Show("Kompyuterga rider ulanmagan yoki drayverlar o'rnatilmagan!!!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new ApplicationException("Kompyuterga rider ulanmagan yoki drayverlar o'rnatilmagan!!! Uskuna bilan bog'liq muammolar");
                }

                _card_OnCardRemoved(SpecReaders[0]);
                _card.StartCardEvents(SpecReaders[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kompyuterga rider ulanmagan yoki drayverlar o'rnatilmagan!!!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new ApplicationException("Kompyuterga rider ulanmagan yoki drayverlar o'rnatilmagan!!! Uskuna bilan bog'liq muammolar " + ex.Message);
            }

            
            CheckedA = false;
            CheckedB = false;
            CheckedC = false;
            CheckedD = false;
            CheckedBE = false;
            CheckedCE = false;
            CheckedDE = false;
        }

        private APDUResponse apduResp;

        const ushort SC_OK = 0x9000;
        const byte SC_PENDING = 0x9F;
        const ushort SC_FileEnd = 0x6282;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cla"></param>
        /// <param name="Ins"></param>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <param name="data"></param>
        /// <param name="_recieveLength"></param>
        /// <returns></returns>
        private byte[] CallApduCommandLeData(byte Cla, byte Ins, byte P1, byte P2, byte[] data, uint _recieveLength)
        {
            _logService.Info("Begin get UUID ...");

            APDUCommand apduSize5 = new APDUCommand(Cla, Ins, P1, P2, null, 0);

            APDUParam apduParam5 = new APDUParam();

            apduParam5.Data = data;

            apduSize5.Update(apduParam5);

            _logService.Info(apduSize5.ToString());

            _logService.Info("_card.Disconnect(DISCONNECT.Reset) begin ");

            _card.Disconnect(DISCONNECT.Reset);

            _logService.Info("_card.Disconnect(DISCONNECT.Reset) finished");

            Connect2Card();

            _logService.Info("Connect2Card() finished 1");

            _card.Disconnect(DISCONNECT.Reset);

            _logService.Info("_card.Disconnect(DISCONNECT.Reset) finished ");

            Connect2Card();

            _logService.Info("Connect2Card() finished 2");

            apduResp = _card.TransmitLe(apduSize5, _recieveLength);
            if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING && apduResp.Status != SC_FileEnd)
            {

                _logService.Info(apduResp.ToString());

                return null;
            }

            _logService.Info(apduResp.Status.ToString());
            _logService.Info(apduResp.ToString());

            return apduResp.Data;
        }

        public int Connect2Card()
        {
            try
            {
                _logService.Info("Connect2Card");

                string[] readers = _card.ListReaders();

                _logService.Info(readers.Length.ToString());

                foreach (string rr in readers)
                {
                    _logService.Info(rr);
                }

                string[] SpecReaders = (from reader in readers
                                        where reader.Contains("CK") || reader.Contains("CL")
                                        select reader).ToArray();


                foreach (string readerInfo in SpecReaders)
                {
                    try
                    {
                        _card.Disconnect(DISCONNECT.Unpower);

                    }
                    catch (Exception)
                    {
                        //
                    }

                    try
                    {
                        _logService.Info(readerInfo);

                        _card.Connect(readerInfo, SHARE.Shared, PROTOCOL.T0orT1);

                        _logService.Info("Connection finished!!!");

                        return 0;
                    }
                    catch (Exception ex)
                    {
                        _logService.Info("Exception happened...");
                        _card.Disconnect(DISCONNECT.Unpower);
                        _logService.Info(ex.ToString());
                    }
                    finally
                    {

                    }
                }

            }
            catch (Exception ex)
            {

                _logService.Info(ex.Message);
                _card.Disconnect(DISCONNECT.Unpower);
            }

            return 4;
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

            CheckedBE = false;
            IssueBE = string.Empty;
            ExpireBE = string.Empty;

            CheckedCE = false;
            IssueCE = string.Empty;
            ExpireCE = string.Empty;

            CheckedDE = false;
            IssueDE = string.Empty;
            ExpireDE = string.Empty;

            BirthPlace = string.Empty;

            Base64ImageData = null;

            Base64ImageData2 = null;
        }

        private void _card_OnCardRemoved(string reader)
        {
            ClearFields();

            fReadCard();

            //Application.Current.Dispatcher.BeginInvoke(
            //DispatcherPriority.Input, new DispatcherOperationCallback(delegate (object param)
            //{
            //    fReadCard();

            //    return null;
            //}), this);
        }

        private string GetUUid()
        {
            var AtrBytes = CallApduCommandLeData(0xFF, 0xCA, 0x00, 0x00, null, 5);

            StringBuilder hex1 = new StringBuilder((4) * 2);
            foreach (byte b in AtrBytes)
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

                                    if (cat._name.Contains("BE"))
                                    {
                                        CheckedBE = true;
            
                                        var IssueEx = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);
            
                                        IssueBE = IssueEx.ToString("dd.MM.yyyy");
            
                                        if (string.IsNullOrEmpty(cat._expiry_date))
                                        {
                                            ExpireBE = IssueEx.AddYears(10).ToString("dd.MM.yyyy");
                                        }
                                        else
                                        {
                                            ExpireBE = DateTime.ParseExact(cat._expiry_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
                                        }
            
                                        continue;
                                    }
            
                                    if (cat._name.Contains("CE"))
                                    {
                                        CheckedCE = true;
            
                                        var IssueEx = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);
            
                                        IssueCE = IssueEx.ToString("dd.MM.yyyy");
            
                                        if (string.IsNullOrEmpty(cat._expiry_date))
                                        {
                                            ExpireCE = IssueEx.AddYears(10).ToString("dd.MM.yyyy");
                                        }
                                        else
                                        {
                                            ExpireCE = DateTime.ParseExact(cat._expiry_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
                                        }
            
                                        continue;
                                    }
            
                                    if (cat._name.Contains("DE"))
                                    {
                                        CheckedDE = true;
            
                                        var IssueEx = DateTime.ParseExact(cat._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);
            
                                        IssueDE = IssueEx.ToString("dd.MM.yyyy");
            
                                        if (string.IsNullOrEmpty(cat._expiry_date))
                                        {
                                            ExpireDE = IssueEx.AddYears(10).ToString("dd.MM.yyyy");
                                        }
                                        else
                                        {
                                            ExpireDE = DateTime.ParseExact(cat._expiry_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
                                        }
            
                                        continue;
                                    }
            
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
                        StatusText = "Kartani o'qishda muammo yuz berdi. Yana harakat qilib ko'ring";

                        _logService.Error(string.Format("{0} {1}", "Kartani o'qishda xatolik yuz berdi", ex.Message));

                        return -1;
                    }

                    StatusText = StatusText + " -  " + "Karta muvaffaqiyatli o'qildi";
                }
                catch (Exception ex)
                {
                    StatusText = ex.Message;
                }

                return 0;

        }

        private string InputString = string.Empty;
        private BackgroundWorker worker = new BackgroundWorker();
        private BackgroundWorker worker2 = new BackgroundWorker();

        private string _txbDocNum;
        private string _DpBirthDate;
        private string _DpExpireDate;

        private Task<bool> CheckExpiredDate(string enteredDate)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                
                byte[] DG1 = null;

                SecuredReaderTest dd = new SecuredReaderTest();

                DG1 = dd.IDL_ReaderDG1(InputString);

                DrivingLicense DrL = new DrivingLicense("");

                var dlExpired = DrL.ParseDG1Expired(DG1);

                var ExpireDate = DateTime.ParseExact(dlExpired, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");

                if (!ExpireDate.Equals(enteredDate))
                {
                    return false;
                }

                return true;
            });

            return resultTask;

        }


        private async void fReadCardAsync()
        {
            var done = false;

            while (!done)
            {
                var customViewModel = new { };

                var task = Task.Factory.StartNew(() => 
                {
                    if ((!string.IsNullOrEmpty(_txbDocNum)) && (!string.IsNullOrEmpty(_DpBirthDate)) &&
               (!string.IsNullOrEmpty(_DpExpireDate)))
                    {
                        //customViewModel.ftxbDocNum = _txbDocNum;
                        //customViewModel.fDpBirthDate = _DpBirthDate.Remove(2, 1).Remove(4, 1);
                        //customViewModel.fDpExpireDate = _DpExpireDate.Remove(2, 1).Remove(4, 1);

                        //customViewModel.Ready2Read = true;
                    }
                });
            }
        }
        private async void fReadCard()
        {
            StatusText = string.Empty;

            MondatoryWindow mon = new MondatoryWindow();

            if ((!string.IsNullOrEmpty(_txbDocNum)) && (!string.IsNullOrEmpty(_DpBirthDate)) &&
                (!string.IsNullOrEmpty(_DpExpireDate)))
            {
                mon.ftxbDocNum = _txbDocNum;
                mon.fDpBirthDate = _DpBirthDate.Remove(2, 1).Remove(4, 1);
                mon.fDpExpireDate = _DpExpireDate.Remove(2, 1).Remove(4, 1);

                mon.Ready2Read = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(_txbDocNum))
                {
                    mon.ftxbDocNum = _txbDocNum;
                }

                if (!string.IsNullOrEmpty(_DpBirthDate))
                    mon.fDpBirthDate = _DpBirthDate.Remove(2, 1).Remove(4, 1);

                if (!string.IsNullOrEmpty(_DpExpireDate))
                    mon.fDpExpireDate = _DpExpireDate.Remove(2, 1).Remove(4, 1);
            }

            //mon.Owner = App.Current.MainWindow;
            mon.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //mon.Owner = App.Current.MainWindow;

            var result1 = mon.ShowDialog();

            if ((!result1.Value) && (!mon.isClosed))
            {
                StatusText = "Ma'lumot to'liq kiritilmagan";

                _txbDocNum = mon.ftxbDocNum;
                _DpBirthDate = mon.fDpBirthDate;
                _DpExpireDate = mon.fDpExpireDate;

                _logService.Error(string.Format("{0} ", "Ma'lumot to'liq kiritilmagan"));

                var result = MessageBox.Show("Ma'lumot to'liq kiritilmagan. Yana harakat qilib ko'rasizmi?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                //if (result == MessageBoxResult.OK)
                //{
                //    fReadCard();
                //}
                //else
                //{
                //    fReadCard();
                //}

                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Input, new DispatcherOperationCallback(delegate(object param) 
                    {
                        fReadCard();

                        return null;
                    }), this);
                
                return;
            }
            else if (mon.isClosed)
            {
                return;
            }

            InputString = mon.InpetString;

            var sc = new SecureMessaging();

            bool dataCheck = false;

            for(int i = 0; i <= 2; i++)
            {
                var loginAndPasw = ConfigurationManager.AppSettings["LoginPswd"];

                if (!string.IsNullOrEmpty(loginAndPasw))
                {
                    string[] log_pas = loginAndPasw.Split(':');

                    if (log_pas.Any())
                    {
                        if (log_pas.Length == 2)
                        {
                            var reqRes = ServerApiController.LoginReqRes(log_pas[0], log_pas[1]);

                            if (reqRes != null)
                                ServerApiController.token = reqRes.data.Token;
                        }
                    }
                }

                var CardNUmber = sc.ReadCardNumber();
                string kKeyValue = string.Empty;

                try
                {
                    var KeyValue = ServerApiController.GetKey(CardNUmber);

                    if (!string.IsNullOrEmpty(KeyValue._data._message))
                    {
                        kKeyValue = KeyValue._data._message;
                    }
                }
                catch
                {
                    kKeyValue = "404142434445464748494A4B4C4D4E4F";
                }

                
                int res= sc.CheckValidityOfKey(Encoding.UTF8.GetBytes(InputString), kKeyValue);

                if (res != 0)
                {
                    //Try to check for new key
                    kKeyValue = "4557594F4F4D4B554C56494E57454731";
                    res = sc.CheckValidityOfKey(Encoding.UTF8.GetBytes(InputString), kKeyValue);
                }

                if (res == 0)
                {
                    //byte[] DG1 = null;

                    //SecuredReaderTest dd = new SecuredReaderTest();

                    //DG1 = dd.IDL_ReaderDG1(InputString);

                    //DrivingLicense DrL = new DrivingLicense("");

                    //var dlExpired = DrL.ParseDG1Expired(DG1);

                    //var ExpireDate = DateTime.ParseExact(dlExpired, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");

                    //if (!ExpireDate.Equals(mon.fDpExpireDate))
                    //{
                    //    dataCheck = false;
                    //    break;
                    //}

                    IsIntermadiate = true;
                    StatusText = "Kiritilgan ma'lumot tekshirilmoqda...";

                    string expiredDate = mon.fDpExpireDate;

                    var reslit = await CheckExpiredDate(expiredDate);

                    IsIntermadiate = false;
                    StatusText = string.Empty;

                    if (!reslit)
                    {
                        dataCheck = false;
                        break;
                    }

                    dataCheck = true;
                    break;
                }
                else if (res == -1)
                {
                    MessageBox.Show("Kartani Riderning ustiga qo'ying. Davom ettirish...", "", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                _txbDocNum = mon.ftxbDocNum;
                _DpBirthDate = mon.fDpBirthDate;
                _DpExpireDate = mon.fDpExpireDate;

                _logService.Error(string.Format("{0} ", "Введенные данные не верны"));

                var result = MessageBox.Show("Ma'lumot xato kiritilgan. Yana harakat qilib ko'rasizmi?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    //fReadCard();

                    Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Input, new DispatcherOperationCallback(delegate (object param)
                    {
                        fReadCard();

                        return null;
                    }), this);
                }

                return;
            }
            worker = new BackgroundWorker();
            StatusText = "O'qimoqda...";

            worker.DoWork += (o, ea) =>
            {
                //no direct interaction with the UI is allowed from this method
                //Mouse.OverrideCursor = Cursors.Wait;

                var res = PfReadCard();

                if (res == 0)
                {
                    _txbDocNum = string.Empty;
                    _DpBirthDate = string.Empty;
                    _DpExpireDate = string.Empty;
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
                StatusText = "Kartani ma'lumotlarini saqlashda xatolik yuz berdi";

                _logService.Error(string.Format("{0} {1}", "Kartaning ma'lumotlarini saqlashda xatolik yuz berdi", ex.Message));

                return;
            }

            StatusText = "Karta ma'lumotlari saqlandi";
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
        private bool _checkedBE;
        private bool _checkedCE;
        private bool _checkedDE;

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

        public bool CheckedBE
        {
            get
            {
                return _checkedBE;
            }

            set
            {
                _checkedBE = value;

                OnPropertyChanged("CheckedBE");
            }
        }

        public bool CheckedCE
        {
            get
            {
                return _checkedCE;
            }

            set
            {
                _checkedCE = value;

                OnPropertyChanged("CheckedCE");
            }
        }

        public bool CheckedDE
        {
            get
            {
                return _checkedDE;
            }

            set
            {
                _checkedDE = value;

                OnPropertyChanged("CheckedDE");
            }
        }

        private string _issuedA;
        private string _issuedB;
        private string _issuedC;
        private string _issuedD;
        private string _issuedBE;
        private string _issuedCE;
        private string _issuedDE;

        private string _expireA;
        private string _expireB;
        private string _expireC;
        private string _expireD;
        private string _expireBE;
        private string _expireCE;
        private string _expireDE;

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


        public string IssueBE
        {
            get { return _issuedBE; }

            set
            {
                _issuedBE = value;

                OnPropertyChanged("IssueBE");
            }
        }

        public string IssueCE
        {
            get { return _issuedCE; }

            set
            {
                _issuedCE = value;

                OnPropertyChanged("IssueCE");
            }
        }

        public string IssueDE
        {
            get { return _issuedDE; }

            set
            {
                _issuedDE = value;

                OnPropertyChanged("IssueDE");
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


        public string ExpireBE
        {
            get { return _expireBE; }

            set
            {
                _expireBE = value;

                OnPropertyChanged("ExpireBE");
            }
        }

        public string ExpireCE
        {
            get { return _expireCE; }

            set
            {
                _expireCE = value;

                OnPropertyChanged("ExpireCE");
            }
        }

        public string ExpireDE
        {
            get { return _expireDE; }

            set
            {
                _expireDE = value;

                OnPropertyChanged("ExpireDE");
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
