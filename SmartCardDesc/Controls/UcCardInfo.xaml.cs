using SmartCardDesc.ViewModel.ControlsViewModel;
using System.Windows.Controls;

namespace SmartCardDesc.Controls
{
    /// <summary>
    /// Interaction logic for UcCardInfo.xaml
    /// </summary>
    public partial class UcCardInfo : UserControl
    {
        public UcCardInfo()
        {
            InitializeComponent();

            DataContext = new UcCardInfoViewModel();
        }
    }
}
