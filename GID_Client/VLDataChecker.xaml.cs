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

            if (fDpIssueDate == null)
            {
                this.DialogResult = false;

                return;
            }

            if (string.IsNullOrEmpty(ftxbDocNum2))
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
            byte[] Vr = null;

            try
            {

                var str = string.Format("{0}{1}{2}{3}", "1", ftxbDocNum, fDpIssueDate.Value.ToString("yyyyMMdd"), ftxbDocNum2).Substring(0, 16);

                InpetString = str;

                SecuredReaderTest dd = new SecuredReaderTest();

                Vr = dd.VR_Reader(str);

                VehicleRegistration vl = new VehicleRegistration("");

                var vll = vl.ParseReadMaterial(Vr);

                var Issue_date = DateTime.ParseExact(vl._vehicleRegistration._issue_date, "yyyyMMdd", CultureInfo.InvariantCulture);  //************


                var Vehicle1_reg_number = vl._vehicleRegistration._vehicle._reg_number; //*******


                var Vehicle1_special_marks = vl._vehicleRegistration._license_number; //************


                if (ftxbDocNum.Equals(Vehicle1_reg_number) && fDpIssueDate.Equals(Issue_date) && Vehicle1_special_marks.Equals(ftxbDocNum2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        private DateTime? _DpIssueDate;

        public DateTime? fDpIssueDate
        {
            get
            {
                return _DpIssueDate;
            }

            set
            {
                _DpIssueDate = value;

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

                OnPropertyChanged("ftxbDocNum2");
            }
        }
    }
}
