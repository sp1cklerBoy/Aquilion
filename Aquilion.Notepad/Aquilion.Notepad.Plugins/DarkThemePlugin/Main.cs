using System;
using System.Windows;

namespace Aquilion.Notepad.Plugins
{
    public class Main
    {
        public ResourceDictionary ThemeDictionary { get; set; } = null;

        public Main()
        {
            ThemeDictionary = (ResourceDictionary)Application.LoadComponent(new Uri("\\Dark.xaml", UriKind.Relative));
        }
    }
}
