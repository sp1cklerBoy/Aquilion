using Aquilion.Notepad.ViewModels;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aquilion.Notepad.Pages.SettingsPage
{
    /// <summary>
    /// Логика взаимодействия для SettingsPageControl.xaml
    /// </summary>
    public partial class SettingsPageControl : UserControl
    {
        public SettingsPageControl()
        {
            InitializeComponent();
            DataContext = App.Host.Resolve<SettingsPageViewModel>();
        }
    }
}
