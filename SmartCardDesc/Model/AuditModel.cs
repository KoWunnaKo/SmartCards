using SmartCardDesc.EntityModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.Model
{
    public class AuditModel
    {
        public static void InsertAudit(string category,
            string message,
            string userId)
        {
            using (var context = new SmartCardDBEntities())
            {
                var audit = new AUDIT();

                audit.CATEGORY = category;
                audit.MESSAGE = message;
                audit.USER_ID = userId;
                audit.CREATE_DATE = DateTime.Now;

                context.AUDITs.Add(audit);

                context.SaveChanges();
            }
        }
    }
}
