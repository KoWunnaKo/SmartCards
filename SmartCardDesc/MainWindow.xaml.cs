using SmartCardDesc.ViewModel;
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
using SmartCardDesc.Controls;

namespace SmartCardDesc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var selectedItem = tvOperations.SelectedItem as TreeViewItem;

            if (selectedItem != null)
            {
                var tag = (string)selectedItem.Tag;

                if (!tag.Equals("0"))
                {
                    stackUc.Children.Clear();

                    UIElement control = null;

                    if (tag.Equals("1"))
                    {
                        control = new UcUserInfo();
                    }
                    else if (tag.Equals("2"))
                    {
                        control = new UcCardInfo();
                    }
                    else if (tag.Equals("3"))
                    {
                        control = new UcInsertCard();
                    }
                    else if (tag.Equals("4"))
                    {
                        control = new UcUpdateCard();
                    }
                    else
                    {
                        return;
                    }
                    
                    stackUc.Children.Add(control);

                }
            }
        }
    }
}
