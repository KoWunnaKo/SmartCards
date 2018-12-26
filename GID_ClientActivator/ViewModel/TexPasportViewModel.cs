using Epigov.Log;
using GemCard;
using GID_Client.ServerApi;
using Iso18013Lib;
using SmartCardApi.SmartCardReader;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace GID_Client.ViewModel
{
    internal class TexPasportViewModel: ViewModelBase
    {
        private readonly ILogService _logService;

        public RelayCommand ReadCard { get; private set; }

        public RelayCommand SaveCard { get; private set; }

        private string readJson { get; set; }

        public TexPasportViewModel()
        {
            _logService = new FileLogService(typeof(TexPasportViewModel));

            ReadCard = new RelayCommand(_ => fReadCard());

            SaveCard = new RelayCommand(_ => fSaveCard());

            try
            {
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

        internal void Release()
        {
            _card.OnCardInserted -= _card_OnCardInserted;
            _card.OnCardRemoved -= _card_OnCardRemoved;
        }

        private void _card_OnCardInserted(string reader)
        {
            
        }

        private void _card_OnCardRemoved(string reader)
        {
            Pinfl = string.Empty;

            Issue_date = string.Empty;

            Ubdd_name = string.Empty;

            Issue_region_name = string.Empty;

            Vehicle1_reg_number = string.Empty;

            Vehicle1_model_name = string.Empty;

            Vehicle1_color_name = string.Empty;

            Vehicle1_vehicle_manufacture_year = string.Empty;

            Vehicle1_gross_weight = string.Empty;

            Vehicle1_curb_weight = string.Empty;

            Vehicle1_engine_number = string.Empty;

            Vehicle1_engine_power = string.Empty;

            Vehicle1_fuel_type = string.Empty;

            Vehicle1_number_of_seats = string.Empty;

            Vehicle1_number_of_standees = string.Empty;

            Vehicle1_special_marks = string.Empty;

            Company1_name = string.Empty;

            Company1_inn = string.Empty;

            Company1_address2_address = string.Empty;

            Company1_address2_region_name = string.Empty;

            Company1_address2_rayon_name = string.Empty;

            Owner_type = 0;

            Owner_last_name = string.Empty;

            Owner_first_name = string.Empty;

            Owner_middle_name = string.Empty;

            Mark_name = string.Empty;

            Vehicle_type = string.Empty;

            Vehicle_identification_number_kuzov = string.Empty;

            Vehicle_identification_number_shassi = string.Empty;

            Engine_measurement = string.Empty;

            License_number = string.Empty;

            Expire_date = string.Empty;

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

        private Task<int> PfReadCard()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {

                try
                {
                    byte[] Vr = null;

                    try
                    {
                        SecuredReaderTest dd = new SecuredReaderTest();

                        Vr = dd.VR_Reader(InputString);

                        _logService.Info("PfReadCard");
                        //if (result != 0)
                        //{
                        //    StatusText = "Ошибка при прочтении карты";

                        //    _logService.Error(string.Format("{0} {1}", "Ошибка при прочтении карты", result));

                        //    return -1;
                        //}

                        VehicleRegistration vl = new VehicleRegistration("");

                        _logService.Info("PfReadCard111");
                        var vll = vl.ParseReadMaterial(Vr);

                        vl._vehicleRegistration._card_number = GetUUid();

                        Pinfl = vl._vehicleRegistration._company._pinfl;
                        _logService.Info("PfReadCard112");
                        //
                        if (!string.IsNullOrEmpty(vl._vehicleRegistration._issue_date))
                        {
                            if (vl._vehicleRegistration._issue_date.Length == 8)
                            {
                                Issue_date = DateTime.ParseExact(vl._vehicleRegistration._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
                            }
                            else
                            {
                                Issue_date = vl._vehicleRegistration._issue_date;
                            }
                        }
                        else
                        {
                            Issue_date = vl._vehicleRegistration._issue_date;
                        }
                        _logService.Info("PfReadCard113");
                        Ubdd_name = vl._vehicleRegistration._ubdd_name;
                        _logService.Info("PfReadCard4");
                        Issue_region_name = vl._vehicleRegistration._license_number;
                        _logService.Info("PfReadCard5");
                        Vehicle1_reg_number = vl._vehicleRegistration._vehicle._reg_number;
                        _logService.Info("PfReadCard6");
                        Vehicle1_model_name = vl._vehicleRegistration._vehicle._model_name;
                        _logService.Info("PfReadCard7");
                        Vehicle1_color_name = vl._vehicleRegistration._vehicle._color_name;
                        _logService.Info("PfReadCard8");
                        Vehicle1_vehicle_manufacture_year = vl._vehicleRegistration._vehicle._vehicle_manufacture_year;
                        _logService.Info("PfReadCard9");
                        Vehicle1_gross_weight = vl._vehicleRegistration._vehicle._gross_weight.ToString();
                        _logService.Info("PfReadCard10");
                        Vehicle1_curb_weight = vl._vehicleRegistration._vehicle._curb_weight.ToString();
                        _logService.Info("PfReadCard1131");
                        Vehicle1_engine_number = vl._vehicleRegistration._vehicle._engine_number;
                        _logService.Info("PfReadCard1132");
                        Vehicle1_engine_power = vl._vehicleRegistration._vehicle._engine_power;
                        _logService.Info("PfReadCard1134");
                        Vehicle1_fuel_type = vl._vehicleRegistration._vehicle._fuel_type;
                        _logService.Info("PfReadCard1135");
                        Vehicle1_number_of_seats = vl._vehicleRegistration._vehicle._number_of_seats.ToString();
                        _logService.Info("PfReadCard1136");
                        Vehicle1_number_of_standees = vl._vehicleRegistration._vehicle._number_of_standees;
                        _logService.Info("PfReadCard1137");
                        Vehicle1_special_marks = vl._vehicleRegistration._vehicle._special_marks;
                        _logService.Info("PfReadCard1138");
                        Company1_name = vl._vehicleRegistration._company._name;
                        _logService.Info("PfReadCard1139");
                        Company1_inn = vl._vehicleRegistration._company._inn;
                        _logService.Info("PfReadCard11310");
                        Company1_address2_address = vl._vehicleRegistration._company._address._address;
                        _logService.Info("PfReadCard11311");
                        Company1_address2_region_name = vl._vehicleRegistration._company._address._region_name;
                        _logService.Info("PfReadCard11312");
                        Company1_address2_rayon_name = vl._vehicleRegistration._company._address._rayon_name;
                        _logService.Info("PfReadCard11313");
                        vl._vehicleRegistration._company._address._address = string.Empty;
                        _logService.Info("PfReadCard11314");
                        vl._vehicleRegistration._company._address._region_name = string.Empty;
                        _logService.Info("PfReadCard11315");
                        vl._vehicleRegistration._company._address._rayon_name = string.Empty;
                        _logService.Info("PfReadCard11316");
                        Owner_type = vl._vehicleRegistration._company._type;
                        _logService.Info("PfReadCard11317");
                        Owner_last_name = vl._vehicleRegistration._company._last_name;
                        _logService.Info("PfReadCard11318");
                        Owner_first_name = vl._vehicleRegistration._company._first_name;
                        _logService.Info("PfReadCard11319");
                        Owner_middle_name = vl._vehicleRegistration._company._middle_name;
                        _logService.Info("PfReadCard11320");
                        Mark_name = vl._vehicleRegistration._vehicle._mark_name;
                        _logService.Info("PfReadCard11321");
                        Vehicle_type = vl._vehicleRegistration._vehicle._type;
                        _logService.Info("PfReadCard11322");
                        Vehicle_identification_number_kuzov = vl._vehicleRegistration._vehicle._vehicle_identification_number_kuzov;
                        _logService.Info("PfReadCard11323");
                        Vehicle_identification_number_shassi = vl._vehicleRegistration._vehicle._vehicle_identification_number_shassi;
                        _logService.Info("PfReadCard11324");
                        Engine_measurement = vl._vehicleRegistration._vehicle._engine_measurement;
                        _logService.Info("PfReadCard11325");
                        License_number = vl._vehicleRegistration._license_number;
                        _logService.Info("PfReadCard11326");
                        if (!string.IsNullOrEmpty(vl._vehicleRegistration._expire_date))
                        {
                            if (vl._vehicleRegistration._expire_date.Length == 8)
                            {
                                Expire_date = DateTime.ParseExact(vl._vehicleRegistration._expire_date, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd.MM.yyyy");
                            }
                            else
                            {
                                Expire_date = vl._vehicleRegistration._expire_date;
                            }
                        }
                        else
                        {
                            Expire_date = vl._vehicleRegistration._expire_date;
                        }

                        _logService.Info("PfReadCard113");
                        readJson = GetStrFromVR(vl._vehicleRegistration);

                        if (Properties.Settings.Default.BackEndMode.Equals("1"))
                        {

                            var activationInfo = ServerApiController.SendBackEndActivation(false, readJson);//06.10.2017

                            StatusText = activationInfo._data._message;//06.10.2017
                        }

                    }
                    catch (Exception ex)
                    {
                        _logService.Info(ex.ToString());
                    }

                }
                catch (Exception ex)
                {
                    StatusText = ex.Message;
                    _logService.Info(ex.ToString());
                }

                return 0;
            });

            return resultTask;
        }

        private string InputString = string.Empty;
        private CardNative _card;

        private string _txbDocNum;
        private string _DpIssueDate;
        private string _txbDocNum2;

        private Task<bool> CheckExpiredDate(string enteredDate)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                try
                {
                    byte[] Vr = null;

                    SecuredReaderTest dd = new SecuredReaderTest();

                    Vr = dd.VR_Reader(InputString);

                    _logService.Info(InputString);

                    string hex = BitConverter.ToString(Vr);

                    _logService.Info("--------------------------------------------");
                    _logService.Info(hex);
                    _logService.Info("--------------------------------------------");

                    VehicleRegistration vl = new VehicleRegistration("");

                    var vll = vl.ParseReadMaterial(Vr);

                    if (!vll._license_number.Equals(enteredDate))
                    {
                        return false;
                    }

                    
                }
                catch(Exception ex)
                {
                    _logService.Info(ex.ToString());
                    return false;
                }

                return true;
            });

            return resultTask;

        }

        private async void fReadCard()
        {
            StatusText = string.Empty;

            VLDataChecker mon = new VLDataChecker();

            //mon.Owner = App.Current.MainWindow;

            mon.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if ((!string.IsNullOrEmpty(_txbDocNum)) && (!string.IsNullOrEmpty(_DpIssueDate)) &&
            (!string.IsNullOrEmpty(_txbDocNum2)))
            {
                mon.ftxbDocNum = _txbDocNum;
                mon.fDpIssueDate = _DpIssueDate.Remove(2, 1).Remove(4, 1);
                mon.ftxbDocNum2 = _txbDocNum2;

                mon.Ready2Read = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(_txbDocNum))
                {
                    mon.ftxbDocNum = _txbDocNum;
                }

                if (!string.IsNullOrEmpty(_DpIssueDate))
                    mon.fDpIssueDate = _DpIssueDate.Remove(2, 1).Remove(4, 1);

                if (!string.IsNullOrEmpty(_txbDocNum2))
                    mon.ftxbDocNum2 = _txbDocNum2;
            }

            var result1 = mon.ShowDialog();

            if ((!result1.Value) && (!mon.isClosed))
            {
                StatusText = "Ma'lumot to'liq kiritilmagan.";

                _txbDocNum = mon.ftxbDocNum;
                _DpIssueDate = mon.fDpIssueDate;
                _txbDocNum2 = mon.ftxbDocNum2;

                _logService.Error(string.Format("{0} ", "Введенные данные не верны"));

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
                DispatcherPriority.Input, new DispatcherOperationCallback(delegate (object param)
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

            for (int i = 0; i <= 2; i++)
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

                //var KeyValue = ServerApiController.GetKey(CardNUmber);

                //string kKeyValue = string.Empty;

                //if (!string.IsNullOrEmpty(KeyValue._data._message))
                //{
                //    kKeyValue = KeyValue._data._message;
                //}

                //int res1 = sc.CheckValidityOfKey(Encoding.UTF8.GetBytes(InputString), kKeyValue);

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


                int res1 = sc.CheckValidityOfKey(Encoding.UTF8.GetBytes(InputString), kKeyValue);

                if (res1 != 0)
                {
                    //Try to check for new key
                    kKeyValue = "4557594F4F4D4B554C56494E57454731";
                    res1 = sc.CheckValidityOfKey(Encoding.UTF8.GetBytes(InputString), kKeyValue);
                }

                if (res1 == 0)
                {
                    IsIntermadiate = true;
                    StatusText = "Kiritilgan ma'lumot tekshirilmoqda...";

                    string guvohnomaRaqami = mon.ftxbDocNum2;

                    var reslit = await CheckExpiredDate(guvohnomaRaqami);

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
                else if (res1 == -1)
                {
                    MessageBox.Show("Kartani Riderning ustiga qo'ying. Davom ettirish...", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (res1 == -99)
                {
                    dataCheck = false;
                    break;
                }
            }

            if (!dataCheck)
            {
                StatusText = "Ma'lumot xato kiritilgan";

                _txbDocNum = mon.ftxbDocNum;
                _DpIssueDate = mon.fDpIssueDate;
                _txbDocNum2 = mon.ftxbDocNum2;

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

            IsIntermadiate = true;

            StatusText = "O'qimoqda...";

            //Mouse.OverrideCursor = Cursors.Wait;

            var res = await PfReadCard();

            if (res == 0)
            {
                _txbDocNum = string.Empty;
                _DpIssueDate = string.Empty;
                _txbDocNum2 = string.Empty;
                StatusText = "Ma'lumot to'liq o'qildi.";
            }
            else
            {
                StatusText = "Xatolik yuz berdi...";
            }

            IsIntermadiate = false;

            //Mouse.OverrideCursor = null;

        }

        private string GetStrFromVR(VehicleRegistrationCL responce)
        {
            string resultString = string.Empty;

            try
            {
                using (var stream1 = new MemoryStream())
                {
                    var ser = new DataContractJsonSerializer(typeof(VehicleRegistrationCL));
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

        private void fSaveCard()
        {
            try
            {
                var activationInfo = ServerApiController.SendBackEndActivation(false, readJson);

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


        /// <summary>
        /// 
        /// </summary>
        private string _pinfl;

        public string Pinfl
        {
            get
            {
                return _pinfl;
            }

            set
            {
                _pinfl = value;

                OnPropertyChanged("Pinfl");
            }
        }

        private string _issue_date;

        public string Issue_date
        {
            get
            {
                return _issue_date;
            }

            set
            {
                _issue_date = value;

                OnPropertyChanged("Issue_date");
            }
        }

        private string _ubdd_name;

        public string Ubdd_name
        {
            get
            {
                return _ubdd_name;
            }

            set
            {
                _ubdd_name = value;

                OnPropertyChanged("Ubdd_name");
            }
        }

        private string _issue_region_name ;

        public string Issue_region_name
        {
            get
            {
                return _issue_region_name;
            }

            set
            {
                _issue_region_name = value;

                OnPropertyChanged("Issue_region_name");
            }
        }

        private string _vehicle1_reg_number;

        public string Vehicle1_reg_number
        {
            get
            {
                return _vehicle1_reg_number;
            }

            set
            {
                _vehicle1_reg_number = value;

                OnPropertyChanged("Vehicle1_reg_number");
            }
        }

        private string _vehicle1_model_name;

        public string Vehicle1_model_name
        {
            get
            {
                return _vehicle1_model_name;
            }

            set
            {
                _vehicle1_model_name = value;

                OnPropertyChanged("Vehicle1_model_name");
            }
        }

        private string _vehicle1_color_name;

        public string Vehicle1_color_name
        {
            get
            {
                return _vehicle1_color_name;
            }

            set
            {
                _vehicle1_color_name = value;

                OnPropertyChanged("Vehicle1_color_name");
            }
        }

        private string _vehicle1_vehicle_manufacture_year ;

        public string Vehicle1_vehicle_manufacture_year
        {
            get
            {
                return _vehicle1_vehicle_manufacture_year;
            }

            set
            {
                _vehicle1_vehicle_manufacture_year = value;

                OnPropertyChanged("Vehicle1_vehicle_manufacture_year");
            }
        }

        private string _vehicle1_gross_weight;

        public string Vehicle1_gross_weight
        {
            get
            {
                return _vehicle1_gross_weight;
            }

            set
            {
                _vehicle1_gross_weight = value;

                OnPropertyChanged("Vehicle1_gross_weight");
            }
        }

        private string _vehicle1_curb_weight ;

        public string Vehicle1_curb_weight
        {
            get
            {
                return _vehicle1_curb_weight;
            }

            set
            {
                _vehicle1_curb_weight = value;

                OnPropertyChanged("Vehicle1_curb_weight");
            }
        }

        private string _vehicle1_engine_number ;

        public string Vehicle1_engine_number
        {
            get
            {
                return _vehicle1_engine_number;
            }

            set
            {
                _vehicle1_engine_number = value;

                OnPropertyChanged("Vehicle1_engine_number");
            }
        }

        private string _vehicle1_engine_power;

        public string Vehicle1_engine_power
        {
            get
            {
                return _vehicle1_engine_power;
            }

            set
            {
                _vehicle1_engine_power = value;

                OnPropertyChanged("Vehicle1_engine_power");
            }
        }

        private string _vehicle1_fuel_type ;

        public string Vehicle1_fuel_type
        {
            get
            {
                return _vehicle1_fuel_type;
            }

            set
            {
                _vehicle1_fuel_type = value;

                OnPropertyChanged("Vehicle1_fuel_type");
            }
        }

        private string _vehicle1_number_of_seats;

        public string Vehicle1_number_of_seats
        {
            get
            {
                return _vehicle1_number_of_seats;
            }

            set
            {
                _vehicle1_number_of_seats = value;

                OnPropertyChanged("Vehicle1_number_of_seats");
            }
        }

        private string _vehicle1_number_of_standees ;

        public string Vehicle1_number_of_standees
        {
            get
            {
                return _vehicle1_number_of_standees;
            }

            set
            {
                _vehicle1_number_of_standees = value;

                OnPropertyChanged("Vehicle1_number_of_standees");
            }
        }

        private string _vehicle1_special_marks ;

        public string Vehicle1_special_marks
        {
            get
            {
                return _vehicle1_special_marks;
            }

            set
            {
                _vehicle1_special_marks = value;

                OnPropertyChanged("Vehicle1_special_marks");
            }
        }

        private string _company1_name;

        public string Company1_name
        {
            get
            {
                return _company1_name;
            }

            set
            {
                _company1_name = value;

                OnPropertyChanged("Company1_name");
            }
        }

        private string _company1_inn ;

        public string Company1_inn
        {
            get
            {
                return _company1_inn;
            }

            set
            {
                _company1_inn = value;

                OnPropertyChanged("Company1_inn");
            }
        }

        private string _company1_address2_address ;

        public string Company1_address2_address
        {
            get
            {
                return _company1_address2_address;
            }

            set
            {
                _company1_address2_address = value;

                OnPropertyChanged("Company1_address2_address");
            }
        }

        private string _company1_address2_region_name ;

        public string Company1_address2_region_name
        {
            get
            {
                return _company1_address2_region_name;
            }

            set
            {
                _company1_address2_region_name = value;

                OnPropertyChanged("Company1_address2_region_name");
            }
        }

        private string _company1_address2_rayon_name ;

        public string Company1_address2_rayon_name
        {
            get
            {
                return _company1_address2_rayon_name;
            }

            set
            {
                _company1_address2_rayon_name = value;

                OnPropertyChanged("Company1_address2_rayon_name");
            }
        }

        private int _owner_type;

        public int Owner_type
        {
            get
            {
                return _owner_type;
            }

            set
            {
                _owner_type = value;

                OnPropertyChanged("Owner_type");
            }
        }

        private string _owner_last_name;

        public string Owner_last_name
        {
            get
            {
                return _owner_last_name;
            }

            set
            {
                _owner_last_name = value;

                OnPropertyChanged("Owner_last_name");
            }
        }

        private string _owner_first_name;

        public string Owner_first_name
        {
            get
            {
                return _owner_first_name;
            }

            set
            {
                _owner_first_name = value;

                OnPropertyChanged("Owner_first_name");
            }
        }

        private string _owner_middle_name;

        public string Owner_middle_name
        {
            get
            {
                return _owner_middle_name;
            }

            set
            {
                _owner_middle_name = value;

                OnPropertyChanged("Owner_middle_name");
            }
        }

        private string _mark_name;

        public string Mark_name
        {
            get
            {
                return _mark_name;
            }

            set
            {
                _mark_name = value;

                OnPropertyChanged("Mark_name");
            }
        }

        private string _vehicle_type;

        public string Vehicle_type
        {
            get
            {
                return _vehicle_type;
            }

            set
            {
                _vehicle_type = value;

                OnPropertyChanged("Vehicle_type");
            }
        }

        private string _vehicle_identification_number_kuzov;

        public string Vehicle_identification_number_kuzov
        {
            get
            {
                return _vehicle_identification_number_kuzov;
            }

            set
            {
                _vehicle_identification_number_kuzov = value;

                OnPropertyChanged("Vehicle_identification_number_kuzov");
            }
        }

        private string _vehicle_identification_number_shassi;

        public string Vehicle_identification_number_shassi
        {
            get
            {
                return _vehicle_identification_number_shassi;
            }

            set
            {
                _vehicle_identification_number_shassi = value;

                OnPropertyChanged("Vehicle_identification_number_shassi");
            }
        }

        private string _engine_measurement;

        public string Engine_measurement
        {
            get
            {
                return _engine_measurement;
            }

            set
            {
                _engine_measurement = value;

                OnPropertyChanged("Engine_measurement");
            }
        }

        private string _license_number;

        public string License_number
        {
            get
            {
                return _license_number;
            }

            set
            {
                _license_number = value;

                OnPropertyChanged("License_number");
            }
        }

        private string _expire_date;

        public string Expire_date
        {
            get
            {
                return _expire_date;
            }

            set
            {
                _expire_date = value;

                OnPropertyChanged("Expire_date");
            }
        }


        // "mark_name": "Chevrolet",
        //    "type": "Sedan",
        //"vehicle_identification_number_kuzov": "11",
        //"vehicle_identification_number_shassi": "333",



        //"engine_measurement": "ot kuchi",
        //"license_number": "AAA000006",
        //"expire_date": "20270917"

    }
}
