using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;

namespace AxesoConsumer.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        static WebClient Client = new WebClient();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string defaulturl = "https://www.axesoapp.com/productos/sin.imagen.png";
            try
            {
                var byteArray = Client.DownloadData(value.ToString());
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }
            catch (Exception ex)
            {
                var byteArray = Client.DownloadData(defaulturl);
                return ImageSource.FromStream(() => new MemoryStream(byteArray));
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}