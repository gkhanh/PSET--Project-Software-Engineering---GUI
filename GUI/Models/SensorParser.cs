using Castle.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GUI.Models
{
    class SensorParser
    {
        private int dataIndex { get; set; }
        private List<PySensor> pySensors { get; set; }
        private List<LhtSensor> lhtSensors { get; set; }

        public Pair<List<PySensor>,List<LhtSensor>> Parse(List<string> data)
        {
            // Set data index to '0'
            dataIndex = 0;

            // Set a flag to indicate if current sensor is a 'Py' sensor
            var isPySensor = false;

            // Create a temporary index variable to notify the index position
            var listIndex = 0;

            // Create a new list of py-sensors
            pySensors = new List<PySensor>();

            // Create a new list of lht-sensors
            lhtSensors = new List<LhtSensor>();

            var countIndex = 0;

            // Parse in terms of index in list
            foreach (var item in data)
            {
                // Attempt to check the device name of the current element in list 'data'
                try
                {
                    if(dataIndex == 0)
                    {
                        // Check if this is a py-sensor
                        isPySensor = data[countIndex + 1].Contains("py");
                    }

                    if(isPySensor && dataIndex == 0)
                    {
                        // Create a 'PySensor' and add to list of py-sensors
                        var thisSensor = new PySensor();
                        pySensors.Add(thisSensor);
                    }
                    
                    if(!isPySensor && dataIndex == 0)
                    {
                        // Create a 'LhtSensor' and add to list of lht-sensors
                        var thisSensor = new LhtSensor();
                        lhtSensors.Add(thisSensor);
                    }
                    countIndex++;
                }
                catch(FormatException)
                {
                    Console.WriteLine($"Unable to identify '{item}'");
                }

                if (isPySensor)
                {
                    switch(dataIndex)
                    {
                        case 0:

                            try
                            {
                                pySensors[listIndex].Sensor_id = int.Parse(item);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            dataIndex++;
                            break;

                        case 1:

                            try
                            {
                                pySensors[listIndex].Name = item;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            dataIndex++;
                            break;

                        case 2:

                            try
                            {
                                pySensors[listIndex].Temperature = float.Parse(item);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            dataIndex++;
                            break;

                        case 3:

                            try
                            {
                                pySensors[listIndex].Pressure = float.Parse(item);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            dataIndex++;
                            break;

                        case 4:

                            try
                            {
                                pySensors[listIndex].Light = int.Parse(item);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            dataIndex++;
                            break;

                        case 5:

                            try
                            {
                                pySensors[listIndex].Time = DateTime.Parse(item);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            
                            // Reset data index
                            dataIndex = 0;

                            // Increment list index
                            listIndex++;

                            break;

                        default:
                            Console.WriteLine("Index is out of scope...");
                            break;
                    }
                }
                else
                {
                    switch (dataIndex)
                    {
                        case 0:

                            try
                            {
                                lhtSensors[listIndex].Sensor_id = int.Parse(item);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            dataIndex++;
                            break;

                        case 1:

                            try
                            {
                                lhtSensors[listIndex].Name = item;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            dataIndex++;
                            break;

                        case 2:

                            try
                            {
                                lhtSensors[listIndex].Temperature = float.Parse(item);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            dataIndex++;
                            break;

                        case 3:

                            try
                            {
                                lhtSensors[listIndex].Humidity = float.Parse(item);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            dataIndex++;
                            break;

                        case 4:

                            try
                            {
                                lhtSensors[listIndex].Light = int.Parse(item);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }
                            dataIndex++;
                            break;

                        case 5:

                            try
                            {
                                lhtSensors[listIndex].Time = DateTime.Parse(item);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine($"Unable to parse '{item}'");
                            }

                            // Reset data index
                            dataIndex = 0;

                            // Increment list index
                            listIndex++;

                            break;

                        default:
                            Console.WriteLine("Index is out of scope...");
                            break;
                    }
                }
            }

            // Create a pair of sensor lists
            var parsedSensors = new Pair<List<PySensor>, List<LhtSensor>>(pySensors, lhtSensors);

            // Return pair of sensor lists
            return parsedSensors;
        }

        // TODO: FUNCTIONS SUCH AS (MAX, MIN AND AVERAGE) SHOULD BE IMPLEMENTED BELOW:

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<float> GetTemperatures(List<PySensor> sensor)
        {
            var temperatures = new List<float>();

            foreach(var element in sensor)
            {
                temperatures.Add(element.Temperature);
            }
            return temperatures;
        }

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<float> GetTemperatures(List<LhtSensor> sensor)
        {
            var temperatures = new List<float>();

            foreach (var element in sensor)
            {
                temperatures.Add(element.Temperature);
            }
            return temperatures;
        }

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<DateTime> GetTimes(List<PySensor> sensor)
        {
            var times = new List<DateTime>();

            foreach(var element in sensor)
            {
                times.Add(element.Time);
            }

            return times;
        }

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<DateTime> GetTimes(List<LhtSensor> sensor)
        {
            var times = new List<DateTime>();

            foreach (var element in sensor)
            {
                times.Add(element.Time);
            }

            return times;
        }

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<float> GetPressure(List<PySensor> sensor)
        {
            var pressure = new List<float>();

            foreach (var element in sensor)
            {
                pressure.Add(element.Pressure);
            }

            return pressure;
        }

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<float> GetHumidity(List<LhtSensor> sensor)
        {
            var humidities = new List<float>();

            foreach(var element in sensor)
            {
                humidities.Add(element.Humidity);
            }

            return humidities;
        }

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<int> GetLight(List<PySensor> sensor)
        {
            var lights = new List<int>();

            foreach(var element in sensor)
            {
                lights.Add(element.Light);
            }

            return lights;
        }

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<int> GetLight(List<LhtSensor> sensor)
        {
            var lights = new List<int>();

            foreach (var element in sensor)
            {
                lights.Add(element.Light);
            }

            return lights;
        }
    }
}
