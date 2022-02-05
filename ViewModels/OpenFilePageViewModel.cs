using Aquilion.Notepad.Services.History;
using Aquilion.Notepad.Services.SynchronizationHelper;
using Autofac;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Aquilion.Notepad.ViewModels
{
    public sealed class OpenFilePageViewModel : PageBaseViewModel
    {
        #region Public Fields
        public MainWindowViewModel _mainWindowViewModel { get; set; }
        #endregion

        #region Private Fields

        private IHistory _history;
        private readonly ISynchronizationHelper _synchronizationHelper;
        #endregion

        #region Public Properties
        public string FileName { get; set; }
        public string Name { get; set; }
        public bool IsSavingMode { get; set; } = false;
        public bool IsOpenMode { get; set; } = true;

        public ObservableCollection<DirectoryViewModel> DirectoryTreeModels { get; set; } = new ObservableCollection<DirectoryViewModel>();
        public ObservableCollection<FileEntityBaseViewModel> DirectoriesAndFiles { get; set; } = new ObservableCollection<FileEntityBaseViewModel>();
        public FileEntityBaseViewModel SelectedFileEntityViewModel { get; set; }

        #endregion

        #region Constructor
        public OpenFilePageViewModel(ISynchronizationHelper synchronizationHelper)
        {
            _synchronizationHelper = synchronizationHelper;

            _history = new NavigationHistory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Documents");

            SelectViewModelCommand = new DelegateCommand<object>(OnSelectViewModel);
            OpenCommand = new DelegateCommand<object>(OnOpen);
            MoveBackCommand = new DelegateCommand<object>(OnMoveBack, OnCanMoveBack);
            MoveForwardCommand = new DelegateCommand<object>(OnMoveForward, OnCanMoveForward);
            MoveToParentCommand = new DelegateCommand<object>(OnMoveToParent, OnCanMoveToParent);

            CloseCommand = new DelegateCommand<object>(OnClose);

            TakeFileCommand = new DelegateCommand<object[]>(OnTakeFile);

            Name = "Documents";
            FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            LoadTreeViewDirectories();
            LoadDirectoriesAndFilesAsync(FileName);

            _history.HistoryChanged += _history_HistoryChanged;
        }

        #endregion

        #region Commands
        public DelegateCommand<object> SelectViewModelCommand { get; }
        public DelegateCommand<object> OpenCommand { get; }
        public DelegateCommand<object> MoveBackCommand { get; }
        public DelegateCommand<object> MoveForwardCommand { get; }
        public DelegateCommand<object> MoveToParentCommand { get; }
        public DelegateCommand<object> CloseCommand { get; }
        public DelegateCommand<object[]> TakeFileCommand { get; }
        #endregion

        #region Commands Methods
        public void OnSelectViewModel(object obj)
        {
            if(obj is FileViewModel fileModel)
            {
                fileModel.FileContent = File.ReadAllText(fileModel.FullName);
            }
        }
        public void OnOpen(object obj)
        {
            string replacedPath = string.Empty;
            if (obj is DirectoryViewModel directoryViewModel)
            {
                replacedPath = directoryViewModel.FullName;
                //Make Name is name of directoryViewModel
                //Make FileName is fullname of directoryViewModel
                if(Name == directoryViewModel.Name & FileName == replacedPath)
                {
                    return;
                }
                Name = directoryViewModel.Name;
                FileName = replacedPath;

                //Add it to history
                _history.Add(FileName, Name);

                //Open
                LoadDirectoriesAndFilesAsync(FileName);
                return;

            }
            if(obj is DriveItemModel driveItemModel)
            {
                //Make Name is name of directoryViewModel
                //Make FileName is fullname of directoryViewModel
                if (Name == driveItemModel.Name & FileName == driveItemModel.FullName)
                {
                    return;
                }
                Name = driveItemModel.Name;
                FileName = driveItemModel.FullName;

                //Add it to history
                _history.Add(FileName, Name);

                //Open
                LoadDirectoriesAndFilesAsync(FileName);
                return;
            }

            
        }
        public bool OnCanMoveBack(object arg)
        {
            //Returns can we move back in history
            return _history.CanMoveBack;
        }
        public void OnMoveBack(object obj)
        {
            //Moving back in history
            _history.MoveBack();

            var current = _history.Current;
            {
                //Make Name is name of currentNode.PathName
                //Make FileName is fullname of currentNode.Path
                Name = current.PathName;
                FileName = current.Path;

                //Open
                LoadDirectoriesAndFilesAsync(FileName);
            }
        }
        public bool OnCanMoveForward(object arg)
        {
            //Returns can we move forward in history
            return _history.CanMoveForward;
        }
        public void OnMoveForward(object obj)
        {
            //Moving forward in history
            _history.MoveForward();

            var current = _history.Current;
            {
                //Make Name is name of currentNode.PathName
                //Make FileName is fullname of currentNode.Path
                Name = current.PathName;
                FileName = current.Path;

                //Open
                LoadDirectoriesAndFilesAsync(FileName);
            }
        }
        public bool OnCanMoveToParent(object arg)
        {
            //Returns can we move to parent directory
            return new DirectoryInfo(FileName).Parent != null;
        }
        public void OnMoveToParent(object obj)
        {
            //Create directory info instance
            var parent = new DirectoryInfo(FileName).Parent;

            //Make Name is name of Parent Folder Name
            //Make FileName is fullname of Parent Folder FullName
            Name = parent.Name;
            FileName = parent.FullName;

            //Open
            LoadDirectoriesAndFilesAsync(FileName);
        }
        public void OnClose(object obj)
        {

            _mainWindowViewModel.homePageViewModel.IsSelected = true;
            var first = _mainWindowViewModel.homePageViewModel;
            _mainWindowViewModel.ActiveSelectedPageViewModel = first;
            _mainWindowViewModel.Pages.RemoveAt(0);
        }
        public void OnTakeFile(object[] obj)
        {
            if (obj[1] is DriveItemModel || obj[1] is DirectoryViewModel)
            {
                OpenCommand.Execute(obj);
                return;
            }

            if (obj[0] is bool saving)
            {
                var model = obj[1] is FileViewModel ? obj[1] as FileViewModel : null;
                if (saving)
                {
                    if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document"))
                        File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document");
                    try
                    {
                        File.WriteAllText(FileName + $"\\{obj[2]}", _mainWindowViewModel.homePageViewModel.FileContent);
                        _mainWindowViewModel.homePageViewModel.TryParseTextAsync(new FileViewModel(new FileInfo(FileName + $"\\{obj[2]}")));
                    }
                    catch { }
                    CloseCommand.Execute(this);
                    return;
                }
                else
                {
                    if (File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document"))
                        File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\[notepad_temp]New Document");
                    _mainWindowViewModel.homePageViewModel.TryParseTextAsync(model);
                    CloseCommand.Execute(this);
                }
            }
        }
        #endregion

        #region Public Methods
        public void LoadDirectoriesAndFilesAsync(string path)
        {
            //Clears DirectoriesAndFiles Collection
            DirectoriesAndFiles.Clear();
            if(Name == "Computer")
            {
                foreach(var LogicalDrive in DriveInfo.GetDrives())
                {
                    DirectoriesAndFiles.Add(new DriveItemModel(LogicalDrive));
                }
                return;
            }
            var di = new DirectoryInfo(path);
            foreach (var Directory in di.EnumerateDirectories())
            {
                var directoryModel = new DirectoryViewModel(Directory);
                if (!Directory.Attributes.HasFlag(FileAttributes.System))
                {

                    DirectoriesAndFiles.Add(directoryModel);
                    
                }
            }
            foreach (var File in di.EnumerateFiles())
            {
                switch (File.Extension.ToUpper())
                {
                    case ".RTF":
                    case ".TXT":
                    case ".DOC":
                    case ".DOCX":
                    case ".CS":
                    case ".XAML":
                    case ".CSPROJ":
                    case ".XML":
                    case ".HTML":
                    case ".HTM":
                        var fileModel = new FileViewModel(File);
                        if (!File.Attributes.HasFlag(FileAttributes.System))
                        {
                            DirectoriesAndFiles.Add(fileModel);
                            
                        }
                        break;
                }
            }
        }
        public void LoadTreeViewDirectories()
        {
            //Load quick folders items
            {
                DirectoryTreeModels.Add(new DirectoryViewModel
                {
                    Name = Environment.UserName,
                    FullName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                });
                
                DirectoryTreeModels.Add(new DirectoryViewModel
                {
                    Name = "Documents",
                    FullName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                });

                DirectoryTreeModels.Add(new DirectoryViewModel
                {
                    Name = "Computer",
                    FullName = "computer:\\"
                });
            }
        }
        #endregion

        #region Private Methods
        private void _history_HistoryChanged(object sender, EventArgs e)
        {
            MoveBackCommand.RaiseCanExecuteChanged();
            MoveForwardCommand.RaiseCanExecuteChanged();
            MoveToParentCommand.RaiseCanExecuteChanged();
        }
        #endregion
    }

}
