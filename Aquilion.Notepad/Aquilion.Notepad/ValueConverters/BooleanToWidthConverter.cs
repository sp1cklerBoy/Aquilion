using System;
using System.Globalization;
using System.Windows;

namespace Aquilion.Notepad.ValueConverters
{
    internal sealed class BooleanToWidthConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool toggle)
                return toggle ? 250 : 48;

            return 48;
        }
    }
    internal sealed class BooleanToVisibilityConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool toggle)
                return toggle ? Visibility.Visible : Visibility.Collapsed;

            return Visibility.Collapsed;
        }
    }
    internal sealed class BooleanToWindowStateConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool toggle)
                return toggle ? WindowState.Maximized : WindowState.Normal;

            if (value is WindowState state)
                return state == WindowState.Maximized ? new Thickness(8) : new Thickness(0);

            return WindowState.Normal;
        }
    }

    internal sealed class WindowStateToBoolConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {if (value is WindowState state)
                return state == WindowState.Maximized ? true : false;

            return true;
        }
    }
    internal sealed class WindowStateToPaddingConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowState state)
                return state == WindowState.Maximized ? 6 : 0;

            return 0;
        }
    }
    internal sealed class WindowStateToIconConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowState state)
                return state == WindowState.Maximized ? "" : "";

            return "";
        }
    }

}
