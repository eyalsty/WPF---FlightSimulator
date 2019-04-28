using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class ManualModel : BaseNotify
    {
        /* data member */
        private IClient client;

        /* initialize the client data member with its singletone attribute. */
        public ManualModel()
        {
            client = CommandSender.Instance;
        }

        #region Throttle
        /* Property responsible for the Throttle value.
         * when being set (from the VM which get its value from the view),
         * rounds the value and sends the new value to the simulator.
         * also notifies the VM that the Thorttle changed. */
        private double throttle;
        public double Throttle
        {
            set
            {
                throttle = value;
                this.ThrottleLabel = Math.Round(value, 2).ToString();
                NotifyPropertyChanged("Throttle");
                SendValue("set controls/engines/current-engine/throttle", value);
            }
            get
            {
                return throttle;
            }
        }

        private string throttleLabel;
        public string ThrottleLabel
        {
            set
            {
                //update the throttle Label and notifies the VM for the change.
                throttleLabel = value;
                NotifyPropertyChanged("ThrottleLabel");
            }
            get
            {
                return Math.Round(throttle, 2).ToString();
            }
        }
        #endregion

        #region Rudder
        /* Property responsible for the Rudder value.
           when being se t(from the VM, which get its value from the view),
           rounds the value and sends the new value to the simulator.
           also notifies the VM that the Rudder changed. */
        private double rudder;
        public double Rudder
        {
            set
            {
                rudder = value;
                this.RudderLabel = Math.Round(value, 2).ToString();
                NotifyPropertyChanged("Rudder");
                SendValue("set controls/flight/rudder", value);
            }
            get
            {
                return rudder;
            }
        }

        private string rudderLabel;
        public string RudderLabel
        {
            set
            {
                // updates the new Label of the rudder Label and notifies the VM for changes.
                rudderLabel = value;
                NotifyPropertyChanged("RudderLabel");
            }
            get
            {
                return Math.Round(rudder, 2).ToString();
            }
        }
        #endregion

        #region Aileron
        private double aileron;
        public double Aileron
        {
            set
            {
                // updates the Simulator with the new value of the Aileron .
                aileron = value;
                this.AileronLabel = Math.Round(value, 2).ToString();
                NotifyPropertyChanged("Aileron");
                SendValue("set controls/flight/aileron", value);
            }
            get
            {
                return aileron;
            }
        }

        private string aileronLabel;
        public string AileronLabel
        {
            set
            {
                //updates the new value of the Aileron Label and notifies the VM for changes
                aileronLabel = value;
                NotifyPropertyChanged("AileronLabel");
            }
            get
            {
                return Math.Round(aileron, 2).ToString();
            }
        }
        #endregion

        #region Elevator
        private double elevator;
        public double Elevator
        {
            set
            {
                // updates the Simulator with the new value of the Elevator.
                elevator = value;
                this.ElevatorLabel = Math.Round(value, 2).ToString();
                NotifyPropertyChanged("Elevator");
                SendValue("set controls/flight/elevator", value);
            }
            get
            {
                return elevator;
            }
        }

        private string elevatorLabel;
        public string ElevatorLabel
        {

            set
            {
                // updates the new value of the elevator Label and notifies the VM for changes.
                elevatorLabel = value;
                NotifyPropertyChanged("ElevatorLabel");
            }
            get
            {
                return Math.Round(elevator, 2).ToString();
            }
        }

        #endregion

        /* function to create correct format string for the Flight Simulator
         * and send it via the client. */
        private void SendValue(string path, double value)
        {
            string line = path + " " + Math.Round(value, 2).ToString() + "\r\n";

            client.Send(line);
        }

    }
}