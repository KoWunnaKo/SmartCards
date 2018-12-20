using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib.Attributes
{
    public enum RequiredFlags
    {
        Mondatory,
        Optional,
        Conditional
    }

    public class ApiInfoAttribute : System.Attribute
    {
        public string Name { get; set; }
        
        public Type DataType { get; set; }

        public int Length { get; set; }

        public string Definition { get; set; }

        public RequiredFlags isMondatory { get; set; }

        public string Tag { get; set; }

        public ApiInfoAttribute(string name, Type dataType, int length, string definition, RequiredFlags is_mondatory)
        {
            Name = name;
            DataType = dataType;
            Length = length;
            Definition = definition;
            isMondatory = is_mondatory;
        }

        public ApiInfoAttribute(string name, Type dataType, int length, string definition, RequiredFlags is_mondatory, string _tag)
        {
            Name = name;
            DataType = dataType;
            Length = length;
            Definition = definition;
            isMondatory = is_mondatory;
            Tag = _tag;
        }
    }
}
