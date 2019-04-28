using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    /// <summary>
    /// an interface for classes tht will be used as clients to
    /// the flight simulatier.
    /// </summary>
    public interface IClient
    {
        void Connect(string ip, int port);
        void Send(string line);
        void Disconnect();
    }
}
