using SmartCardDesc.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SmartCardDesc.ViewModel.Converters
{
    public class ImageConverter : IValueConverter
    {
        public ImageConverter()
        {
            this.DecodeHeight = -1;
            this.DecodeWidth = -1;
        }

        public int DecodeWidth { get; set; }
        public int DecodeHeight { get; set; }

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value == null)
            {
                return Resources.NoImage;
            }
            else if (string.IsNullOrEmpty(value.ToString()))
            {
                return Resources.NoImage;
            }

            var pictureUri = new Uri(value.ToString(), UriKind.RelativeOrAbsolute);

            //var uri = value as Uri;
            if (pictureUri != null)
            {
                var source = new BitmapImage();
                source.BeginInit();
                source.UriSource = pictureUri;
                if (this.DecodeWidth >= 0)
                    source.DecodePixelWidth = this.DecodeWidth;
                if (this.DecodeHeight >= 0)
                    source.DecodePixelHeight = this.DecodeHeight;
                source.EndInit();
                return source;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
