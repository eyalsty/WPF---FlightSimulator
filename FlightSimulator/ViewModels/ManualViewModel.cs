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

        /* the ViewModel that responsible of the Manual.
         creates new Manual model and registers itself to be
         notified when its properties change, when the model's
         properties change, the vm notifies the view*/
        public ManualViewModel()
        {
            model = new ManualModel();
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        /* double type property responsible for value of throttle,binded to the slider
         * updates the model of the manual about changes happen in the view
         * manual and vice versa */
        public double VM_Throttle
        {
            set { model.Throttle = value; }
            get { return model.Throttle; }
        }

        /* string type property, binded to the Label of the throttle in the ManualView,
         * updates the model of the manual about changes happen in the view
         * manual and vice versa */
        public string VM_ThrottleLabel
        {
            set { model.ThrottleLabel = value; }
            get { return model.ThrottleLabel; }
        }

        //binded to the Rudder slider,updates the ManualModel about changes happen in the view
        public double VM_Rudder
        {
            set { model.Rudder = value; }
            get { return model.Rudder; }
        }

        //binded to the Rudder Label, display the value of the rudder according to the model
        public string VM_RudderLabel
        {
            set { model.RudderLabel = value; }
            get { return model.RudderLabel; }
        }

        /* the BELOW IS FOR CHECK AND IT IS NEW ! */

        //binded to the joystick, updates the model for changes
        public double VM_Aileron
        {
            set { model.Aileron = value; }
            get { return model.Aileron; }
        }

        //binded to the Aileron Label, display the value of the Aileron according to the model
        public string VM_AileronLabel
        {
            set { model.AileronLabel = value; }
            get { return model.AileronLabel; }
        }

        //binded to the joystick, updates the model for changes
        public double VM_Elevator
        {
            set { model.Elevator = value; }
            get { return model.Elevator; }
        }

        //binded to the Elevator Label, display the value of the Elevator according to the model

        public string VM_ElevatorLabel
        {
            set { model.ElevatorLabel = value; }
            get { return model.ElevatorLabel; }
        }
    }
}
