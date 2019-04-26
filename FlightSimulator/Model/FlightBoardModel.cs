using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator.Model
{
    public class FlightBoardModel : BaseNotifyModel
    {
        private IClient sender;    
        private INotifierServer reciever;                   // check only .
        private bool stop;
        
        public FlightBoardModel()
        {
            sender = CommandSender.Instance;
            reciever = InfoReceiver.Instance;          /// check only .

            reciever.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                this.NotifyPropertyChanged(e.PropertyName);
            };

            stop = false;
        }

        public void Connect()
        {
            // getting the ip and the ports of the channels from the settings window.
            string ip = ApplicationSettingsModel.Instance.FlightServerIP;
            int commandPort = ApplicationSettingsModel.Instance.FlightCommandPort;
            int infoPort = ApplicationSettingsModel.Instance.FlightInfoPort;

            // listening at the info channel for clients.
            reciever.Start(ip, infoPort);

            if (reciever.IsConnected)
            {
                // starting to recieve information from the simulator .
                Thread thread = new Thread(() => reciever.Recieve(this, ref stop));
                thread.Start();
            }

            // connecting the app to the simulator as client .
            sender.Connect(ip, commandPort);
        }

        public void Dissconnect()
        {
            stop = true;
            sender.Dissconnect();
        }

        #region Latitude
        private double lat;
        public double Lat
        {
            set
            {
                lat = value;
            }
            get
            {
                return reciever.Lat;                // change
            }
        }
        #endregion

        #region Longitude
        private double lon;
        public double Lon
        {
            set
            {
                lon = value;
            }
            get
            {
                return reciever.Lon;             // change
            }
        }
        #endregion
    }
}
