using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    /*inherits the INotifyPropertyChanged interface, forces everyone that inherits
     * this class to send a string with the property name when calling NotifyPropertyChanged.
     * also, holds the Propertychanged eventHandler - saves duplicated code */
    public abstract class BaseNotifyModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
