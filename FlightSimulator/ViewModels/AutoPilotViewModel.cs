using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using FlightSimulator.Model;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {
        // data members
        private AutoPilotModel model;


        /* the ViewModel that responsible of the Auto Pilot bar.
         creates new AutoPilot model and registers itself to be
         notified when its properties change, when the model's
         properties change, the vm notifies the view*/
        public AutoPilotViewModel()
        {
            model = new AutoPilotModel();

            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        /* Property that in charge of updating the model about the text being insert
         * to the text box (in the view) */
        public string VM_Text
        {
            get { return model.Text; }
            set { model.Text = value; }
        }

        /* Property that in charge of connecting between the Brush color displayed
         * in the Auto pilot text box(in VIEW) to the color in the model */
        public Brush VM_Background
        {
            get { return model.Background; }
            set { model.Background = value; }
        }

        /* Command Property, activates the clickclear() function as lambda expression, but 
         * first creates new clearCommand object if its NULL */
        #region ClearCommand
        private ICommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new CommandHandler(() => ClickClear()));

            }
        }

        //clear the text displayed in the view textbox of the autopilot
        void ClickClear()
        {
            VM_Text = "";
        }
        #endregion

        /* Command Property, activates the clickok() function as lambda expression, but 
         * first creates new clearCommand object if its NULL */
        #region OkCommand
        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() => ClickOk()));
            }
        }

        //runs the clickok() function of the model in a new Thread
        void ClickOk()
        {
            Thread t = new Thread(model.ClickOk);
            t.Start();
        }
        #endregion
    }
}
