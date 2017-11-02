using CardAPILib.InterfaceCL;
using GID_Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GID_Client.Views
{
    /// <summary>
    /// Interaction logic for InitCardView.xaml
    /// </summary>
    public partial class InitCardView : UserControl
    {

        public InitCardView(CardFactoryMode mode)
        {
            InitializeComponent();

            DataContext = new IDL_ViewModel(mode);
        }
    }
}
