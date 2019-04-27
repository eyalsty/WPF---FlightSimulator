using FlightSimulator.Views.Interface;
using FlightSimulator.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator.Views
{
    public class SettingsDisplayer : IWindowDisplayer
    {
        Window settings;   
        public SettingsDisplayer()
        {
            this.settings = new SettingsWindow();    
        }
        
        public void Show()
        {
            settings?.ShowDialog();
        }

        public object Clone()
        {
            return new SettingsDisplayer();
        }

        public void Close()
        {
            // currently no need with this method.
            settings.Close();
        }
    }
}
