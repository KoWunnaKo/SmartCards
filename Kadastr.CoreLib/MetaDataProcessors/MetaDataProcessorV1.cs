using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Kadastr.CoreLib.MetaDataProcessors;
using System.Text;

namespace Kadastr.CoreLib
{
    public class MetaDataProcessorV1 : IMetaDataProcessor
    {
        private MetaDataCollection _metaData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonRes"></param>
        /// <returns></returns>
        public IEnumerable<MetaDataContainer> ParsMetaData(string jsonRes)
        {
            if (string.IsNullOrEmpty(jsonRes)) return null;

            var result = (MetaDataCollection)JsonConvert.DeserializeObject(jsonRes, typeof(MetaDataCollection));

            _metaData = result;

            return result.list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadMetaData(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;

            if (!File.Exists(path)) return null;

            var jsonResMas = File.ReadAllLines(path, Encoding.UTF8);

            var fullJson = string.Join("", jsonResMas);

            return fullJson;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metaList"></param>
        /// <param name="inputDataList"></param>
        /// <returns></returns>
        public bool ValidateDataByMetadata(IEnumerable<MetaDataContainer> metaList, IDictionary<string, string> inputDataList)
        {
            if (metaList == null) return false;
            if (inputDataList == null) return false;

            foreach(var meta in metaList)
            {
                //If in metadata difinition field defined as mondatory
                //than in inputDataList such element should exists
                if (meta.check.Equals("M"))
                {
                    var inputQ_Res = inputDataList.Where(z => z.Key == meta.code);

                    if (!inputQ_Res.Any())
                    {
                        //If no element found inputDataList is invalid
                        return false;
                    }
                    else
                    {
                        var inputF = inputQ_Res.First();

                        if (string.IsNullOrEmpty(inputF.Value))
                        {
                            return false;
                        }
                        else
                        {
                            if (!CheckDataTypeAndFormat(meta.data_type , meta.format , inputF.Value))
                            {
                                return false;
                            }
                        }
                    }
                }
                else if (meta.check.Equals("O"))
                {
                    var inputQ_Res = inputDataList.Where(z => z.Key == meta.code);

                    if (!inputQ_Res.Any())
                    {
                        //no problem this is optional element
                    }
                    else
                    {
                        var inputF = inputQ_Res.First();

                        if (string.IsNullOrEmpty(inputF.Value))
                        {
                            //no problem this is optional element
                        }
                        else
                        {
                            if (!CheckDataTypeAndFormat(meta.data_type, meta.format, inputF.Value))
                            {
                                return false;
                            }
                        }
                    }
                }

            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data_type"></param>
        /// <param name="format"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckDataTypeAndFormat(string data_type, string format , string value)
        {
            if (data_type.Equals("C"))
            {
                var resStr = value.Trim();

                if (string.IsNullOrEmpty(resStr))
                {
                    return false;
                }
            }
            else if (data_type.Equals("D"))
            {
                DateTime out_dete;

                if (!DateTime.TryParseExact(value, format,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out out_dete))
                {
                    return false;
                }
            }
            else if (data_type.Equals("N")) //integer
            {
                int out_value;

                if (!int.TryParse(value, out out_value))
                {
                    return false;
                }
            }
            else if (data_type.Equals("F")) //Double
            {
                double out_value;

                if (!double.TryParse(value, out out_value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mList"></param>
        /// <returns></returns>
        public bool ValidateMetaData(IEnumerable<MetaDataContainer> mList)
        {
            if (mList == null)
            {
                return false;
            }

            if (mList.Count() == 0) return false;

            foreach(var meta in mList)
            {
                if (string.IsNullOrEmpty(meta.code) ||
                    string.IsNullOrEmpty(meta.check) ||
                    string.IsNullOrEmpty(meta.data_type) ||
                    string.IsNullOrEmpty(meta.desc) 
                    ) return false;
            }

            return true;
        }
    }
}
