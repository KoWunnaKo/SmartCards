using SCfunctional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            Exponental = _exponental;

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

                    uint[] exp = new uint[256];
                    uint[] modul = new uint[256];

                    fixed (uint* public_exp = exp)
                    fixed (uint* public_modul = modul)

                        try
                        {
                           returnValue = RSAGeneration.generate_RSA(public_exp, public_modul);

                            //returnValue = RSAGeneration.getRSAPrivateComponents(public_exp, public_modul);

                            //RSAGeneration.getRSAPublicComponents()
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    if (returnValue != 0)
                    {
                        Exponental = stateList[returnValue];

                        return;
                    }

                    string expStr = string.Empty;
                    string[] expStrArr = new string[exp.Length];
                    int counter = 0;

                    foreach (uint xx in exp)
                    {
                        expStr = xx.ToString("x");

                        expStrArr[counter++] = expStr;
                    }

                    Exponental = string.Join(" ", expStrArr);

                    if (!Exponental.Equals("1 0 1"))
                    {
                        StatusText = "Error! Try again please!!!";

                        Exponental = StatusText;

                        return;
                    }

                    string mexpStr = string.Empty;
                    string[] mexpStrArr = new string[modul.Length];
                    int mcounter = 0;

                    foreach (uint xx in modul)
                    {
                        mexpStr = xx.ToString("x");

                        mexpStrArr[mcounter++] = mexpStr;
                    }

                    Modules = string.Join(" ", mexpStrArr);
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
    }
}
