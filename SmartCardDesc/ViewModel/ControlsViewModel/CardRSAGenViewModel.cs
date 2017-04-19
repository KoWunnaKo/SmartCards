using SCfunctional;
using SmartCardDesc.EntityModel.EntityModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace SmartCardDesc.ViewModel.ControlsViewModel
{
    internal class CardRSAGenViewModel : ViewModelBase
    {
        public Dictionary<int, string> stateList;
        public RelayCommand GetRsa { get; private set; }

        public CardRSAGenViewModel()
        {
            GetRsa = new RelayCommand(_ => fGenRsa());

            stateList = new Dictionary<int, string>();

            stateList.Add(0, "Success!");
            stateList.Add(-1, "Reader is not in groups");
            stateList.Add(-2, "Failed SCardListReaders");
            stateList.Add(-3, "SCard isn't connected to reader");
            stateList.Add(-4, "Smartcard is defective");
            stateList.Add(-5, "RSA Generating applet is not installed in SCard");
            stateList.Add(-6, "Error! Smth get wrong during the clearing process!");
            stateList.Add(-7, "Error! Unable to open data space!");
            stateList.Add(-8, "Error! Unable to create RSA pair!");
            stateList.Add(-9, "Error! Unable to put data into array!");
            stateList.Add(-10, "Error! There's no data in array!");
            stateList.Add(-11, "Error! Data couldn't be cleared!");
            stateList.Add(-12, "Error! Context couldn't be released!");
        }

        private async void fGenRsa()
        {
            IsIntermadiate = true;

            StatusText = "Генерация ключа...";

            await CallCard();

            IsIntermadiate = false;

            StatusText = string.Empty;

            //Exponental = _exponental;

            Modules = _modules;
        }

        public string _exponental;

        public string Exponental
        {
            get
            {
                return _exponental;
            }
            set
            {
                _exponental = value;

                ConvertStr2ByteExponent(_exponental);

                OnPropertyChanged("Exponental");
            }
        }

        public string _modules;

        public string Modules
        {
            get
            {
                return _modules;
            }
            set
            {
                _modules = value;

                ConvertStr2ByteModulus(_modules);

                OnPropertyChanged("Modules");
            }
        }

        public Task CallCard()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                int returnValue = 20;
                unsafe
                {

                    uint[] exp = new uint[3];
                    uint[] modul = new uint[256];

                    fixed (uint* public_exp = exp)
                    fixed (uint* public_modul = modul)

                        try
                        {
                           returnValue = RSAGeneration.generate_RSA(public_exp, public_modul);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    if (returnValue != 0)
                    {
                        StatusText = stateList[returnValue];

                        return;
                    }

                    string expStr = string.Empty;
                    string[] expStrArr = new string[exp.Length];
                    int counter = 0;

                    foreach (uint xx in exp)
                    {
                        expStr = xx.ToString();//"x"

                        expStrArr[counter++] = expStr;
                    }

                    Exponental = string.Join(" ", expStrArr);

                    if (!Exponental.Equals("1 0 1"))
                    {
                        StatusText = "Error! Try again please!!!";

                        return;
                    }

                    string mexpStr = string.Empty;
                    string[] mexpStrArr = new string[modul.Length];
                    int mcounter = 0;

                    foreach (uint xx in modul)
                    {
                        mexpStr = xx.ToString();//"x"

                        mexpStrArr[mcounter++] = mexpStr;
                    }

                    Modules = string.Join(" ", mexpStrArr);

                    UpdateCardKeyInfo();
                }
            });

            return resultTask;
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

        private USER _selectedUser;

        public USER SelectedUser
        {
            get
            {
                return _selectedUser;
            }

            set
            {
                _selectedUser = value;

                UserId = _selectedUser.LOGIN;

                OnPropertyChanged("SelectedUser");
            }
        }

        private List<USER> _usersList;

        public List<USER> UsersList
        {
            get
            {
                try
                {
                    using (var context = new SmartCardDBEntities())
                    {
                        _usersList = context.USERS.ToList();
                    }

                }
                catch (Exception)
                {

                }

                if (_usersList == null)
                {
                    _usersList = new List<USER>();
                }

                return _usersList;
            }
        }

        private string userId;

        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;

                OnPropertyChanged("UserId");
            }
        }

        private byte[] exponent { get; set; }

        private byte[] modulus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pexponent"></param>
        private void ConvertStr2ByteExponent(string pexponent)
        {
            if (string.IsNullOrEmpty(pexponent))
                return;

            exponent = new byte[3];

            string[] strexpArray = pexponent.Split(' ');

            for (int i= 0; i < 3; i++)
            {
                byte each = Encoding.UTF8.GetBytes(strexpArray[i])[0];

                exponent[i] = each;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pmodulus"></param>
        private void ConvertStr2ByteModulus(string pmodulus)
        {
            if (string.IsNullOrEmpty(pmodulus))
                return;

            modulus = new byte[256];

            string[] strmodArray = pmodulus.Split(' ');

            for (int i = 0; i < 256; i++)
            {
                byte each = Encoding.UTF8.GetBytes(strmodArray[i])[0];

                modulus[i] = each;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateCardKeyInfo()
        {
            if ((exponent == null) || (modulus == null) || (SelectedUser ==null))
                return;

            using (var context = new SmartCardDBEntities())
            {
                var Card = context.CARD_INFO.ToList().FirstOrDefault(x => x.OWNER_USER == SelectedUser.REC_ID &&
                x.IS_ACTIVE.Value);

                if (Card != null)
                {
                    Card.EXPONENT = exponent;
                    Card.MODULUS = modulus;
                }

                context.SaveChanges();
            }
        }
    }
}
