using SmartCardDesc.EntityModel.EntityModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartCardDesc.Controls.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для UcCurrentWarehouse.xaml
    /// </summary>
    public partial class UcCurrentWarehouse : UserControl
    {
        SmartCardDBEntities context = new SmartCardDBEntities();
        CollectionViewSource warehouseViewSource;

        public UcCurrentWarehouse()
        {
            InitializeComponent();

            warehouseViewSource = ((CollectionViewSource)
            (FindResource("wAREHOUSEViewSource")));

            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            warehouseViewSource.Source = context.WAREHOUSEs.ToList();
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
