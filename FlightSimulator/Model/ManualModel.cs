using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class ManualModel : BaseNotifyModel
    {
        private IClient client;
        
        public ManualModel()
        {
            client = CommandSender.Instance;
        }

        #region Throttle
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
                elevatorLabel = value;
                NotifyPropertyChanged("ElevatorLabel");
            }
            get
            {
                return Math.Round(elevator, 2).ToString();
            }
        }

        #endregion

        private void SendValue(string path, double value)
        {
            string line = path + " " + Math.Round(value, 2).ToString() + "\r\n";

            client.Send(line);
        }

    }
}