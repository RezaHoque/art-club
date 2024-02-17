using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace artclub.Converters
{
    [ContentProperty(nameof(Convert))]
    public class ConcatenateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;

            if (parameter == null)
            {
                return string.Join(".", values);
            }
            else
            {
                string staticText = parameter.ToString();
                string dynamicText = string.Join(".", values);

                return staticText + dynamicText;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
