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

        void Recieve(ref bool stop);

        bool IsConnected { get; }

        double Lon { set; get; }

        double Lat { set; get; }
    }
}
