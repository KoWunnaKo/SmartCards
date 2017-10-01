using GID_Client.Views;
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

namespace GID_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEditCard_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new InitCardView();
            spPanel.Children.Add(uObject);
        }

        private void btnCertificate_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new CertificateMngView();
            spPanel.Children.Add(uObject);
        }

        private void btnUserInfolist_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new IDL_View();
            spPanel.Children.Add(uObject);
        }

        private void btnCardInfolist_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new TexPasportView();
            spPanel.Children.Add(uObject); 
            
        }
    }
}
