using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using SmartCardDesc.EntityModel.EntityModel;
using System.Data.Entity;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;

namespace SmartCardDesc.Controls
{
    /// <summary>
    /// Interaction logic for UcPrintCard.xaml
    /// </summary>
    public partial class UcPrintCard : UserControl
    {
        public PrinterView newPrinterElement { get; set; }

        SmartCardDBEntities context = new SmartCardDBEntities();
        CollectionViewSource printerViewSource;

        public UcPrintCard()
        {
            InitializeComponent();

            newPrinterElement = new PrinterView();
            printerViewSource = ((CollectionViewSource)
                (FindResource("printerViewViewSource")));

            DataContext = this;

            //printerViewViewSource
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            context.PrinterViews.Load();

            printerViewSource.Source = context.PrinterViews.AsNoTracking().ToList().Distinct();

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void btnPrinter_Click(object sender, RoutedEventArgs e)
        {

           var Executablepath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            var FullPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Executablepath), "Printer","myTemplate.mds");

            FileInfo file = new FileInfo(FullPath);

            if (file.Exists)
            {
                Process.Start(FullPath);
            }
            else
            {
                StatusText = "Файл шаблон не найден";
            }
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var index = printerViewDataGrid.SelectedIndex;

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    var card = context.CARD_INFO.ToList().FirstOrDefault(x => x.REC_ID == ((PrinterView)printerViewSource.View.CurrentItem).CARD_ID);

                    if (card != null)
                    {
                        FileInfo file = new FileInfo(openFileDialog.FileName);

                        var destFilePath = Path.Combine(Properties.Settings.Default.CardPhotoPath,
                            string.Format("{0}.{1}", card.CARD_NUMBER, file.Extension));

                        file.CopyTo(destFilePath,true);

                        card.PICTURE_PATH = destFilePath;
                    }

                    context.SaveChanges();

                    printerViewSource.Source = context.PrinterViews.AsNoTracking().ToList();

                    printerViewSource.View.Refresh();

                    printerViewDataGrid.SelectedIndex = index;
                }
            }
            catch(Exception ex)
            {
                StatusText = ex.Message;
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var index = printerViewDataGrid.SelectedIndex;

                var card = context.CARD_INFO.ToList().FirstOrDefault(x => x.REC_ID == ((PrinterView)printerViewSource.View.CurrentItem).CARD_ID);

                if (card != null)
                {
                    card.IS_PRINTED = true;
                }

                context.SaveChanges();

                printerViewSource.Source = context.PrinterViews.AsNoTracking().ToList();

                printerViewSource.View.Refresh();

                printerViewDataGrid.SelectedIndex = index;
            }
            catch(Exception ex)
            {
                StatusText = ex.Message;
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

                //OnPropertyChanged(new DependencyPropertyChangedEventArgs());
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

                txbStatus.Text = _statusText;
                //OnPropertyChanged(new DependencyPropertyChangedEventArgs());
            }
        }
    }
}
