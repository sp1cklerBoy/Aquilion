using System;
using System.IO;

namespace Aquilion.Notepad.ViewModels
{
    public sealed class DriveItemModel : FileEntityBaseViewModel
    {
        public long TotalFreeSpace { get; }
        public long TotalSize { get; }
        public double UsedPercentage { get; set; }
        public string Size { get; set; }
        public DriveItemModel(DriveInfo info)
        {
            FullName = info.Name ?? "";
            Name = info.IsReady ? info.VolumeLabel != "" ? $"{info.VolumeLabel} ({info.Name})" : $"{info.DriveType} ({info.Name})" : $"{info.DriveType} ({info.Name})";
            Date = "";
            Type = "Drive";
            TotalSize = info.IsReady ? info.TotalSize : 0;
            TotalFreeSpace = info.IsReady ? info.TotalFreeSpace : 0;
            UsedPercentage = (TotalSize - TotalFreeSpace) / (double)TotalSize * 100;
            Size = $"{FormatSize(TotalFreeSpace)} / {FormatSize(TotalSize)}";
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
