using SmartCardDesc.EntityModel.EntityModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SmartCardDesc.ViewModel.Converters
{
    public class UserIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string userFullName = string.Empty;

            using (var context = new SmartCardDBEntities())
            {
                var user = context.USERS.ToList().FirstOrDefault(x => x.REC_ID == (int)value);

                if (user != null)
                {
                    userFullName = string.Format("{0} {1} {2}", user.SURNAME_NAME, user.FIRST_NAME, user.MIDDLE_NAME);
                }
            }

            return userFullName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
