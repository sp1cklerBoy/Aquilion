using Aquilion.Notepad.Pages.HomePage;
using Aquilion.Notepad.Pages.OpenPage;
using Aquilion.Notepad.Pages.SettingsPage;
using Aquilion.Notepad.Core;
using Aquilion.Notepad.Helper;
using Aquilion.Notepad.ViewModels;
using Autofac;

namespace Aquilion.Notepad.IoC
{
    internal sealed class IoC
    {
        public static IContainer Bulid()
        {
            var services = new ContainerBuilder();

            RegisterServices(services);

            return services.Build();
        }

        private static void RegisterServices(ContainerBuilder services)
        {
            services.RegisterType<MainWindow>();
            services.RegisterType<MainWindowViewModel>().SingleInstance();

            services.RegisterType<Synchronization>().As<ISynchronizationHelper>().SingleInstance();
            services.RegisterType<SettingsPageViewModel>().SingleInstance();
            services.RegisterType<HomePageControl>().SingleInstance();
            services.RegisterType<SettingsPageControl>().SingleInstance();
            services.RegisterType<HomePageViewModel>().SingleInstance();
            services.RegisterType<OpenFilePageViewModel>().SingleInstance();
            services.RegisterType<OpenFilePage>().SingleInstance();


        }
    }
}
