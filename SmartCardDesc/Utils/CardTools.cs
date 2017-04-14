using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCardDesc.Utils
{
    public class CardTools
    {
        public static string GenerateCardNumber()
        {
            //Get from DataBase LastNum + 1
            string number = 1.ToString().PadLeft(16, '0');

            return number;
        }
    }
}
