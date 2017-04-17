using SmartCardDesc.EntityModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task InsertCardInfoEnt()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                using (var context = new SmartCardDBEntities())
                {
                    //Audit

                    AuditModel.InsertAudit("CARD_INFO",
                        string.Format("Got information by card {0}", card_num)
                        , "Current User!!!");

                    var count = (from cardx in context.CARD_INFO
                                 where cardx.CARD_NUMBER == card_num
                                 select cardx.CARD_NUMBER).Count();

                    if (count > 0) return;


                }
            });

            return resultTask;
        }
    }
}
