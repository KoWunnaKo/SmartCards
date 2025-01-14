﻿using SmartCardDesc.EntityModel.EntityModel;
using SmartCardDesc.ViewModel.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        public static void InsertAudit(string category,
            string message)
        {
            using (var context = new SmartCardDBEntities())
            {
                var audit = new AUDIT();

                audit.CATEGORY = category;
                audit.MESSAGE = message;
                audit.USER_ID = LoginModel.currentUser.LOGIN;
                audit.CREATE_DATE = DateTime.Now;

                context.AUDITs.Add(audit);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Task InsertAuditAsync(string category,
                                            string message)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                using (var context = new SmartCardDBEntities())
                {
                    var audit = new AUDIT();

                    audit.CATEGORY = category;
                    audit.MESSAGE = message;
                    audit.USER_ID = LoginModel.currentUser.LOGIN;
                    audit.CREATE_DATE = DateTime.Now;

                    context.AUDITs.Add(audit);

                    context.SaveChanges();
                }
            });

            return resultTask;
        }
    }
}
