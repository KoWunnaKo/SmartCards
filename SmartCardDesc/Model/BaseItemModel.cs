using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCardDesc.Model
{
    public class BaseItemModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string globalTransactionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string localTransactionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; } 
    }
}
