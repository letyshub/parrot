using Letys.Parrot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Letys.Parrot.Converters
{
    public class LanguageEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(TestLanguage))
            {
                return (int)value;
            }

            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(int))
            {
                return (TestLanguage)value;
            }

            return TestLanguage.English;
        }
    }
}
