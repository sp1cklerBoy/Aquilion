using Aquilion.Notepad.Services.SynchronizationHelper;
using Prism.Mvvm;

namespace Aquilion.Notepad.ViewModels
{
    public abstract class FileEntityBaseViewModel : BindableBase
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
    }
}
