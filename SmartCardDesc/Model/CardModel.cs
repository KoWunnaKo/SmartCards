using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCardDesc.Model
{
    public class CardModel : BaseItemModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string card_stat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string issue_date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string card_num { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string expiry_date { get; set; }

        /*
         *  <result>Success</result>
            <card_stat>Y</card_stat>
            <issue_date>2018-12-10</issue_date>
            <card_num>CARD001</card_num>
            <user_id>ulugbek</user_id>
            <expiry_date>2019-12-10</expiry_date>
         */
    }
}
