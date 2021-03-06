using System;

namespace GUI.Models
{
    public interface ISensor
    {
        int Sensor_id { get; set; }
        string Name { get; set; }
        float Temperature { get; set; }
        int Light { get; set; }
        DateTime Time { get; set; }
    }
}
