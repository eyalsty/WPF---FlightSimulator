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
    /*implements the INotifierServer interface, responsible for acting as a server-side, 
      function runs in new thread, being updated from the simulator and updates the VM 
      the view for relevant changes */
    public class InfoReceiver : INotifierServer
    {
        /* implementation of the singletone design pattern, in order to force existence of
           only one instance of the InfoReceiver object in our program (more then one 
           InfoReceiver can cause ambiguous behavior)we make it static and the
           constructor first check if the member already been initialized, if not-creates it */
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

        // data members
        private TcpListener listener;
        private TcpClient client;

        //constructor
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
        /*starting new server-side connection, starts listening for clients to connect
        and is blocked until connection has been established */
        public void Start(string ip, int port)
        {
            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();

            client = listener.AcceptTcpClient();
        }
        /* function that acts like a server,runs in a different thread, always
         * reading data from the Flight Simulator (the client) and updates the
         * lon and the lat values */
        public void Recieve(ref bool stop)
        {
            NetworkStream stream = client.GetStream();
            bool updated = false;

            while (!stop)
            {
                byte[] data = new byte[client.ReceiveBufferSize];
                int read = stream.Read(data, 0, data.Length);
                string[] recieved = Encoding.ASCII.GetString(data, 0, read).Split(',');
                double throttle = Convert.ToDouble(recieved[21]);
                if (!updated && (throttle > 0))
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
            //stop open thread's run and close connections before terminating the program
            listener.Stop();
            client.Close();
        }
        /*holds the latitude values, being updated from the Simulator
          values received by the server */
        #region Longitude
        private double lon;
        public double Lon
        {
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
            get
            {
                return lon;
            }
        }
        #endregion
        /*holds the latitude values, being updated from the Simulator
        values received by the server */
        #region latitude
        private double lat;
        public double Lat
        {
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
            get
            {
                return lat;
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
