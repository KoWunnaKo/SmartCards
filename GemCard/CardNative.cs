using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace GemCard
{
    /// <summary>
    /// CARD_STATE enumeration, used by the PC/SC function SCardGetStatusChanged
    /// </summary>
    enum CARD_STATE
    {
        UNAWARE = 0x00000000,
        IGNORE = 0x00000001,
        CHANGED = 0x00000002,
        UNKNOWN = 0x00000004,
        UNAVAILABLE = 0x00000008,
        EMPTY = 0x00000010,
        PRESENT = 0x00000020,
        ATRMATCH = 0x00000040,
        EXCLUSIVE = 0x00000080,
        INUSE = 0x00000100,
        MUTE = 0x00000200,
        UNPOWERED = 0x00000400
    }



	/// <summary>
	/// Wraps the SCARD_IO_STRUCTURE
    ///  
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct	SCard_IO_Request
	{
		public UInt32	m_dwProtocol;
		public UInt32	m_cbPciLength;
	}


    /// <summary>
    /// Wraps theSCARD_READERSTATE structure of PC/SC
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SCard_ReaderState
    {
        public string m_szReader;
        public IntPtr m_pvUserData;
        public UInt32 m_dwCurrentState;
        public UInt32 m_dwEventState;
        public UInt32 m_cbAtr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
        public byte[] m_rgbAtr;
    }
    
	/// <summary>
	/// Implementation of ICard using native (P/Invoke) interoperability for PC/SC
	/// </summary>
	public class CardNative : CardBase
	{

        /*===========================================================
        '   Error Codes
        '===========================================================*/
        public const int SCARD_F_INTERNAL_ERROR = -2146435071; //  An internal consistency check failed.  
        public const int SCARD_E_CANCELLED = -2146435070; //  The action was cancelled by an SCardCancel request.  
        public const int SCARD_E_INVALID_HANDLE = -2146435069; //  The supplied handle was invalid.  
        public const int SCARD_E_INVALID_PARAMETER = -2146435068; //  One or more of the supplied parameters could not be properly interpreted.  
        public const int SCARD_E_INVALID_TARGET = -2146435067; //  Registry startup information is missing or invalid. 
        public const int SCARD_E_NO_MEMORY = -2146435066; //  Not enough memory available to complete this command. 
        public const int SCARD_F_WAITED_TOO_LONG = -2146435065; //  An internal consistency timer has expired. 
        public const int SCARD_E_INSUFFICIENT_BUFFER = -2146435064; //  The data buffer to receive returned data is too small for the returned data.
        public const int SCARD_E_UNKNOWN_READER = -2146435063; //  The specified reader name is not recognized.
        public const int SCARD_E_TIMEOUT = -2146435062; //  The user-specified timeout value has expired.
        public const int SCARD_E_SHARING_VIOLATION = -2146435061; //  The smart card cannot be accessed because of other connections outstanding.
        public const int SCARD_E_NO_SMARTCARD = -2146435060; //  The operation requires a Smart Card, but no Smart Card is currently in the device.
        public const int SCARD_E_UNKNOWN_CARD = -2146435059; //  The specified smart card name is not recognized.
        public const int SCARD_E_CANT_DISPOSE = -2146435058; //  The system could not dispose of the media in the requested manner.
        public const int SCARD_E_PROTO_MISMATCH = -2146435057; //  The requested protocols are incompatible with the protocol currently in use with the smart card.
        public const int SCARD_E_NOT_READY = -2146435056; //  The reader or smart card is not ready to accept commands.
        public const int SCARD_E_INVALID_VALUE = -2146435055; //  One or more of the supplied parameters values could not be properly interpreted.
        public const int SCARD_E_SYSTEM_CANCELLED = -2146435054; //  The action was cancelled by the system, presumably to log off or shut down.
        public const int SCARD_F_COMM_ERROR = -2146435053; //  An internal communications error has been detected.
        public const int SCARD_F_UNKNOWN_ERROR = -2146435052; //  An internal error has been detected, but the source is unknown.
        public const int SCARD_E_INVALID_ATR = -2146435051; //  An ATR obtained from the registry is not a valid ATR string.
        public const int SCARD_E_NOT_TRANSACTED = -2146435050; //  An attempt was made to end a non-existent transaction.
        public const int SCARD_E_READER_UNAVAILABLE = -2146435049; //  The specified reader is not currently available for use.
        public const int SCARD_P_SHUTDOWN = -2146435048; //  The operation has been aborted to allow the server application to exit.
        public const int SCARD_E_PCI_TOO_SMALL = -2146435047; //  The PCI Receive buffer was too small.
        public const int SCARD_E_READER_UNSUPPORTED = -2146435046; //  The reader driver does not meet minimal requirements for support.
        public const int SCARD_E_DUPLICATE_READER = -2146435045; //  The reader driver did not produce a unique reader name.
        public const int SCARD_E_CARD_UNSUPPORTED = -2146435044; //  The smart card does not meet minimal requirements for support.
        public const int SCARD_E_NO_SERVICE = -2146435043; //  The Smart card resource manager is not running.
        public const int SCARD_E_SERVICE_STOPPED = -2146435042; //  The Smart card resource manager has shut down.
        public const int SCARD_E_UNEXPECTED = -2146435041; //  An unexpected card error has occurred.
        public const int SCARD_E_ICC_INSTALLATION = -2146435040; //  No Primary Provider can be found for the smart card.
        public const int SCARD_E_ICC_CREATEORDER = -2146435039; //  The requested order of object creation is not supported.
        public const int SCARD_E_UNSUPPORTED_FEATURE = -2146435038; //  This smart card does not support the requested feature.
        public const int SCARD_E_DIR_NOT_FOUND = -2146435037; //  The identified directory does not exist in the smart card.
        public const int SCARD_E_FILE_NOT_FOUND = -2146435036; //  The identified file does not exist in the smart card.
        public const int SCARD_E_NO_DIR = -2146435035; //  The supplied path does not represent a smart card directory.
        public const int SCARD_E_NO_FILE = -2146435034; //  The supplied path does not represent a smart card file.
        public const int SCARD_E_NO_ACCESS = -2146435033; //  Access is denied to this file.
        public const int SCARD_E_WRITE_TOO_MANY = -2146435032; //  The smartcard does not have enough memory to store the information.
        public const int SCARD_E_BAD_SEEK = -2146435031; //  There was an error trying to set the smart card file object pointer.
        public const int SCARD_E_INVALID_CHV = -2146435030; //  The supplied PIN is incorrect.
        public const int SCARD_E_UNKNOWN_RES_MNG = -2146435029; //  An unrecognized error code was returned from a layered component.
        public const int SCARD_E_NO_SUCH_CERTIFICATE = -2146435028; //  The requested certificate does not exist.
        public const int SCARD_E_CERTIFICATE_UNAVAILABLE = -2146435027; //  The requested certificate could not be obtained.
        public const int SCARD_E_NO_READERS_AVAILABLE = -2146435026; //  Cannot find a smart card reader.
        public const int SCARD_E_COMM_DATA_LOST = -2146435025; //  A communications error with the smart card has been detected.  Retry the operation.
        public const int SCARD_E_NO_KEY_CONTAINER = -2146435024; //  The requested key container does not exist on the smart card.
        public const int SCARD_E_SERVER_TOO_BUSY = -2146435023; //  The Smart card resource manager is too busy to complete this operation.
        // These are warning codes.
        public const int SCARD_W_UNSUPPORTED_CARD = -2146434971; //  The reader cannot communicate with the smart card, due to ATR configuration conflicts.
        public const int SCARD_W_UNRESPONSIVE_CARD = -2146434970; //  The smart card is not responding to a reset.
        public const int SCARD_W_UNPOWERED_CARD = -2146434969; //  Power has been removed from the smart card, so that further communication is not possible.
        public const int SCARD_W_RESET_CARD = -2146434968; //  The smart card has been reset, so any shared state information is invalid.
        public const int SCARD_W_REMOVED_CARD = -2146434967; //  The smart card has been removed, so that further communication is not possible.
        public const int SCARD_W_SECURITY_VIOLATION = -2146434966; //  Access was denied because of a security violation.
        public const int SCARD_W_WRONG_CHV = -2146434965; //  The card cannot be accessed because the wrong PIN was presented.
        public const int SCARD_W_CHV_BLOCKED = -2146434964; //  The card cannot be accessed because the maximum number of PIN entry attempts has been reached.
        public const int SCARD_W_EOF = -2146434963; //  The end of the smart card file has been reached.
        public const int SCARD_W_CANCELLED_BY_USER = -2146434962; //  The action was cancelled by the user.
        public const int SCARD_W_CARD_NOT_AUTHENTICATED = -2146434961; //  No PIN was presented to the smart card.
        public const int SCARD_W_CACHE_ITEM_NOT_FOUND = -2146434960; //  The requested item could not be found in the cache.
        public const int SCARD_W_CACHE_ITEM_STALE = -2146434959; //  The requested cache item is too old and was deleted from the cache.
        public const int SCARD_W_CACHE_ITEM_TOO_BIG = -2146434958; //  The new cache item exceeds the maximum per-item size defined for the cache.

        public static string GetScardErrMsg(int ReturnCode)
        {
            switch (ReturnCode)
            {
                case SCARD_F_INTERNAL_ERROR:
                    return ("An internal consistency check failed.");

                case SCARD_E_CANCELLED:
                    return ("The action was cancelled by an SCardCancel request.");

                case SCARD_E_INVALID_HANDLE:
                    return ("The supplied handle was invalid.");

                case SCARD_E_INVALID_PARAMETER:
                    return ("One or more of the supplied parameters could not be properly interpreted.");

                case SCARD_E_INVALID_TARGET:
                    return ("Registry startup information is missing or invalid.");

                case SCARD_E_NO_MEMORY:
                    return ("Not enough memory available to complete this command.");

                case SCARD_F_WAITED_TOO_LONG:
                    return ("An internal consistency timer has expired.");

                case SCARD_E_INSUFFICIENT_BUFFER:
                    return ("The data buffer to receive returned data is too small for the returned data.");

                case SCARD_E_UNKNOWN_READER:
                    return ("The specified reader name is not recognized.");

                case SCARD_E_TIMEOUT:
                    return ("The user-specified timeout value has expired.");

                case SCARD_E_SHARING_VIOLATION:
                    return ("The smart card cannot be accessed because of other connections outstanding.");

                case SCARD_E_NO_SMARTCARD:
                    return ("The operation requires a Smart Card, but no Smart Card is currently in the device.");

                case SCARD_E_UNKNOWN_CARD:
                    return ("The specified smart card name is not recognized.");

                case SCARD_E_CANT_DISPOSE:
                    return ("The system could not dispose of the media in the requested manner.");

                case SCARD_E_PROTO_MISMATCH:
                    return ("The requested protocols are incompatible with the protocol currently in use with the smart card.");

                case SCARD_E_NOT_READY:
                    return ("The reader or smart card is not ready to accept commands.");

                case SCARD_E_INVALID_VALUE:
                    return ("One or more of the supplied parameters values could not be properly interpreted.");

                case SCARD_E_SYSTEM_CANCELLED:
                    return ("The action was cancelled by the system, presumably to log off or shut down.");

                case SCARD_F_COMM_ERROR:
                    return ("An internal communications error has been detected.");

                case SCARD_F_UNKNOWN_ERROR:
                    return ("An internal error has been detected, but the source is unknown.");

                case SCARD_E_INVALID_ATR:
                    return ("An ATR obtained from the registry is not a valid ATR string.");

                case SCARD_E_NOT_TRANSACTED:
                    return ("An attempt was made to end a non-existent transaction.");

                case SCARD_E_READER_UNAVAILABLE:
                    return ("The specified reader is not currently available for use.");

                case SCARD_P_SHUTDOWN:
                    return ("The operation has been aborted to allow the server application to exit.");

                case SCARD_E_PCI_TOO_SMALL:
                    return ("The PCI Receive buffer was too small.");

                case SCARD_E_READER_UNSUPPORTED:
                    return ("The reader driver does not meet minimal requirements for support.");

                case SCARD_E_DUPLICATE_READER:
                    return ("The reader driver did not produce a unique reader name.");

                case SCARD_E_CARD_UNSUPPORTED:
                    return ("The smart card does not meet minimal requirements for support.");

                case SCARD_E_NO_SERVICE:
                    return ("The Smart card resource manager is not running.");

                case SCARD_E_SERVICE_STOPPED:
                    return ("The Smart card resource manager has shut down.");

                case SCARD_E_UNEXPECTED:
                    return ("An unexpected card error has occurred.");

                case SCARD_E_ICC_INSTALLATION:
                    return ("No Primary Provider can be found for the smart card.");

                case SCARD_E_ICC_CREATEORDER:
                    return ("The requested order of object creation is not supported.");

                case SCARD_E_UNSUPPORTED_FEATURE:
                    return ("This smart card does not support the requested feature.");

                case SCARD_E_DIR_NOT_FOUND:
                    return ("The identified directory does not exist in the smart card.");

                case SCARD_E_FILE_NOT_FOUND:
                    return ("The identified file does not exist in the smart card.");

                case SCARD_E_NO_DIR:
                    return ("The supplied path does not represent a smart card directory.");

                case SCARD_E_NO_FILE:
                    return ("The supplied path does not represent a smart card file.");

                case SCARD_E_NO_ACCESS:
                    return ("Access is denied to this file.");

                case SCARD_E_WRITE_TOO_MANY:
                    return ("The smartcard does not have enough memory to store the information.");

                case SCARD_E_BAD_SEEK:
                    return ("There was an error trying to set the smart card file object pointer.");

                case SCARD_E_INVALID_CHV:
                    return ("The supplied PIN is incorrect.");

                case SCARD_E_UNKNOWN_RES_MNG:
                    return ("An unrecognized error code was returned from a layered component.");

                case SCARD_E_NO_SUCH_CERTIFICATE:
                    return ("The requested certificate does not exist.");

                case SCARD_E_CERTIFICATE_UNAVAILABLE:
                    return ("The requested certificate could not be obtained.");

                case SCARD_E_NO_READERS_AVAILABLE:
                    return ("Cannot find a smart card reader.");

                case SCARD_E_COMM_DATA_LOST:
                    return ("A communications error with the smart card has been detected.  Retry the operation.");

                case SCARD_E_NO_KEY_CONTAINER:
                    return ("The requested key container does not exist on the smart card.");

                case SCARD_E_SERVER_TOO_BUSY:
                    return ("The Smart card resource manager is too busy to complete this operation.");

                // Warning codes
                case SCARD_W_UNSUPPORTED_CARD:
                    return ("The reader cannot communicate with the smart card, due to ATR configuration conflicts.");

                case SCARD_W_UNRESPONSIVE_CARD:
                    return ("The smart card is not responding to a reset.");

                case SCARD_W_UNPOWERED_CARD:
                    return ("Power has been removed from the smart card, so that further communication is not possible.");

                case SCARD_W_RESET_CARD:
                    return ("The smart card has been reset, so any shared state information is invalid.");

                case SCARD_W_REMOVED_CARD:
                    return ("The smart card has been removed, so that further communication is not possible.");

                case SCARD_W_SECURITY_VIOLATION:
                    return ("Access was denied because of a security violation.");

                case SCARD_W_WRONG_CHV:
                    return ("The card cannot be accessed because the wrong PIN was presented.");

                case SCARD_W_CHV_BLOCKED:
                    return ("The card cannot be accessed because the maximum number of PIN entry attempts has been reached.");

                case SCARD_W_EOF:
                    return ("The end of the smart card file has been reached.");

                case SCARD_W_CANCELLED_BY_USER:
                    return ("The action was cancelled by the user.");

                case SCARD_W_CARD_NOT_AUTHENTICATED:
                    return ("No PIN was presented to the smart card.");

                case SCARD_W_CACHE_ITEM_NOT_FOUND:
                    return ("The requested item could not be found in the cache.");

                case SCARD_W_CACHE_ITEM_STALE:
                    return ("The requested cache item is too old and was deleted from the cache.");

                case SCARD_W_CACHE_ITEM_TOO_BIG:
                    return ("The new cache item exceeds the maximum per-item size defined for the cache.");

                default:
                    return ("? - " + ReturnCode.ToString());
            }
        }

        private IntPtr m_hContext = IntPtr.Zero;
        private IntPtr m_hCard = IntPtr.Zero;
		private	UInt32	m_nProtocol = (uint) PROTOCOL.T0;
		private	int	m_nLastError = 0;
        const int SCARD_S_SUCCESS = 0;

		#region PCSC_FUNCTIONS
        /// <summary>
        /// Native SCardGetStatusChanged from winscard.dll
        /// </summary>
        /// <param name="hContext"></param>
        /// <param name="dwTimeout"></param>
        /// <param name="rgReaderStates"></param>
        /// <param name="cReaders"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardGetStatusChange(IntPtr hContext,
            UInt32 dwTimeout,
            [In,Out] SCard_ReaderState[] rgReaderStates,
            UInt32 cReaders);

		/// <summary>
		/// Native SCardListReaders function from winscard.dll
		/// </summary>
		/// <param name="hContext"></param>
		/// <param name="mszGroups"></param>
		/// <param name="mszReaders"></param>
		/// <param name="pcchReaders"></param>
		/// <returns></returns>
		[DllImport("winscard.dll", SetLastError=true)]
        internal static extern int SCardListReaders(IntPtr hContext,
			[MarshalAs(UnmanagedType.LPTStr)] string mszGroups,
			IntPtr mszReaders,
            out UInt32 pcchReaders);

		/// <summary>
		/// Native SCardEstablishContext function from winscard.dll
		/// </summary>
		/// <param name="dwScope"></param>
		/// <param name="pvReserved1"></param>
		/// <param name="pvReserved2"></param>
		/// <param name="phContext"></param>
		/// <returns></returns>
		[DllImport("winscard.dll", SetLastError=true)]
		internal	static	extern	int	SCardEstablishContext(UInt32 dwScope,
			IntPtr pvReserved1,
			IntPtr pvReserved2,
			IntPtr phContext);

		/// <summary>
		/// Native SCardReleaseContext function from winscard.dll
		/// </summary>
		/// <param name="hContext"></param>
		/// <returns></returns>
		[DllImport("winscard.dll", SetLastError=true)]
        internal static extern int SCardReleaseContext(IntPtr hContext);

        /// <summary>
        /// Native SCardIsValidContext function from winscard.dll
        /// </summary>
        /// <param name="hContext"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardIsValidContext(IntPtr hContext);

		/// <summary>
		/// Native SCardConnect function from winscard.dll
		/// </summary>
		/// <param name="hContext"></param>
		/// <param name="szReader"></param>
		/// <param name="dwShareMode"></param>
		/// <param name="dwPreferredProtocols"></param>
		/// <param name="phCard"></param>
		/// <param name="pdwActiveProtocol"></param>
		/// <returns></returns>
		[DllImport("winscard.dll", SetLastError=true, CharSet=CharSet.Auto)]
        internal static extern int SCardConnect(IntPtr hContext,
			[MarshalAs(UnmanagedType.LPTStr)] string szReader,
			UInt32	dwShareMode, 
			UInt32	dwPreferredProtocols,
			IntPtr	phCard, 
			IntPtr	pdwActiveProtocol);

		/// <summary>
		/// Native SCardDisconnect function from winscard.dll
		/// </summary>
		/// <param name="hCard"></param>
		/// <param name="dwDisposition"></param>
		/// <returns></returns>
		[DllImport("winscard.dll", SetLastError=true)]
        internal static extern int SCardDisconnect(IntPtr hCard,
			UInt32 dwDisposition);

		/// <summary>
		/// Native SCardTransmit function from winscard.dll
		/// </summary>
		/// <param name="hCard"></param>
		/// <param name="pioSendPci"></param>
		/// <param name="pbSendBuffer"></param>
		/// <param name="cbSendLength"></param>
		/// <param name="pioRecvPci"></param>
		/// <param name="pbRecvBuffer"></param>
		/// <param name="pcbRecvLength"></param>
		/// <returns></returns>
		[DllImport("winscard.dll", SetLastError=true)]
        internal static extern int SCardTransmit(IntPtr hCard,
			[In] ref SCard_IO_Request pioSendPci,
			byte[] pbSendBuffer,
			UInt32 cbSendLength,
			IntPtr pioRecvPci,
			[Out] byte[] pbRecvBuffer,
			out UInt32 pcbRecvLength
			);

        /// <summary>
        /// Native SCardBeginTransaction function of winscard.dll
        /// </summary>
        /// <param name="hContext"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardBeginTransaction(IntPtr hContext);

        /// <summary>
        /// Native SCardEndTransaction function of winscard.dll
        /// </summary>
        /// <param name="hContext"></param>
        /// <returns></returns>
        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardEndTransaction(IntPtr hContext, UInt32 dwDisposition);

        [DllImport("winscard.dll", SetLastError = true)]
        internal static extern int SCardGetAttrib(IntPtr hCard,
            UInt32 dwAttribId,
            [Out] byte[] pbAttr,
            out UInt32 pcbAttrLen);

        #endregion WINSCARD_FUNCTIONS

		/// <summary>
		/// Default constructor
		/// </summary>
		public CardNative()
		{
		}

		/// <summary>
		/// Object destruction
		/// </summary>
		~CardNative()
		{
			Disconnect(DISCONNECT.Unpower);

			ReleaseContext();
		}

		#region ICard Members

		/// <summary>
		/// Wraps the PCSC function
		/// LONG SCardListReaders(SCARDCONTEXT hContext, 
		///		LPCTSTR mszGroups, 
		///		LPTSTR mszReaders, 
		///		LPDWORD pcchReaders 
		///	);
		/// </summary>
		/// <returns>A string array of the readers</returns>
		public	override string[]	ListReaders()
		{
			EstablishContext(SCOPE.User);

			string[]	sListReaders = null;

            try
            {
                UInt32 pchReaders = 0;
			    IntPtr	szListReaders = IntPtr.Zero;

			    m_nLastError = SCardListReaders(m_hContext, null, szListReaders, out pchReaders);
			    if (m_nLastError == 0)
			    {
			    	szListReaders = Marshal.AllocHGlobal((int) pchReaders);
			    	m_nLastError = SCardListReaders(m_hContext, null, szListReaders, out pchReaders);
			    	if (m_nLastError == 0)
			    	{
			    		char[] caReadersData = new char[pchReaders];
			    		int	nbReaders = 0;
			    		for (int nI = 0; nI < pchReaders; nI++)
			    		{
			    			caReadersData[nI] = (char) Marshal.ReadByte(szListReaders, nI);

			    			if (caReadersData[nI] == 0)
			    				nbReaders++;
			    		}

			    		// Remove last 0
			    		--nbReaders;

			    		if (nbReaders != 0)
			    		{
			    			sListReaders = new string[nbReaders];
			    			char[] caReader = new char[pchReaders];
			    			int	nIdx = 0;
			    			int	nIdy = 0;
			    			int	nIdz = 0;
			    			// Get the nJ string from the multi-string

			    			while(nIdx < pchReaders - 1)
			    			{
			    				caReader[nIdy] = caReadersData[nIdx];
			    				if (caReader[nIdy] == 0)
			    				{
			    					sListReaders[nIdz] = new string(caReader, 0, nIdy);
			    					++nIdz;
			    					nIdy = 0;
			    					caReader = new char[pchReaders];
			    				}
			    				else
			    					++nIdy;

			    				++nIdx;
			    			}
			    		}

			    	}

			    	Marshal.FreeHGlobal(szListReaders);
			    }

			    ReleaseContext();
            }
            catch
            {

            }

            return sListReaders;
		}

		/// <summary>
		/// Wraps the PCSC function 
		/// LONG SCardEstablishContext(
		///		IN DWORD dwScope,
		///		IN LPCVOID pvReserved1,
		///		IN LPCVOID pvReserved2,
		///		OUT LPSCARDCONTEXT phContext
		///	);
		/// </summary>
		/// <param name="Scope"></param>
		public void EstablishContext(SCOPE Scope)
		{
            try
            {
                IntPtr hContext = Marshal.AllocHGlobal(Marshal.SizeOf(m_hContext));

                m_nLastError = SCardEstablishContext((uint)Scope, IntPtr.Zero, IntPtr.Zero, hContext);
                if (m_nLastError != 0)
                {
                    string msg = "SCardEstablishContext error: " + m_nLastError;

                    Marshal.FreeHGlobal(hContext);
                    throw new Exception(msg);
                }

                m_hContext = Marshal.ReadIntPtr(hContext);

                Marshal.FreeHGlobal(hContext);
            }
            catch
            {

            }

		}


		/// <summary>
		/// Wraps the PCSC function
		/// LONG SCardReleaseContext(
		///		IN SCARDCONTEXT hContext
		///	);
		/// </summary>
		public void ReleaseContext()
		{
            try
            {
                if (SCardIsValidContext(m_hContext) == SCARD_S_SUCCESS)
                {
                    m_nLastError = SCardReleaseContext(m_hContext);

                    if (m_nLastError != 0)
                    {
                        string msg = "SCardReleaseContext error: " + m_nLastError;
                        throw new Exception(msg);
                    }

                    m_hContext = IntPtr.Zero;
                }
            }
            catch
            {

            }
		}

		/// <summary>
		///  Wraps the PCSC function
		///  LONG SCardConnect(
		///		IN SCARDCONTEXT hContext,
		///		IN LPCTSTR szReader,
		///		IN DWORD dwShareMode,
		///		IN DWORD dwPreferredProtocols,
		///		OUT LPSCARDHANDLE phCard,
		///		OUT LPDWORD pdwActiveProtocol
		///	);
		/// </summary>
		/// <param name="Reader"></param>
		/// <param name="ShareMode"></param>
		/// <param name="PreferredProtocols"></param>
		public override void Connect(string Reader, SHARE ShareMode, PROTOCOL PreferredProtocols)
		{
            try
            {
                EstablishContext(SCOPE.User);

                IntPtr hCard = Marshal.AllocHGlobal(Marshal.SizeOf(m_hCard));
                IntPtr pProtocol = Marshal.AllocHGlobal(Marshal.SizeOf(m_nProtocol));

                m_nLastError = SCardConnect(m_hContext,
                    Reader,
                    (uint)ShareMode,
                    (uint)PreferredProtocols,
                    hCard,
                    pProtocol);

                if (m_nLastError != 0)
                {
                    string msg = "SCardConnect error: " + m_nLastError;

                    Marshal.FreeHGlobal(hCard);
                    Marshal.FreeHGlobal(pProtocol);
                    throw new Exception(msg);
                }

                m_hCard = Marshal.ReadIntPtr(hCard);
                m_nProtocol = (uint)Marshal.ReadInt32(pProtocol);

                Marshal.FreeHGlobal(hCard);
                Marshal.FreeHGlobal(pProtocol);
            }
            catch
            {

            }
		}

		/// <summary>
		/// Wraps the PCSC function
		///	LONG SCardDisconnect(
		///		IN SCARDHANDLE hCard,
		///		IN DWORD dwDisposition
		///	);
		/// </summary>
		/// <param name="Disposition"></param>
		public override void Disconnect(DISCONNECT Disposition)
		{
            try
            {
                if (SCardIsValidContext(m_hContext) == SCARD_S_SUCCESS)
                {
                    m_nLastError = SCardDisconnect(m_hCard, (uint)Disposition);
                    m_hCard = IntPtr.Zero;

                    if (m_nLastError != 0)
                    {
                        string msg = "SCardDisconnect error: " + m_nLastError;
                        throw new Exception(msg);
                    }

                    ReleaseContext();
                }
            }
            catch
            {

            }
		}

		/// <summary>
		/// Wraps the PCSC function
		/// LONG SCardTransmit(
		///		SCARDHANDLE hCard,
		///		LPCSCARD_I0_REQUEST pioSendPci,
		///		LPCBYTE pbSendBuffer,
		///		DWORD cbSendLength,
		///		LPSCARD_IO_REQUEST pioRecvPci,
		///		LPBYTE pbRecvBuffer,
		///		LPDWORD pcbRecvLength
		///	);
		/// </summary>
		/// <param name="ApduCmd">APDUCommand object with the APDU to send to the card</param>
		/// <returns>An APDUResponse object with the response from the card</returns>
		public override APDUResponse Transmit(APDUCommand ApduCmd)
		{
            try
            {
                uint RecvLength = (uint)(ApduCmd.Le + APDUResponse.SW_LENGTH);
                byte[] ApduBuffer = null;
                byte[] ApduResponse = new byte[ApduCmd.Le + APDUResponse.SW_LENGTH];
                SCard_IO_Request ioRequest = new SCard_IO_Request();
                ioRequest.m_dwProtocol = m_nProtocol;
                ioRequest.m_cbPciLength = 8;

                // Build the command APDU
                if (ApduCmd.Data == null)
                {
                    ApduBuffer = new byte[APDUCommand.APDU_MIN_LENGTH + ((ApduCmd.Le != 0) ? 1 : 0)];

                    if (ApduCmd.Le != 0)
                        ApduBuffer[4] = (byte)ApduCmd.Le;
                }
                else
                {
                    ApduBuffer = new byte[APDUCommand.APDU_MIN_LENGTH + 1 + ApduCmd.Data.Length];

                    for (int nI = 0; nI < ApduCmd.Data.Length; nI++)
                        ApduBuffer[APDUCommand.APDU_MIN_LENGTH + 1 + nI] = ApduCmd.Data[nI];

                    ApduBuffer[APDUCommand.APDU_MIN_LENGTH] = (byte)ApduCmd.Data.Length;
                }

                ApduBuffer[0] = ApduCmd.Class;
                ApduBuffer[1] = ApduCmd.Ins;
                ApduBuffer[2] = ApduCmd.P1;
                ApduBuffer[3] = ApduCmd.P2;

                m_nLastError = SCardTransmit(m_hCard, ref ioRequest, ApduBuffer, (uint)ApduBuffer.Length, IntPtr.Zero, ApduResponse, out RecvLength);
                if (m_nLastError != 0)
                {
                    string msg = "SCardTransmit error: " + m_nLastError;
                    throw new Exception(msg);
                }

                byte[] ApduData = new byte[RecvLength];

                for (int nI = 0; nI < RecvLength; nI++)
                    ApduData[nI] = ApduResponse[nI];

                return new APDUResponse(ApduData);
            }
            catch
            {

            }

            return null;

		}

        /// <summary>
        /// Wraps the PCSC function
        /// LONG SCardTransmit(
        ///		SCARDHANDLE hCard,
        ///		LPCSCARD_I0_REQUEST pioSendPci,
        ///		LPCBYTE pbSendBuffer,
        ///		DWORD cbSendLength,
        ///		LPSCARD_IO_REQUEST pioRecvPci,
        ///		LPBYTE pbRecvBuffer,
        ///		LPDWORD pcbRecvLength
        ///	);
        /// </summary>
        /// <param name="ApduCmd">APDUCommand object with the APDU to send to the card</param>
        /// <returns>An APDUResponse object with the response from the card</returns>
        public APDUResponse TransmitLe(APDUCommand ApduCmd, uint _RecvLength = 0, bool isInstall= false)
        {
            try
            {
                uint RecvLength = (uint)(_RecvLength + APDUResponse.SW_LENGTH);
                byte[] ApduBuffer = null;
                byte[] ApduResponse = new byte[_RecvLength + APDUResponse.SW_LENGTH];
                SCard_IO_Request ioRequest = new SCard_IO_Request();
                ioRequest.m_dwProtocol = m_nProtocol;
                ioRequest.m_cbPciLength = 8;

                // Build the command APDU
                if (ApduCmd.Data == null)
                {
                    ApduBuffer = new byte[APDUCommand.APDU_MIN_LENGTH + 1];

                    ApduBuffer[4] = (byte)ApduCmd.Le;
                }
                else
                {
                    ApduBuffer = new byte[APDUCommand.APDU_MIN_LENGTH + 2 + ApduCmd.Data.Length];

                    int nI = 0;

                    for (nI = 0; nI < ApduCmd.Data.Length; nI++)
                        ApduBuffer[APDUCommand.APDU_MIN_LENGTH + 1 + nI] = ApduCmd.Data[nI];

                    ApduBuffer[APDUCommand.APDU_MIN_LENGTH] = (byte)ApduCmd.Data.Length;

                    if (!isInstall)
                        ApduBuffer[APDUCommand.APDU_MIN_LENGTH + ApduCmd.Data.Length + 1] = (byte)ApduCmd.Le;
                }

                ApduBuffer[0] = ApduCmd.Class;
                ApduBuffer[1] = ApduCmd.Ins;
                ApduBuffer[2] = ApduCmd.P1;
                ApduBuffer[3] = ApduCmd.P2;

                m_nLastError = SCardTransmit(m_hCard, ref ioRequest, ApduBuffer, (uint)ApduBuffer.Length, IntPtr.Zero, ApduResponse, out RecvLength);
                if (m_nLastError != 0)
                {
                    string msg = "SCardTransmit error: " + m_nLastError;
                    throw new Exception(msg);
                }

                byte[] ApduData = new byte[RecvLength];

                for (int nI = 0; nI < RecvLength; nI++)
                    ApduData[nI] = ApduResponse[nI];

                return new APDUResponse(ApduData);
            }
            catch
            {

            }

            return null;
        }


        private byte[] Int2ByteArray(int val)
        {
            int intValue = val;
            byte[] intBytes = BitConverter.GetBytes(intValue);
            Array.Reverse(intBytes);
            byte[] result = intBytes;

            List<byte> ll = new List<byte>();

            ll.Add(result[1]);
            ll.Add(result[2]);
            ll.Add(result[3]);

            return ll.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApduCmd"></param>
        /// <returns></returns>
        public APDUResponse TransmitImage(APDUCommand ApduCmd)
        {
            uint RecvLength = (uint)(ApduCmd.Le + APDUResponse.SW_LENGTH);
            byte[] ApduBuffer = null;
            byte[] ApduResponse = new byte[ApduCmd.Le + APDUResponse.SW_LENGTH];
            SCard_IO_Request ioRequest = new SCard_IO_Request();
            ioRequest.m_dwProtocol = m_nProtocol;
            ioRequest.m_cbPciLength = 8;

            // Build the command APDU
            if (ApduCmd.Data == null)
            {
                ApduBuffer = new byte[APDUCommand.APDU_MIN_LENGTH + ((ApduCmd.Le != 0) ? 1 : 0)];

                if (ApduCmd.Le != 0)
                    ApduBuffer[4] = (byte)ApduCmd.Le;
            }
            else
            {
                ApduBuffer = new byte[APDUCommand.APDU_MIN_LENGTH + 3 + ApduCmd.Data.Length];

                for (int nI = 0; nI < ApduCmd.Data.Length; nI++)
                    ApduBuffer[APDUCommand.APDU_MIN_LENGTH + 3 + nI] = ApduCmd.Data[nI];

                byte[] Len = Int2ByteArray(ApduCmd.Data.Length);

                ApduBuffer[APDUCommand.APDU_MIN_LENGTH] = Len[0];
                ApduBuffer[APDUCommand.APDU_MIN_LENGTH + 1] = Len[1];
                ApduBuffer[APDUCommand.APDU_MIN_LENGTH + 2] = Len[2];

            }

            ApduBuffer[0] = ApduCmd.Class;
            ApduBuffer[1] = ApduCmd.Ins;
            ApduBuffer[2] = ApduCmd.P1;
            ApduBuffer[3] = ApduCmd.P2;

            m_nLastError = SCardTransmit(m_hCard, ref ioRequest, ApduBuffer, (uint)ApduBuffer.Length, IntPtr.Zero, ApduResponse, out RecvLength);
            if (m_nLastError != 0)
            {
                string msg = "SCardTransmit error: " + m_nLastError;
                throw new Exception(msg);
            }

            byte[] ApduData = new byte[RecvLength];

            for (int nI = 0; nI < RecvLength; nI++)
                ApduData[nI] = ApduResponse[nI];

            return new APDUResponse(ApduData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApduCmd"></param>
        /// <returns></returns>
        public APDUResponse TransmitImageLe(APDUCommand ApduCmd, uint _RecvLength = 0)
        {
            uint RecvLength = (uint)(_RecvLength + APDUResponse.SW_LENGTH);
            byte[] ApduBuffer = null;
            byte[] ApduResponse = new byte[_RecvLength + APDUResponse.SW_LENGTH];
            SCard_IO_Request ioRequest = new SCard_IO_Request();
            ioRequest.m_dwProtocol = m_nProtocol;
            ioRequest.m_cbPciLength = 8;

            byte[] Len = Int2ByteArray((int)_RecvLength);

            ApduBuffer = new byte[APDUCommand.APDU_MIN_LENGTH + 3];

            ApduBuffer[APDUCommand.APDU_MIN_LENGTH] = Len[0];
            ApduBuffer[APDUCommand.APDU_MIN_LENGTH + 1] = Len[1];
            ApduBuffer[APDUCommand.APDU_MIN_LENGTH + 2] = Len[2];


            ApduBuffer[0] = ApduCmd.Class;
            ApduBuffer[1] = ApduCmd.Ins;
            ApduBuffer[2] = ApduCmd.P1;
            ApduBuffer[3] = ApduCmd.P2;

            m_nLastError = SCardTransmit(m_hCard, ref ioRequest, ApduBuffer, (uint)ApduBuffer.Length, IntPtr.Zero, ApduResponse, out RecvLength);
            if (m_nLastError != 0)
            {
                string msg = "SCardTransmit error: " + m_nLastError;
                throw new Exception(msg);
            }

            byte[] ApduData = new byte[RecvLength];

            for (int nI = 0; nI < RecvLength; nI++)
                ApduData[nI] = ApduResponse[nI];

            return new APDUResponse(ApduData);
        }

        /// <summary>
        /// Wraps the PSCS function
        /// LONG SCardBeginTransaction(
        ///     SCARDHANDLE hCard
        //  );
        /// </summary>
        public override void BeginTransaction()
        {
            try
            {
                if (SCardIsValidContext(m_hContext) == SCARD_S_SUCCESS)
                {
                    m_nLastError = SCardBeginTransaction(m_hCard);
                    if (m_nLastError != 0)
                    {
                        string msg = "SCardBeginTransaction error: " + m_nLastError;
                        throw new Exception(msg);
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Wraps the PCSC function
        /// LONG SCardEndTransaction(
        ///     SCARDHANDLE hCard,
        ///     DWORD dwDisposition
        /// );
        /// </summary>
        /// <param name="Disposition">A value from DISCONNECT enum</param>
        public override void EndTransaction(DISCONNECT Disposition)
        {
            try
            {
                if (SCardIsValidContext(m_hContext) == SCARD_S_SUCCESS)
                {
                    m_nLastError = SCardEndTransaction(m_hCard, (UInt32)Disposition);
                    if (m_nLastError != 0)
                    {
                        string msg = "SCardEndTransaction error: " + m_nLastError;
                        throw new Exception(msg);
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// Gets the attributes of the card
        /// </summary>
        /// <param name="AttribId">Identifier for the Attribute to get</param>
        /// <returns>Attribute content</returns>
        public override byte[] GetAttribute(UInt32 AttribId)
        {
            try
            {
                byte[] attr = null;
                UInt32 attrLen = 0;

                m_nLastError = SCardGetAttrib(m_hCard, AttribId, attr, out attrLen);
                if (m_nLastError == 0)
                {
                    if (attrLen != 0)
                    {
                        attr = new byte[attrLen];
                        m_nLastError = SCardGetAttrib(m_hCard, AttribId, attr, out attrLen);
                        if (m_nLastError != 0)
                        {
                            string msg = "SCardGetAttr error: " + m_nLastError;
                            throw new Exception(msg);
                        }
                    }
                }
                else
                {
                    string msg = "SCardGetAttr error: " + m_nLastError;
                    throw new Exception(msg);
                }

                return attr;
            }
            catch
            {

            }

            return null;

        }
        #endregion

        /// <summary>
        /// This function must implement a card detection mechanism.
        /// 
        /// When card insertion is detected, it must call the method CardInserted()
        /// When card removal is detected, it must call the method CardRemoved()
        /// 
        /// </summary>
        protected override void RunCardDetection(object Reader)
        {
            try
            {
                bool bFirstLoop = true;
                IntPtr hContext = IntPtr.Zero;    // Local context
                IntPtr phContext;

                phContext = Marshal.AllocHGlobal(Marshal.SizeOf(hContext));

                if (SCardEstablishContext((uint)SCOPE.User, IntPtr.Zero, IntPtr.Zero, phContext) == 0)
                {
                    hContext = Marshal.ReadIntPtr(phContext);
                    Marshal.FreeHGlobal(phContext);

                    UInt32 nbReaders = 1;
                    SCard_ReaderState[] readerState = new SCard_ReaderState[nbReaders];

                    readerState[0].m_dwCurrentState = (UInt32)CARD_STATE.UNAWARE;
                    readerState[0].m_szReader = (string)Reader;

                    UInt32 eventState;
                    UInt32 currentState = readerState[0].m_dwCurrentState;

                    // Card detection loop
                    do
                    {
                        try
                        {
                            var cardState = SCardGetStatusChange(hContext, WAIT_TIME, readerState, nbReaders);

                            if (cardState == 0)
                            {
                                eventState = readerState[0].m_dwEventState;
                                currentState = readerState[0].m_dwCurrentState;

                                // Check state
                                if (((eventState & (uint)CARD_STATE.CHANGED) == (uint)CARD_STATE.CHANGED) && !bFirstLoop)
                                {
                                    // State has changed
                                    if ((eventState & (uint)CARD_STATE.EMPTY) == (uint)CARD_STATE.EMPTY)
                                    {
                                        // There is no card, card has been removed -> Fire CardRemoved event
                                        CardRemoved((string)Reader);
                                    }

                                    if (((eventState & (uint)CARD_STATE.PRESENT) == (uint)CARD_STATE.PRESENT) &&
                                        ((eventState & (uint)CARD_STATE.PRESENT) != (currentState & (uint)CARD_STATE.PRESENT)))
                                    {
                                        // There is a card in the reader -> Fire CardInserted event
                                        CardInserted((string)Reader);
                                    }

                                    if ((eventState & (uint)CARD_STATE.ATRMATCH) == (uint)CARD_STATE.ATRMATCH)
                                    {
                                        // There is a card in the reader and it matches the ATR we were expecting-> Fire CardInserted event
                                        CardInserted((string)Reader);
                                    }
                                }

                                // The current stateis now the event state
                                readerState[0].m_dwCurrentState = eventState;

                                bFirstLoop = false;
                            }

                            Thread.Sleep(100);

                            if (m_bRunCardDetection == false)
                                break;
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                    while (true);    // Exit on request
                }
                else
                {
                    Marshal.FreeHGlobal(phContext);
                    throw new Exception("PC/SC error");
                }

                SCardReleaseContext(hContext);
            }
            catch
            {

            }
        }
	}
}