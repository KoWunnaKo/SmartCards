using CardAPILib.CardAPI;
using CardAPILib.InterfaceCL;
using Epigov.Log;
using GemCard;
using GID_Client.ServerApi;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GID_Client.ViewModel
{
    internal class IDL_ViewModel : ViewModelBase
    {
        #region Properties
        private readonly ILogService _logService;

        public RelayCommand OpenCardDR { get; private set; }

        public RelayCommand OpenCardVR { get; private set; }

        public RelayCommand SaveCardInfo { get; private set; }

        public RelayCommand GetCertificate { get; private set; }

        public RelayCommand Apply2Card { get; private set; }

        private Brush backgroundTb;

        public Brush BackgroundTb
        {
            get
            {
                return backgroundTb;
            }

            set
            {
                backgroundTb = value;

                OnPropertyChanged("BackgroundTb");
            }
        }

        private Brush foregroundTb;

        public Brush ForegroundTb
        {
            get
            {
                return foregroundTb;
            }

            set
            {
                foregroundTb = value;

                OnPropertyChanged("ForegroundTb");
            }
        }

        private string myReaderName;

        public string MyReaderName
        {
            get
            {
                return myReaderName;
            }

            set
            {
                myReaderName = value;

                OnPropertyChanged("MyReaderName");
            }
        }

        private string myUIDofCard;

        public string MyUIDofCard
        {
            get
            {
                return myUIDofCard;
            }
            set
            {
                myUIDofCard = value;

                OnPropertyChanged("MyUIDofCard");
            }
        }

        private string mycardStatus;

        public string MycardStatus
        {
            get
            {
                return mycardStatus;
            }

            set
            {
                mycardStatus = value;

                OnPropertyChanged("MycardStatus");
            }
        }


        private string _privateKey;

        public string PrivateKey
        {
            get
            {
                return _privateKey;
            }

            set
            {
                _privateKey = value;

                OnPropertyChanged("PrivateKey");
            }
        }

        private string _publicKey;

        public string PublicKey
        {
            get
            {
                return _publicKey;
            }

            set
            {
                _publicKey = value;

                OnPropertyChanged("PublicKey");
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

        private string _logsList;

        public string LogsList
        {
            get
            {
                return _logsList;
            }

            set
            {
                _logsList = value;

                OnPropertyChanged("LogsList");
            }
        }

        #endregion

        private CardNative _card;

        private string _ReaderInfo;

        public IDL_ViewModel(CardFactoryMode mode)
        {
            _logService = new FileLogService(typeof(IDL_ViewModel));

            OpenCardDR = new RelayCommand(_ => fOpenCard());

            SaveCardInfo = new RelayCommand(_ => fSaveCardInfo());

            GetCertificate = new RelayCommand(_ => fGetcertificate());

            Apply2Card = new RelayCommand(_ => fApply2Card());

            OpenCardVR = new RelayCommand(_ => fOpenCardVR());

            try
            {
                _card = new CardNative();

                _controller = new CardApiController(_card ,false);

                _currentMode = mode;

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
                    MessageBox.Show("Отсуствует ридер или не установленны драйвера!!!", "Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                    throw new ApplicationException("Отсуствует ридер или не установленны драйвера!!! Проблемы с устройством");
                }

                //Old version
                //_card_OnCardRemoved(SpecReaders[0]);
                //_card.StartCardEvents(SpecReaders[0]);

                //New version 22.01.2018
                foreach(var reader in SpecReaders)
                {
                    _card_OnCardRemoved(reader);
                    _card.StartCardEventsMulti(reader);
                }

            }
            catch
            {
                MessageBox.Show("Отсуствует ридер или не установленны драйвера!!!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new ApplicationException("Отсуствует ридер или не установленны драйвера!!! Проблемы с устройством");
            }
        }

        private int fOpenCardVR()
        {
            _logService.Info("Open Card Vehicle Registration");
            int count = 0;

            while (true)
            {
                try
                {
                    Write2Log("Идет пре-персонализация...");
                    MycardStatus = "Идет пре-персонализация...";
                    BackgroundTb = Brushes.FloralWhite;
                    ForegroundTb = Brushes.Black;

                    if (_controller.OpenCardVR() != 0)
                    {
                        MycardStatus = "Ошибка пре персонализации. Посмотрите логи для деталей.";
                        Write2Log("Ошибка пре персонализации. Посмотрите логи для деталей.");
                        BackgroundTb = Brushes.OrangeRed;
                        ForegroundTb = Brushes.Black;
                    }
                    else
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    _logService.Error(ex.ToString());
                }

                count++;

                if (count >= 10)
                {
                    StatusText = "Бракованная карта";
                    Write2Log("Бракованная карта. Посмотрите логи для деталей.");
                    MycardStatus = "Бракованная карта. Посмотрите логи для деталей.";
                    BackgroundTb = Brushes.OrangeRed;
                    ForegroundTb = Brushes.Black;
                    return -1;
                }
            }

            StatusText = "Карта удачно открыта";
            Write2Log("Карта удачно открыта");

            return 0;
        }

        private void _card_OnCardRemoved(string reader)
        {
            _logService.Warn("Карта удалена или попробуйте переставить ее");

            MycardStatus = "Карта удалена или попробуйте переставить ее";
            BackgroundTb = Brushes.Red;
            ForegroundTb = Brushes.White;
            MyReaderName = reader;
            MyUIDofCard = string.Empty;
            PrivateKey = string.Empty;
            PublicKey = string.Empty;
            Certificate = string.Empty;
            
        }

        private void Write2Log(string text)
        {
            LogsList += DateTime.Now.ToString("hh:mm:ss") + " - " + text +   Environment.NewLine;
            _logService.Warn(LogsList);
        }

        private void _card_OnCardInserted(string reader)
        {
            try
            {
                Write2Log("*****************************************");
                Write2Log("*****************************************");
                Write2Log("*****************************************");
                Write2Log("Карта вставленна в ридер");
                MycardStatus = "Карта вставленна в ридер";
                BackgroundTb = Brushes.GreenYellow;
                ForegroundTb = Brushes.Black;
                MyReaderName = reader;

                try
                {
                    Write2Log("Начинаем читать уникальный ИД...");

                    var AtrBytes = CallApduCommandLeData(0xFF, 0xCA, 0x00, 0x00, null, 5);

                    StringBuilder hex1 = new StringBuilder((5) * 2);
                    foreach (byte b in AtrBytes)
                        hex1.AppendFormat("{0:X2}", b);
                    var uid_temp = hex1.ToString();

                    MyUIDofCard = uid_temp;
                    Write2Log(MyUIDofCard);
                }
                catch(Exception ex)
                {
                    StatusText = "Бракованная карта";
                    Write2Log("Бракованная карта. Посмотрите логи для деталей.");
                    MycardStatus = "Бракованная карта. Посмотрите логи для деталей.";
                    BackgroundTb = Brushes.OrangeRed;
                    ForegroundTb = Brushes.Black;

                    _logService.Error(ex.Message);

                    throw new ApplicationException("UUID Take Error");
                }

                try
                {
                    Write2Log("Начинаем делать Цех");

                    TsexProcRouter();
                }
                catch(Exception ex)
                {
                    StatusText = "Бракованная карта";
                    Write2Log("Бракованная карта. Посмотрите логи для деталей.");
                    MycardStatus = "Бракованная карта. Посмотрите логи для деталей.";
                    BackgroundTb = Brushes.OrangeRed;
                    ForegroundTb = Brushes.Black;

                    _logService.Error(ex.Message);


                    throw new ApplicationException("TSEX Error");
                }

            }
            catch(ApplicationException ex)
            {
                StatusText = "Бракованная карта";
                Write2Log("Бракованная карта. Посмотрите логи для деталей.");
                MycardStatus = "Бракованная карта. Посмотрите логи для деталей.";
                BackgroundTb = Brushes.OrangeRed;
                ForegroundTb = Brushes.Black;

                _logService.Error(ex.Message);
            }
            catch(Exception ex)
            {
                Write2Log(ex.Message);
                Write2Log("Ошибка в карте или попробуйте заного");
                MycardStatus = "Ошибка в карте или попробуйте заного";
                BackgroundTb = Brushes.Red;
                ForegroundTb = Brushes.Black;
            }
            finally
            {
                try
                {
                    _card.Disconnect(DISCONNECT.Reset);
                }
                catch
                {
                    //
                }
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


        private int CallApduCommandLe(byte Cla, byte Ins, byte P1, byte P2, byte[] data, uint _recieveLength)
        {
            APDUCommand apduSize5 = new APDUCommand(Cla, Ins, P1, P2, null, 0);

            APDUParam apduParam5 = new APDUParam();

            apduParam5.Data = data;

            apduSize5.Update(apduParam5);

            _logService.Info(apduSize5.ToString());

            Connect2Card();

            apduResp = _card.TransmitLe(apduSize5, _recieveLength);
            if (apduResp.Status != SC_OK && apduResp.SW1 != SC_PENDING)
            {

                _logService.Info(apduResp.ToString());

                return -1;
            }

            _logService.Info(apduResp.Status.ToString());
            _logService.Info(apduResp.ToString());

            return 0;
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

                        _ReaderInfo = readerInfo;

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

                Write2Log("Невозможно присоединиться к карте - " + ex.Message);
            }

            return 4;
        }

        private CardApiController _controller;

        /// <summary>
        /// Old version
        /// </summary>
        /// <returns></returns>
        private int fOpenCard()
        {
            _logService.Info("Open Card Driving Licence");
            int count = 0;

            while (true)
            {
                try
                {
                    Write2Log("Идет пре-персонализация...");
                    MycardStatus = "Идет пре-персонализация...";
                    BackgroundTb = Brushes.FloralWhite;
                    ForegroundTb = Brushes.Black;

                    if (_controller.OpenCardDR() != 0)
                    {
                        MycardStatus = "Ошибка пре персонализации. Посмотрите логи для деталей.";
                        Write2Log("Ошибка пре персонализации. Посмотрите логи для деталей.");
                        BackgroundTb = Brushes.OrangeRed;
                        ForegroundTb = Brushes.Black;
                    }
                    else
                    {
                        break;
                    }
                }
                catch(Exception ex)
                {
                    _logService.Error(ex.ToString());
                }

                count++;

                if (count >= 10)
                {
                    StatusText = "Бракованная карта";
                    Write2Log("Бракованная карта. Посмотрите логи для деталей.");
                    MycardStatus = "Бракованная карта. Посмотрите логи для деталей.";
                    BackgroundTb = Brushes.OrangeRed;
                    ForegroundTb = Brushes.Black;
                    return -1;
                }
            }

            StatusText = "Карта удачно открыта";
            Write2Log("Карта удачно открыта");

            return 0;
        }

        private Task PfSaveCardInfo()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                try
                {
                    MycardStatus = "Сохранение карты...";
                    Write2Log("Сохранение карты...");
                    BackgroundTb = Brushes.PaleGoldenrod;
                    ForegroundTb = Brushes.Black;

                    getResponceCardInsert obj = ServerApiController.RegisterCard(MyUIDofCard);

                    PrivateKey = obj._data.PrivateKey;

                    PublicKey = obj._data.PublicKey;

                    if (obj._status.ToLower().Contains("success"))
                    {
                        CertificateButton = true;

                        StatusText = obj._status;
                        MycardStatus = "Сохранение карты удачно завершилась";
                        Write2Log("Сохранение карты удачно завершилась");
                        BackgroundTb = Brushes.GreenYellow;
                        ForegroundTb = Brushes.Black;
                    }
                    else
                    {
                        Write2Log(obj._data._message);
                        StatusText = obj._data._message;
                        MycardStatus = obj._data._message;
                    }
                }
                catch (Exception ex)
                {
                    Write2Log("Ошибка при сохранении карты. Ошибка сервера");
                    StatusText = "Ошибка при сохранении карты. Ошибка сервера";
                    MycardStatus = "Ошибка при сохранении карты. Ошибка сервера";
                    BackgroundTb = Brushes.OrangeRed;
                    ForegroundTb = Brushes.Black;

                    _logService.Error(ex.Message);
                }
            });

            return resultTask;
        }

        private async Task fSaveCardInfo()
        {
            try
            {
                await PfSaveCardInfo();
            }
            catch (Exception ex)
            {
                StatusText = "Ошибка при сохранении карты. Ошибка сервера";
                Write2Log("Ошибка при сохранении карты. Ошибка сервера");
                MycardStatus = "Ошибка при сохранении карты. Ошибка сервера";
                BackgroundTb = Brushes.OrangeRed;
                ForegroundTb = Brushes.Black;

                _logService.Error(ex.Message);
            }
            finally
            {

            }

        }

        private bool _certificateButton;

        public bool CertificateButton
        {
            get
            {
                return _certificateButton;
            }

            set
            {
                _certificateButton = value;

                OnPropertyChanged("CertificateButton");
            }
        }

        private Task PGetcertificate()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                try
                {
                    MycardStatus = "Получение сертификата...";
                    Write2Log("Получение сертификата...");
                    BackgroundTb = Brushes.Gold;
                    ForegroundTb = Brushes.Black;

                    getResponceCardInsert obj = ServerApiController.GetCertificate(MyUIDofCard);

                    Certificate = obj._data._certificate;

                    if (obj._status.ToLower().Contains("success"))
                    {
                        StatusText = obj._status;
                        BackgroundTb = Brushes.GreenYellow;
                        ForegroundTb = Brushes.Black;
                        MycardStatus = "Получение сертификата удачно завершилась";
                        Write2Log("Получение сертификата удачно завершилась");
                        StatusText = "Удачная запись на карту!!!";
                    }
                    else
                    {
                        _logService.Warn("Получение сертификата завершилась с ошибкой");
                        StatusText = obj._data._message;
                        BackgroundTb = Brushes.GreenYellow;
                        ForegroundTb = Brushes.Black;
                        MycardStatus = "Получение сертификата удачно завершилась";
                        Write2Log("Получение сертификата удачно завершилась");
                        StatusText = "Удачная запись на карту!!!";
                    }
                }
                catch (Exception ex)
                {
                    _logService.Warn(ex.ToString());
                    MycardStatus = "Получение сертификата удачно завершилась";
                    Write2Log("Получение сертификата удачно завершилась");
                    Write2Log("Удачная запись на карту!!!");
                    StatusText = "Удачная запись на карту!!!";
                    MycardStatus = "Удачная запись на карту!!!";
                    BackgroundTb = Brushes.GreenYellow;
                    ForegroundTb = Brushes.Black;

                    _logService.Error(ex.Message);
                }
            });

            return resultTask;
        }

        private async Task fGetcertificate()
        {
            try
            {
                await PGetcertificate();
            }
            catch (Exception ex)
            {
                StatusText = "Ошибка при получении сертификата. Ошибка сервера";
                MycardStatus = "Ошибка при получении сертификата. Ошибка сервера";
                //BackgroundTb = Brushes.OrangeRed;
                //ForegroundTb = Brushes.Black;

                _logService.Error(ex.Message);
            }

        }

        private Task<int> PkiSave2Card()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {

                try
                {
                    //_controller.SaveCertificate(Certificate);
                }
                catch (Exception ex)
                {
                    StatusText = ex.Message;
                }

                return 0;
            });

            return resultTask;
        }

        private async Task fApply2Card()
        {
            if (string.IsNullOrEmpty(MyUIDofCard))
            {
                StatusText = "Вставьте карту...";

                return;
            }

            IsIntermadiate = true;

            StatusText = "Загрузка...";

            var res = await PkiSave2Card();

            if (res == 0)
            {
                StatusText = "Удачная запись на карту!!!";
            }
            else
            {
                StatusText = "Error...";
            }

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

        private CardFactoryMode _currentMode;

        private async void TsexProcRouter()
        {
            int ret = -1;
            int counter = 0;

            while (true)
            {
                try
                {
                    Write2Log("Цех попытка " + counter + 1);

                    ret = CheckCardAlreadyOpen();

                    break;
                }
                catch
                {
                    counter++;

                    if (counter >= 10)
                    {
                        break;
                    }

                    continue;
                }
            }

            if (ret == -1)
            {
                Write2Log("Impossible to check card for readyness");
            }

            if (CheckCardAlreadyOpen() != 0)
            {
                if (_currentMode == CardFactoryMode.DrivingLicence)
                {
                    if (fOpenCard() != 0)
                    {
                        Write2Log("Ошибка в цеху");
                        return;
                    }
                }
                else
                {
                    if (fOpenCardVR() != 0)
                    {
                        Write2Log("Ошибка в цеху");
                        return;
                    }
                }
            }
            else
            {
                await fSaveCardInfo();
                Write2Log("Карта уже открыта");
                StatusText = "Карта уже открыта";
                BackgroundTb = Brushes.GreenYellow;
                ForegroundTb = Brushes.Black;
                MycardStatus = "Карта уже открыта";

                return;
            }

            await fSaveCardInfo();

            await fGetcertificate();

            await fApply2Card();
        }

        private int CheckCardAlreadyOpen()
        {
            _logService.Info("CheckCardAlreadyOpen Function Start");

            Connect2Card();

            _logService.Info("External Authentificate... ");

            var extr = new ExternalAuthentificate(_card);

            for (int i = 0; i <= 10; i++)
            {
                if (extr.ExternalAuth() != 0)
                {
                    _logService.Error("External Authentificate failed");
                    _logService.Info("=================================");
                    _logService.Info("Card is not opened");
                    _logService.Info("=================================");
                    
                }
                else
                {
                    break;
                }
            }

            _logService.Info("Select Applet... ");

            //SendApdu: ISO7816.SelectFileByDFName
            if (CallApduCommandLe(0x00, 0xA4, 0x04, 0x00, new byte[07] { 0xA0, 0x00, 0x00, 0x02, 0x48, 0x02, 0x00 }, 28) != 0)
            {
                _logService.Error("Select Applet failed ");
                _logService.Info("=================================");
                _logService.Info("Card is not opened or Applet not Installed");
                _logService.Info("=================================");
                return -2;
            }

            _logService.Info("*********************************");
            _logService.Info("Card Is Opened!!!");
            _logService.Info("*********************************");
            return 0;
        }
    }
}
