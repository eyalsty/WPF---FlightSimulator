using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.Views.Interface;
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
        private IWindowDisplayer displayer;

        /* the ViewModel responsible of the Flight Board.
         * receiving a displayer as a parameter and initializes it.
         * also, creates new FlightBoard model and registers itself to be
         * notified when properties change*/ 
        public FlightBoardViewModel(IWindowDisplayer windowDisplayer)
        {
            this.displayer = windowDisplayer;

            model = new FlightBoardModel();
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                this.NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        //the view uses this property to receive the Lon value from the model(data binding)
        #region VM_Longitude
        public double VM_Lon
        {
            get { return model.Lon; }
        }
        #endregion

        //the view uses this property to receive the Lat value from the model(data binding)
        #region VM_Latitude
        public double VM_Lat
        {
            get { return model.Lat; }
        }
        #endregion

        /*Binded to Command property of a Button in order to execute the "onConnect()" 
         * function(lambda exp) when the button being pressed */
        #region ConnectCommand
        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new CommandHandler(() => OnConnect()));
            }
        }
        //runs the connect() func of the model that connects with the Flight Simulator
        private void OnConnect()
        {
            model.Connect();
        }
        #endregion

        /*Binded to Command property of a Button in order to execute the "onSettings()" 
         *function(lambda exp) when the button being pressed */
        #region SettingCommand
        private ICommand settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnSettings()));
            }
        }
        
        //displays the Settings Window
        private void OnSettings()
        {
            displayer.Show();
            displayer = displayer.Clone() as IWindowDisplayer;
        }
        #endregion

        //when program ends, terminates the connection with the Flight Simulator
        public void Dissconnect(object sender, EventArgs e)
        {
            model.Disconnect();
        }
    }
}
