using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace GlobalSolutions
{
    /// <summary>
    /// A base value converter that enables direct XML usage
    /// </summary>
    /// <typeparam name="T">The type of this value converter</typeparam>
    public abstract class BaseValueConvertor<T> : MarkupExtension, IValueConverter
        where T:class ,new()
    {
        #region Private Members
        /// <summary>
        /// Instance of this value converter
        /// </summary>
        private static T mConvertor = null;

        #endregion

        #region Markup Extension Methods

        /// <summary>
        /// Returns instance of this value converter
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConvertor ?? (mConvertor = new T());
        }

        #endregion

        #region Value converter Methods

        /// <summary>
        /// Method to convert one type to another
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Method to convert form one type to another
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        #endregion
    }
}
