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
        /* data members */
        private FlightBoardModel model;
        private IWindowDisplayer displayer;

        /* the ViewModel responsible of the Flight Board.
         * receiving a displayer as a parameter.
         * also, creates new FlightBoard model and registers itself to be
         * notified when properties change. */ 
        public FlightBoardViewModel(IWindowDisplayer windowDisplayer)
        {
            this.displayer = windowDisplayer;
            model = new FlightBoardModel();
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                this.NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        #region VM_Longitude
        /* the view uses this property to receive the Lon value 
         * from the model (data binding). */
        public double VM_Lon
        {
            get { return model.Lon; }
        }
        #endregion

        #region VM_Latitude
        /* the view uses this property to receive the Lat value
         * from the model (data binding). */
        public double VM_Lat
        {
            get { return model.Lat; }
        }
        #endregion

        #region ConnectCommand
        /// <summary>
        /// Binded to Command property of the
        ///  'Connect' Button, in order to execute the "onConnect()" 
        /// method (inside the lambda exp) when the button being pressed.
        /// </summary>
        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new CommandHandler(() => OnConnect()));
            }
        }

        /* runs the connect() method of the model that connects with the Flight Simulator. */
        private void OnConnect()
        {
            model.Connect();
        }
        #endregion
        
        #region SettingCommand
        /// <summary>
        /// Binded to Command property of the 'Settings' Button 
        /// in order to execute the "onSettings()" 
        /// method (inside the lambda exp) when the button being pressed. */
        /// </summary>
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

        /* terminates the connection with the Flight Simulator.
         * will be used when the user will exit the main window. */
        public void Dissconnect(object sender, EventArgs e)
        {
            model.Disconnect();
        }
    }
}
