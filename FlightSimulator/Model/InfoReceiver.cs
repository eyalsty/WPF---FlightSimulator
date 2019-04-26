using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator.Model
{
    public class InfoReceiver : INotifierServer        // for check only !
    {
        #region Singelton
        private static INotifierServer instance = null;
        public static INotifierServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InfoReceiver();
                }

                return instance;
            }
        }
        #endregion

        private TcpListener listener;
        private TcpClient client;

        
        public InfoReceiver()
        {
            listener = null;
            client = null;
        }

        public bool IsConnected
        {
            get
            {
                return client.Connected;
            }
        }

        public void Start(string ip, int port)
        {
            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();

            client = listener.AcceptTcpClient();
        }

        public void Recieve(FlightBoardModel model, ref bool stop)
        {

            NetworkStream stream = client.GetStream();

            bool updated = false;

            while (!stop)
            {
                byte[] data = new byte[client.ReceiveBufferSize];
                int read = stream.Read(data, 0, data.Length);

                string[] recieved = Encoding.ASCII.GetString(data, 0, read).Split(',');

                // FOR CHECK ONLY !!!
                if (recieved.Length >= 2)
                    Console.WriteLine(" the recieved values are: " + recieved[0] + " " + recieved[1]);

                if (!updated)
                {
                    lon = Convert.ToDouble(recieved[0]);
                    lat = Convert.ToDouble(recieved[1]);
                    updated = true;
                }

                if (updated)
                {
                    this.Lon = Convert.ToDouble(recieved[0]);
                    this.Lat = Convert.ToDouble(recieved[1]);
                }
            }

            listener.Stop();
            client.Close();
        }

        #region Longitude
        private double lon;
        public double Lon
        {
            get
            {
                return lon;
            }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }
        #endregion

        #region latitude
        private double lat;
        public double Lat
        {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }
        #endregion

        #region Notifier
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
