using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCardDesc.Model
{
    public class UserInfoModel
    {
        public string reg_dttm { get; set; }

        public string result { get; set; }

        public string mid_name { get; set; }

        public string pin { get; set; }

        public string dob { get; set; }

        public string gd { get; set; }

        public string surname { get; set; }

        public string per_adr { get; set; }

        public string tin { get; set; }

        public string pport_no { get; set; }

        /*

            <first_name>ULUG‘BEK</first_name>
            <result>success</result>
            <mid_name>ABDUVOIT O‘G‘LI</mid_name>
            <pin>32512920201717</pin>
         * 
            <dob>25/12/1992</dob>
            <gd>M</gd>
            <surname>KO‘CHAROV</surname>
            <per_adr>ГОРОД ТАШКЕНТ МИРАБАДСКИЙ РАЙОН НУКУС 1- ТУПИК 4-3</per_adr>
            <tin>498975465</tin>
            <pport_no>AA1011149</pport_no>
         */
    }
}
