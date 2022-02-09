using System;
using System.Globalization;
using System.Windows.Data;

namespace Aquilion.Notepad.ValueConverters
{
    internal class MultiCommandParametersConverter : BaseMultiValueConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
