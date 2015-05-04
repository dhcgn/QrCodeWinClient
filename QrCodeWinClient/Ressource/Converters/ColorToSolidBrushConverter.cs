using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace QrCodeWinClient
{
    [ValueConversion(typeof(SolidBrush), typeof(System.Windows.Media.Color))]
    public class ColorToSolidBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var solidBrush = value as SolidBrush;

            if (solidBrush == null) return null;

            var color = solidBrush.Color;
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is System.Windows.Media.Color)) return null;
            var color = (System.Windows.Media.Color) value;

            return new SolidBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
        }
    }
}