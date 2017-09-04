using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Iso18013Lib
{
    public class DrivingLicense
    {
        private string _Json { get; set; }

        private DrivingLicenseExample _DL { get; set; }

        private bool IsJsonParsed = false;

        public DrivingLicense(string Json)
        {
            _Json = Json;

            //DrivingLicenseExample DL = ParseInputRequest(Json);

            
        }

        public int ParseInputJson()
        {
            try
            {
                if (string.IsNullOrEmpty(_Json))
                {
                    return -2;
                }

                _DL = ParseInputRequest(_Json);

                IsJsonParsed = true;
            }
            catch(Exception)
            {
                return -1;
            }

            return 0;
        }

        private DrivingLicenseExample ParseInputRequest(string Json)
        {
            DrivingLicenseExample request = null;

            try
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(Json)))
                {
                    var ser = new DataContractJsonSerializer(typeof(DrivingLicenseExample));
                    request = (DrivingLicenseExample)ser.ReadObject(stream);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("JSON Parse Error {0}", ex.Message));
            }

            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte [] GetDG1()
        {
            string _total = string.Empty;

            _total = string.Format("[{0}];[{1}];[{2}];[{3}];[{4}];[{5}];",
                _DL._driver._first_name,
                _DL._driver._last_name,
                _DL._driver._middle_name,
                _DL._driver._date_of_birth,
                _DL._driver._region_name_birth,
                _DL._driver._pinfl);

            foreach(Category cat in _DL._categories)
            {
                string temp = string.Format("[{0}];[{1}];", cat._name, cat._issue_date);

                _total = _total + temp;
            }

            _total = _total + string.Format("[{0}];[{1}];[{2}];[{3}]", _DL._issue_date, _DL._expire_date, _DL._issue_region_name, _DL._license_number);

            return Encoding.UTF8.GetBytes(_total);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetDG2()
        {
            string _total = string.Empty;

            _total = string.Format("[{0}];[{1}];[{2}];",
                _DL._driver._address._address,
                _DL._driver._address._region_name,
                _DL._driver._address._rayon_name);

            return Encoding.UTF8.GetBytes(_total);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetDG3()
        {
            return new byte[] { 0x00 };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetDG4()
        {
            string _total = string.Empty;

            _total = string.Format("[{0}];[{1}];[{2}];", DateTime.Now.ToString("yyyyMMddhhmiss"),
                "3", _DL._Photo);

            return Encoding.UTF8.GetBytes(_total); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetDG5()
        {
            string _total = string.Empty;

            _total = string.Format("[{0}];[{1}];", 
                "3", _DL._Signature);

            return Encoding.UTF8.GetBytes(_total);
        }

        //public byte [] 
    }

    [DataContract]
    public class DrivingLicenseExample
    {
        [DataMember(Name = "driver")]
        public DriverCL _driver { get; set; }

        [DataMember(Name = "categories")] //DG1
        public Category [] _categories { get; set; }

        [DataMember(Name = "issue_date")] //DG1
        public string _issue_date { get; set; }

        [DataMember(Name = "expire_date")] //DG1
        public string _expire_date { get; set; }

        [DataMember(Name = "issue_region_name")] //DG1
        public string _issue_region_name { get; set; }

        [DataMember(Name = "license_number")] //DG1
        public string _license_number { get; set; }

        [DataMember(Name = "photo")]
        public string _Photo { get; set; }

        [DataMember(Name = "signature")]
        public string _Signature { get; set; }

    }

    [DataContract]
    public class DriverCL
    {
        [DataMember(Name = "first_name")] //DG1
        public string _first_name { get; set; }

        [DataMember(Name = "last_name")] //DG1
        public string _last_name { get; set; }

        [DataMember(Name = "middle_name")] //DG1
        public string _middle_name { get; set; }

        [DataMember(Name = "date_of_birth")] //DG1
        public string _date_of_birth { get; set; }

        [DataMember(Name = "region_name_birth")] //DG1
        public string _region_name_birth { get; set; }

        [DataMember(Name = "pinfl")] //DG1
        public string _pinfl { get; set; }

        [DataMember(Name = "address")]  //DG1
        public AddressCL _address { get; set; }

    }

    [DataContract]
    public class AddressCL
    {
        [DataMember(Name = "address")] //DG2
        public string _address { get; set; } 

        [DataMember(Name = "region_name")] //DG2
        public string _region_name { get; set; }

        [DataMember(Name = "rayon_name")] //DG2
        public string _rayon_name { get; set; }
    }

    [DataContract]
    public class Category
    {
        [DataMember(Name = "name")]
        public string _name { get; set; }

        [DataMember(Name = "issue_date")]
        public string _issue_date { get; set; }
    }
}
