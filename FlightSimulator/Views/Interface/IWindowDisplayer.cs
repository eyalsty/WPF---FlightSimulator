using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulator.Views.Interface
{
    public interface IWindowDisplayer : ICloneable
    {
        void Show();

        void Close();
    }
}
