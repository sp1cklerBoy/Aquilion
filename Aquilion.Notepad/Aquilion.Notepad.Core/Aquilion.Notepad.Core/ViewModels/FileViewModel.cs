using System;
using System.IO;
using System.Threading.Tasks;

namespace Aquilion.Notepad.Core
{
    public sealed class FileViewModel : FileEntityBaseViewModel
    {
        public string FileContent { get; set; }
        public string Size { get; set; }
        public FileViewModel(FileInfo info = null)
        {
            if(info != null)
            {
                FullName = info.FullName;
                Name = info.Name;
                Date = info.LastWriteTimeUtc.ToString();
                Type = $"File {info.Extension.ToUpper()}";
                Size = FormatSize(info.Length);
            }
        }
        protected internal string FormatSize(long length)
        {
            string size;

            if (length <= (long)Math.Pow(2, 10)) size = length != 0 ? $"1 KB" : "0 KB";
            else if (length <= (long)Math.Pow(2, 20)) size = $"{Math.Round(length / Math.Pow(2, 10))} KB";
            else if (length <= (long)Math.Pow(2, 30)) size = $"{Math.Round(length / Math.Pow(2, 20))} MB";
            else if (length <= (long)Math.Pow(2, 40)) size = $"{Math.Round(length / Math.Pow(2, 30))} GB";
            else if (length <= (long)Math.Pow(2, 50)) size = $"{Math.Round(length / Math.Pow(2, 40))} TB";
            else if (length <= (long)Math.Pow(2, 60)) size = $"{Math.Round(length / Math.Pow(2, 50))} PB";
            else if (length <= (long)Math.Pow(2, 70)) size = $"{Math.Round(length / Math.Pow(2, 60))} EB";
            else if (length <= (long)Math.Pow(2, 80)) size = $"{Math.Round(length / Math.Pow(2, 70))} ZB";
            else if (length <= (long)Math.Pow(2, 90)) size = $"{Math.Round(length / Math.Pow(2, 80))} YB";
            else size = "HZ";

            return size;
        }
    }
}
