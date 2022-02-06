using Aquilion.Notepad.Services.SynchronizationHelper;
using Prism.Commands;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
        public string? SelectedText { get; set; }
        #endregion

        #region Constructor
        public HomePageViewModel(ISynchronizationHelper synchronizationHelper)
        {
            _synchronizationHelper = synchronizationHelper;

            CreateCommand = new DelegateCommand<object>(OnCreateFile);
            OpenCommand = new DelegateCommand<object>(OnOpenFile);
            SaveCommand = new DelegateCommand<object>(OnSaveFile);

            CopyCommand = new DelegateCommand<object>(OnCopy);
            PasteCommand = new DelegateCommand<object>(OnPaste);
            CutCommand = new DelegateCommand<object>(OnCut);

            OpenInDefaultNotepadCommand = new DelegateCommand<object>(OnOpenInWindowsNotepad);
            
        }
        #endregion

        #region Commands
        public DelegateCommand<object> CreateCommand { get; }
        public DelegateCommand<object> OpenCommand { get; }
        public DelegateCommand<object> OpenInDefaultNotepadCommand { get; }
        public DelegateCommand<object> SaveCommand { get; }
        public DelegateCommand<object> CopyCommand { get; }
        public DelegateCommand<object> PasteCommand { get; }
        public DelegateCommand<object> CutCommand { get; }
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
            _mainWindowViewModel.navigationPaneViewModel.NavigateCommand.Execute(_mainWindowViewModel.openFilePageViewModel);
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
            _mainWindowViewModel.navigationPaneViewModel.NavigateCommand.Execute(_mainWindowViewModel.openFilePageViewModel);
        }
        private void OnOpenInWindowsNotepad(object obj)
        {
            Process.Start("notepad.exe", FileName);
            Process.GetCurrentProcess().Kill();
        }
        private void OnCopy(object obj)
        {
            Clipboard.SetText((string)obj);
        }
        private void OnCut(object obj)
        {
            Clipboard.SetText((string)obj);
            FileContent.Trim(obj.ToString().ToCharArray());
        }
        private void OnPaste(object obj)
        {
            if (Clipboard.ContainsText())
            {
                FileContent = FileContent + @Clipboard.GetText();
            }
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
