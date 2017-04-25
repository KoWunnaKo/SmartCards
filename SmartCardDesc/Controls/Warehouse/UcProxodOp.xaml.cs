using SmartCardDesc.ViewModel.Warehouse;
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
    /// Логика взаимодействия для UcProxodOp.xaml
    /// </summary>
    public partial class UcProxodOp : UserControl
    {
        public UcProxodOp()
        {
            InitializeComponent();

            DataContext = new PrixodViewModel();
        }
    }
}
