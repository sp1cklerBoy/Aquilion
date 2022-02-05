using Prism.Mvvm;
using System.Collections.ObjectModel;
using Autofac;
using System.Linq;

namespace Aquilion.Notepad.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Public Properties
        public string TitleWindow { get; set; } = "Notepad (Pre-Alpha)";
        public HomePageViewModel homePageViewModel { get; set; }
        public SettingsPageViewModel settigsPageViewModel { get; set; }
        public OpenFilePageViewModel openFilePageViewModel { get; set; }

        public NavigationPaneViewModel navigationPaneViewModel { get; set; } = new NavigationPaneViewModel();
        public TextFormatPanelViewModel textFormatPanelViewModel { get; set; } = new TextFormatPanelViewModel();
        public ObservableCollection<PageBaseViewModel> Pages { get; set; } =
            new ObservableCollection<PageBaseViewModel>();

        public PageBaseViewModel ActiveSelectedPageViewModel { get; set; }
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            homePageViewModel = App.Host.Resolve<HomePageViewModel>();
            settigsPageViewModel = App.Host.Resolve<SettingsPageViewModel>();
            openFilePageViewModel = App.Host.Resolve<OpenFilePageViewModel>();

            openFilePageViewModel._mainWindowViewModel = this;
            openFilePageViewModel.Header = "Browse";
            openFilePageViewModel.Type = PageType.OpenFile;
            homePageViewModel._mainWindowViewModel = this;
            homePageViewModel.Header = "Editing";
            homePageViewModel.Type = PageType.Home;
            homePageViewModel.CreateCommand.Execute(null);
            settigsPageViewModel.Header = "Settings";
            settigsPageViewModel.Type = PageType.Settings;



            navigationPaneViewModel.MainWindowViewModel = this;

            Pages.Add(openFilePageViewModel);
            Pages.Add(homePageViewModel);
            Pages.Add(settigsPageViewModel);

            ActiveSelectedPageViewModel = Pages.FirstOrDefault();
        }
        #endregion
    }
}
