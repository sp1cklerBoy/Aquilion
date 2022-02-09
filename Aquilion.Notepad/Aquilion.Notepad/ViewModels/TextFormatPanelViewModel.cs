using System.Windows;
using System.Linq;
using System.Collections.Generic;
using Prism.Mvvm;

namespace Aquilion.Notepad.ViewModels
{
    public class TextFormatPanelViewModel : BindableBase
    {
        #region Public Properties
        public bool IsExpanded { get; set; }
        public string FontName { get; set; }
        public int Size { get; set; }
        public FontWeight Style { get; set; }
        public string Background { get; set; }
        public string Foreground { get; set; }

        public List<string> FontNames { get; set; } = new List<string>()
        {
            "Arial",
            "Calibri",
            "Georgia",
            "Impact",
            "MS Sans Serif",
            "Segoe UI",
            "Tahoma",
            "Times New Roman",
            "Veradana",
            "Webdings",
            "Windgdings"
        };
        public List<int> FontSizes { get; set; } = new List<int>()
        {
            4,
            8,
            9,
            10,
            12,
            14,
            16,
            18,
            20,
            24
        };
        public List<string> Colors { get; set; } = new List<string>()
        {
            "Red",
            "Yellow",
            "Black",
            "Blue",
            "White",
            "None"
        };
        #endregion

        #region Constructor
        public TextFormatPanelViewModel()
        {
            FontName = FontNames.FirstOrDefault();
            Size = FontSizes[5];
            Background = Colors[5];
            Foreground = Colors[0];
        }
        #endregion
    }
}
