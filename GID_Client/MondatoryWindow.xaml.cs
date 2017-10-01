using CardAPILib.CardAPI;
using Iso18013Lib;
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
        private CardApiController _controller;

        public MondatoryWindow()
        {
            InitializeComponent();

            DataContext = this;

            _controller = new CardApiController(true);
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
                var result = _controller.ReadIDLCardNext(ref DG1, ref DG2, ref DG3, ref DG4, ref DG5, ref DGCommon);

                if (result != 0)
                {
                    return false;
                }

                DrivingLicense DrL = new DrivingLicense("");

                var dl = DrL.ParseReadMaterial(DG1, DG2, DG3, DG4, DG5, DGCommon);

                BirthDate = DateTime.ParseExact(dl._driver._date_of_birth, "yyyyMMdd", CultureInfo.InvariantCulture);

                ExpireDate = DateTime.ParseExact(dl._expire_date, "yyyyMMdd", CultureInfo.InvariantCulture);

                LicenseNumber = dl._license_number;

                if (BirthDate.Equals(fDpBirthDate) && ExpireDate.Equals(fDpExpireDate) && LicenseNumber.Equals(ftxbDocNum))
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
    }
}
