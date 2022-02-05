using Aquilion.Notepad.Services.SynchronizationHelper;
using Prism.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Aquilion.Notepad.ViewModels
{
    public sealed class HomePageViewModel : PageBaseViewModel
    {
        #region Public Fields
        public MainWindowViewModel _mainWindowViewModel { get; set; }
        #endregion

        #region Private Fields
        private ISynchronizationHelper _synchronizationHelper;
        #endregion

        #region Public Properties
        public string FileName { get; set; }
        public string Name { get; set; }
        public string IsChanged { get; set; } = "";
        public string? FileContent { get; set; }
        #endregion

        #region Constructor
        public HomePageViewModel(ISynchronizationHelper synchronizationHelper)
        {
            _synchronizationHelper = synchronizationHelper;

            CreateCommand = new DelegateCommand<object>(OnCreateFile);
            OpenCommand = new DelegateCommand<object>(OnOpenFile);
            SaveCommand = new DelegateCommand<object>(OnSaveFile);
            OpenInDefaultNotepadCommand = new DelegateCommand<object>(OnOpenInWindowsNotepad);
        }
        #endregion

        #region Commands
        public DelegateCommand<object> CreateCommand { get; }
        public DelegateCommand<object> OpenCommand { get; }
        public DelegateCommand<object> OpenInDefaultNotepadCommand { get; }
        public DelegateCommand<object> SaveCommand { get; }
        #endregion

        #region Commands Methods
        private void OnCreateFile(object obj)
        {
            if(!File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document"))
            {
                File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document");
                File.Create($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document");
            }
            TryParseTextAsync(new FileViewModel(new FileInfo($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document")));
        }
        private void OnOpenFile(object obj)
        {
            _mainWindowViewModel.openFilePageViewModel.IsOpenMode = true;
            _mainWindowViewModel.openFilePageViewModel.IsSavingMode = false;
            _mainWindowViewModel.openFilePageViewModel.IsSelected = true;
            if (!_mainWindowViewModel.Pages.Contains(_mainWindowViewModel.openFilePageViewModel))
            {
                _mainWindowViewModel.Pages.Insert(0, _mainWindowViewModel.openFilePageViewModel);
            }
            var open = _mainWindowViewModel.openFilePageViewModel;
            _mainWindowViewModel.ActiveSelectedPageViewModel = open;
        }
        private void OnSaveFile(object obj)
        {
            _mainWindowViewModel.openFilePageViewModel.IsOpenMode = false;
            _mainWindowViewModel.openFilePageViewModel.IsSavingMode = true;
            _mainWindowViewModel.openFilePageViewModel.IsSelected = true;
            if (!_mainWindowViewModel.Pages.Contains(_mainWindowViewModel.openFilePageViewModel))
            {
                _mainWindowViewModel.Pages.Insert(0, _mainWindowViewModel.openFilePageViewModel);
            }
            var save = _mainWindowViewModel.openFilePageViewModel;
            _mainWindowViewModel.ActiveSelectedPageViewModel = save;
        }
        private void OnOpenInWindowsNotepad(object obj)
        {
            Process.Start("notepad.exe", FileName);
            Process.GetCurrentProcess().Kill();
        }
        #endregion

        #region Public Methods
        public async void TryParseTextAsync(FileViewModel file)
        {
            FileName = file.FullName;
            Name = file.Name;

            await Task.Run(() =>
            {
                _synchronizationHelper.InvokeAsync(() =>
                {
                    if(FileName != null)
                    {
                        try
                        {
                            FileContent = File.ReadAllText(FileName);
                        }
                        catch
                        {
                            FileContent = "";
                        }
                        
                    }
                    else
                    {
                        FileContent = "";
                    }
                });
            });
        }
        #endregion

        #region Private Methods
        #endregion
    }
}
