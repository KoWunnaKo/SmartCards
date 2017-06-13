using CardAPILib.CardAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class UcUserPinChangeViewModel : ViewModelBase
    {
        private CardApiController cardApiObj;

        public RelayCommand ChangePin { get; private set; }

        public RelayCommand RestorePin { get; private set; }

        public UcUserPinChangeViewModel()
        {
            ChangePin = new RelayCommand(_ => fChangePin());

            RestorePin = new RelayCommand(_ => fRestorePin());

            cardApiObj = new CardApiController();
        }

        private void fChangePin()
        {
            try
            {
                StatusText = string.Empty;

                if (string.IsNullOrEmpty(OldPin) || string.IsNullOrEmpty(NewPin) || string.IsNullOrEmpty(ConfirmNewPin))
                {
                    StatusText = "Please fill all fields";
                    return;
                }

                if (cardApiObj.Connect2Card() != 0)
                {
                    throw new ApplicationException("Unable to connect to Card");
                }

                //Check old pin
                cardApiObj.userPinCodeLogin(OldPin);

                //Check new and Confirm
                if (!NewPin.Equals(ConfirmNewPin))
                {
                    throw new ApplicationException("Please confirm new PIN");
                }

                //Change to new
                cardApiObj.userChangePin(NewPin);

                StatusText = string.Format("{0} {1}", "Password Changed to ", NewPin);
            }
            catch(Exception ex)
            {
                StatusText = ex.Message;
            }

        }

        private void fRestorePin()
        {
            try
            {
                StatusText = string.Empty;

                if (cardApiObj.Connect2Card() != 0)
                {
                    throw new ApplicationException("Unable to connect to Card");
                }

                cardApiObj.adminPukCodeLogin(Properties.Settings.Default.AdminPINLogin);

                cardApiObj.adminRestoreUserPin();

                StatusText = "User PIN Restored to 123456";
            }
            catch (Exception ex)
            {
                StatusText = ex.Message;
            }
        }

        private string oldPin;

        public string OldPin
        {
            get
            {
                return oldPin;
            }

            set
            {
                oldPin = value;

                OnPropertyChanged("OldPin");
            }
        }

        private string newPin;

        public string NewPin
        {
            get
            {
                return newPin;
            }

            set
            {
                newPin = value;

                OnPropertyChanged("NewPin");
            }
        }

        private string confirmnewPin;

        public string ConfirmNewPin
        {
            get
            {
                return confirmnewPin;
            }

            set
            {
                confirmnewPin = value;

                OnPropertyChanged("ConfirmNewPin");
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
    }
}
