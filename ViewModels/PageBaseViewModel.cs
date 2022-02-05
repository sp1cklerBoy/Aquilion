using Prism.Mvvm;

namespace Aquilion.Notepad.ViewModels
{
    public abstract class PageBaseViewModel : BindableBase
    {
        public string? Header { get; set; }
        public PageType Type { get; set; }
        public bool? IsSelected { get; set; }
    }
}
