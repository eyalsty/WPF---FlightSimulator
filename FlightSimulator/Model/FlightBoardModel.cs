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
        private INotifierServer reciever;
        private bool stop;
        
        public FlightBoardModel()
        {
            sender = CommandSender.Instance;
            reciever = InfoReceiver.Instance;

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
                Thread thread = new Thread(() => reciever.Recieve(ref stop));
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
        public double Lat
        {
            get { return reciever.Lat; }
        }
        #endregion

        #region Longitude
        public double Lon
        {
            get { return reciever.Lon; }
        }
        #endregion
    }
}
