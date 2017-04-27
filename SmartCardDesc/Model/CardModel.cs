using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.ViewModel.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public string picturePath { get; set; }


        public string PinNumber { get; set; }



        public Task InsertCardInfoEnt()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                using (var context = new SmartCardDBEntities())
                {
                    //Audit

                    AuditModel.InsertAudit("CARD_INFO",
                        string.Format("Got information by card {0}", card_num));

                    //Card Insert Logic
                    var count = (from cardx in context.CARD_INFO
                                 where cardx.CARD_NUMBER == card_num
                                 select cardx.CARD_NUMBER).Count();

                    if ((count > 0) || (string.IsNullOrEmpty(card_num)))
                        return;

                    var card = new CARD_INFO();

                    card.CARD_NUMBER = card_num;
                    card.CARD_STATE = card_stat;

                    if (!string.IsNullOrEmpty(issue_date))
                    {
                        card.ISSUE_DATE = DateTime.ParseExact(issue_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }
                    
                    if (!string.IsNullOrEmpty(expiry_date))
                    {
                        card.EXPIRE_DATE = DateTime.ParseExact(expiry_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }
                    
                    if (LoginModel.currentUser != null)
                    {
                        card.CREATE_USER = LoginModel.currentUser.REC_ID;
                    }
                    
                    card.OWNER_USER = context.USERS.ToList().First(t => t.LOGIN == user_id).REC_ID;

                    context.CARD_INFO.Add(card);

                    var user = context.USERS.ToList().First(t => t.LOGIN == user_id);

                    user.CARD_FLG = true;

                    context.SaveChanges();
                }
            });

            return resultTask;
        }

        public void InsertCardInfoEntx()
        {
                using (var context = new SmartCardDBEntities())
                {
                    //Audit

                    AuditModel.InsertAudit("CARD_INFO",
                        string.Format("Got information by card {0}", card_num));

                    //Card Insert Logic
                    var count = (from cardx in context.CARD_INFO
                                 where cardx.CARD_NUMBER == card_num
                                 select cardx.CARD_NUMBER).Count();

                    if ((count > 0) || (string.IsNullOrEmpty(card_num)))
                        return;

                    var card = new CARD_INFO();

                    card.CARD_NUMBER = card_num;
                    card.CARD_STATE = card_stat;

                    if (!string.IsNullOrEmpty(issue_date))
                    {
                        card.ISSUE_DATE = DateTime.ParseExact(issue_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }

                    if (!string.IsNullOrEmpty(expiry_date))
                    {
                        card.EXPIRE_DATE = DateTime.ParseExact(expiry_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }

                    if (LoginModel.currentUser != null)
                    {
                        card.CREATE_USER = LoginModel.currentUser.REC_ID;
                    }

                    card.OWNER_USER = context.USERS.ToList().First(t => t.LOGIN == user_id).REC_ID;

                    card.IS_ACTIVE = true;

                    card.IS_PRINTED = false;

                    card.PICTURE_PATH = picturePath;

                    card.PIN = PinNumber;

                    context.CARD_INFO.Add(card);

                    var user = context.USERS.ToList().First(t => t.LOGIN == user_id);

                    user.CARD_FLG = true;

                    context.SaveChanges();
                }
        }
    }
}
