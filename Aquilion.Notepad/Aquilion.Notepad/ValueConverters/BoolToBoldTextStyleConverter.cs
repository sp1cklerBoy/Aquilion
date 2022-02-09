using System;
using System.Globalization;
using System.Windows;

namespace Aquilion.Notepad.ValueConverters
{
    internal sealed class BoolToBoldTextStyleConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool toggle)
                return toggle ? FontWeights.Bold : FontWeights.Normal;

            return FontWeights.Normal;
        }
    }
    internal sealed class BoolToItalicTextStyleConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool toggle)
                return toggle ? FontStyles.Italic : FontStyles.Normal;

            return FontStyles.Normal;
        }
    }
    internal sealed class BoolToObliqueTextStyleConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool toggle)
                return toggle ? TextDecorations.Underline : null;

            return null;
        }
    }

}
