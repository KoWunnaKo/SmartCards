using CardAPILib.CardAPI;
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

    //Class for Constants
    public class HiDWinscard
    {
        // Context Scope

        public const int SCARD_STATE_UNAWARE = 0x0;

        //The application is unaware about the curent state, This value results in an immediate return
        //from state transition monitoring services. This is represented by all bits set to zero

        public const int SCARD_SHARE_SHARED = 2;

        // Application will share this card with other 
        // applications.

        //   Disposition

        public const int SCARD_UNPOWER_CARD = 2; // Power down the card on close

        //   PROTOCOL

        public const int SCARD_PROTOCOL_T0 = 0x1;                  // T=0 is the active protocol.
        public const int SCARD_PROTOCOL_T1 = 0x2;                  // T=1 is the active protocol.
        public const int SCARD_PROTOCOL_UNDEFINED = 0x0;

        //IO Request Control
        public struct SCARD_IO_REQUEST
        {
            public int dwProtocol;
            public int cbPciLength;
        }


        //Reader State

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SCARD_READERSTATE
        {
            public string RdrName;
            public string UserData;
            public uint RdrCurrState;
            public uint RdrEventState;
            public uint ATRLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x24, ArraySubType = UnmanagedType.U1)]
            public byte[] ATRValue;
        }
        //Card Type
        public const int card_Type_Mifare_1K = 1;
        public const int card_Type_Mifare_4K = 2;

    }

    //**************************************************************************
    //class for Hexidecimal to Byte and Byte to Hexidecimal conversion
    //**************************************************************************
    public class HexToBytenByteToHex
    {
        public HexToBytenByteToHex()
        {

            // constructor

        }
        public static int GetByteCount(string hexString)
        {
            int numHexChars = 0;
            char c;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                    numHexChars++;
            }
            // if odd number of characters, discard last character
            if (numHexChars % 2 != 0)
            {
                numHexChars--;
            }
            return numHexChars / 2; // 2 characters per byte
        }

        public static byte[] GetBytes(string hexString, out int discarded)
        {
            discarded = 0;
            string newString = "";
            char c;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                    newString += c;
                else
                    discarded++;
            }
            // if odd number of characters, discard last character
            if (newString.Length % 2 != 0)
            {
                discarded++;
                newString = newString.Substring(0, newString.Length - 1);
            }

            int byteLength = newString.Length / 2;
            byte[] bytes = new byte[byteLength];
            string hex;
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new String(new Char[] { newString[j], newString[j + 1] });
                bytes[i] = HexToByte(hex);
                j = j + 2;
            }
            return bytes;
        }
        public static string ToString(byte[] bytes)
        {
            string hexString = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString += bytes[i].ToString("X2");
            }
            return hexString;
        }
        public static bool InHexFormat(string hexString)
        {
            bool hexFormat = true;

            foreach (char digit in hexString)
            {
                if (!IsHexDigit(digit))
                {
                    hexFormat = false;
                    break;
                }
            }
            return hexFormat;
        }

        public static bool IsHexDigit(Char c)
        {
            int numChar;
            int numA = Convert.ToInt32('A');
            int num1 = Convert.ToInt32('0');
            c = Char.ToUpper(c);
            numChar = Convert.ToInt32(c);
            if (numChar >= numA && numChar < (numA + 6))
                return true;
            if (numChar >= num1 && numChar < (num1 + 10))
                return true;
            return false;
        }
        private static byte HexToByte(string hex)
        {
            if (hex.Length > 2 || hex.Length <= 0)
                throw new ArgumentException("hex must be 1 or 2 characters in length");
            byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return newByte;
        }


    }

    class HID
    {
        //*********************************************************************************************************
        // Define Constants, To Add "About Dialog Box" in System Menu
        //*********************************************************************************************************
        public const Int32 WM_SYSCOMMAND = 0x112;
        public const Int32 MF_SEPARATOR = 0x800;
        public const Int32 MF_BYPOSITION = 0x400;
        public const Int32 MF_STRING = 0x0;

        public const Int32 _SettingsSysMenuID = 1000;
        public const Int32 _AboutSysMenuID = 1001;


        //*********************************************************************************************************
        // Function Name: GetSystemMenu
        // In Parameter : hWnd - A handle to the window that will own a copy of the window menu.
        //                bRevert - The action to be taken. If this parameter is FALSE, GetSystemMenu returns a handle to the copy of the window menu currently in use. 
        // Out Parameter: ------
        // Description  : Enables the application to access the window menu for copying and modifying.
        //*********************************************************************************************************
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);


        //*********************************************************************************************************
        // Function Name: InsertMenu
        // In Parameter : hWnd - A handle to the menu to be changed. 
        //                Position - The menu item before which the new menu item is to be inserted, as determined by the uFlags parameter. 
        //                Flag - Controls the interpretation of the uPosition parameter and the content, appearance, and behavior of the new menu item.
        //                IDNewItem - The identifier of the new menu item or, if the uFlags parameter has the MF_POPUP flag set, a handle to the drop-down menu or submenu.
        //                newItem - The content of the new menu item.
        // Out Parameter: ---------
        // Description  : Inserts a new menu item into a menu, moving other items down the menu.
        //*********************************************************************************************************
        [DllImport("user32.dll")]
        public static extern bool InsertMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);


        // *********************************************************************************************************
        // Function Name: SCardEstablishContext
        // In Parameter : dwScope - Scope of the resource manager context.
        //                pvReserved1 - Reserved for future use and must be NULL
        //                pvReserved2 - Reserved for future use and must be NULL.
        // Out Parameter: phContext - A handle to the established resource manager context
        // Description  : Establishes context to the reader
        //*********************************************************************************************************
        [DllImport("WinScard.dll")]
        public static extern int SCardEstablishContext(uint dwScope,
        IntPtr notUsed1,
        IntPtr notUsed2,
        out IntPtr phContext);


        // *********************************************************************************************************
        // Function Name: SCardReleaseContext
        // In Parameter : phContext - A handle to the established resource manager context              
        // Out Parameter: -------
        // Description  :Releases context from the reader
        //*********************************************************************************************************
        [DllImport("WinScard.dll")]
        public static extern int SCardReleaseContext(IntPtr phContext);


        // *********************************************************************************************************
        // Function Name: SCardConnect
        // In Parameter : hContext - A handle that identifies the resource manager context.
        //                cReaderName  - The name of the reader that contains the target card.
        //                dwShareMode - A flag that indicates whether other applications may form connections to the card.
        //                dwPrefProtocol - A bitmask of acceptable protocols for the connection.  
        // Out Parameter: ActiveProtocol - A flag that indicates the established active protocol.
        //                hCard - A handle that identifies the connection to the smart card in the designated reader. 
        // Description  : Connect to card on reader
        //*********************************************************************************************************
        [DllImport("WinScard.dll")]
        public static extern int SCardConnect(IntPtr hContext,
        string cReaderName,
        uint dwShareMode,
        uint dwPrefProtocol,
        ref IntPtr hCard,
        ref IntPtr ActiveProtocol);


        // *********************************************************************************************************
        // Function Name: SCardDisconnect
        // In Parameter : hCard - Reference value obtained from a previous call to SCardConnect.
        //                Disposition - Action to take on the card in the connected reader on close.  
        // Out(Parameter)
        // Description  : Disconnect card from reader
        //*********************************************************************************************************
        [DllImport("WinScard.dll")]
        public static extern int SCardDisconnect(IntPtr hCard, int Disposition);


        //    *********************************************************************************************************
        // Function Name: SCardListReaders
        // In Parameter : hContext - A handle to the established resource manager context
        //                mszReaders - Multi-string that lists the card readers with in the supplied readers groups
        //                pcchReaders - length of the readerlist buffer in characters
        // Out Parameter: mzGroup - Names of the Reader groups defined to the System
        //                pcchReaders - length of the readerlist buffer in characters
        // Description  : List of all readers connected to system 
        //*********************************************************************************************************
        [DllImport("WinScard.dll", EntryPoint = "SCardListReadersA", CharSet = CharSet.Ansi)]
        public static extern int SCardListReaders(
          IntPtr hContext,
          byte[] mszGroups,
          byte[] mszReaders,
          ref UInt32 pcchReaders
          );


        // *********************************************************************************************************
        // Function Name: SCardState
        // In Parameter : hCard - Reference value obtained from a previous call to SCardConnect.
        // Out Parameter: state - Current state of smart card in  the reader
        //                protocol - Current Protocol
        //                ATR - 32 bytes buffer that receives the ATR string
        //                ATRLen - Supplies the length of ATR buffer
        // Description  : Current state of the smart card in the reader
        //*********************************************************************************************************
        [DllImport("WinScard.dll")]
        public static extern int SCardState(IntPtr hCard, ref IntPtr state, ref IntPtr protocol, ref Byte[] ATR, ref int ATRLen);


        // *********************************************************************************************************
        // Function Name: SCardTransmit
        // In Parameter : hCard - A reference value returned from the SCardConnect function.
        //                pioSendRequest - A pointer to the protocol header structure for the instruction.
        //                SendBuff- A pointer to the actual data to be written to the card.
        //                SendBuffLen - The length, in bytes, of the pbSendBuffer parameter. 
        //                pioRecvRequest - Pointer to the protocol header structure for the instruction ,Pointer to the protocol header structure for the instruction, 
        //                followed by a buffer in which to receive any returned protocol control information (PCI) specific to the protocol in use.
        //                RecvBuffLen - Supplies the length, in bytes, of the pbRecvBuffer parameter and receives the actual number of bytes received from the smart card.
        // Out Parameter: pioRecvRequest - Pointer to the protocol header structure for the instruction ,Pointer to the protocol header structure for the instruction, 
        //                followed by a buffer in which to receive any returned protocol control information (PCI) specific to the protocol in use.
        //                RecvBuff - Pointer to any data returned from the card.
        //                RecvBuffLen - Supplies the length, in bytes, of the pbRecvBuffer parameter and receives the actual number of bytes received from the smart card.
        // Description  : Transmit APDU to card 
        //*********************************************************************************************************
        [DllImport("WinScard.dll")]
        public static extern int SCardTransmit(IntPtr hCard, ref HiDWinscard.SCARD_IO_REQUEST pioSendRequest,
                                                            Byte[] SendBuff,
                                                            int SendBuffLen,
                                                            ref HiDWinscard.SCARD_IO_REQUEST pioRecvRequest,
                                                            Byte[] RecvBuff, ref int RecvBuffLen);


        // *********************************************************************************************************
        // Function Name: SCardGetStatusChange
        // In Parameter : hContext - A handle that identifies the resource manager context.
        //                value_TimeOut - The maximum amount of time, in milliseconds, to wait for an action.
        //                ReaderState -  An array of SCARD_READERSTATE structures that specify the readers to watch, and that receives the result.
        //                ReaderCount -  The number of elements in the rgReaderStates array.
        // Out Parameter: ReaderState - An array of SCARD_READERSTATE structures that specify the readers to watch, and that receives the result.
        // Description  : The current availability of the cards in a specific set of readers changes.
        //*********************************************************************************************************
        [DllImport("winscard.dll", CharSet = CharSet.Unicode)]
        public static extern int SCardGetStatusChange(IntPtr hContext,
        int value_Timeout,
        ref HiDWinscard.SCARD_READERSTATE ReaderState,
        uint ReaderCount);

    }

    internal class InitCardViewModel: ViewModelBase
    {
        public RelayCommand OpenCard { get; private set; }

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
            OpenCard = new RelayCommand(_ => fOpenCard());

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
                    BackgroundTb = System.Windows.Media.Brushes.Red;
                    ForegroundTb = System.Windows.Media.Brushes.White;
                }
                else
                {
                    retval = HID.SCardGetStatusChange(hContext, value_Timeout, ref ReaderState, ReaderCount);
                    if ((ReaderState.ATRLength == 0) || (retval != 0))
                    {
                        MycardStatus = "SmartCard Removed";
                        BackgroundTb = System.Windows.Media.Brushes.Red;
                        ForegroundTb = System.Windows.Media.Brushes.White;
                        //DisconnectButton_Click(sender1, e1);
                        //mifarecardtypeLabel.Content = "";
                        MyUIDofCard = "";
                    }
                    else
                    {
                        MycardStatus = "SmartCard Inserted";
                        BackgroundTb = System.Windows.Media.Brushes.GreenYellow;
                        ForegroundTb = System.Windows.Media.Brushes.Black;
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
            try
            {
                _controller.OpenCard();
            }
            catch(Exception ex)
            {
                StatusText = ex.Message;
            }
        }

        private void fSaveCardInfo()
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

        private Task<int> PkiSave2Card()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                

                int i = 0;

                while(true)
                {
                    Thread.Sleep(1000);
                    i++;

                    if (string.IsNullOrEmpty(MyUIDofCard))
                    {
                        return -1;
                    }

                    if (i>=10)
                    {
                        try
                        {
                            _controller.OpenCard();
                        }
                        catch (Exception ex)
                        {
                            StatusText = ex.Message;
                        }

                        break;
                    }
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
