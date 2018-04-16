using System;
using System.Globalization;
using System.Windows;

namespace GlobalSolutions
{
    class AttachedPropertyValueConvertor : BaseValueConvertor<AttachedPropertyValueConvertor>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
