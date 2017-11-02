using GID_Client.ViewModel;
using GID_Client.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public void SetAccessRules()
        {
            if (Properties.Settings.Default.WorkMode.Equals("1"))
            {
                Verification.Visibility = Visibility.Hidden;
                Tsex.Visibility = Visibility.Visible;
            }
            else if (Properties.Settings.Default.WorkMode.Equals("2"))
            {
                Verification.Visibility = Visibility.Visible;
                Tsex.Visibility = Visibility.Hidden;
            }
            else
            {
                Verification.Visibility = Visibility.Hidden;
                Tsex.Visibility = Visibility.Hidden;
            }
        }

        private void btnEditCard_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new InitCardView(CardAPILib.InterfaceCL.CardFactoryMode.DrivingLicence);
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
            if (spPanel.Children.Count > 0)
            {
                if (spPanel.Children[0] is TexPasportView)
                {
                    TexPasportView obj = (TexPasportView)spPanel.Children[0];
                    obj.Release();
                }

                if (spPanel.Children[0] is IDL_View)
                {
                    IDL_View obj = (IDL_View)spPanel.Children[0];
                    obj.Release();
                }
            }

            spPanel.Children.Clear();
            var uObject = new IDL_View();
            spPanel.Children.Add(uObject);
        }

        private void btnCardInfolist_Click(object sender, RoutedEventArgs e)
        {
            if (spPanel.Children.Count > 0)
            {
                if (spPanel.Children[0] is IDL_View)
                {
                    IDL_View obj = (IDL_View)spPanel.Children[0];
                    obj.Release();
                }

                if (spPanel.Children[0] is TexPasportView)
                {
                    TexPasportView obj = (TexPasportView)spPanel.Children[0];
                    obj.Release();
                }
            }
            spPanel.Children.Clear();
            var uObject = new TexPasportView();
            spPanel.Children.Add(uObject); 
            
        }

        private void btnUserInfolist2_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new InitCardView(CardAPILib.InterfaceCL.CardFactoryMode.DrivingLicence);
            spPanel.Children.Add(uObject);
        }

        private void btnCardInfolist2_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new InitCardView(CardAPILib.InterfaceCL.CardFactoryMode.VehicleRegistration);
            spPanel.Children.Add(uObject);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("GID_Client"))
            {
                process.Kill();
            }
        }
    }
}
