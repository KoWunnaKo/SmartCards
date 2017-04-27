using System;
using System.Globalization;
using System.Threading.Tasks;
using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.Cryptography;
using System.Linq;


namespace SmartCardDesc.Model
{
    public class UserInfoModel : BaseItemModel
    {

        public UserInfoModel()
        {
            rec_id = -1;
        }
        /// <summary>
        /// 
        /// </summary>
        public int rec_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool is_active { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Department { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string reg_dttm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string first_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mid_name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dob { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string surname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PerAdr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string tin { get; set; }

        /// <summary>2
        /// 
        /// 
        /// </summary>
        public string pport_no { get; set; }

        public Task InsertUserInfoEnt()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                using (var context = new SmartCardDBEntities())
                {
                    //Audit

                    AuditModel.InsertAudit("USER_INFO", 
                        string.Format("Got information by {0}",userId));


                    ///User information logic
                    var count = (from userx in context.USERS
                                where userx.LOGIN == userId
                                select userx.LOGIN).Count();

                    if (count > 0) return;

                    var user = new USER();

                    user.LOGIN = userId;
                    user.PASSWORD = HashPassword.HashPasswordWithSalt(password);
                    user.IS_ACTIVE = is_active;
                    user.DEPARTMENT = context.DEPARTMENTs.ToList().First().REC_ID;

                    if (string.IsNullOrEmpty(reg_dttm))
                    {
                        user.REG_DATE = null;
                    }
                    else
                    {
                        user.REG_DATE = DateTime.ParseExact(reg_dttm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    if (string.IsNullOrEmpty(first_name))
                    {
                        user.FIRST_NAME = null;
                    }
                    else
                    {
                        user.FIRST_NAME = first_name;
                    }

                    if (string.IsNullOrEmpty(surname))
                    {
                        user.SURNAME_NAME = null;
                    }
                    else
                    {
                        user.SURNAME_NAME = surname;
                    }

                    if (string.IsNullOrEmpty(mid_name))
                    {
                        user.MIDDLE_NAME = null;
                    }
                    else
                    {
                        user.MIDDLE_NAME = mid_name;
                    }

                    if (string.IsNullOrEmpty(gd))
                    {
                        user.GENDER = null;
                    }
                    else
                    {
                        if (gd.Equals("M"))
                        {
                            user.GENDER = true;
                        }
                        else
                        {
                            user.GENDER = false;
                        }
                    }

                    if (string.IsNullOrEmpty(dob))
                    {
                        user.BIRTH_DATE = null;
                    }
                    else
                    {
                        user.BIRTH_DATE= DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                    }

                    if (string.IsNullOrEmpty(PerAdr))
                    {
                        user.ADDRESS = null;
                    }
                    else
                    {
                        user.ADDRESS = PerAdr;
                    }


                    if (string.IsNullOrEmpty(pport_no))
                    {
                        user.PASSPORT = null;
                    }
                    else
                    {
                        user.PASSPORT = pport_no;
                    }


                    if (string.IsNullOrEmpty(tin))
                    {
                        user.TIN = null;
                    }
                    else
                    {
                        user.TIN = tin;
                    }

                    if (string.IsNullOrEmpty(pin))
                    {
                        user.PIN = null;
                    }
                    else
                    {
                        user.PIN = pin;
                    }

                    user.CARD_FLG = false;
                    user.KEY_FLG = false;
                    user.CERT_CRT_FLG = false;
                    user.CERT_WRT_FLG = false;

                    context.USERS.Add(user);

                    context.SaveChanges();
                }
            });

            return resultTask;
        }


        public void InsertUserInfoEntx()
        {
                using (var context = new SmartCardDBEntities())
                {
                    //Audit

                    AuditModel.InsertAudit("USER_INFO",
                        string.Format("Got information by {0}", userId));


                    ///User information logic
                    var count = (from userx in context.USERS
                                 where userx.LOGIN == userId
                                 select userx.LOGIN).Count();

                    if (count > 0) return;

                    var user = new USER();

                    user.LOGIN = userId;
                    user.PASSWORD = HashPassword.HashPasswordWithSalt(password);
                    user.IS_ACTIVE = is_active;
                    user.DEPARTMENT = context.DEPARTMENTs.ToList().First().REC_ID;

                    if (string.IsNullOrEmpty(reg_dttm))
                    {
                        user.REG_DATE = null;
                    }
                    else
                    {
                        user.REG_DATE = DateTime.ParseExact(reg_dttm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    if (string.IsNullOrEmpty(first_name))
                    {
                        user.FIRST_NAME = null;
                    }
                    else
                    {
                        user.FIRST_NAME = first_name;
                    }

                    if (string.IsNullOrEmpty(surname))
                    {
                        user.SURNAME_NAME = null;
                    }
                    else
                    {
                        user.SURNAME_NAME = surname;
                    }

                    if (string.IsNullOrEmpty(mid_name))
                    {
                        user.MIDDLE_NAME = null;
                    }
                    else
                    {
                        user.MIDDLE_NAME = mid_name;
                    }

                    if (string.IsNullOrEmpty(gd))
                    {
                        user.GENDER = null;
                    }
                    else
                    {
                        if (gd.Equals("M"))
                        {
                            user.GENDER = true;
                        }
                        else
                        {
                            user.GENDER = false;
                        }
                    }

                    if (string.IsNullOrEmpty(dob))
                    {
                        user.BIRTH_DATE = null;
                    }
                    else
                    {
                        user.BIRTH_DATE = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                    }

                    if (string.IsNullOrEmpty(PerAdr))
                    {
                        user.ADDRESS = null;
                    }
                    else
                    {
                        user.ADDRESS = PerAdr;
                    }


                    if (string.IsNullOrEmpty(pport_no))
                    {
                        user.PASSPORT = null;
                    }
                    else
                    {
                        user.PASSPORT = pport_no;
                    }


                    if (string.IsNullOrEmpty(tin))
                    {
                        user.TIN = null;
                    }
                    else
                    {
                        user.TIN = tin;
                    }

                    if (string.IsNullOrEmpty(pin))
                    {
                        user.PIN = null;
                    }
                    else
                    {
                        user.PIN = pin;
                    }

                    user.CARD_FLG = false;
                    user.KEY_FLG = false;
                    user.CERT_CRT_FLG = false;
                    user.CERT_WRT_FLG = false;

                    context.USERS.Add(user);

                    context.SaveChanges();
                }
        }

        public void ModifyUserInfo()
        {

        }

        public void DeleteUserInfo()
        {

        }

    }
}
