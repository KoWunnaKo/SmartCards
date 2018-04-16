using System;
using System.Globalization;
using System.Diagnostics;

namespace GlobalSolutions
{
    /// <summary>
    /// Converts <see cref="ApplicationPage"></see> to an actual view/page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConvertor<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((ApplicationPage)value)
            {
                //case ApplicationPage.Login:
                //    return new LoginPage();
                case ApplicationPage.Main:
                    return new MainWindow();
                //case ApplicationPage.Register:
                //    return new RegisterPage();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
