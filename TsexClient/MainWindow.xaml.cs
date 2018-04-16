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
using TsexClient.TsexLogic;
using TsexClient.UserControls;

namespace TsexClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CardProcessController ctr { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ctr = new CardProcessController();

            ctr.StartPros();

            foreach(var reader in ctr.terminalsList)
            {
                var uObject = new TerminalControl();

                uObject.txtNumber.Text = reader.terminalName[reader.terminalName.Length - 1].ToString();
                uObject.tbxName.Text = reader.terminalName;
                sp_general.Children.Add(uObject);
            }
        }
    }
}
