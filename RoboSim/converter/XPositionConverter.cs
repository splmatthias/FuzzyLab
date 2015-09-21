using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RoboSim.converter
{
    public class XPositionConverter: IValueConverter
    {
        public int Width { get; set; }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return ((double)value) + 5200 - (Width / 2);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return ((double)value) - 5200 + (Width / 2);
            }
            return null;
        }
    }
}
