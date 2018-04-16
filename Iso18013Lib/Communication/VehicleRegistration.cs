using System;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections.Generic;

namespace Iso18013Lib
{
    public class VehicleRegistration
    {
        private string _Json { get; set; }

        public VehicleRegistrationCL _VR { get; set; }

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

        public byte[] GetVehicleData()
        {
            List<byte> TotalList = new List<byte>();

            if (_VR != null)
            {

                if (_VR._issue_date != null)
                {
                    TotalList.Add(0xA1);
                    TotalList.Add((byte)_VR._issue_date.Length);
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._issue_date));
                }

                if (_VR._ubdd_name != null)
                {
                    TotalList.Add(0xA2);
                    TotalList.Add((byte)_VR._ubdd_name.Length);
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._ubdd_name));
                }

                if (_VR._license_number != null)
                {
                    TotalList.Add(0xA4);
                    TotalList.Add((byte)_VR._license_number.Length);
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._license_number));
                }

                if (_VR._expire_date != null)
                {
                    TotalList.Add(0xC4);
                    TotalList.Add((byte)_VR._expire_date.Length);
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._expire_date));
                }

                if (_VR._vehicle != null)
                {
                    /*
                     *   	"reg_number":"01X197TA",
                          	"model_name":"Chevrolet Spark",
                          	"color_name":"Mystic Lake",
                          	"vehicle_manufacture_year":2015,
                          	"type":"X/Z",
                          	"vehicle_identification_number":"1x1x1x1x1",
                          	"gross_weight":900,
                          	"curb_weight":800,
                          	"engine_number":"2x2x2x2x2x2",
                          	"engine_power":"86",
                          	"fuel_type":"91",
                          	"number_of_seats":5,
                          	"number_of_standees":null,
                          	"special_marks":null
                     */

                    if (!string.IsNullOrEmpty(_VR._vehicle._reg_number))
                    {
                        TotalList.Add(0xA5);
                        TotalList.Add((byte)_VR._vehicle._reg_number.Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._reg_number));
                    }

                    if (!string.IsNullOrEmpty(_VR._vehicle._model_name))
                    {
                        TotalList.Add(0xA6);
                        TotalList.Add((byte)_VR._vehicle._model_name.Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._model_name));
                    }

                    if (!string.IsNullOrEmpty(_VR._vehicle._mark_name))
                    {
                        TotalList.Add(0xC9);
                        TotalList.Add((byte)_VR._vehicle._mark_name.Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._mark_name));
                    }

                    if (!string.IsNullOrEmpty(_VR._vehicle._color_name))
                    {
                        TotalList.Add(0xA7);
                        TotalList.Add((byte)_VR._vehicle._color_name.Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._color_name));
                    }

                    if (!string.IsNullOrEmpty(_VR._vehicle._vehicle_manufacture_year))
                    {
                        TotalList.Add(0xA8);
                        TotalList.Add((byte)_VR._vehicle._vehicle_manufacture_year.Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._vehicle_manufacture_year));
                    }

                    if (!string.IsNullOrEmpty(_VR._vehicle._type))
                    {
                        TotalList.Add(0xD1);
                        TotalList.Add((byte)_VR._vehicle._type.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._type.ToString()));
                    }

                    if (!string.IsNullOrEmpty(_VR._vehicle._vehicle_identification_number_kuzov))
                    {
                        TotalList.Add(0xD2);
                        TotalList.Add((byte)_VR._vehicle._vehicle_identification_number_kuzov.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._vehicle_identification_number_kuzov.ToString()));
                    }

                    if (_VR._vehicle._vehicle_identification_number_shassi != null)
                    {
                        TotalList.Add(0xD3);
                        TotalList.Add((byte)_VR._vehicle._vehicle_identification_number_shassi.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._vehicle_identification_number_shassi.ToString()));
                    }

                    if (_VR._vehicle._gross_weight >= 0)
                    {
                        TotalList.Add(0xA9);
                        TotalList.Add((byte)_VR._vehicle._gross_weight.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._gross_weight.ToString()));
                    }

                    if (_VR._vehicle._curb_weight >= 0)
                    {
                        TotalList.Add(0xB1);
                        TotalList.Add((byte)_VR._vehicle._curb_weight.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._curb_weight.ToString()));
                    }

                    if (!string.IsNullOrEmpty(_VR._vehicle._engine_number))
                    {
                        TotalList.Add(0xB2);
                        TotalList.Add((byte)_VR._vehicle._engine_number.Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._engine_number));
                    }

                    if (!string.IsNullOrEmpty(_VR._vehicle._engine_power))
                    {
                        TotalList.Add(0xB3);
                        TotalList.Add((byte)_VR._vehicle._engine_power.Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._engine_power));
                    }

                    if (!string.IsNullOrEmpty(_VR._vehicle._fuel_type))
                    {
                        TotalList.Add(0xB4);
                        TotalList.Add((byte)_VR._vehicle._fuel_type.Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._fuel_type));
                    }

                    if (!string.IsNullOrEmpty(_VR._vehicle._engine_measurement))
                    {
                        TotalList.Add(0xD4);
                        TotalList.Add((byte)_VR._vehicle._engine_measurement.Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._engine_measurement));
                    }

                    if (_VR._vehicle._number_of_seats >= 0)
                    {
                        TotalList.Add(0xB5);
                        TotalList.Add((byte)_VR._vehicle._number_of_seats.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._number_of_seats.ToString()));
                    }

                    if (_VR._vehicle._number_of_standees != null)
                    {
                        TotalList.Add(0xB6);
                        TotalList.Add((byte)_VR._vehicle._number_of_standees.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._number_of_standees.ToString()));
                    }

                    if (_VR._vehicle._special_marks != null)
                    {
                        TotalList.Add(0xB7);
                        TotalList.Add((byte)_VR._vehicle._special_marks.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._vehicle._special_marks.ToString()));
                    }

                }

                /*
                 *   
                      "owner": {
                        "type": 1,
                        "address": {
                          "region_name": "Andijon viloyati",
                          "rayon_name": "Asaka tumani",
                          "address": "1-22-44"
                        },
                        "name": "Ivanov Ivan Ivanich",
                        "last_name": "Ivanov",
                        "first_name": "Ivan",
                        "middle_name": "Ivanich",
                        "pinfl": "12345678912345",
                        "inn": null
                      }
                 */
                if (_VR._company != null)
                 {
                    if (_VR._company._type >= 0)
                    {
                        TotalList.Add(0xC5);
                        TotalList.Add((byte)_VR._company._type.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._company._type.ToString()));
                    }

                    if (_VR._company._address != null)
                    {
                        if (!string.IsNullOrEmpty(_VR._company._address._address))
                        {
                            TotalList.Add(0xC1);
                            TotalList.Add((byte)_VR._company._address._address.ToString().Length);
                            TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._company._address._address.ToString()));
                        }

                        if (!string.IsNullOrEmpty(_VR._company._address._region_name))
                        {
                            TotalList.Add(0xC2);
                            TotalList.Add((byte)_VR._company._address._region_name.ToString().Length);
                            TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._company._address._region_name.ToString()));
                        }

                        if (!string.IsNullOrEmpty(_VR._company._address._rayon_name))
                        {
                            TotalList.Add(0xC3);
                            TotalList.Add((byte)_VR._company._address._rayon_name.ToString().Length);
                            TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._company._address._rayon_name.ToString()));
                        }
                    }

                    TotalList.Add(0xB8);
                    TotalList.Add((byte)_VR._company._name.ToString().Length);
                    TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._company._name.ToString()));


                    if (_VR._company._last_name != null)
                    {
                        TotalList.Add(0xC6);
                        TotalList.Add((byte)_VR._company._last_name.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._company._last_name.ToString()));
                    }

                    if (_VR._company._first_name != null)
                    {
                        TotalList.Add(0xC7);
                        TotalList.Add((byte)_VR._company._first_name.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._company._first_name.ToString()));
                    }

                    if (_VR._company._middle_name != null)
                    {
                        TotalList.Add(0xC8);
                        TotalList.Add((byte)_VR._company._middle_name.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._company._middle_name.ToString()));
                    }

                    if (_VR._company._pinfl != null)
                    {
                        TotalList.Add(0xA3);
                        TotalList.Add((byte)_VR._company._pinfl.Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._company._pinfl));
                    }

                    if (_VR._company._inn != null)
                    {
                        TotalList.Add(0xB9);
                        TotalList.Add((byte)_VR._company._inn.ToString().Length);
                        TotalList.AddRange(Encoding.UTF8.GetBytes(_VR._company._inn.ToString()));
                    }


                }

            }

            TotalList.InsertRange(0, Int2ByteArray(TotalList.Count));

            var ResultinStr02 = ByteArrayToString(Int2ByteArray(TotalList.Count));
            TotalList.Insert(0, 0x65);

            TotalList.InsertRange(0, Int2ByteArray(TotalList.Count));
            var ResultinStr03 = ByteArrayToString(Int2ByteArray(TotalList.Count));
            TotalList.Insert(0, 0x53);
            TotalList.Insert(0, 0x00);
            TotalList.Insert(0, 0x01);
            TotalList.Insert(0, 0x54);

            var ResultinStr = ByteArrayToString(TotalList.ToArray());

            return TotalList.ToArray();
        }

        private byte[] Int2ByteArray(int val)
        {
            int intValue = val;
            byte[] intBytes = BitConverter.GetBytes(intValue);
            Array.Reverse(intBytes);
            byte[] result = intBytes;

            List<byte> ll = new List<byte>();

            ll.Add(0x82);
            //ll.Add(result[1]);
            ll.Add(result[2]);
            ll.Add(result[3]);

            return ll.ToArray();
        }


        private string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
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

        public VehicleRegistrationCL _vehicleRegistration = new VehicleRegistrationCL();

        public VehicleRegistrationCL ParseReadMaterial(byte[] Vr)
        {
            ParseVR(Vr);

            return _vehicleRegistration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void ParseVR(byte[] Vr)
        {

            var TrimmedVr = Decode(Vr);

            var ResultinStr = ByteArrayToString(TrimmedVr);

            bool valueCountingBegin = false;
            bool tagGetting = true;
            bool lengthGetting = false;
            bool valueCountingFinished = false;

            byte tag = 0x00;
            byte[] value = null;
            int length = 0;
            int endOfValue = 0;

            for (int j = 0; j < TrimmedVr.Length-4; j++)
            {
                //Action
                if (tagGetting)
                {
                    tag = TrimmedVr[j+4];
                }
                else if (lengthGetting)
                {
                    byte[] lenArray = new byte[4];

                    lenArray[0] = TrimmedVr[j + 4]; 

                    length = BitConverter.ToInt32(lenArray, 0);

                    if (length == 0)
                    {
                        lengthGetting = false;
                        tagGetting = false;
                        valueCountingBegin = false;
                        valueCountingFinished = true;
                    }
               
                }
                else if (valueCountingBegin)
                {

                     value = new byte[length];

                     if (((j + 4) + length) > TrimmedVr.Length)
                     {
                         //
                         length = TrimmedVr.Length - (j + 4);

                         Array.Copy(TrimmedVr, j + 4, value, 0, length);
                     }
                     else
                     {
                         Array.Copy(TrimmedVr, j + 4, value, 0, length);
                     }

                     SetData(tag, value);

                     endOfValue = j + 4 + length - 1;

                    if ((j + 4) >= endOfValue)
                    {
                        lengthGetting = false;
                        tagGetting = true;
                        valueCountingBegin = false;
                        valueCountingFinished = false;

                        tag = 0x00;
                        value = null;
                        length = 0;
                        endOfValue = 0;

                        continue;
                    }

                }
                else if (valueCountingFinished)
                {
                    if ((j + 4) >= endOfValue)
                    {
                        //lengthGetting = false;
                        //tagGetting = true;
                        //valueCountingBegin = false;
                        //valueCountingFinished = false;

                        tag = 0x00;
                        value = null;
                        length = 0;
                        endOfValue = 0;
                    }
                    else
                    {
                        continue;
                    }
                }


                //Setting
                if (tagGetting)
                {
                    lengthGetting = true;
                    tagGetting = false;
                    valueCountingBegin = false;
                    valueCountingFinished = false;
                }
                else if (lengthGetting)
                {
                    lengthGetting = false;
                    tagGetting = false;
                    valueCountingBegin = true;
                    valueCountingFinished = false;
                }
                else if (valueCountingBegin)
                {
                    lengthGetting = false;
                    tagGetting = false;
                    valueCountingBegin = false;
                    valueCountingFinished = true;
                }
                else if (valueCountingFinished)
                {
                    lengthGetting = false;
                    tagGetting = true;
                    valueCountingBegin = false;
                    valueCountingFinished = false;
                }
            }
                
        }

        private void SetData(byte tag, byte[] value)
        {
            switch(tag)
            {
                case 0xA3:
                    _vehicleRegistration._company._pinfl = Encoding.UTF8.GetString(value);
                    break;
                case 0xA1:
                    _vehicleRegistration._issue_date= Encoding.UTF8.GetString(value);
                    break;
                case 0xA2:
                    _vehicleRegistration._ubdd_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xA4:
                    _vehicleRegistration._license_number = Encoding.UTF8.GetString(value);
                    break;
                case 0xA5:
                    _vehicleRegistration._vehicle._reg_number = Encoding.UTF8.GetString(value);
                    break;
                case 0xA6:
                    _vehicleRegistration._vehicle._model_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xA7:
                    _vehicleRegistration._vehicle._color_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xA8:
                    _vehicleRegistration._vehicle._vehicle_manufacture_year = Encoding.UTF8.GetString(value);
                    break;
                case 0xA9:
                    _vehicleRegistration._vehicle._gross_weight = int.Parse(Encoding.UTF8.GetString(value));
                    break;
                case 0xB1:
                    _vehicleRegistration._vehicle._curb_weight = int.Parse(Encoding.UTF8.GetString(value));
                    break;
                case 0xB2:
                    _vehicleRegistration._vehicle._engine_number = Encoding.UTF8.GetString(value);
                    break;
                case 0xB3:
                    _vehicleRegistration._vehicle._engine_power = Encoding.UTF8.GetString(value);
                    break;
                case 0xB4:
                    _vehicleRegistration._vehicle._fuel_type = Encoding.UTF8.GetString(value);
                    break;
                case 0xB5:
                    _vehicleRegistration._vehicle._number_of_seats= int.Parse(Encoding.UTF8.GetString(value));
                    break;
                case 0xB6:
                    _vehicleRegistration._vehicle._number_of_standees = Encoding.UTF8.GetString(value);
                    break;
                case 0xB7:
                    _vehicleRegistration._vehicle._special_marks = Encoding.UTF8.GetString(value);
                    break;
                case 0xB8:
                    _vehicleRegistration._company._name = Encoding.UTF8.GetString(value);
                    break;
                case 0xB9:
                    _vehicleRegistration._company._inn = Encoding.UTF8.GetString(value);
                    break;
                case 0xC1:
                    _vehicleRegistration._company._address._address = Encoding.UTF8.GetString(value);
                    break;
                case 0xC2:
                    _vehicleRegistration._company._address._region_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xC3:
                    _vehicleRegistration._company._address._rayon_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xC4:
                    _vehicleRegistration._expire_date = Encoding.UTF8.GetString(value);
                    break;
                case 0xC5:
                    _vehicleRegistration._company._type = int.Parse(Encoding.UTF8.GetString(value)); 
                    break;
                case 0xC6:
                    _vehicleRegistration._company._last_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xC7:
                    _vehicleRegistration._company._first_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xC8:
                    _vehicleRegistration._company._middle_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xC9:
                    _vehicleRegistration._vehicle._mark_name = Encoding.UTF8.GetString(value);
                    break;
                case 0xD1:
                    _vehicleRegistration._vehicle._type = Encoding.UTF8.GetString(value);
                    break;
                case 0xD2:
                    _vehicleRegistration._vehicle._vehicle_identification_number_kuzov = Encoding.UTF8.GetString(value);
                    break;
                case 0xD3:
                    _vehicleRegistration._vehicle._vehicle_identification_number_shassi = Encoding.UTF8.GetString(value);
                    break;
                case 0xD4:
                    _vehicleRegistration._vehicle._engine_measurement = Encoding.UTF8.GetString(value);
                    break;

            }
        }

        public byte[] Decode(byte[] packet)
        {
            var i = packet.Length - 1;
            while (packet[i] == 0)
            {
                --i;
            }
            var temp = new byte[i + 1];
            Array.Copy(packet, temp, i + 1);
            return temp;
        }
    }

    [DataContract]
    public class VehicleRegistrationCL
    {
        public VehicleRegistrationCL()
        {
            _company = new Owner();
            _vehicle = new Vehicle();
        }

        [DataMember(Name = "card_number")]
        public string _card_number { get; set; }

        [DataMember(Name = "owner")]
        public Owner _company { get; set; }

        [DataMember(Name = "vehicle")]
        public Vehicle _vehicle { get; set; }

        [DataMember(Name = "issue_date")]
        public string _issue_date { get; set; }

        [DataMember(Name = "ubdd_name")]
        public string _ubdd_name { get; set; }

        [DataMember(Name = "license_number")]
        public string _license_number { get; set; }

        [DataMember(Name = "expire_date")]
        public string _expire_date { get; set; }
    }

    [DataContract]
    public class Owner
    {
        public Owner()
        {
            _address = new AddressCL();
        }

        [DataMember(Name = "type")]
        public int _type;

        [DataMember(Name = "address")]
        public AddressCL _address { get; set; }

        [DataMember(Name = "name")]
        public string _name { get; set; }

        [DataMember(Name = "last_name")]
        public string _last_name;

        [DataMember(Name = "first_name")]
        public string _first_name;

        [DataMember(Name = "middle_name")]
        public string _middle_name;

        [DataMember(Name = "pinfl")]
        public string _pinfl { get; set; }

        [DataMember(Name = "inn")]
        public string _inn { get; set; }

    }

    [DataContract]
    public class Vehicle
    {
        [DataMember(Name = "reg_number")]
        public string _reg_number { get; set; }

        [DataMember(Name = "model_name")]
        public string _model_name { get; set; }

        [DataMember(Name = "mark_name")]
        public string _mark_name { get; set; }

        [DataMember(Name = "color_name")]
        public string _color_name { get; set; }

        [DataMember(Name = "vehicle_manufacture_year")]
        public string _vehicle_manufacture_year { get; set; }

        [DataMember(Name = "type")]
        public string _type { get; set; }

        [DataMember(Name = "vehicle_identification_number_kuzov")]
        public string _vehicle_identification_number_kuzov { get; set; }

        [DataMember(Name = "vehicle_identification_number_shassi")]
        public string _vehicle_identification_number_shassi { get; set; }

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

        [DataMember(Name = "engine_measurement")]
        public string _engine_measurement { get; set; }

        [DataMember(Name = "number_of_seats")]
        public int _number_of_seats { get; set; }

        [DataMember(Name = "number_of_standees")]
        public string _number_of_standees { get; set; }

        [DataMember(Name = "special_marks")]
        public string _special_marks { get; set; }
    }

}
