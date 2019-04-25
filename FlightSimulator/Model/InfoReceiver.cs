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
        

        public void Recieve(FlightBoardModel model, ref bool stop)
        {
            
            NetworkStream stream = client.GetStream();
            
            while (!stop)
            {
                byte[] data = new byte[client.ReceiveBufferSize];
                int red = stream.Read(data, 0, data.Length);

                string[] recieved = Encoding.ASCII.GetString(data, 0, red).Split(',');
                
                // FOR CHECK ONLY !!!
                if (recieved.Length >= 2)
                    Console.WriteLine(" the recieved values are: " + recieved[0] + " " + recieved[1]);
                
                model.Lon = Convert.ToDouble(recieved[0]);
                model.Lat = Convert.ToDouble(recieved[1]);   
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
