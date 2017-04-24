using SmartCardDesc.EntityModel.EntityModel;
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

namespace SmartCardDesc.Controls.OperationLists
{
    /// <summary>
    /// Логика взаимодействия для UcUsersList.xaml
    /// </summary>
    public partial class UcUsersList : UserControl
    {
        SmartCardDBEntities context = new SmartCardDBEntities();
        CollectionViewSource usersViewSource;

        public UcUsersList()
        {
            InitializeComponent();

            usersViewSource = ((CollectionViewSource)
                (FindResource("uSERViewSource")));

            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            usersViewSource.Source = context.USERS.ToList();

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                prStatus.IsIndeterminate = true;

                await context.SaveChangesAsync();

                prStatus.IsIndeterminate = false;

                txbStatus.Text = "Данные успешно сохранены.";
            }
            catch(Exception ex)
            {
                txbStatus.Text = ex.Message;
            }
            
        }
    }
}
