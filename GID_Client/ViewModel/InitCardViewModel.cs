using CardAPILib.CardAPI;
using Epigov.Log;
using GID_Client.ServerApi;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Media;

namespace GID_Client.ViewModel
{

    internal class InitCardViewModel: ViewModelBase
    {
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

        /**************************************************/
        //////////////////Global Variables//////////////////
        /**************************************************/
        IntPtr hContext;                                        //Context Handle value
        String readerName;                                      //Global Reader Variable
        int retval;                                             //Return Value
        uint dwscope;                                           //Scope of the resource manager context
        Boolean IsAuthenticated;                                //Boolean variable to check the authentication
        Boolean release_flag;                                   //Flag to release 
        IntPtr hCard;                                           //Card handle
        IntPtr protocol;                                        //Protocol used currently
        Byte[] ATR = new Byte[33];                              //Array stores Card ATR
        int card_Type;                                          //Stores the card type
        Byte[] sendBuffer = new Byte[255];                        //Send Buffer in SCardTransmit
                                                                  //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x16)]
                                                                  // public byte receiveBuffer;
        Byte[] receiveBuffer = new Byte[255];                   //Receive Buffer in SCardTransmit
        int sendbufferlen, receivebufferlen;                    //Send and Receive Buffer length in SCardTransmit
        Byte bcla;                                             //Class Byte
        Byte bins;                                             //Instruction Byte
        Byte bp1;                                              //Parameter Byte P1
        Byte bp2;                                              //Parameter Byte P2
        Byte len;                                              //Lc/Le Byte
        Byte[] data = new Byte[255];                            //Data Bytes
        HiDWinscard.SCARD_READERSTATE ReaderState;              //Object of SCARD_READERSTATE
        int value_Timeout;                                      //The maximum amount of time to wait for an action
        uint ReaderCount;                                       //Count for number of readers
        String ReaderList;                                      //List Of Reader
        System.Object sender1;                                  //Object of the Sender
        System.Windows.RoutedEventArgs e1;                      //Object of the Event
        Byte currentBlock;                                      //Stores the current block selected
        //String keych;                                           //Stores the string in key textbox
        int discarded;                                          //Stores the number of discarded character
        public delegate void DelegateTimer();                   //delegate of the Timer
        private System.Timers.Timer timer;                      //Object of the Timer
        public bool bTxtWrongInputChange;                       //Variable to check the wrong input in key textbox. Used in text change event
        bool read_pressed;                                      //flag to check read pressed
        string myReader;

        private CardApiController _controller; 

        private void Connect2Card()
        {
            {
                retval = HID.SCardConnect(hContext, myReader, HiDWinscard.SCARD_SHARE_SHARED, HiDWinscard.SCARD_PROTOCOL_T1,
                                 ref hCard, ref protocol
                                  );       //Command to connect the card ,protocol T=1
            }

            ReaderState.RdrName = myReader;
            ReaderState.RdrCurrState = HiDWinscard.SCARD_STATE_UNAWARE;
            ReaderState.RdrEventState = 0;
            ReaderState.UserData = "Mifare Card";
            value_Timeout = 0;
            ReaderCount = 1;

            if (retval == 0)
                retval = HID.SCardGetStatusChange(hContext, value_Timeout, ref ReaderState, ReaderCount);

            ATR_UID();
        }

        private void InitCard()
        {
            uint pcchReaders = 0;
            int nullindex = -1;
            char nullchar = (char)0;
            dwscope = 2;

            // Establish context.
            retval = HID.SCardEstablishContext(dwscope, IntPtr.Zero, IntPtr.Zero, out hContext);
            retval = HID.SCardListReaders(hContext, null, null, ref pcchReaders);
            byte[] mszReaders = new byte[pcchReaders];

            // Fill readers buffer with second call.
            retval = HID.SCardListReaders(hContext, null, mszReaders, ref pcchReaders);

            // Populate List with readers.
            string currbuff = Encoding.ASCII.GetString(mszReaders);
            ReaderList = currbuff;
            int len = (int)pcchReaders;

            if (len > 0)
            {
                while (currbuff[0] != nullchar)
                {
                    nullindex = currbuff.IndexOf(nullchar);   // Get null end character.
                    string reader = currbuff.Substring(0, nullindex);

                    if (reader.Contains("CL"))
                    {
                        myReader = reader;
                        MyReaderName = myReader;
                    }
                    //selectreadercombobox.Items.Add(reader);
                    len = len - (reader.Length + 1);
                    currbuff = currbuff.Substring(nullindex + 1, len);
                }
            }

            try
            {
                Connect2Card();
            }
            catch(Exception ex)
            {

            }

            _controller = new CardApiController(true);

            timerWorkItem();
            // Creating a timer with a ten second interval.
            timer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer.
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;
        }

        public InitCardViewModel()
        {
            _logService = new FileLogService(typeof(InitCardViewModel));

            OpenCardDR = new RelayCommand(_ => fOpenCard());

            OpenCardVR = new RelayCommand(_ => fOpenCardVR());

            SaveCardInfo = new RelayCommand(_ => fSaveCardInfo());

            GetCertificate = new RelayCommand(_ => fGetcertificate());

            Apply2Card = new RelayCommand(_ => fApply2Card());

            InitCard();
        }

        private void ATR_UID()
        {
            HiDWinscard.SCARD_IO_REQUEST sioreq;
            sioreq.dwProtocol = 0x2;
            sioreq.cbPciLength = 8;
            HiDWinscard.SCARD_IO_REQUEST rioreq;
            rioreq.cbPciLength = 8;
            rioreq.dwProtocol = 0x2;

            String uid_temp;
            String atr_temp;
            String s;
            atr_temp = "";
            uid_temp = "";
            s = "";
            StringBuilder hex = new StringBuilder(ReaderState.ATRValue.Length * 2);
            foreach (byte b in ReaderState.ATRValue)
                hex.AppendFormat("{0:X2}", b);
            atr_temp = hex.ToString();
            atr_temp = atr_temp.Substring(0, ((int)(ReaderState.ATRLength)) * 2);

            

            for (int k = 0; k <= ((ReaderState.ATRLength) * 2 - 1); k += 2)
            {
                s = s + atr_temp.Substring(k, 2) + " ";
            }

            atr_temp = s;

            bcla = 0xFF;
            bins = 0xCA;
            bp1 = 0x0;
            bp2 = 0x0;
            len = 0x0;
            sendBuffer[0] = bcla;
            sendBuffer[1] = bins;
            sendBuffer[2] = bp1;
            sendBuffer[3] = bp2;
            sendBuffer[4] = len;
            sendbufferlen = 0x5;
            receivebufferlen = 255;
            retval = HID.SCardTransmit(hCard, ref sioreq, sendBuffer, sendbufferlen, ref rioreq, receiveBuffer, ref receivebufferlen);
            if (retval == 0)
            {
                if ((receiveBuffer[receivebufferlen - 2] == 0x90) && (receiveBuffer[receivebufferlen - 1] == 0))
                {
                    StringBuilder hex1 = new StringBuilder((receivebufferlen - 2) * 2);
                    foreach (byte b in receiveBuffer)
                        hex1.AppendFormat("{0:X2}", b);
                    uid_temp = hex1.ToString();
                    uid_temp = uid_temp.Substring(0, ((int)(receivebufferlen - 2)) * 2);

                    MyUIDofCard = uid_temp;
                }
                else
                {
                    ;
                }
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //this.Dispatcher.Invoke(DispatcherPriority.Normal, new DelegateTimer(timerWorkItem));
            timerWorkItem();
        }

        private bool timerInProc;

        private void timerWorkItem()
        {
            if (timerInProc)
            {
                return;
            }
            else
            {
                timerInProc = true;
            }

            ReaderState.RdrName = myReader;
            ReaderState.RdrCurrState = HiDWinscard.SCARD_STATE_UNAWARE;
            ReaderState.RdrEventState = 0;
            ReaderState.UserData = "Mifare Card";
            value_Timeout = 0;
            ReaderCount = 1;

            if (ReaderList == "")
                {
                    MycardStatus = "SmartCard Removed";
                    BackgroundTb = Brushes.Red;
                    ForegroundTb = Brushes.White;
                }
                else
                {
                    retval = HID.SCardGetStatusChange(hContext, value_Timeout, ref ReaderState, ReaderCount);
                    if ((ReaderState.ATRLength == 0) || (retval != 0))
                    {
                        MycardStatus = "SmartCard Removed";
                        BackgroundTb = Brushes.Red;
                        ForegroundTb = Brushes.White;
                        //DisconnectButton_Click(sender1, e1);
                        //mifarecardtypeLabel.Content = "";
                        MyUIDofCard = "";
                    }
                    else
                    {
                        MycardStatus = "SmartCard Inserted";
                        BackgroundTb = Brushes.GreenYellow;
                        ForegroundTb = Brushes.Black;
                        Connect2Card();
                    }
                }

            timerInProc = false;
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

        private void fOpenCard()
        {
            _logService.Info("Open Card Driving Licence");

            try
            {
                _controller.OpenCardDR();

                fSaveCardInfo();

                fGetcertificate();

                fApply2Card();
            }
            catch(Exception ex)
            {
                StatusText = "Ошибка пре персонализации. Посмотрите логи для деталей.";

                _logService.Error(ex.Message);

                return;
            }

            StatusText = "Карта удачно открыта";

            return;
        }

        private void fOpenCardVR()
        {
            _logService.Info("Open Card Vehicle Registration");

            try
            {
                _controller.OpenCardVR();

                fSaveCardInfo();

                fGetcertificate();

                fApply2Card();
            }
            catch (Exception ex)
            {
                StatusText = "Ошибка пре персонализации. Посмотрите логи для деталей.";

                _logService.Error(ex.Message);
            }
        }

        private void fSaveCardInfo()
        {
            try
            {
                getResponceCardInsert obj = ServerApiController.RegisterCard(MyUIDofCard);

                PrivateKey = obj._data.PrivateKey;

                PublicKey = obj._data.PublicKey;

                if (obj._status.ToLower().Contains("success"))
                {
                    CertificateButton = true;

                    StatusText = obj._status;
                }
                else
                {
                    StatusText = obj._data._message;
                }
            }
            catch(Exception ex)
            {
                StatusText = "Ошибка при сохранении карты. Ошибка сервера";

                _logService.Error(ex.Message);
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

        private void fGetcertificate()
        {
            try
            {
                getResponceCardInsert obj = ServerApiController.GetCertificate(MyUIDofCard);

                Certificate = obj._data._certificate;

                if (obj._status.ToLower().Contains("success"))
                {
                    StatusText = obj._status;
                }
                else
                {
                    StatusText = obj._data._message;
                }
            }
            catch(Exception ex)
            {
                StatusText = "Ошибка при получении сертификата. Ошибка сервера";

                _logService.Error(ex.Message);
            }

        }

        private Task<int> PkiSave2Card()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                
                try
                {
                    _controller.SaveCertificate(Certificate);
                }
                catch (Exception ex)
                {
                    StatusText = ex.Message;
                }

                return 0;
            });

            return resultTask;
        }

        private async void fApply2Card()
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
    }
}
