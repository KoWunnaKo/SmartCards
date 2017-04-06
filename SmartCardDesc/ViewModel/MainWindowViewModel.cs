using SmartCardDesc.InfocomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            OpenUchetTs = new RelayCommand(_ => fOpenUchetTs());
            OpenServiceIA = new RelayCommand(_ => fOpenServiceIA());
            OpenKeyGen = new RelayCommand(_ => fOpenKeyGen());

            api = new EpiService();
        }

        private void fOpenUchetTs()
        {
            //MessageBox.Show("fOpenUchetTs");

            api.TestGetUserCard();
        }

        private void fOpenServiceIA()
        {
            MessageBox.Show("fOpenServiceIA");
        }

        private void fOpenKeyGen()
        {
            MessageBox.Show("fOpenKeyGen");
        }
    }
}
