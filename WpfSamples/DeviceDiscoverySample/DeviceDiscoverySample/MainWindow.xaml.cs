
using Logging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using UsbManagement;
using System.IO.Ports;

namespace DeviceDiscoverySample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<string> ComPortNames { get; set; }  = new ObservableCollection<string>();
        private UsbSearcher searcher = new UsbSearcher();
        private StringBuilder _statusBoxText = new StringBuilder();
        public event PropertyChangedEventHandler PropertyChanged;
        private Dictionary<string, SerialPort> serialPorts = new Dictionary<string, SerialPort>();

        private string vendorId = "VID_2A75";
        public string VendorId
        {
            get { return vendorId; }
            set
            {
                vendorId = value;
                OnPropertyChanged("VendorId");
            }
        }

        string productId = "PID_0003";
        public string ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                OnPropertyChanged("ProductId");
            }
        }

        private string command = "";
        public string CommandToSend
        {
            get { return command; }
            set
            {
                command = value;
                OnPropertyChanged("CommandToSend");
            }
        }

        public String StatusBoxText
        {
            get { return _statusBoxText.ToString(); }

            set
            {
                _statusBoxText.AppendLine(value);
                OnPropertyChanged("StatusBoxText");
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            Log.Initialize();

            // search for devices already plugged in before the start of the application
            Log.Logger.Information("{Class} - search for devices", GetType());
            SearchForDevices();

            // Setup event registration for plugged and unplugged events
            searcher.DevicePluggedEvent += OnDevicePluggedEvent;
            searcher.DeviceUnpluggedEvent += OnDeviceUnpluggedEvent;
            searcher.SetupDeviceChangeEvents();
        }

        private void SearchForDevices()
        {
            try
            {
                Log.Logger.Information("SearchButtonCIMV2_Click Clicked");
                ComPortNames.Clear();
                var devices = searcher.SearchForCimV2SerialDevices();
                foreach (var device in devices)
                {
                    ComPortNames.Add(device);
                    serialPorts[device] = new SerialPort(device, 115200);
                    serialPorts[device].ReadTimeout = 250;
                }
                Log.Logger.Information("Found: {ComPorts}", ComPortNames);
            }
            catch (System.Exception ex)
            {
                Log.Logger.Error(ex.ToString());
            }
        }

        private void SendDataClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Log.Logger.Information("{Class} SendDataClicked - sending command {Command}", GetType(), command);

                String selected = (String)Ports.SelectedItem;
                if (!String.IsNullOrEmpty(selected) && !string.IsNullOrEmpty(command) && serialPorts.ContainsKey(selected))
                {
                    var port = serialPorts[selected];
                    if (!port.IsOpen) port.Open();
                    port.WriteLine(command);

                    try
                    {
                        do
                        {
                            StatusBoxText = port.ReadLine();
                        } while (true); // read until ReadLine throws
                    }
                    catch (TimeoutException)
                    {
                        // nothing to do.  This just indicates that there is no more data
                    }
                }
            }
            catch (System.Exception ex)
            {
                Log.Logger.Error(ex, "{Class} SendDataClicked exception", GetType());
            }
        }

        private void OnDevicePluggedEvent(object sender, UsbSearcher.DeviceChangeEventArgs e)
        {
            string portNum = (string)e.DeviceId;

            Dispatcher.Invoke(new Action(() =>
            {
                if (!ComPortNames.Contains(portNum))
                {
                    ComPortNames.Add(portNum);
                    serialPorts[portNum] = new SerialPort(portNum, 115200);
                    serialPorts[portNum].ReadTimeout = 250;
                    Log.Logger.Information("{Class} OnDevicePluggedEvent - {ComPort} discovered", GetType(), portNum);
                    StatusBoxText = string.Format("{0} plugged in", portNum);
                }
            }
            ));
        }

        private void OnDeviceUnpluggedEvent(object sender, UsbSearcher.DeviceChangeEventArgs e)
        {
            string portNum = (string)e.DeviceId;
            Dispatcher.Invoke(new Action(() => ComPortNames.Remove(portNum)));
            ReleasePort(portNum);
            Log.Logger.Information("{Class} OnDeviceUnPluggedEvent - {ComPort} unplugged", GetType(), portNum);
            StatusBoxText = string.Format("{0} unplugged", portNum);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ReleasePort(string portNum)
        {
            if (string.IsNullOrEmpty(portNum)) return;

            SerialPort port;
            if(serialPorts.TryGetValue(portNum, out port))
            {
                try
                {
                    serialPorts.Remove(portNum);
                    if (port.IsOpen)
                    {
                        port.DiscardInBuffer();
                        port.DiscardOutBuffer();
                    }
                   // if (port.IsOpen) port.Close();
                    port.Dispose();
                }
                catch (System.Exception ex)
                {
                    Log.Logger.Error(ex, "{Class} ReleasePort - failed to cleanup {ComPort}", GetType(), portNum);
                }
            }
        }
    }
}
