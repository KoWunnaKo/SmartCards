
using System.Windows.Input;

namespace GlobalSolutions
{
    public class CardControlViewModel : BaseViewModel
    {
        #region Private members
        private bool mIsBusy;
        private bool mIsConnected;
        private bool mIsError;
        private bool mIsSuccess = false;
        private string mName;
        private string mCardName;
        private string mStatus;
        private string mKey;
        #endregion

        #region Public properties
        /// <summary>
        /// The flag to indicate whether it is busy or not
        /// </summary>
        public bool IsBusy
        {
            set
            {
                mIsBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
            get { return mIsBusy; }
        }
        /// <summary>
        /// The flag to indicate whether the card is connected or not
        /// </summary>
        public bool IsConnected
        {
            set
            {
                mIsConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
            get { return mIsConnected; }
        }
        /// <summary>
        /// The flag to indicate whether there is an error or not
        /// </summary>
        public bool IsError
        {
            set
            {
                mIsError = value;
                if (mIsError)
                    IsBusy = false;
                OnPropertyChanged(nameof(IsError));
            }
            get { return mIsError; }
        }
        /// <summary>
        /// Flag indicating if it is busy or not
        /// </summary>
        public bool IsSuccess
        {
            set
            {
                mIsSuccess = value;
                if (mIsSuccess)
                    IsBusy = false;
                OnPropertyChanged(nameof(IsSuccess));
            }
            get { return mIsSuccess; }
        }
        /// <summary>
        /// Card name
        /// </summary>
        public string CardName
        {
            set
            {
                mCardName = value;
                OnPropertyChanged(nameof(CardName));
            }
            get { return mCardName; }
        }
        /// <summary>
        /// Name of the card
        /// </summary>
        public string Name
        {
            set
            {
                mName = value;
                OnPropertyChanged(nameof(Name));
            }
            get { return mName; }
        }
        /// <summary>
        /// Status of the card
        /// </summary>
        public string Status
        {
            set
            {
                mStatus = value;
                OnPropertyChanged(nameof(Status));
            }
            get { return mStatus; }
        }
        /// <summary>
        /// Key of the card
        /// </summary>
        public string Key
        {
            set
            {
                mKey = value;
                OnPropertyChanged(nameof(Key));
            }
            get { return mKey; }
        }
        /// <summary>
        /// Command to turn <see cref="IsSuccess"/> back
        /// </summary>
        public ICommand OkCommand { set; get; }
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CardControlViewModel()
        {
            OkCommand = new RelayCommand(() =>
            {
                this.IsSuccess = false;
                this.IsError = false;
            });
            OnPropertyChanged(nameof(OkCommand));
        }
        #endregion
    }
}
