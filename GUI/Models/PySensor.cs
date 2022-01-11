using System;

namespace GUI.Models
{
    public class PySensor : ISensor
    {
        // INTERFACE SENSOR DATA
        public int Sensor_id { get; set; }
        public string Name { get; set; }
        public float Temperature { get; set; }
        public int Light { get; set; }
        public DateTime Time { get; set; }

        // EXPLICIT PY SENSOR DATA
        public float Pressure { get; set; }

        // DEFAULT CONSTRUCTOR
        public PySensor()
        {
            Sensor_id = 0;
            Name = "Default";
            Temperature = 0.0f;
            Pressure = 0.0f;
            Light = 0;
            Time = new DateTime { };
        }

        // OVERLOADED CONSTRUCTOR
        public PySensor(int device_id, string device_name, float temperature, float pressure, int light, DateTime time)
        {
            Sensor_id = device_id;
            Name = device_name;
            Temperature = temperature;
            Pressure = pressure;
            Light = light;
            Time = time;
        }
    }
}
