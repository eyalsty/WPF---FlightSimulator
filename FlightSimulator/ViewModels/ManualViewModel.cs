using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    public class ManualViewModel : BaseNotify
    {
        private ManualModel model;

        public ManualViewModel()
        {
            model = new ManualModel();
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public double VM_Throttle
        {
            set { model.Throttle = value; }
            get { return model.Throttle; }
        }

        public string VM_ThrottleLabel
        {
            set { model.ThrottleLabel = value; }
            get { return model.ThrottleLabel; }
        }

        public double VM_Rudder
        {
            set { model.Rudder = value; }
            get { return model.Rudder; }
        }

        public string VM_RudderLabel
        {
            set { model.RudderLabel = value; }
            get { return model.RudderLabel; }
        }

        /* the BELOW IS FOR CHECK AND IT IS NEW ! */

        public double VM_Aileron
        {
            set { model.Aileron = value; }
            get { return model.Aileron; }
        }

        public string VM_AileronLabel
        {
            set { model.AileronLabel = value; }
            get { return model.AileronLabel; }
        }

        public double VM_Elevator
        {
            set { model.Elevator = value; }
            get { return model.Elevator; }
        }

        public string VM_ElevatorLabel
        {
            set { model.ElevatorLabel = value; }
            get { return model.ElevatorLabel; }
        }
    }
}
