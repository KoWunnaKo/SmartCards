using GID_Client.ViewModel;
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

namespace GID_Client.Views
{
    /// <summary>
    /// Interaction logic for IDL_View.xaml
    /// </summary>
    public partial class IDL_View : UserControl
    {
        private VoditelPravaViewModel _model;

        public IDL_View()
        {
            InitializeComponent();

            _model = new VoditelPravaViewModel();

            DataContext = _model;
        }

        public void Release()
        {
            _model.Release();
        }

    }
}
