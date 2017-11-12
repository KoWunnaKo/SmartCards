using CardAPILib.CardAPI;
using Iso18013Lib;
using SmartCardApi.SmartCardReader;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace GID_Client
{
    /// <summary>
    /// Interaction logic for VLDataChecker.xaml
    /// </summary>
    public partial class VLDataChecker : Window
    {

        public string InpetString = string.Empty;

        private CardApiController _controller;

        public VLDataChecker()
        {
            InitializeComponent();

            //this.Owner = App.Current.MainWindow;

            DataContext = this;

            _controller = new CardApiController(false);

            txbDocNum.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ftxbDocNum))
            {
                this.DialogResult = false;

                return;
            }
            else
            {
                if (ftxbDocNum.Length != 8)
                {
                    this.DialogResult = false;

                    return;
                }
            }

            if (string.IsNullOrEmpty(fDpIssueDate))
            {
                this.DialogResult = false;

                return;
            }
            else
            {
                if (fDpIssueDate.Length != 10)
                {
                    this.DialogResult = false;

                    return;
                }
            }

            if (string.IsNullOrEmpty(ftxbDocNum2))
            {
                this.DialogResult = false;

                return;
            }
            else
            {
                if (ftxbDocNum2.Length != 9)
                {
                    this.DialogResult = false;

                    return;
                }
            }

            if (!fReadCard())
            {
                this.DialogResult = false;

                return;
            }

            this.DialogResult = true;
        }

        private bool fReadCard()
        {
            byte[] Vr = null;

            try
            {
                fDpIssueDate = fDpIssueDate.Replace(',', '.');

                var str = string.Format("{0}{1}{2}{3}", "1", ftxbDocNum, DateTime.ParseExact(fDpIssueDate, "dd.MM.yyyy", CultureInfo.CurrentCulture).ToString("yyyyMMdd"), ftxbDocNum2).Substring(0, 16);

                InpetString = str;

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private bool ftxbDocNumValidate = false;
        private bool fDpIssueDateValidate = false;
        private bool ftxbDocNum2Validate = false;

        private string _txbDocNum;

        public string ftxbDocNum
        {
            get
            {
                return _txbDocNum;
            }

            set
            {
                _txbDocNum = value;

                ftxbDocNumValidate = txbDocNum.IsMaskFull;

                CheckReadyness();

                OnPropertyChanged("ftxbDocNum");
            }
        }

        private string _DpIssueDate;

        public string fDpIssueDate
        {
            get
            {
                return _DpIssueDate;
            }

            set
            {
                _DpIssueDate = value;

                if (!string.IsNullOrEmpty(_DpIssueDate))
                {
                    if (_DpIssueDate.Length == 8)
                        _DpIssueDate = _DpIssueDate.Insert(2, ",").Insert(5, ",");
                }

                fDpIssueDateValidate = DpBirthDate.IsMaskFull;

                CheckReadyness();

                OnPropertyChanged("fDpIssueDate");
            }
        }


        private string _txbDocNum2;

        public string ftxbDocNum2
        {
            get
            {
                return _txbDocNum2;
            }

            set
            {
                _txbDocNum2 = value;

                ftxbDocNum2Validate = txbGuvohnoma.IsMaskFull;

                CheckReadyness();

                OnPropertyChanged("ftxbDocNum2");
            }
        }

        private void CheckReadyness()
        {
            if (ftxbDocNumValidate && fDpIssueDateValidate && ftxbDocNum2Validate)
            {
                Ready2Read = true;
                btnStart.IsEnabled = true;
            }
            else
            {
                Ready2Read = false;
            }
        }


        private bool _ready2read;

        public bool Ready2Read
        {
            get
            {
                return _ready2read;
            }

            set
            {
                _ready2read = value;

                OnPropertyChanged("Ready2Read");
            }
        }

        private void txbDocNum_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key == System.Windows.Input.Key.Enter) || (e.Key == System.Windows.Input.Key.Tab))
            {
                if (txbDocNum.IsMaskFull)
                {
                    DpBirthDate.Focus();
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void DpBirthDate_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key == System.Windows.Input.Key.Enter) || (e.Key == System.Windows.Input.Key.Tab))
            {
                if (DpBirthDate.IsMaskFull)
                {
                    txbGuvohnoma.Focus();
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txbGuvohnoma_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key == System.Windows.Input.Key.Enter) || (e.Key == System.Windows.Input.Key.Tab))
            {
                if (txbGuvohnoma.IsMaskFull && ftxbDocNumValidate && fDpIssueDateValidate)
                {
                    txbDocNum.Focus();
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}
