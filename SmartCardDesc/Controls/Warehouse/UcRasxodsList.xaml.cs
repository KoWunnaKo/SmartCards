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
    /// Логика взаимодействия для UcRasxodsList.xaml
    /// </summary>
    public partial class UcRasxodsList : UserControl
    {
        SmartCardDBEntities context = new SmartCardDBEntities();
        CollectionViewSource warehouseViewSource;

        public UcRasxodsList()
        {
            InitializeComponent();

            warehouseViewSource = ((CollectionViewSource)
            (FindResource("wAREHOUSE_DTLViewSource")));

            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            warehouseViewSource.Source = from wh in context.WAREHOUSE_DTL.ToList()
                                         where wh.OP_TYPE.Equals("1")
                                         select wh;
        }
    }
}
