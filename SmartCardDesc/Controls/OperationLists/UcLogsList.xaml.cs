using SmartCardDesc.EntityModel.EntityModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SmartCardDesc.Controls.OperationLists
{
    /// <summary>
    /// Interaction logic for UcLogsList.xaml
    /// </summary>
    public partial class UcLogsList : UserControl
    {
        SmartCardDBEntities context = new SmartCardDBEntities();
        CollectionViewSource logsViewSource;

        public UcLogsList()
        {
            InitializeComponent();

            logsViewSource = ((CollectionViewSource)
            (FindResource("aUDITViewSource")));

            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            logsViewSource.Source = context.AUDITs.ToList();
            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }
    }
}
