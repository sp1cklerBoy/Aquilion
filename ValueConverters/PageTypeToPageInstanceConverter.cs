using Aquilion.Notepad.ViewModels;
using System;
using System.Globalization;
using Autofac;
using Aquilion.Notepad.Pages.HomePage;
using Aquilion.Notepad.Pages.SettingsPage;
using System.Windows.Controls;
using Aquilion.Notepad.Pages.OpenPage;

namespace Aquilion.Notepad.ValueConverters
{
    internal sealed class PageTypeToPageInstanceConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserControl ActivePage = null;
            var homePage = App.Host.Resolve<HomePageControl>();
            var settingsPage = App.Host.Resolve<SettingsPageControl>();
            var openPage = App.Host.Resolve<OpenFilePage>();
            switch ((PageType)value)
            {
                case PageType.Home:
                    ActivePage = homePage;
                    break;
                case PageType.Settings:
                    ActivePage = settingsPage;
                    break;
                case PageType.OpenFile:
                    ActivePage = openPage;
                    break;
            }
            return ActivePage;
        }
    }
}
