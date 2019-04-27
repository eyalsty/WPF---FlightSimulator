using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FlightSimulator.Model
{
    class AutoPilotModel : BaseNotifyModel
    {
        private IClient cs;

        //initializes the client data member, using a singletone design pattern
        public AutoPilotModel()
        {
            cs = CommandSender.Instance;
        }

        /*runs in a thread of its own. first sets the background color to red,
         * then sending every command in a seperated line to the simulator
         * 1 by 1 with 2 seconds delay. when done sets color back to white */
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

        //holds the text of the AutoPilot textBox, when being updated notifies the VM
        #region Text
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

        /*holds the Background color of the AutoPilot textBox,
         * when being updated notifies the VM */
        #region Background
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
