using System;
using System.Collections.Generic;
using System.Management;
using System.Windows;
using System.IO.Ports;
using System.Diagnostics;
using Logging;
using Serilog;

namespace UsbManagement
{
    public class UsbSearcher
    {
        private readonly string _instanceNameKey = "InstanceName";
        private readonly string _portNameKey = "PortName";
        private readonly object _watcherLock = new object();
        private ManagementEventWatcher _devicePlugWatcher;  //Event watcher for plugged devices
        private ManagementEventWatcher _deviceUnplugWatcher; //Event watcher for Unplugged devices

        public string ProductId { get; set; }
        public string VendorId { get; set; }

        /// <summary>
        ///  Event Args
        /// </summary>
        public class DeviceChangeEventArgs : EventArgs
        {
            public string DeviceId { get; set; }
        }

        /// <summary>
        /// Event triggered from a target device plugged detection
        /// 
        /// </summary>
        public event Action<object, DeviceChangeEventArgs> DevicePluggedEvent;

        /// <summary>
        /// Event triggered from a target device removal detection
        /// 
        /// </summary>
        public event Action<object, DeviceChangeEventArgs> DeviceUnpluggedEvent;

        ILogger _log = LogWrapper.Logger.ForContext<UsbSearcher>();


        public List<string> SearchForWmiSerialDevices()
        {
            if (string.IsNullOrEmpty(VendorId) || string.IsNullOrEmpty(ProductId))
            {
                _log.Error("VendorId and ProductId must be set before searching for devices!");
            }

            var ports = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "root\\WMI", 
                "SELECT * FROM MSSerial_PortName");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    var instanceName = queryObj[_instanceNameKey].ToString();
                    var searchString = string.Format("USB\\{0}&{1}", VendorId, ProductId);
                    //if (instanceName.StartsWith(@"USB\VID_2A75&PID_0003"))
                    if (instanceName.StartsWith(searchString))
                    {
                        var portName = queryObj[_portNameKey].ToString();
                        _log.Information("Instance Name: {InstanceName}", queryObj[_instanceNameKey]);
                        _log.Information("Port Name: {PortName}", portName);

                        //If the serial port's instance name contains USB it must be a USB to serial device  
                        if (instanceName.Contains("USB"))
                        {
                            ports.Add(portName);
                        }
                    }
                }
            }
            catch (ManagementException e)
            {
                _log.Error(e, e.Message);
            }

            return ports;
        }

        public List<string> SearchForCimV2SerialDevices()
        {
            if (string.IsNullOrEmpty(VendorId) || string.IsNullOrEmpty(ProductId))
            {
                _log.Error("VendorId and ProductId must be set before searching for devices!");
            }

            var ports = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "root\\CIMV2",
                "SELECT * FROM Win32_SerialPort");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    string deviceId = queryObj["PNPDeviceID"].ToString();
                    string deviceSerialCom = queryObj["DeviceID"].ToString();
                    var searchString = string.Format("USB\\{0}&{1}", VendorId, ProductId);
                    if (deviceId.StartsWith(searchString))
                    {
                        _log.Information("Instance Name: {DeviceId}", deviceId);
                        _log.Information("Port Name: {DeviceSerialCom}", deviceSerialCom);

                        //If the serial port's instance name contains USB it must be a USB to serial device  
                        if (deviceId.Contains("USB"))
                        {
                            ports.Add(deviceSerialCom);
                        }
                    }
                }
            }
            catch (ManagementException e)
            {
                _log.Error(e.Message);
            }
            return ports;
        }        
        
        /// <summary>
        /// Setup Removal and Plug event on exclusive devices given the device's unique vendor ID and product ID
        /// </summary>
        /// <returns></returns>
        public void SetupDeviceChangeEvents()
        {
            if (string.IsNullOrEmpty(VendorId) || string.IsNullOrEmpty(ProductId))
            {
                _log.Error("VendorId and ProductId must be set before searching for devices!");
            }

            lock (_watcherLock)
            {
                try
                {
                    if (_devicePlugWatcher == null)
                    {
                        string pluggedQueryStr =
                            "SELECT * FROM __InstanceCreationEvent " +
                            "WITHIN 1 "
                            + "WHERE TargetInstance ISA 'Win32_SerialPort' AND TargetInstance.PNPDeviceID like 'USB\\\\" +
                            VendorId + "&%" + ProductId + "\\\\%'";

                        _devicePlugWatcher = new ManagementEventWatcher(pluggedQueryStr);
                        _devicePlugWatcher.EventArrived += (DevicePluggedEventReceived);
                        _devicePlugWatcher.Start();
                        _log.Information(" Registered for device plugged events");
                    }

                    if (_deviceUnplugWatcher == null)
                    {
                        string unpluggedQueryStr =
                            "SELECT * FROM __InstanceDeletionEvent " +
                            "WITHIN 1 "
                            + "WHERE TargetInstance ISA 'Win32_SerialPort' AND TargetInstance.PNPDeviceID like 'USB\\\\" +
                            VendorId + "&%" + ProductId + "\\\\%'";

                        _deviceUnplugWatcher = new ManagementEventWatcher(unpluggedQueryStr);
                        _deviceUnplugWatcher.EventArrived += (DeviceUnpluggedEventReceived);
                        _deviceUnplugWatcher.Start();
                        _log.Information(" Registered for device unplugged events");
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex, "Error Initializing Component ");
                }
            }
        }

        /// <summary>
        /// Device change event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DevicePluggedEventReceived(object sender, EventArrivedEventArgs e)
        {
            lock (_watcherLock)
            {
                try
                {
                    PropertyData p = e.NewEvent.Properties["TargetInstance"];
                    ManagementBaseObject mbo = p.Value as ManagementBaseObject;
                    PropertyData deviceId = mbo.Properties["DeviceID"];
                    PropertyData instanceId = mbo.Properties["PNPDeviceID"];

                    try
                    {

                        DevicePluggedEvent(this, new DeviceChangeEventArgs
                        {
                            DeviceId = (string) deviceId.Value
                        });
                    }
                    catch (ArgumentException argEx)
                    {
                        _log.Error(argEx, " failed to send DevicePluggedEvent");
                    }
                }
                catch (NullReferenceException nullEx)
                {
                    _log.Error(nullEx, " failed to send DevicePluggedEvent");
                }
            }
        }

        /// <summary>
        /// Device change event handler - Removed devices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DeviceUnpluggedEventReceived(object sender, EventArrivedEventArgs e)
        {
            lock (_watcherLock)
            {
                try
                {
                    PropertyData p = e.NewEvent.Properties["TargetInstance"];
                    ManagementBaseObject mbo = p.Value as ManagementBaseObject;
                    PropertyData deviceId = mbo.Properties["DeviceID"];
                    PropertyData instanceId = mbo.Properties["PNPDeviceID"];

                    try
                    {

                        DeviceUnpluggedEvent(this, new DeviceChangeEventArgs
                        {
                            DeviceId = (string)deviceId.Value
                        });
                    }
                    catch (ArgumentException argEx)
                    {
                        _log.Error(argEx, " failed to send DeviceUnpluggedEvent");
                    }
                }
                catch (NullReferenceException nullEx)
                {
                    _log.Error(nullEx, " failed to send DeviceUnpluggedEvent");
                }
            }
        }
    }
}
