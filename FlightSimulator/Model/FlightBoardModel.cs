using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
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
        private IServer reciever;
        private bool stop;
        
        public FlightBoardModel()
        {
            sender = CommandSender.Instance;
            reciever = InfoReceiver.Instance;

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
                NotifyPropertyChanged("Lat");
                Console.WriteLine("Lat set, in flightboardModel\n");       // CHECK ONLY !
            }
            get
            {
                return lat;
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
                NotifyPropertyChanged("Lon");
                Console.WriteLine("Lon set, in flightboardModel\n");       // CHECK ONLY !
            }
            get
            {
                return lon;
            }
        }
        #endregion
    }
}
