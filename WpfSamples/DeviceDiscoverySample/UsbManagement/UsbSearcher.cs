using System;
using System.Collections.Generic;
using System.Management;
using System.Windows;
using System.IO.Ports;
using System.Diagnostics;
using Logging;

namespace UsbManagement
{
    public class UsbSearcher
    {
        private readonly string _instanceNameKey = "InstanceName";
        private readonly string _portNameKey = "PortName";
        private readonly object _watcherLock = new object();
        private ManagementEventWatcher _devicePlugWatcher;  //Event watcher for plugged devices
        private ManagementEventWatcher _deviceUnplugWatcher; //Event watcher for Unplugged devices

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
        

        public List<string> SearchForWmiSerialDevices()
        {
            var ports = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                "root\\WMI", 
                "SELECT * FROM MSSerial_PortName");

            try
            {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    var instanceName = queryObj[_instanceNameKey].ToString();
                    if (instanceName.StartsWith(@"USB\VID_2A75&PID_0003"))
                    {
                        var portName = queryObj[_portNameKey].ToString();
                        Log.Logger.Information("Instance Name: {InstanceName}", queryObj[_instanceNameKey]);
                        Log.Logger.Information("Port Name: {PortName}", portName);

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
                Log.Logger.Error(e.Message);
            }

            return ports;
        }

        public List<string> SearchForCimV2SerialDevices()
        {
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
                    if (deviceId.Contains(@"USB\VID_2A75&PID_0003"))
                    {
                        Log.Logger.Information("Instance Name: {DeviceId}", deviceId);
                        Log.Logger.Information("Port Name: {DeviceSerialCom}", deviceSerialCom);

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
                Log.Logger.Error(e.Message);
            }
            return ports;
        }        
        
        /// <summary>
        /// Setup Removal and Plug event on exclusive devices given the device's unique vendor ID and product ID
        /// </summary>
        /// <returns></returns>
        public void SetupDeviceChangeEvents()
        {
            string vid = "VID_2A75";
            string pid = "PID_0003";
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
                            vid + "&%" + pid + "\\\\%'";

                        _devicePlugWatcher = new ManagementEventWatcher(pluggedQueryStr);
                        _devicePlugWatcher.EventArrived += (DevicePluggedEventReceived);
                        _devicePlugWatcher.Start();
                        Log.Logger.Information("{Class} Registered for device plugged events", GetType());
                    }

                    if (_deviceUnplugWatcher == null)
                    {
                        string unpluggedQueryStr =
                            "SELECT * FROM __InstanceDeletionEvent " +
                            "WITHIN 1 "
                            + "WHERE TargetInstance ISA 'Win32_SerialPort' AND TargetInstance.PNPDeviceID like 'USB\\\\" +
                            vid + "&%" + pid + "\\\\%'";

                        _deviceUnplugWatcher = new ManagementEventWatcher(unpluggedQueryStr);
                        _deviceUnplugWatcher.EventArrived += (DeviceUnpluggedEventReceived);
                        _deviceUnplugWatcher.Start();
                        Log.Logger.Information("{Class} Registered for device unplugged events", GetType());
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.Error(ex, "Error Initializing Component {Class}", GetType());
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
                        Log.Logger.Error(argEx, "{Class} failed to send DevicePluggedEvent", GetType());
                    }
                }
                catch (NullReferenceException nullEx)
                {
                    Log.Logger.Error(nullEx, "{Class} failed to send DevicePluggedEvent", GetType());
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
                        Log.Logger.Error(argEx, "{Class} failed to send DeviceUnpluggedEvent", GetType());
                    }
                }
                catch (NullReferenceException nullEx)
                {
                    Log.Logger.Error(nullEx, "{Class} failed to send DeviceUnpluggedEvent", GetType());
                }
            }
        }
    }
}
