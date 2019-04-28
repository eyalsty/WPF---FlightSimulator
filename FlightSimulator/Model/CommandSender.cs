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
        /* implementation of the singletone design pattern, in order to force existence of
         * only one instance of the CommandSender object in our program (more then one 
         * CommandSender can cause ambiguous behavior)we make it static and the
         * constructor first check if the member already been initialized, if not -
         * creates it */
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

        //starting a client-side connection with the Flight Simulator
        public void Connect(string ip, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            tcpClient.Connect(endPoint);
        }

        /*receive line (to be written) as a parameter, convert it to bytes array and sends
        it to the Flight Simulator(the server) */
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
        //termintate the client-side connection with the Flight Simulator
        public void Disconnect()
        {
            tcpClient.Close();
        }
    }
}
