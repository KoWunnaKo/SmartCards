using SmartCardDesc.ViewModel;
using SmartCardDesc.ViewModel.ControlsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartCardDesc.Controls
{
    /// <summary>
    /// Interaction logic for UcInsertCard.xaml
    /// </summary>
    public partial class UcInsertCard : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public UcInsertCard()
        {
            InitializeComponent();

            DataContext = new UcInsertCardViewModel();
        }

        private void txbUserId_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string selectedLogin = ViewModelBase.CurrentSelectedLogin;
        }
    }
}
