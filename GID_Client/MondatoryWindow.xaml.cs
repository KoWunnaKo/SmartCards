using CardAPILib.CardAPI;
using Iso18013Lib;
using SmartCardApi.MRZ;
using SmartCardApi.SmartCardReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

            if (fDpBirthDate == null)
            {
                this.DialogResult = false;

                return;
            }

            if (fDpExpireDate == null)
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

        DateTime BirthDate;

        DateTime ExpireDate;

        string LicenseNumber;

        private bool fReadCard()
        {
            byte[] DG1 = null;

            byte[] DG2 = null;

            byte[] DG3 = null;

            byte[] DG4 = null;

            byte[] DG5 = null;

            byte[] DGCommon = null;

            try
            {
                //var result = _controller.ReadIDLCardNext(ref DG1, ref DG2, ref DG3, ref DG4, ref DG5, ref DGCommon);

                //if (result != 0)
                //{
                //    return false;
                //}

                var mrzInfo = new MRZInfo(
                    ftxbDocNum,
                    fDpBirthDate.Value,
                    fDpExpireDate.Value
                );

                var str = string.Format("{0}{1}", "1", mrzInfo.ToString()).Substring(0, 16);

                SecuredReaderTest dd = new SecuredReaderTest();

                InpetString = str;
                //1

                //DG1 = dd.IDL_ReaderDG1(InpetString);
                //DG2 = dd.IDL_ReaderDG2(InpetString);

                //DG4 = dd.IDL_ReaderDG4(InpetString);
                //DG5 = dd.IDL_ReaderDG5(InpetString);

                //DrivingLicense DrL = new DrivingLicense("");

                //var dl = DrL.ParseReadMaterial(DG1, DG2, DG3, DG4, DG5, DGCommon);

                //BirthDate = DateTime.ParseExact(dl._driver._date_of_birth, "yyyyMMdd", CultureInfo.InvariantCulture);

                //ExpireDate = DateTime.ParseExact(dl._expire_date, "yyyyMMdd", CultureInfo.InvariantCulture);

                //LicenseNumber = dl._license_number;

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

        //[NotifyPropertyChangedInvocator]
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

        private DateTime? _DpBirthDate;

        public DateTime? fDpBirthDate
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

        private DateTime? _DpExpireDate;

        public DateTime? fDpExpireDate
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
