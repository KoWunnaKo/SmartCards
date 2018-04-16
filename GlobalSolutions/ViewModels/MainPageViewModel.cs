using GemCard;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace GlobalSolutions
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The window ViewModel controls
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// Margin around the window to allow dropshadow
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// Window radius
        /// </summary>
        private int mWindowRadious = 10;

        /// <summary>
        /// To store the value of current page
        /// </summary>
        private ApplicationPage mCurrentPage { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// List of cards
        /// </summary>
        public ObservableCollection<CardControlViewModel> Cards { set; get; }

        /// <summary>
        /// Singletone Instance
        /// </summary>
        public static MainPageViewModel Instance;

        /// <summary>
        /// Smallest heigjht
        /// </summary>
        public double WindowMinimumHeight { get; set; }

        /// <summary>
        /// Smallest heigjht
        /// </summary>
        public double WindowMinimumWidth { get; set; }


        /// <summary>
        /// The size of the Resize border
        /// </summary>
        public int ResizeBorder { get { return Borderless ? 0 : 6; } set { } }

        /// <summary>
        /// Thickness of resize border
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        /// <summary>
        /// Inner padding of main content
        /// </summary>
        public int MainWindowInnerPadding { get; set; }

        /// <summary>
        /// Inner padding of main content
        /// </summary>
        public Thickness MainContentInnerPaddingThickness { get { return new Thickness(MainWindowInnerPadding); } }

        /// <summary>
        /// Margin around window to allow dropshadow
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return mOuterMarginSize;
            }
            set { OuterMarginSize = value; }
        }

        /// <summary>
        /// Margin around window to allow dropshadow
        /// </summary>
        public Thickness OuterMarginThickness { get { return mWindow.WindowState == WindowState.Maximized ? new Thickness(OuterMarginSize, OuterMarginSize, OuterMarginSize, 50) : new Thickness(OuterMarginSize); } }

        /// <summary>
        /// The radius of the edges
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadious;
            }
            set { WindowRadius = value; }
        }

        /// <summary>
        /// Corner radius of the edges
        /// </summary>
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// Height of the region that can drag the form
        /// </summary>
        public int TitleHeight { set; get; }

        /// <summary>
        /// Title height grid length of Title
        /// </summary>
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        /// <summary>
        /// Represents that if the window maximized it should be borderless
        /// </summary>
        public bool Borderless { get { return (mWindow.WindowState == WindowState.Maximized); } set { } }

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get { return mCurrentPage; } set { mCurrentPage = value; OnPropertyChanged("CurrentPage"); } }

        #endregion

        #region Commands

        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainPageViewModel(Window window)
        {
            mWindow = window;
            Cards = new ObservableCollection<CardControlViewModel>();
            var gc = new CardNative();
            gc.OnCardInserted += Gc_OnCardInserted;
            gc.OnCardRemoved += Gc_OnCardRemoved;
            Application.Current.Dispatcher.Invoke(() =>
            {
                Cards.Add(new CardControlViewModel { IsConnected = false, Name = "superCard" });
                Cards.Add(new CardControlViewModel { IsConnected = false, Name = "masterCard" });
                Cards.Add(new CardControlViewModel { IsConnected = false, Name = "easyCard" });
                Cards.Add(new CardControlViewModel { IsConnected = false, Name = "lowCard" });
            });
            Gc_OnCardInserted("superCard");
            Gc_OnCardInserted("easyCard");
            Gc_OnCardInserted("masterCard");
            mWindow.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(("ResizeBorder"));
                OnPropertyChanged(("ResizeBorderThickness"));
                OnPropertyChanged(("OuterMarginSize"));
                OnPropertyChanged(("OuterMarginThickness"));
                OnPropertyChanged(("WindowRadius"));
                OnPropertyChanged(("WindowCornerRadius"));

            };
            MinimizeCommand = new RelayCommand(() =>
            {
                mWindow.WindowState = WindowState.Minimized;
            });
            MaximizeCommand = new RelayCommand(() =>
            {
                mWindow.WindowState ^= WindowState.Maximized;
            });
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            WindowMinimumWidth = 400;
            TitleHeight = 42;
            MainWindowInnerPadding = 0;
            WindowMinimumHeight = 400;
            Instance = this;

        }

        #endregion

        #region Card events

        private void Gc_OnCardRemoved(string reader)
        {
            var card = (from c in Cards where c.Name == reader select c).First();
            if (card == null)
            {
                //This is fatal. This should never ever happen!!!
                //TODO: Avoid coming here || Log the error || Warn the user || Warn the programmer
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    card.IsConnected = false;
                    card.Key = string.Empty;
                    card.Name = string.Empty;
                    card.Status = string.Empty;
                });
            }
            //TODO whatever you like
        }

        private void Gc_OnCardInserted(string reader)
        {
            var card = (from c in Cards where c.Name == reader select c).First();
            switch (card.Name)
            {
                case "superCard":
                    {
                        (new Thread(() =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                card.IsConnected = true;
                            });
                            Thread.Sleep(2000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                card.IsBusy = true;
                            });
                            Thread.Sleep(2000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                card.IsSuccess = true;
                            });
                            Thread.Sleep(2000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                card.IsConnected = false;
                            });
                            Thread.Sleep(2000);

                        })).Start();
                    }
                    break;

                case "easyCard":
                    {
                        (new Thread(() =>
                        {
                            Thread.Sleep(2000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                card.IsConnected = true;
                            });
                            Thread.Sleep(2000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                card.IsBusy = true;
                            });
                            Thread.Sleep(2000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                card.IsError = true;
                            });
                            Thread.Sleep(2000);

                        })).Start();
                    }
                    break;
                case "masterCard":
                    {
                        (new Thread(() =>
                        {
                            Thread.Sleep(5000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                card.IsConnected = true;
                            });
                            Thread.Sleep(2000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                card.IsBusy = true;
                            });
                        })).Start();
                    }
                    break;
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                card.IsConnected = true;
            });
            //TODO whatever you like
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the current position of mouse
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            var positions = Mouse.GetPosition(mWindow);
            return new Point(positions.X + mWindow.Left, positions.Y + mWindow.Top);
        }

        #endregion

    }
}
