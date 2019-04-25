using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface IServer
    {
        void Start(string ip, int port);

        void Recieve(FlightBoardModel model, ref bool stop);

        bool IsConnected { get; }
    }
}
