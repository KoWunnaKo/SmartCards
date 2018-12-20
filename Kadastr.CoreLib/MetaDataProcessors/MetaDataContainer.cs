using System.Runtime.Serialization;

namespace Kadastr.CoreLib
{
    [DataContract]
    public class MetaDataContainer
    {
        [DataMember(Name = "Code")]
        public string code { get; set; }
        [DataMember(Name = "Desc")]
        public string desc { get; set; }
        [DataMember(Name = "Type")]
        public string data_type { get; set; }
        [DataMember(Name = "Format")]
        public string format { get; set; }
        [DataMember(Name = "Chk")]
        public string check { get; set; }
        [DataMember(Name = "Default")]
        public string Default { get; set; }
    }
}