using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace QrCodeWinClient
{
    [ValueConversion(typeof (SolidBrush), typeof (SolidColorBrush))]
    public class BrushToSolidBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value as SolidBrush;
            if (brush == null) return null;

            var solidColorBrush = new SolidColorBrush(Color.FromArgb(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B));
            return solidColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}