using CardAPILib.CardAPI;
using CardAPILib.InterfaceCL;
using Epigov.Log;
using GemCard;
using GID_Client.ServerApi;
using Iso18013Lib;
using SmartCardApi.SmartCardReader;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GID_Client.ViewModel
{
    internal class TexPasportViewModel: ViewModelBase
    {
        private readonly ILogService _logService;

        private CardApiController _controller;

        public RelayCommand ReadCard { get; private set; }

        public RelayCommand SaveCard { get; private set; }

        private string readJson { get; set; }

        public TexPasportViewModel()
        {
            _logService = new FileLogService(typeof(InitCardViewModel));

            ReadCard = new RelayCommand(_ => fReadCard());

            SaveCard = new RelayCommand(_ => fSaveCard());

            try
            {
                _controller = new CardApiController(false);
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

                        //if (result != 0)
                        //{
                        //    StatusText = "Ошибка при прочтении карты";

                        //    _logService.Error(string.Format("{0} {1}", "Ошибка при прочтении карты", result));

                        //    return -1;
                        //}

                        VehicleRegistration vl = new VehicleRegistration("");

                        var vll = vl.ParseReadMaterial(Vr);

                        vl._vehicleRegistration._card_number = GetUUid();

                        Pinfl = vl._vehicleRegistration._company._pinfl;

                        Issue_date = vl._vehicleRegistration._issue_date;

                        Ubdd_name = vl._vehicleRegistration._ubdd_name;

                        Issue_region_name = vl._vehicleRegistration._license_number;

                        Vehicle1_reg_number = vl._vehicleRegistration._vehicle._reg_number;

                        Vehicle1_model_name = vl._vehicleRegistration._vehicle._model_name;

                        Vehicle1_color_name = vl._vehicleRegistration._vehicle._color_name;

                        Vehicle1_vehicle_manufacture_year = vl._vehicleRegistration._vehicle._vehicle_manufacture_year;

                        Vehicle1_gross_weight = vl._vehicleRegistration._vehicle._gross_weight.ToString();

                        Vehicle1_curb_weight = vl._vehicleRegistration._vehicle._curb_weight.ToString();

                        Vehicle1_engine_number = vl._vehicleRegistration._vehicle._engine_number;

                        Vehicle1_engine_power = vl._vehicleRegistration._vehicle._engine_power;

                        Vehicle1_fuel_type = vl._vehicleRegistration._vehicle._fuel_type;

                        Vehicle1_number_of_seats = vl._vehicleRegistration._vehicle._number_of_seats.ToString();

                        Vehicle1_number_of_standees = vl._vehicleRegistration._vehicle._number_of_standees;

                        Vehicle1_special_marks = vl._vehicleRegistration._vehicle._special_marks;

                        Company1_name = vl._vehicleRegistration._company._name;

                        Company1_inn = vl._vehicleRegistration._company._inn;

                        Company1_address2_address = vl._vehicleRegistration._company._address._address;

                        Company1_address2_region_name = vl._vehicleRegistration._company._address._region_name;

                        Company1_address2_rayon_name = vl._vehicleRegistration._company._address._rayon_name;

                        vl._vehicleRegistration._company._address._address = string.Empty;

                        vl._vehicleRegistration._company._address._region_name = string.Empty;

                        vl._vehicleRegistration._company._address._rayon_name = string.Empty;

                        Owner_type = vl._vehicleRegistration._company._type;

                        Owner_last_name = vl._vehicleRegistration._company._last_name;

                        Owner_first_name = vl._vehicleRegistration._company._first_name;

                        Owner_middle_name = vl._vehicleRegistration._company._middle_name;

                        Mark_name = vl._vehicleRegistration._vehicle._mark_name;

                        Vehicle_type = vl._vehicleRegistration._vehicle._type;

                        Vehicle_identification_number_kuzov = vl._vehicleRegistration._vehicle._vehicle_identification_number_kuzov;

                        Vehicle_identification_number_shassi = vl._vehicleRegistration._vehicle._vehicle_identification_number_shassi;

                        Engine_measurement = vl._vehicleRegistration._vehicle._engine_measurement;

                        License_number = vl._vehicleRegistration._license_number;

                        Expire_date = vl._vehicleRegistration._expire_date;

                        readJson = GetStrFromVR(vl._vehicleRegistration);

                        if (Properties.Settings.Default.BackEndMode.Equals("1"))
                        {

                            var activationInfo = ServerApiController.SendBackEndActivation(false, readJson);//06.10.2017

                            StatusText = activationInfo._data._message;//06.10.2017
                        }

                    }
                    catch (Exception ex)
                    {

                    }

                }
                catch (Exception ex)
                {
                    StatusText = ex.Message;
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

            if (!result1.Value)
            {
                StatusText = "Ma'lumot to'liq kiritilmagan.";

                _txbDocNum = mon.ftxbDocNum;
                _DpIssueDate = mon.fDpIssueDate;
                _txbDocNum2 = mon.ftxbDocNum2;

                _logService.Error(string.Format("{0} ", "Введенные данные не верны"));

                var result = MessageBox.Show("Ma'lumot to'liq kiritilmagan. Yana harakat qilib ko'rasizmi?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    fReadCard();
                }

                return;
            }

            InputString = mon.InpetString;

            var sc = new SecureMessaging();

            bool dataCheck = false;

            for (int i = 0; i <= 2; i++)
            {
                int res1 = sc.CheckValidityOfKey(Encoding.UTF8.GetBytes(InputString));

                if (res1 == 0)
                {
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
                    fReadCard();
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
