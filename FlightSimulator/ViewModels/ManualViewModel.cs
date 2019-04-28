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
        /* data member */
        private ManualModel model;

        /* the ViewModel that responsible of the Manual.
         creates new Manual model and registers itself to be
         notified when its properties change, when the model's
         properties change, the vm notifies the view. */
        public ManualViewModel()
        {
            model = new ManualModel();
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        /* double type property responsible for value of throttle,binded to the slider,
         * updates the model of the manual about changes happen in the view
         * manual  */
        public double VM_Throttle
        {
            set { model.Throttle = value; }
        }

        /* string type property, binded to the Label of the throttle in the ManualView,
         * updates the VIEW of the manual about changes happen in the  manual model */
        public string VM_ThrottleLabel
        {
            get { return model.ThrottleLabel; }
        }

        /* binded to the Rudder Slider,updates the ManualModel 
         * about changes happen in the view. */
        public double VM_Rudder
        {
            set { model.Rudder = value; }
        }

        /* binded to the Rudder Label, display the value of the
         * rudder according to the model. */
        public string VM_RudderLabel
        {
            get { return model.RudderLabel; }
        }

        /* binded to the Joystick, updates the ManualModel 
         * for changes happen in the VIEW. */
        public double VM_Aileron
        {
            set { model.Aileron = value; }
        }

        /* binded to the Aileron Label, display the value of 
         * the Aileron according to the model. */
        public string VM_AileronLabel
        {
            get { return model.AileronLabel; }
        }

        /* binded to the Joystick, updates the model 
         * for changes happen in the VIEW. */
        public double VM_Elevator
        {
            set { model.Elevator = value; }
        }

        /* binded to the Elevator Label, display the value of 
         * the Elevator according to the model. */
        public string VM_ElevatorLabel
        {
            get { return model.ElevatorLabel; }
        }
    }
}
