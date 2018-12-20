using Kadastr.CoreLib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kadastr.CoreLib.KadastrApi
{
    public class Kadastr2MetaDataMapper
    {
        public static IDictionary<string, string> Map(OutputKadastrInfo kadastr)
        {
            if (kadastr == null) return null;

            var keyValuePair = new Dictionary<string, string>();

            PropertyInfo[] props = typeof(OutputKadastrInfo).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    ApiInfoAttribute authAttr = attr as ApiInfoAttribute;
                    if (authAttr != null)
                    {
                        if (!string.IsNullOrEmpty(authAttr.Tag))
                        {
                            string propValue = prop.GetValue(kadastr, null).ToString();

                            if (!string.IsNullOrEmpty(propValue))
                            {
                                string out_value;
                                if (!keyValuePair.TryGetValue(authAttr.Tag , out out_value))
                                {
                                    keyValuePair.Add(authAttr.Tag, propValue);
                                }
                            }
                        }
                    }
                }
            }

            return keyValuePair;
        }
    }
}
