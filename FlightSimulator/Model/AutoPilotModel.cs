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

        public AutoPilotModel()
        {
            cs = CommandSender.Instance;
        }

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
