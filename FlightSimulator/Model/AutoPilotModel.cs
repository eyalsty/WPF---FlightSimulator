using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FlightSimulator.Model
{
    class AutoPilotModel : BaseNotify
    {
        private IClient cs;

        /* initializes the client data member, using its singleton attribute. */
        public AutoPilotModel()
        {
            cs = CommandSender.Instance;
        }

        /* runs in a different thread. first sets the background color to red,
         * then sending each command to the simulator
         * with 2 seconds delay. when done sets color back to white. */
        public void ClickOk()
        {
            this.Background = Brushes.IndianRed;

            string[] commands = text.Split('\n');
            foreach(string command in commands)
            {
                cs.Send(command);
                System.Threading.Thread.Sleep(2000);
            }
            
            this.Background = Brushes.White;
        }

        #region Text
        /* holds the text of the AutoPilot textBox, when being updated notifies the VM. */
        private string text;
        public string Text
        {
            set
            {
                text = value;
                NotifyPropertyChanged("Text");
            }
            get
            {
                return text;
            } 
         }
        #endregion

        #region Background
        /* holds the Background color of the AutoPilot textBox,
         * when being updated notifies the VM. */
        private Brush background;
        public Brush Background
        {
            set
            {
                background = value;
                NotifyPropertyChanged("Background");
            }
            get
            {
                return background;
            }
        }
        #endregion
    }
}
