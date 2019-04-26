using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    public interface INotifierServer : INotifyPropertyChanged
    {
        void Start(string ip, int port);

        void Recieve(FlightBoardModel model, ref bool stop);

        bool IsConnected { get; }

        // needs a better solution ! maybe with flightboard model as argument to recieve.
        double Lon { set; get; }
        double Lat { set; get; }
    }
}
