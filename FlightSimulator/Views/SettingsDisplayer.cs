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
    /// <summary>
    /// A class which creates stting window and has the abillity
    /// to show ansd close it. also can create a new instances
    /// that  window .
    /// </summary>
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
            settings.Close();
        }
    }
}
