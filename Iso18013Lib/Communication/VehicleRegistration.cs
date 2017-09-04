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
    public class VehicleRegistration
    {
        private string _Json { get; set; }

        private VehicleRegistrationCL _VR { get; set; }

        private bool IsJsonParsed = false;

        public VehicleRegistration(string Json)
        {
            _Json = Json;
        }

        public int ParseInputJson()
        {
            try
            {
                if (string.IsNullOrEmpty(_Json))
                {
                    return -2;
                }

                _VR = ParseInputRequest(_Json);

                IsJsonParsed = true;
            }
            catch (Exception)
            {
                return -1;
            }

            return 0;
        }

        private VehicleRegistrationCL ParseInputRequest(string Json)
        {
            VehicleRegistrationCL request = null;

            try
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(Json)))
                {
                    var ser = new DataContractJsonSerializer(typeof(VehicleRegistrationCL));
                    request = (VehicleRegistrationCL)ser.ReadObject(stream);
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
        public byte[] GetDG1()
        {
            return new byte[] { 0x00 };
        }
    }

    [DataContract]
    public class VehicleRegistrationCL
    {
        [DataMember(Name = "pinfl")]
        public string _pinfl { get; set; }

        [DataMember(Name = "company")]
        public Company _company { get; set; }

        [DataMember(Name = "issue_date")]
        public string _issue_date { get; set; }

        [DataMember(Name = "ubdd_name")]
        public string _ubdd_name { get; set; }

        [DataMember(Name = "issue_region_name")]
        public string _issue_region_name { get; set; }
    }

    [DataContract]
    public class Company
    {
        [DataMember(Name = "name")]
        public string _name { get; set; }

        [DataMember(Name = "inn")]
        public string _inn { get; set; }

        [DataMember(Name = "address")]
        public AddressCL _address { get; set; }

    }

    [DataContract]
    public class Vehicle
    {
        [DataMember(Name = "reg_number")]
        public string _reg_number { get; set; }

        [DataMember(Name = "model_name")]
        public string _model_name { get; set; }

        [DataMember(Name = "color_name")]
        public string _color_name { get; set; }

        [DataMember(Name = "vehicle_manufacture_year")]
        public string _vehicle_manufacture_year { get; set; }

        [DataMember(Name = "type")]
        public string _type { get; set; }

        [DataMember(Name = "vehicle_identification_number")]
        public string _vehicle_identification_number { get; set; }

        [DataMember(Name = "gross_weight")]
        public int _gross_weight { get; set; }

        [DataMember(Name = "curb_weight")]
        public int _curb_weight { get; set; }

        [DataMember(Name = "engine_number")]
        public string _engine_number { get; set; }

        [DataMember(Name = "engine_power")]
        public string _engine_power { get; set; }

        [DataMember(Name = "fuel_type")]
        public string _fuel_type { get; set; }

        [DataMember(Name = "number_of_seats")]
        public int _number_of_seats { get; set; }

        [DataMember(Name = "number_of_standees")]
        public string _number_of_standees { get; set; }

        [DataMember(Name = "special_marks")]
        public string _special_marks { get; set; }
    }

}
