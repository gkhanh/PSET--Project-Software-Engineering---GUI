using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.Models
{
    public class GetData : IGettingInfo
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Humidity { get; set; }
        public int Temperature { get; set; }
        public int AirPressure { get; set; }
        public int LightInfo { get; set; }
        public string Date { get; set; }

    }

    public class DeviceInfo : GetData
    {
        string DeviceName { get; set; }
        string DeviceLocation { get; set; }
    }

    public class GetHumidity : GetData
    {
        int Humidity{get; set; }
        string Date { get; set; }
    }

    public class GetTemperature : GetData
    {
        int Temperature { get; set; }
        string Date { get; set; }
    }

    public class Air_pressure: GetData
    {
        int pressure { get; set; }
        string Date { get; set; }
    }
}
