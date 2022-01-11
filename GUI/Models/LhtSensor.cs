using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.Models
{
    public class LhtSensor : ISensor
    {
        // INTERFACE SENSOR DATA
        public int Sensor_id { get; set; }
        public string Name { get; set; }
        public float Temperature { get; set; }
        public int Light { get; set; }
        public DateTime Time { get; set; }

        // EXPLICIT LHT SENSOR DATA
        public float Humidity { get; set; }

        // DEFAULT CONSTRUCTOR
        public LhtSensor()
        {
            Sensor_id = 0;
            Name = "Default";
            Temperature = 0.0f;
            Humidity = 0.0f;
            Light = 0;
            Time = new DateTime { };
        }

        // OVERLOADED CONSTRUCTOR
        public LhtSensor(int device_id, string device_name, float temperature, float humidity, int light, DateTime time)
        {
            Sensor_id = device_id;
            Name = device_name;
            Temperature = temperature;
            Humidity = humidity;
            Light = light;
            Time = time;
        }
    }
}
