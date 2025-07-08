using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Converter
{
    public class SecondToStringConverter : IValueConverter
    {
        object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var ts=TimeSpan.FromSeconds((double)value);
            return ts.ToString(@"mm\:ss");
        }

        object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var ts = TimeSpan.Parse((string)value);
            return ts.TotalSeconds;
        }
    }
}
