using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.Models
{
    public interface IGetting_info
    {
        string Name { get; set; }
        string Location { get; set; }
        int Humidity { get; set; }
        int temperature { get; set; }
        int air_pressure { get; set; }
        int light_info { get; set; }
        string day { get; set; }

    }

    
}
