using CardAPILib.CardAPI;
using SmartCardApi.MRZ;
using SmartCardApi.SmartCardReader;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace GID_Client
{
    /// <summary>
    /// Interaction logic for MondatoryWindow.xaml
    /// </summary>
    public partial class MondatoryWindow : Window
    {
        public string InpetString = string.Empty;

        private CardApiController _controller;

        public MondatoryWindow()
        {
            InitializeComponent();

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

            if (string.IsNullOrEmpty(fDpBirthDate))
            {
                this.DialogResult = false;

                return;
            }

            if (string.IsNullOrEmpty(fDpExpireDate))
            {
                this.DialogResult = false;

                return;
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
            try
            {
                fDpBirthDate = fDpBirthDate.Replace(',', '.');
                fDpExpireDate = fDpExpireDate.Replace(',', '.');

                var mrzInfo = new MRZInfo(
                    ftxbDocNum,
                    DateTime.ParseExact(fDpBirthDate, "dd.MM.yyyy", CultureInfo.CurrentCulture),
                    DateTime.ParseExact(fDpExpireDate, "dd.MM.yyyy", CultureInfo.CurrentCulture)
                );

                var str = string.Format("{0}{1}", "1", mrzInfo.ToString()).Substring(0, 16);

                InpetString = str;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

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

                OnPropertyChanged("ftxbDocNum");
            }
        }

        private string _DpBirthDate;

        public string fDpBirthDate
        {
            get
            {
                return _DpBirthDate;
            }

            set
            {
                _DpBirthDate = value;

                OnPropertyChanged("fDpBirthDate");
            }
        }

        private string _DpExpireDate;

        public string fDpExpireDate
        {
            get
            {
                return _DpExpireDate;
            }

            set
            {
                _DpExpireDate = value;

                OnPropertyChanged("fDpExpireDate");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void DatePicker_TextChanged(object sender, RoutedEventArgs e)
        {
            //DateTime dt;
            //DatePicker dp = (sender as DatePicker);
            //string currentText = (e.OriginalSource as TextBox).Text;
            //if (!DateTime.TryParse(currentText, out dt))
            //{
            //    try
            //    {
            //        string month = currentText.Substring(0, 2);
            //        string day = currentText.Substring(2, 2);
            //        string year = currentText.Substring(4, 4);

            //        dt = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
            //        dp.SelectedDate = dt;
            //    }
            //    catch (Exception ex)
            //    {
            //        dp.SelectedDate = null;
            //    }
            //}

            DatePicker dp = (sender as DatePicker);
            
        }

        private void DpBirthDate_TextInput(object sender, RoutedEventArgs e)
        {
            //DatePicker dp = (sender as DatePicker);
            //dp.Text = string.Empty;
        }

        private void DpBirthDate_GotFocus(object sender, RoutedEventArgs e)
        {
            //DatePicker dp = (sender as DatePicker);
            //dp.Text = string.Empty;
        }

        private void DpBirthDate_KeyDown(object sender, RoutedEventArgs e)
        {
            //DatePicker dp = (sender as DatePicker);
            //dp.Text = string.Empty;
        }
    }
}
