using Prism.Mvvm;

namespace Aquilion.Notepad.Core
{
    public abstract class FileEntityBaseViewModel : BindableBase
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
    }
}
