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
    }
}
