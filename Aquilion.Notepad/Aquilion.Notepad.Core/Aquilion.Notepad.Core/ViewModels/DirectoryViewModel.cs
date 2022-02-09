using System.IO;

namespace Aquilion.Notepad.Core
{
    public sealed class DirectoryViewModel : FileEntityBaseViewModel
    {
        public DirectoryViewModel(DirectoryInfo info = null)
        {
            FullName = info != null ? info.FullName : "";
            Name = info != null ? info.Name : "";
            Date = info != null ? info.LastWriteTimeUtc.ToString() : "";
            Type = "File Folder";
        }
    }
}
