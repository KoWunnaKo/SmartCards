using SmartCardDesc.Controls;
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
            catch(Exception ex)
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
    }
}
