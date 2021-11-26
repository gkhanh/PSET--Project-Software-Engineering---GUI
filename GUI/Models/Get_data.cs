using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.Models
{
    public class Get_Data : IGetting_info
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Humidity { get; set; }
        public int temperature { get; set; }
        public int air_pressure { get; set; }
        public int light_info { get; set; }
        public string day { get; set; }

    }

    public class Device_info : Get_Data
    {
        string Device_name { get; set; }
        string Device_location { get; set; }
    }

    public class Humidity : Get_Data
    {
        int humidity{get; set; }
        string day { get; set; }
    }

    public class Temperature : Get_Data
    {
        int temperature { get; set; }
        string day { get; set; }
    }

    public class Air_pressure: Get_Data
    {
        int pressure { get; set; }
    }
}
