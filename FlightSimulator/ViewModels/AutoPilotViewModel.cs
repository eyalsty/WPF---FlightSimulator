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

        public AutoPilotViewModel()
        {
            model = new AutoPilotModel();

            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public string VM_Text
        {
            get { return model.Text; }
            set { model.Text = value; }
        }

        public Brush VM_Background
        {
            get { return model.Background; }
            set { model.Background = value; }
        }

        #region ClearCommand
        private ICommand clearCommand;

        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new CommandHandler(() => ClickClear()));

            }
        }

        void ClickClear()
        {
            VM_Text = "";
        }
        #endregion

        #region OkCommand
        private ICommand okCommand;

        public ICommand OkCommand
        {
            get
            {
                return okCommand ?? (okCommand = new CommandHandler(() => ClickOk()));
            }
        }

        void ClickOk()
        {
            Thread t = new Thread(model.ClickOk);
            t.Start();
        }
        #endregion
    }
}
