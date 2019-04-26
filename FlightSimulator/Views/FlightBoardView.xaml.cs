using FlightSimulator.ViewModels;
using FlightSimulator.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for FlightBoardView.xaml
    /// </summary>
    public partial class FlightBoardView : UserControl
    {
        private FlightBoardViewModel vm; 

        public FlightBoardView()
        {
            InitializeComponent();
            vm = new FlightBoardViewModel();
            this.DataContext = vm;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindowView settings_v = new SettingsWindowView();
            settings_v.ShowDialog();
        }
    }
}
