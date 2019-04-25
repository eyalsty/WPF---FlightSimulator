using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator.Model
{
    public class InfoReceiver : IServer
    {
        #region Singelton
        private static IServer instance = null;
        public static IServer Instance
        {
            get
            {
                if(instance == null)
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

        public void Start(string ip, int port)
        {
            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();

            client = listener.AcceptTcpClient();
        }
        
        // this is the true one .
        /*public void Recieve(FlightBoardModel model, ref bool stop)
        {
            
            NetworkStream stream = client.GetStream();
            
            while (!stop)
            {
                byte[] data = new byte[client.ReceiveBufferSize];
                int read = stream.Read(data, 0, data.Length);

                string[] recieved = Encoding.ASCII.GetString(data, 0, read).Split(',');
                
                // FOR CHECK ONLY !!!
                if (recieved.Length >= 2)
                    Console.WriteLine(" the recieved values are: " + recieved[0] + " " + recieved[1]);
                
                model.Lon = Convert.ToDouble(recieved[0]);
                model.Lat = Convert.ToDouble(recieved[1]);   
            }

            listener.Stop();
            client.Close();
        }*/

        public void Recieve(FlightBoardModel model, ref bool stop)
        {

            NetworkStream stream = client.GetStream();
            double lon = 0;
            double lat = 0;
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
                    Console.WriteLine("updated = true !!!!\n");     // CHECK ONLY !
                    updated = true;
                }
                
                if (updated)
                {
                    model.Lon = Convert.ToDouble(recieved[0]);
                    model.Lat = Convert.ToDouble(recieved[1]);
                    Console.WriteLine("continue with updated = true !!!\n");     // CHECK ONLY !
                }
            }

            listener.Stop();
            client.Close();
        }

        public bool IsConnected
        {
            get
            {
                return client.Connected;
            }
        }
    }
}
