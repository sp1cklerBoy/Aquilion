using Aquilion.Notepad.ViewModels;
using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Aquilion.Notepad
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static IContainer Host { get; private set; } = null;
        public IList<string> PluginsList { get; set; } = new List<string>();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Host = IoC.IoC.Bulid();
            Application.Current.MainWindow = Host.Resolve<MainWindow>();
            var ViewModel = Host.Resolve<MainWindowViewModel>();
            Application.Current.MainWindow.DataContext = ViewModel;
            Application.Current.MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document"))
                File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document");
        }

    }
}
