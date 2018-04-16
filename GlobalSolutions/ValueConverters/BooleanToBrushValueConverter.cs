using System;
using System.Globalization;
using System.Windows.Media;

namespace GlobalSolutions
{
    public class BooleanToBrushValueConverter : BaseValueConvertor<BooleanToBrushValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Brushes.Green;
            else
                return Brushes.Red;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
