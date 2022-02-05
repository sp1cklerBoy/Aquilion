using Aquilion.Notepad.ViewModels;
using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Aquilion.Notepad
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Host { get; private set; } = null;

        private MainWindow window { get; set; } = null;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Host = IoC.IoC.Bulid();
            window = Host.Resolve<MainWindow>();
            window.DataContext = Host.Resolve<MainWindowViewModel>();
            window.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if(File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document"))
                File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document");
        }


    }
}
