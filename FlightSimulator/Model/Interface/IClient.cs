using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface IClient
    {
        void Connect(string ip, int port);
        void Send(string line);
        void Dissconnect();
    }
}
