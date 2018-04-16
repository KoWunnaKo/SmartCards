using SmartCardApi.MRZ;
using SmartCardApi.SmartCardReader;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GID_Client
{
    /// <summary>
    /// Interaction logic for MondatoryWindow.xaml
    /// </summary>
    public partial class MondatoryWindow : Window
    {
        public string InpetString = string.Empty;

        private DispatcherTimer timer;
        private DispatcherTimer timer2;

        public MondatoryWindow()
        {
            InitializeComponent();

            DataContext = this;
            isClosed = false;

            txbDocNum.Focus();
        }

        private void StartReading()
        {
            try
            {
                if (string.IsNullOrEmpty(ftxbDocNum))
                {
                    this.DialogResult = false;

                    return;
                }
                else
                {
                    if (ftxbDocNum.Length != 9)
                    {
                        this.DialogResult = false;

                        return;
                    }
                }


                if (string.IsNullOrEmpty(fDpBirthDate))
                {
                    this.DialogResult = false;

                    return;
                }
                else
                {
                    if (fDpBirthDate.Length != 10)
                    {
                        this.DialogResult = false;

                        return;
                    }
                }

                if (string.IsNullOrEmpty(fDpExpireDate))
                {
                    this.DialogResult = false;

                    return;
                }
                else
                {
                    if (fDpExpireDate.Length != 10)
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
            catch
            {
                //
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartReading();
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
            //this.DialogResult = false;
            ftxbDocNum = string.Empty;
            fDpBirthDate = string.Empty;
            fDpExpireDate = string.Empty;

            txbDocNum.Text = string.Empty;
            DpBirthDate.Text = string.Empty;
            DpExpireDate.Text = string.Empty;

            txbDocNum.Focus();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool ftxbDocNumValidate = false;
        private bool fDpBirthDateValidate = false;
        private bool fDpExpireDateValidate = false;

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

                if (!string.IsNullOrEmpty(_DpBirthDate))
                {
                    if (_DpBirthDate.Length == 8)
                        _DpBirthDate = _DpBirthDate.Insert(2, ",").Insert(5, ",");
                }

                fDpBirthDateValidate = DpBirthDate.IsMaskFull;

                CheckReadyness();

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

                if (!string.IsNullOrEmpty(_DpExpireDate))
                {
                    if (_DpExpireDate.Length == 8)
                        _DpExpireDate = _DpExpireDate.Insert(2, ",").Insert(5, ",");
                }

                fDpExpireDateValidate = DpExpireDate.IsMaskFull;

                CheckReadyness();

                OnPropertyChanged("fDpExpireDate");
            }
        }

        private void CheckReadyness()
        {
            if (ftxbDocNumValidate && fDpBirthDateValidate && fDpExpireDateValidate)
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void DatePicker_TextChanged(object sender, RoutedEventArgs e)
        {

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

        private void DpBirthDate_KeyDown_1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key == System.Windows.Input.Key.Enter) || (e.Key == System.Windows.Input.Key.Tab))
            {
                if (DpBirthDate.IsMaskFull)
                {
                    DpExpireDate.Focus();
                }
                else
                {
                    e.Handled = true;
                }
            }

        }

        private void DpExpireDate_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key == System.Windows.Input.Key.Enter) || (e.Key == System.Windows.Input.Key.Tab))
            {
                if (DpExpireDate.IsMaskFull && ftxbDocNumValidate && fDpBirthDateValidate)
                {
                    txbDocNum.Focus();
                }
                else
                {
                    e.Handled = true;
                } 
            }

        }

        private void txbDocNum_LostFocus(object sender, RoutedEventArgs e)
        {
            //var check = txbDocNum.Text.TrimEnd('_');

            //if (check.Length < 9)
            //{
            //    e.Handled = true;
            //}
        }

        private void ActivateTimer()
        {
            timer = new DispatcherTimer(DispatcherPriority.SystemIdle);
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = TimeSpan.FromMilliseconds(7000);
            timer.Start();

            timer2 = new DispatcherTimer(DispatcherPriority.SystemIdle);
            timer2.Tick += new EventHandler(OnTimedEvent2);
            timer2.Interval = TimeSpan.FromMilliseconds(1000);
            timer2.Start();
        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            timer.Stop();
            timer2.Stop();
            StartReading();
        }

        private int counter = 0;

        private void OnTimedEvent2(object sender, EventArgs e)
        {
            counter++;
            btnStart.Content = string.Format("Boshlash ({0})", counter);
        }

        private void DpExpireDate_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (DpExpireDate.IsMaskFull && ftxbDocNumValidate && fDpBirthDateValidate)
            {
                txbDocNum.Focus();
                btnStart.IsEnabled = true;
                ActivateTimer();

            }
        }

        private void txbDocNum_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (fDpExpireDateValidate && txbDocNum.IsMaskFull && fDpBirthDateValidate)
            {
                DpBirthDate.Focus();
                btnStart.IsEnabled = true;
                ActivateTimer();
            }
        }

        private void DpBirthDate_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (fDpExpireDateValidate && ftxbDocNumValidate && DpBirthDate.IsMaskFull)
            {
                DpExpireDate.Focus();
                btnStart.IsEnabled = true;
                ActivateTimer();
            }
        }

        public bool isClosed = false;

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //bool wasCodeClosed = new StackTrace().GetFrames().FirstOrDefault(x => x.GetMethod() == typeof(MondatoryWindow).GetMethod("Close")) != null;
            //if (wasCodeClosed)
            //{
            //    isClosed = true;
            //    this.DialogResult = false;
            //}
            //else
            //{
            //    // Closed some other way.
            //}

            //base.OnClosing(e);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            isClosed = true;
            this.DialogResult = false;
        }
    }
}
