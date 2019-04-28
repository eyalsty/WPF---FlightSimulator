using FlightSimulator.Model;
using FlightSimulator.ViewModels.Windows;
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
using System.Windows.Shapes;

namespace FlightSimulator.Views.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindowView.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsWindowViewModel vm;

        public SettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsWindowViewModel(new ApplicationSettingsModel());
            this.DataContext = vm;

            if (vm.CloseAction == null)
            {
                // register the close method of the settings window to its VM .
                vm.CloseAction = () => this.Close();
            }
        }
    }
    
}
