using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using FlightSimulator.Views.Windows;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class FlightBoard : UserControl
    {
        ObservableDataSource<Point> planeLocations = null;
        private FlightBoardViewModel vm = null;

        public FlightBoard()
        {
            InitializeComponent();
            vm = new FlightBoardViewModel();
            this.DataContext = vm;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);

            plotter.AddLineGraph(planeLocations, 2, "Route");
            vm.PropertyChanged += Vm_PropertyChanged;

            // closing the client and server while closing the GUI.
             this.Dispatcher.ShutdownStarted += vm.Dissconnect;
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals("VM_Lat") || e.PropertyName.Equals("VM_Lon"))
            {
                Point p1 = new Point(vm.VM_Lat, vm.VM_Lon);
                planeLocations.AppendAsync(Dispatcher, p1);
            }
        }


        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindowView settings_v = new SettingsWindowView();
            settings_v.ShowDialog();
        }

    }

}

