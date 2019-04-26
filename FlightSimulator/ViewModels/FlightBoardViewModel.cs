using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private FlightBoardModel model;

        public FlightBoardViewModel()
        {
            model = new FlightBoardModel();
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                this.NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        #region ConnectCommand
        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new CommandHandler(() => OnConnect()));
            }
        }

        private void OnConnect()
        {
            model.Connect();
        }
        #endregion

        #region VM_Longitude
        public double VM_Lon
        {
            set
            {
                model.Lon = value;
                NotifyPropertyChanged("VM_Lon");
            }
            get
            {
                return model.Lon;
            }
        }
        #endregion

        #region VM_Latitude
        public double VM_Lat
        {
            set
            {
                model.Lat = value;
                NotifyPropertyChanged("VM_Lat");
            }
            get
            {
                return model.Lat;
            }
        }
        #endregion

        public void Dissconnect(object sender, EventArgs e)
        {
            model.Dissconnect();
        }
        
    }
}
