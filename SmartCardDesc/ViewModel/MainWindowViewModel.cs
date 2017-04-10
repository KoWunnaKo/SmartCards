using SmartCardDesc.Db;
using SmartCardDesc.InfocomService;
using SmartCardDesc.ViewModel.ControlsViewModel;
using System;
using System.Windows;

namespace SmartCardDesc.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand OpenUchetTs { get; private set; }

        public RelayCommand OpenServiceIA { get; private set; }

        public RelayCommand OpenKeyGen { get; private set; }

        public EpiService api;

        public MainWindowViewModel()
        {
            try
            {
                DbModel.db = DbConnection.GetInstance(ConnectionProperty.Default.GetConnectionString());

                DbModel.dataSetSc = new EntityModel.SmartCardDs();
            }
            catch(Exception ex)
            {
                Status = ex.Message;
            }
            
            OpenUchetTs = new RelayCommand(_ => fOpenUchetTs());
            OpenServiceIA = new RelayCommand(_ => fOpenServiceIA());
            OpenKeyGen = new RelayCommand(_ => fOpenKeyGen());

            api = new EpiService();
        }

        private void fOpenUchetTs()
        {
            //CardRSAGenViewModel obj = new CardRSAGenViewModel();



            //obj.TestCard();
        }

        private void fOpenServiceIA()
        {
            MessageBox.Show("fOpenServiceIA");
        }

        private void fOpenKeyGen()
        {
            MessageBox.Show("fOpenKeyGen");
        }

        private string _status;

        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;

                OnPropertyChanged("Status");
            }
        }
    }
}
