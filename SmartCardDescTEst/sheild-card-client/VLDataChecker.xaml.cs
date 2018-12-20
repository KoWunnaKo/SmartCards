using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace GID_Client
{
    /// <summary>
    /// Interaction logic for VLDataChecker.xaml
    /// </summary>
    public partial class VLDataChecker : Window
    {

        public string InpetString = string.Empty;

        private DispatcherTimer timer;
        private DispatcherTimer timer2;

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
            timer.IsEnabled = false;
            timer2.Stop();
            timer2.IsEnabled = false;
            StartReading();
        }

        private int counter = 0;

        private void OnTimedEvent2(object sender, EventArgs e)
        {
            counter++;
            btnStart.Content = string.Format("Boshlash ({0})", counter);
        }

        public VLDataChecker()
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
                    //if (ftxbDocNum.Length != 8)
                    //{
                    //    this.DialogResult = false;

                    //    return;
                    //}
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
                    if (ftxbDocNum2.Length != 10)
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

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartReading();
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
            //this.DialogResult = false;

            ftxbDocNum = string.Empty;
            txbDocNum.Text = string.Empty;

            fDpIssueDate = string.Empty;
            DpBirthDate.Text = string.Empty;

            ftxbDocNum2 = string.Empty;
            txbGuvohnoma.Text = string.Empty;

            txbDocNum.Focus();
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

                if (txbDocNum.Text.Length >= 8 && txbDocNum.Text.Length <=10)
                {
                    ftxbDocNumValidate = true;
                }
                //ftxbDocNumValidate = txbDocNum.IsMaskFull;

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
                if (true)
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

        private void txbDocNum_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txbDocNum.IsMaskFull && ftxbDocNum2Validate && fDpIssueDateValidate)
            {
                DpBirthDate.Focus();
                btnStart.IsEnabled = true;
                ActivateTimer();
            }
        }

        private void DpBirthDate_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (DpBirthDate.IsMaskFull && ftxbDocNum2Validate && ftxbDocNumValidate)
            {
                txbGuvohnoma.Focus();
                btnStart.IsEnabled = true;
                ActivateTimer();
            }
        }

        private void txbGuvohnoma_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txbGuvohnoma.IsMaskFull && fDpIssueDateValidate && ftxbDocNumValidate)
            {
                txbDocNum.Focus();
                btnStart.IsEnabled = true;
                ActivateTimer();
            }
        }

        public bool isClosed = false;

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //bool wasCodeClosed = new StackTrace().GetFrames().FirstOrDefault(x => x.GetMethod() == typeof(Window).GetMethod("Close")) != null;
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
