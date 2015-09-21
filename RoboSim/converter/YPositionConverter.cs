using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RoboSim.converter
{
    public class YPositionConverter : IValueConverter
    {
        public int Height { get; set; }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return ((double)value) + 3700 - (Height / 2);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return ((double)value) - 3700 + (Height / 2);
            }
            return null;
        }
    }
}
