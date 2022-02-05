using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aquilion.Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Windows.Count > 1)
            {
                this.Close();
            }
            else
            {
                Process.GetCurrentProcess().Kill();
            }
        }
        private void MinimizeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExpandWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else if(WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
        }
    }
}
