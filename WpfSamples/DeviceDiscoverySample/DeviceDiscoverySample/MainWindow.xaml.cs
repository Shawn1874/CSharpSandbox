
using Logging;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using UsbManagement;

namespace DeviceDiscoverySample
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> ComPortNames { get; set; }  = new ObservableCollection<string>();
        private UsbSearcher searcher = new UsbSearcher();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            Log.Initialize();
            Log.Logger.Information("Application started");
        }

        private void SearchButtonCIMV2_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Log.Logger.Information("SearchButtonCIMV2_Click Clicked");
                ComPortNames.Clear();
                var devices = searcher.SearchForCimV2SerialDevices();
                foreach (var device in devices)
                {
                    ComPortNames.Add(device);
                }
                Log.Logger.Information("Found: {ComPorts}", ComPortNames);
            }
            catch (System.Exception ex)
            {
                Log.Logger.Error(ex.ToString());
            }
        }

        private void SearchButtonWMI_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Log.Logger.Information("SearchButtonCIMV2_Click Clicked");

                ComPortNames.Clear();
                var devices = searcher.SearchForWmiSerialDevices();
                foreach (var device in devices)
                {
                    ComPortNames.Add(device);
                }
                Log.Logger.Information("Found: {ComPorts}", ComPortNames);
            }
            catch (System.Exception ex)
            {
                Log.Logger.Error(ex.ToString());
            }
        }
    }
}
