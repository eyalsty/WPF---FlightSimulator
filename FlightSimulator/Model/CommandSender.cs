using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class CommandSender : IClient
    {
        #region singelton
        private static IClient instance = null;
        public static IClient Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CommandSender();
                }
                return instance;
            }
        }
        #endregion

        private TcpClient tcpClient;

        public CommandSender()
        {
            tcpClient = new TcpClient();

        }

        public void Connect(string ip, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            tcpClient.Connect(endPoint);
        }

        public void Send(string line)
        {
            if (tcpClient.Connected)
            {
                NetworkStream stream = tcpClient.GetStream();

                byte[] encoded = Encoding.ASCII.GetBytes(line + "\r\n");
                stream.Write(encoded, 0, encoded.Length);
                stream.Flush();
            }
        }

        public void Dissconnect()
        {
            tcpClient.Close();
        }
    }
}
