using Castle.Core;
using System;
using System.Collections.Generic;

namespace GUI.Models
{
    public class SensorParser
    {
        //-----------------------------------------------------------------------------------------------------------------------------
        // Variables used to parse received data from the remote database
        private int dataIndex { get; set; }
        private List<PySensor> pySensors { get; set; }
        private List<LhtSensor> lhtSensors { get; set; }

        //-----------------------------------------------------------------------------------------------------------------------------
        // Device name to compare with the data retrieved to split it up in the right list
        private string wierden_py = "py-wierden";
        private string saxion_py = "py-saxion";
        private string wierden_lht = "lht-wierden";
        private string gronau_lht = "lht-gronau";
        
        //-----------------------------------------------------------------------------------------------------------------------------
        // In this list the ordered data is stored
        private List<PySensor> WierdenList_Py { get; set; }
        private List<PySensor> SaxionList_Py { get; set; }
        private List<LhtSensor> WierdenList_LHT { get; set; }
        private List<LhtSensor> GronauList_LHT { get; set; }

        //-----------------------------------------------------------------------------------------------------------------------------
        // Functions to get the sorted data 
        public List<PySensor> getWierenPy() { return WierdenList_Py; }
        public List<PySensor> getSaxionPy() { return SaxionList_Py; }
        public List<LhtSensor> getWierenLHT() { return WierdenList_LHT; }
        public List<LhtSensor> getGronauLHT() { return GronauList_LHT; }

        public SensorParser()
        {
            WierdenList_Py = new List<PySensor>();
            SaxionList_Py = new List<PySensor>();
            WierdenList_LHT = new List<LhtSensor>();
            GronauList_LHT = new List<LhtSensor>();
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        // Function to parse the received data from the remote database
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

        //-----------------------------------------------------------------------------------------------------------------------------
        // Function to sort the data from the Py device and split it up in two lists one with the device in Wierden
        // and one with the device in Saxion.
        //-----------------------------------------------------------------------------------------------------------------------------
        public void SortData_Py(List<PySensor> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                //-----------------------------------------------------------------------------------------------------------------------------
                // If statement: If the device name is wierden-py it gets stored in the WierdenList_Py else in the SaxionList_py
                //-----------------------------------------------------------------------------------------------------------------------------
                if (data[i].Name == wierden_py)
                {
                    WierdenList_Py.Add(data[i]);
                    // Write to debug to show the data stored in the wierden_py list
                    //Debug.WriteLine($"Name:  { data[i].device_name}, temperature: {data[i].temperature}, pressure: " +
                    //     $"{data[i].pressure} light: {data[i].light} timestamp: {data[i].timestamp}");
                }
                else if (data[i].Name == saxion_py)
                {
                    SaxionList_Py.Add(data[i]);
                    // Write to debug to show the data stored in the saxion_py list
                    //Debug.WriteLine($"Name:  { data[i].device_name}, temperature: {data[i].temperature}, pressure: " +
                    //    $"{data[i].pressure} light: {data[i].light} timestamp: {data[i].timestamp}");
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        // Function to sort the data from the LHT device and split it up in two lists one with the device in Wierden
        // and one with the device in Gronau.
        //-----------------------------------------------------------------------------------------------------------------------------
        public void SortData_LHT(List<LhtSensor> data)
        {
            //Debug.WriteLine("Sortdata LHT by wieren & gronau: ");
            for (int i = 0; i < data.Count; i++)
            {
                //-----------------------------------------------------------------------------------------------------------------------------
                // If statement: If the device name is wierden-lht it gets stored in the WierdenList_LHT else in the GronauList_LHT
                //-----------------------------------------------------------------------------------------------------------------------------
                if (data[i].Name == wierden_lht)
                {
                    WierdenList_LHT.Add(data[i]);
                    // Write to debug to show the data stored in the wierden_lht list
                    //Debug.WriteLine($"Name:  { data[i].device_name}, temperature: {data[i].temperature}, pressure: " +
                    //    $"{data[i].pressure} light: {data[i].light} timestamp: {data[i].timestamp}");
                }
                else if (data[i].Name == gronau_lht)
                {
                    GronauList_LHT.Add(data[i]);
                    // Write to debug to show the data stored in the gronau_lht list
                    //Debug.WriteLine($"Name:  { data[i].device_name}, temperature: {data[i].temperature}, pressure: " +
                    //$"{data[i].pressure} light: {data[i].light} timestamp: {data[i].timestamp}");
                }
            }
        }

        // TODO: FUNCTIONS SUCH AS (MAX, MIN AND AVERAGE) SHOULD BE IMPLEMENTED BELOW:

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<float> GetTemperatures(List<float> sensor)
        {
            var temperatures = new List<float>();

            foreach(var element in sensor)
            {
                temperatures.Add(element);
            }
            return temperatures;
        }

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<string> GetTimes(List<GraphData> sensor)
        {
            var times = new List<string>();

            foreach (var element in sensor)
            {
                times.Add(element.date);
            }

            return times;
        }

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<float> GetPressure(List<GraphData> sensor)
        {
            var pressure = new List<float>();

            foreach (var element in sensor)
            {
                pressure.Add(element.average_pressure);
            }

            return pressure;
        }

        // TEMPORARY FUNCTION (PLEASE REVIEW)
        public List<float> GetLight(List<GraphData> sensor)
        {
            var lights = new List<float>();

            foreach(var element in sensor)
            {
                lights.Add(element.average_light);
            }

            return lights;
        }
    }
}
