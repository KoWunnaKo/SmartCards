using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.ViewModel.Security;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SmartCardDesc.Controls.Warehouse
{
    /// <summary>
    /// Логика взаимодействия для UcSpisaniyeOp.xaml
    /// </summary>
    public partial class UcSpisaniyeOp : UserControl
    {
        SmartCardDBEntities context = new SmartCardDBEntities();
        CollectionViewSource warehouseViewSource;

        public UcSpisaniyeOp()
        {
            InitializeComponent();

            warehouseViewSource = ((CollectionViewSource)
            (FindResource("wAREHOUSEViewSource")));

            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            warehouseViewSource.Source = from wh in context.WAREHOUSEs.ToList()
                                         where wh.QUANTITY > 0
                                         select wh;
            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void btnWithdrawal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selindex = wAREHOUSEDataGrid.SelectedIndex;

                if (warehouseViewSource.View.CurrentItem == null) return;

                var selWh = (WAREHOUSE)warehouseViewSource.View.CurrentItem;

                if (selWh == null) return;

                var whDtl = new WAREHOUSE_DTL();

                whDtl.REC_ID = -1;
                whDtl.PARENT_ID = selWh.REC_ID;
                whDtl.OP_TYPE = "1";//Withdrawal
                whDtl.CREATE_USER = LoginModel.currentUser.REC_ID;
                whDtl.STATE = 0; //Active

                if (DateTime.TryParse(dpSpisaniyeDt.Text, out _inpurdate))
                {
                    whDtl.TR_DATE = _inpurdate;
                }
                else
                {
                    whDtl.TR_DATE = DateTime.MinValue;
                }


                if (int.TryParse(txbAmount.Text, out _quantity))
                {
                    whDtl.quantity = _quantity;
                }
                else
                {
                    whDtl.quantity = 0;
                }

                var wh = context.WAREHOUSEs.ToList().FirstOrDefault(x => x.REC_ID == selWh.REC_ID);

                if (wh == null) return;

                if (wh.INIT_QUANTITY.Value > 0)
                {
                    decimal perAmount = Math.Round(wh.TOTAL_AMOUNT.Value / wh.INIT_QUANTITY.Value, 2);

                    whDtl.TR_AMOUNT = perAmount * _quantity;

                    wh.QUANTITY = wh.QUANTITY - _quantity;

                    wh.REMAINING_AMOUNT = wh.REMAINING_AMOUNT - whDtl.TR_AMOUNT;
                }

                context.WAREHOUSE_DTL.Add(whDtl);

                context.SaveChanges();

                warehouseViewSource.Source = from whx in context.WAREHOUSEs.ToList()
                                             where whx.QUANTITY > 0
                                             select whx;


                wAREHOUSEDataGrid.SelectedIndex = selindex;

                txbAmount.Text = string.Empty;

                txbStatus.Text = "Списание прошла успешно!!!";
            }
            catch(Exception ex)
            {
                txbStatus.Text = ex.Message;
            }
        }

        private DateTime _inpurdate;

        public DateTime Inputdate
        {
            get
            {
                return _inpurdate;
            }

            set
            {
                _inpurdate = value;

                //OnPropertyChanged("Inputdate");
            }
        }

        private int _quantity;

        public int Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                _quantity = value;

                //OnPropertyChanged("Quantity");
            }
        }
    }
}
