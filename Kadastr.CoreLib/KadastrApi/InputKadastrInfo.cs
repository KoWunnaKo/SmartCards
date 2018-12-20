using Kadastr.CoreLib.Attributes;
using System;
using System.Xml.Serialization;

namespace Kadastr.CoreLib
{
    [Serializable]
    [XmlRoot("RequesToKadastr")]
    public class InputKadastrInfo
    {
        [XmlElement("ID")]
        [ApiInfo("Уникальный номер запроса", typeof(long), 12 , "ID", RequiredFlags.Mondatory)]
        public long id { get; set; }

        [XmlElement("DATE_UPD")]
        [ApiInfo("Дата последнего обновления", typeof(DateTime), 12, "DATE_UPD", RequiredFlags.Mondatory)]
        public string lastUpdateDate { get; set; }
    }
}