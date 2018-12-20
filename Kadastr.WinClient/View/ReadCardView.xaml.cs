using Kadastr.WinClient.ViewModel;
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

namespace Kadastr.WinClient.View
{
    /// <summary>
    /// Interaction logic for ReadCardView.xaml
    /// </summary>
    public partial class ReadCardView : UserControl
    {
        private MainViewModel _model;

        public ReadCardView()
        {
            InitializeComponent();

            _model = new MainViewModel();

            DataContext = _model;

            var metaList = _model.GetControlsList();

            DynPanel.Children.Clear();

            foreach (var obj in metaList)
            {
                var uObject = new U_TextBoxView();
                uObject.txbLabel.Text = obj.desc;
                uObject.txbLabel.Tag = obj.code;
                //uObject.txbData.Text = obj.Default;

                DynPanel.Children.Add(uObject);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _model.fReadData();

            foreach (var child in _model.readData)
            {
                foreach (var uobj in DynPanel.Children)
                {
                    var uuobj = (U_TextBoxView)uobj;

                    if (uuobj.txbLabel.Tag.Equals(child.Key))
                    {
                        uuobj.txbData.Text = child.Value;
                    }
                }
            }
        }
    }
}
