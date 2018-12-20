using Epigov.Log;
using GID_Client.ViewModel;
using Kadastr.CoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.WinClient.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly ILogService _logService;

        private ProcessControllerBase processor { get; set; }

        public RelayCommand GetKadastrInfo { get; private set; }

        public RelayCommand SendToPrint { get; private set; }

        public RelayCommand ReadData { get; private set; }

        public RelayCommand MockGetKadastrInfo { get; private set; }

        public RelayCommand ResetCard { get; private set; }

        public RelayCommand Write2Cart { get; private set; }

        private long _kadastrId { get; set; }

        public IDictionary<string,string> readData { get; set; }

        public long KadastrId
        {
            get
            {
                return _kadastrId;
            }

            set
            {
                _kadastrId = value;

                OnPropertyChanged("KadastrId");
            }
        }

        private string _updateDateStr { get; set; }

        public string UpdateDateStr
        {
            get
            {
                return _updateDateStr;
            }

            set
            {
                _updateDateStr = value;

                OnPropertyChanged("UpdateDateStr");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public MainViewModel()
        {
            _logService = new FileLogService(typeof(MainViewModel));

            GetKadastrInfo = new RelayCommand(_ => fGetKadastrInfo());

            SendToPrint = new RelayCommand(_ => fSendToPrint());

            ReadData = new RelayCommand(_ => fReadData());

            MockGetKadastrInfo = new RelayCommand(_ => fMockGetKadastrInfo());

            ResetCard = new RelayCommand(_ => fResetCard());

            Write2Cart = new RelayCommand(_ => fWrite2Cart());

            processor = new ProcessControllerV1();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void fGetKadastrInfo()
        {
            var input = new InputKadastrInfo();

            input.id = KadastrId;

            input.lastUpdateDate = UpdateDateStr;

            await processor.GetKadastrInfo(null, input);
        }

        /// <summary>
        /// 
        /// </summary>
        private async void fSendToPrint()
        {
            await processor.SendToPrint(null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        public void fReadData()
        {
            readData =  processor.ReadData(null);
        }

        /// <summary>
        /// 
        /// </summary>
        private void fMockGetKadastrInfo()
        {
            processor.MockGetKadastrInfo(null);
        }

        /// <summary>
        /// 
        /// </summary>
        public void fResetCard()
        {
            processor.ResetCard(null, Properties.Settings.Default.Card_Reset_Tool);
        }

        /// <summary>
        /// 
        /// </summary>
        private void fWrite2Cart()
        {
            processor.Write2Cart(null, null, null);
        }

        public IEnumerable<MetaDataContainer> GetControlsList()
        {
            processor.ProcessMetaData(null, Properties.Settings.Default.MetaDataFilePath);

            return ((ProcessControllerV1)processor).metaList;
        }

        public async Task Write2Card(IDictionary<string, string> pair)
        {


            await processor.Write2Cart2(null, null, null, pair);
        }
    }
}
