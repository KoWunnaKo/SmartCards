using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib.MetaDataProcessors
{
    [DataContract]
    public class MetaDataCollection
    {
        public MetaDataCollection()
        {
            list = new List<MetaDataContainer>();
        }

        [DataMember(Name = "list")]
        public List<MetaDataContainer> list { get; set; }
    }
}
