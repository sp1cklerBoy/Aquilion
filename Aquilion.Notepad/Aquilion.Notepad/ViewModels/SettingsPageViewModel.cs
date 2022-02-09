using Autofac;
using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Aquilion.Notepad.ViewModels
{
    public sealed class SettingsPageViewModel : PageBaseViewModel
    {
        private List<ResourceDictionary> resources = new List<ResourceDictionary>();
        
        public bool DarkModeEnabled { get; set; }
        public SettingsPageViewModel()
        {
            SwitchThemeCommand = new DelegateCommand<object>(OnSwitchTheme);



            foreach(ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
            {
                resources.Add(dictionary);
            }

            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\settings.json", json);

            var settings = JsonConvert.DeserializeObject($"{AppDomain.CurrentDomain.BaseDirectory}\\settings.json");

            //OnSwitchTheme(settings.DarkModeEnabled);
        }

        public DelegateCommand<object> SwitchThemeCommand { get; }
        private void OnSwitchTheme(object obj)
        {
           if(obj is bool toggle)
            {
                if (toggle)
                {
                    Application.Current.Resources.MergedDictionaries.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(resources[1]);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(resources[0]);
                }
            }

            string json = JsonConvert.SerializeObject(this);

            File.WriteAllText($"{AppDomain.CurrentDomain.BaseDirectory}\\settings.json", json);

            SettingsPageViewModel settings = JsonConvert.DeserializeObject<SettingsPageViewModel>($"{AppDomain.CurrentDomain.BaseDirectory}\\settings.json");

            OnSwitchTheme(settings.DarkModeEnabled);
        }

    }
}
