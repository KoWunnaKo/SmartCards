using SmartCardDesc.EntityModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.Utils
{
    public class CardTools
    {
        public static Task<string> GenerateCardNumber()
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                string number = string.Empty;

                using (var context = new SmartCardDBEntities())
                {
                    var card_num = context.FIXED_INTERNAL_VALUES.ToList().First(x => x.DESCRIPTION.
                    Equals("CARD_NUMBER")).VALUE + 1;

                    number = card_num.ToString().PadLeft(16, '0');

                    //Change Number +1
                    var card = context.FIXED_INTERNAL_VALUES.ToList().First(x => x.DESCRIPTION.
                    Equals("CARD_NUMBER"));

                    card.VALUE = card_num;

                    context.SaveChanges();
                }

                return number;
            });

            return resultTask;
        }
    }
}
