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
        /* data member. */
        private AutoPilotModel model;


        /* the ViewModel that responsible of the Auto Pilot TextBox.
         creates new AutoPilot model and registers itself to be
         notified when its properties change, when the model's
         properties change, the vm notifies it to the view. */
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
         * in the Auto pilot text box(in VIEW) to the color in the model. */
        public Brush VM_Background
        {
            get { return model.Background; }
            set { model.Background = value; }
        }

        #region ClearCommand
        /// <summary>
        /// Command property which in charge on the logic 
        /// which activated after 'clear' button clicked.
        /// </summary>
        private ICommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new CommandHandler(() => ClickClear()));

            }
        }

        /* clear the text displayed in the view textbox of the autopilot */
        void ClickClear()
        {
            VM_Text = "";
        }
        #endregion

        #region OkCommand
        /// <summary>
        /// Command property which in charge on the logic 
        /// which activated after 'Ok' button clicked.
        /// </summary>
        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() => ClickOk()));
            }
        }

        /* runs model's ClickOk method with a different thread . */
        void ClickOk()
        {
            Thread t = new Thread(model.ClickOk);
            t.Start();
        }
        #endregion
    }
}