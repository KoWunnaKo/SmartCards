using SmartCardDesc.Controls;
using SmartCardDesc.ViewModel.Security;
using System;
using System.Windows;

namespace SmartCardDesc
{
    /// <summary>
    /// Логика взаимодействия для RibbonWindow.xaml
    /// </summary>
    public partial class RibbonWindow : Window
    {
        public RibbonWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception)
            {

            }
            
        }

        private void btnUserInfo_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new UcUserInfo();
            spPanel.Children.Add(uObject);
        }

        private void btnCardInfo_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new UcCardInfo();
            spPanel.Children.Add(uObject);
        }

        private void btnAddCard_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new UcInsertCard();
            spPanel.Children.Add(uObject);
        }

        private void btnEditCard_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new UcUpdateCard();
            spPanel.Children.Add(uObject);
        }

        private void btnDeleteCard_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new UcDeleteCard();
            spPanel.Children.Add(uObject);
        }

        private void btnKeyGen_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new UcKeyGen();
            spPanel.Children.Add(uObject);
        }

        private void btnCertificate_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new UcCertificate();
            spPanel.Children.Add(uObject);
        }

        private void WriteCert_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new UcWriteCert();
            spPanel.Children.Add(uObject);
        }

        private void RibbonApplicationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //private string _fullName;

        public string FullName
        {
            get
            {
                return string.Format("Текущий пользователь: {0} {1} {2}",
                    LoginModel.currentUser.SURNAME_NAME ,
                    LoginModel.currentUser.FIRST_NAME,
                    LoginModel.currentUser.MIDDLE_NAME);
            }
        }

        private void btnCardPrint_Click(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new UcPrintCard();
            spPanel.Children.Add(uObject);
        }
    }
}
