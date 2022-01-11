namespace GUI.Models
{
    public class GraphData
    {
        public float average_temperature { get; set; }
        public float average_pressure { get; set; }
        public float average_humidity { get; set; }
        public float average_light { get; set; }
        public string device_name { get; set; }
        public string date { get; set; }

        public GraphData(float t, float p, float h, float l, string n, string d)
        {
            average_temperature = t;
            average_pressure = p;
            average_humidity = h;
            average_light = l;
            device_name = n;
            date = d;
        }
    }
}
