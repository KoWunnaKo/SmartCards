using Kadastr.WinClient.View;
using System;
using System.Diagnostics;
using System.Windows;

namespace Kadastr.WinClient
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
            //if (Properties.Settings.Default.WorkMode.Equals("1"))
            //{
            //    Verification.Visibility = Visibility.Hidden;
            //    Tsex.Visibility = Visibility.Visible;
            //}
            //else if (Properties.Settings.Default.WorkMode.Equals("2"))
            //{
            //    Verification.Visibility = Visibility.Visible;
            //    Tsex.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    Verification.Visibility = Visibility.Hidden;
            //    Tsex.Visibility = Visibility.Hidden;
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("Kadastr.WinClient"))
            {
                process.Kill();
            }
        }

        private void RibbonButton_Click(object sender, RoutedEventArgs e)
        {
            var strCurrentPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            var currentExecutableFolder = System.IO.Path.GetDirectoryName(strCurrentPath);

            var helpFilePath = System.IO.Path.Combine(currentExecutableFolder, "help.pdf");

            if (System.IO.File.Exists(helpFilePath))
            {
                Process.Start(helpFilePath);
            }
            else
            {
                MessageBox.Show("Foydalanuvchi uchun yordam faylini: " + helpFilePath + " dan topilmadi");
            }
        }

        private void btnGetKadastrInfo(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new KadastrInfoView();
            spPanel.Children.Add(uObject);
        }

        private void btnGetMockDataInfo(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new MockInfoView();
            spPanel.Children.Add(uObject);
        }

        private void btnPrint2Card(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new CardPrinterView();
            spPanel.Children.Add(uObject);
        }

        private void btnReadFromCard(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new ReadCardView();
            spPanel.Children.Add(uObject);
        }

        private void btnResetTool(object sender, RoutedEventArgs e)
        {
            spPanel.Children.Clear();
            var uObject = new ResetToolView();
            spPanel.Children.Add(uObject);
        }
    }
}
