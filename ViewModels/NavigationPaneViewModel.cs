using Prism.Mvvm;
using Prism.Commands;
using System;

namespace Aquilion.Notepad.ViewModels
{
    public class NavigationPaneViewModel : BindableBase
    {
        public bool IsExpanded { get; set; }
        public MainWindowViewModel MainWindowViewModel { get; set; }

        public NavigationPaneViewModel()
        {
            NavigateCommand = new DelegateCommand<object>(OnNavigate);
        }


        public DelegateCommand<object> NavigateCommand { get; }
        private void OnNavigate(object obj)
        {
            MainWindowViewModel.ActiveSelectedPageViewModel = (PageBaseViewModel)obj;
        }
    }

    public enum PageType
    {
        Home,
        Settings,
        OpenFile,
        SaveFile
    }
}
