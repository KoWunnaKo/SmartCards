using Kadastr.CoreLib.KadastrApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessControllerV1 : ProcessControllerBase
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private string kadastrApiReturnValue { get; set; }
        
        /// <summary>
        /// Show to Client or change by client
        /// </summary>
        public  OutputKadastrInfo objKadastrRes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<MetaDataContainer> metaList { get; set; }

        #endregion

        public ProcessControllerV1()
        {
            Settings.MakeInterfaceSetting();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="path"></param>
        public override void ProcessMetaData(IMetaDataProcessor metaData, string path)
        {
            if (metaData == null)
            {
                metaData = Settings.metadataprocessor;
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ApplicationException("Invalid path");
            }

            var jsonRes = metaData.ReadMetaData(path);

            metaList = metaData.ParsMetaData(jsonRes);

            if (!metaData.ValidateMetaData(metaList))
            {
                throw new ApplicationException("Not Valid metaData");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        public override Task GetKadastrInfo(IKadastrApi api, InputKadastrInfo inputData)
        {
            if (api == null)
            {
                api = Settings.kadastrapi_real;
            }

            var resultTask = Task.Factory.StartNew(() =>
            {
                kadastrApiReturnValue = api.GetKadastrInfo(inputData);

                objKadastrRes = api.ConvertInputToObj(kadastrApiReturnValue);
            });

            return resultTask;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="api"></param>
        public override void MockGetKadastrInfo(IKadastrApi api)
        {
            if (api == null)
            {
                api = Settings.kadastrapi_mock;
            }

            kadastrApiReturnValue = api.GetKadastrInfo(new InputKadastrInfo() { id = 11, lastUpdateDate = "01.01.2018" });

            objKadastrRes = api.ConvertInputToObj(kadastrApiReturnValue);

            //var resultTask = Task.Factory.StartNew(() =>
            //{


            //});

            //await resultTask;
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public override IDictionary<string, string> ReadData(IReader reader)
        {
            if (reader == null)
            {
                reader = Settings.reader;
            }

            var res = reader.ReadData();

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public override void ResetCard(IReader reader, string path)
        {
            if (reader == null)
            {
                reader = Settings.reader;
            }

            string pathV = string.Empty;

            if (string.IsNullOrEmpty(path))
            {
                pathV = Settings.Reset_Tool_Path;
            }
            else
            {
                pathV = path;
            }

            reader.Reset(pathV);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printer"></param>
        public override Task SendToPrint(IPrinterService printer, IMetaDataProcessor metaData)
        {
            if (printer == null)
            {
                printer = Settings.printerservice;
            }

            if (metaData == null)
            {
                metaData = Settings.metadataprocessor;
            }

            var map = Kadastr2MetaDataMapper.Map(objKadastrRes);

            if (!metaData.ValidateDataByMetadata(metaList, map))
            {
                throw new ApplicationException("Not Valid data");
            }

            var resultTask = Task.Factory.StartNew(() =>
            {
                printer.SendToPrint(map);
            });

            return resultTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public override async Task Write2Cart(IReader reader, IMetaDataProcessor metaData, IParser parser)
        {
            if (reader == null)
            {
                reader = Settings.reader;
            }

            if (metaData == null)
            {
                metaData = Settings.metadataprocessor;
            }

            if (parser == null)
            {
                parser = Settings.parser;
            }

            var map = Kadastr2MetaDataMapper.Map(objKadastrRes);

            if (!metaData.ValidateDataByMetadata(metaList, map))
            {
                throw new ApplicationException("Not Valid data");
            }

            var resByte =  parser.ConvertInput2ByteArray(map);

            await reader.Write2Kart(resByte);
        }


        public override async Task Write2Cart2(IReader reader, IMetaDataProcessor metaData, IParser parser, IDictionary<string, string> pair)
        {
            if (reader == null)
            {
                reader = Settings.reader;
            }

            if (metaData == null)
            {
                metaData = Settings.metadataprocessor;
            }

            if (parser == null)
            {
                parser = Settings.parser;
            }

            var map = pair;

            if (!metaData.ValidateDataByMetadata(metaList, map))
            {
                throw new ApplicationException("Not Valid data");
            }

            var resByte = parser.ConvertInput2ByteArray(map);

            await reader.Write2Kart(resByte);
        }
    }
}
