using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.Models
{
    public interface IGettingInfo
    {
        string Name { get; set; }
        string Location { get; set; }
        int Humidity { get; set; }
        int Temperature { get; set; }
        int AirPressure { get; set; }
        int LightInfo { get; set; }
        string Date { get; set; }
    }
}
