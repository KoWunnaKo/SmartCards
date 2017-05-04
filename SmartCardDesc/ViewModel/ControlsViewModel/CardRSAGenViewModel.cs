using SCfunctional;
using SmartCardDesc.EntityModel.EntityModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SmartCardDesc.Model;

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

            await AuditModel.InsertAuditAsync("RSAGEN_CARD",
                string.Format("user = {0} ", userId));

            IsIntermadiate = false;

            //StatusText = string.Empty;

            //Exponental = _exponental;

            //Modules = _modules;
        }

        public Task fGenRsax()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                IsIntermadiate = true;

                StatusText = "1";

                CallCardx();

                AuditModel.InsertAudit("RSAGEN_CARD",
                    string.Format("user = {0} ", userId));

                IsIntermadiate = false;

            });

            return resultTask;
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


        private string _privateN;

        private string _privateD;


        public Task CallCard()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                int returnValue = 20;
                unsafe
                {

                    uint[] exp = new uint[3];
                    uint[] modul = new uint[256];
                    uint[] privN = new uint[256];
                    uint[] privD = new uint[256];

                    fixed (uint* public_exp = exp)
                    fixed (uint* public_modul = modul)
                    fixed (uint* private_N = privN)
                    fixed (uint* private_D = privD)

                        try
                        {
                           //returnValue = RSAGeneration.generate_RSA(public_exp, public_modul);

                            returnValue = CardOperationApi.generate_RSA();

                            returnValue = CardOperationApi.get_RSA_components(public_exp, public_modul, private_N, private_D);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    if (returnValue != 0)
                    {
                        StatusText = stateList[returnValue];

                        Exponental = string.Empty;

                        Modules = string.Empty;

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

                    //if (!Exponental.Equals("1 0 1"))
                    //{
                    //    StatusText = "Error! Try again please!!!";

                    //    Exponental = string.Empty;

                    //    Modules = string.Empty;

                    //    return;
                    //}

                    string mexpStr = string.Empty;
                    string[] mexpStrArr = new string[modul.Length];
                    int mcounter = 0;

                    foreach (uint xx in modul)
                    {
                        mexpStr = xx.ToString();//"x"

                        mexpStrArr[mcounter++] = mexpStr;
                    }

                    Modules = string.Join(" ", mexpStrArr);


                    ///////////////////////////////////////////////////////

                    string StrprivN = string.Empty;
                    string[] StrprivNArr = new string[privN.Length];
                    int Ncounter = 0;

                    foreach (uint xx in privN)
                    {
                        StrprivN = xx.ToString();//"x"

                        StrprivNArr[Ncounter++] = StrprivN;
                    }

                    _privateN = string.Join(" ", StrprivNArr);

                    ConvertStr2BytePrivN(_privateN);

                    string StrprivD = string.Empty;
                    string[] StrprivDArr = new string[privD.Length];
                    int Dcounter = 0;

                    foreach (uint xx in privD)
                    {
                        StrprivD = xx.ToString();//"x"

                        StrprivDArr[Dcounter++] = StrprivD;
                    }

                    _privateD = string.Join(" ", StrprivDArr);

                    ConvertStr2BytePrivD(_privateD);
                    
                    UpdateCardKeyInfo();

                    StatusText = string.Empty;
                }
            });

            return resultTask;
        }

        public void CallCardx()
        {
                int returnValue = 20;
                unsafe
                {

                    uint[] exp = new uint[3];
                    uint[] modul = new uint[256];
                    uint[] privN = new uint[256];
                    uint[] privD = new uint[256];

                    fixed (uint* public_exp = exp)
                    fixed (uint* public_modul = modul)
                    fixed (uint* private_N = privN)
                    fixed (uint* private_D = privD)

                        try
                        {
                            //returnValue = RSAGeneration.generate_RSA(public_exp, public_modul);

                            returnValue = CardOperationApi.generate_RSA();

                            returnValue = CardOperationApi.get_RSA_components(public_exp, public_modul, private_N, private_D);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    if (returnValue != 0)
                    {
                        StatusText = stateList[returnValue];

                        Exponental = string.Empty;

                        Modules = string.Empty;

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

                    //if (!Exponental.Equals("1 0 1"))
                    //{
                    //    StatusText = "Error! Try again please!!!";

                    //    Exponental = string.Empty;

                    //    Modules = string.Empty;

                    //    return;
                    //}

                    string mexpStr = string.Empty;
                    string[] mexpStrArr = new string[modul.Length];
                    int mcounter = 0;

                    foreach (uint xx in modul)
                    {
                        mexpStr = xx.ToString();//"x"

                        mexpStrArr[mcounter++] = mexpStr;
                    }

                    Modules = string.Join(" ", mexpStrArr);


                    ///////////////////////////////////////////////////////

                    string StrprivN = string.Empty;
                    string[] StrprivNArr = new string[privN.Length];
                    int Ncounter = 0;

                    foreach (uint xx in privN)
                    {
                        StrprivN = xx.ToString();//"x"

                        StrprivNArr[Ncounter++] = StrprivN;
                    }

                    _privateN = string.Join(" ", StrprivNArr);

                    ConvertStr2BytePrivN(_privateN);

                    string StrprivD = string.Empty;
                    string[] StrprivDArr = new string[privD.Length];
                    int Dcounter = 0;

                    foreach (uint xx in privD)
                    {
                        StrprivD = xx.ToString();//"x"

                        StrprivDArr[Dcounter++] = StrprivD;
                    }

                    _privateD = string.Join(" ", StrprivDArr);

                    ConvertStr2BytePrivD(_privateD);

                    UpdateCardKeyInfo();

                    StatusText = string.Empty;
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

        private byte[] private_N_Element { get; set; }

        private byte[] private_D_Element { get; set; }

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




        private void ConvertStr2BytePrivN(string pPrivN)
        {
            if (string.IsNullOrEmpty(pPrivN))
                return;

            private_N_Element = new byte[256];

            string[] strmodArray = pPrivN.Split(' ');

            for (int i = 0; i < 256; i++)
            {
                byte each = Encoding.UTF8.GetBytes(strmodArray[i])[0];

                private_N_Element[i] = each;
            }
        }


        private void ConvertStr2BytePrivD(string pPrivD)
        {
            if (string.IsNullOrEmpty(pPrivD))
                return;

            private_D_Element = new byte[256];

            string[] strmodArray = pPrivD.Split(' ');

            for (int i = 0; i < 256; i++)
            {
                byte each = Encoding.UTF8.GetBytes(strmodArray[i])[0];

                private_D_Element[i] = each;
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
                    Card.PRIVATE_N = private_N_Element;
                    Card.PRIVATE_D = private_D_Element;

                    var user = context.USERS.ToList().FirstOrDefault(x => x.REC_ID == SelectedUser.REC_ID);

                    user.KEY_FLG = true;
                }

                context.SaveChanges();
            }
        }
    }
}
