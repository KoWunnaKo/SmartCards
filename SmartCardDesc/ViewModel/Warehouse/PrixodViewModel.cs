using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.ViewModel.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.ViewModel.Warehouse
{
    internal class PrixodViewModel : ViewModelBase
    {
        public RelayCommand InputItem { get; private set; }

        public PrixodViewModel()
        {
            InputItem = new RelayCommand(_ => fInputItem());

            Inputdate = DateTime.Now;
        }

        private async void fInputItem()
        {
            IsIntermadiate = true;

            await EnterWarehouse();

            IsIntermadiate = false;

            StatusText = "Товар удачно оприходован!!!";

            Clear();
        }

        private void Clear()
        {
            Inputdate = DateTime.MinValue;
            Amount = 0;
            Quantity = 0;
            Name = string.Empty;
        }

        private Task EnterWarehouse()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                using (var context = new SmartCardDBEntities())
                {
                    var wh = new WAREHOUSE();

                    wh.REC_ID = -1;
                    wh.INPUT_DT = Inputdate;
                    wh.ITEN_NAME = Name;
                    wh.QUANTITY = Quantity;
                    wh.INIT_QUANTITY = Quantity;
                    wh.TOTAL_AMOUNT = Amount;
                    wh.REMAINING_AMOUNT = Amount;

                    var whDtl = new WAREHOUSE_DTL();

                    whDtl.CREATE_USER = LoginModel.currentUser.REC_ID;
                    whDtl.OP_TYPE = "0";//Incoming Transaction
                    whDtl.PARENT_ID = -1;
                    whDtl.STATE = 0; //Active
                    whDtl.TR_AMOUNT = Amount;
                    whDtl.TR_DATE = Inputdate;
                    whDtl.quantity = Quantity;

                    wh.WAREHOUSE_DTL.Add(whDtl);

                    context.WAREHOUSEs.Add(wh);

                    context.SaveChanges();
                }
            });

            return resultTask;
         }

        private string _statusText;

        public string StatusText
        {
            get
            {
                return _statusText;
            }

            set
            {
                _statusText = value;

                OnPropertyChanged("StatusText");
            }
        }

        private bool _isIntermadiate;

        public bool IsIntermadiate
        {
            get
            {
                return _isIntermadiate;
            }

            set
            {
                _isIntermadiate = value;

                OnPropertyChanged("IsIntermadiate");
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

                OnPropertyChanged("Inputdate");
            }
        }

        private decimal _amount;

        public decimal Amount
        {
            get
            {
                return _amount;
            }

            set
            {
                _amount = value;

                OnPropertyChanged("Amount");
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

                OnPropertyChanged("Quantity");
            }
        }

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;

                OnPropertyChanged("Name");
            }
        }
    }
}
