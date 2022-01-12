using GUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GUI.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        // Declare a parsed py-sensor data list
        private List<PySensor> pySensors { get; set; }

        // Declare a parsed lht-sensor data list
        private List<LhtSensor> lhtSensors { get; set; }

        // Declare an average temperature
        private string averageTemperature { get; set; }

        // Declare an average humidity
        private string averageHumidity { get; set; }

        // Declare an average pressure
        private string averagePressure { get; set; }

        // Declare an average light
        private string averageLight { get; set; }

        // Declare a value holder for each city
        private string averageTempSaxion { get; set; }

        // Declare a value holder for each city
        private string averagePresSaxion { get; set; }

        // Declare a value holder for each city
        private string averageLightSaxion { get; set; }

        // Declare a value holder for each city
        private string averageTempWierden { get; set; }

        // Declare a value holder for each city
        private string averageHumWierden { get; set; }

        // Declare a value holder for each city
        private string averageLightWierden { get; set; }

        // Declare a value holder for each city
        private string averageTempGronau { get; set; }

        // Declare a value holder for each city
        private string averageHumGronau { get; set; }

        // Declare a value holder for each city
        private string averageLightGronau { get; set; }

        // Declare a sensor parser
        private SensorParser sensorParser { get; set; }

        // Declare a list of cities
        private List<string> cities { get; set; }

        // Declare a city title
        private string cityTitle { get; set; }

        // Declare an daily average parser
        private CalculateAverageDay averageDay { get; set; }

        private List<GraphData> averageGraph { get; set; }

        /// <summary>
        /// //////////////////////////////////////////////////////
        /// </summary>

        // Declare a PUBLIC average temperature to display
        public string AverageTemperature
        {
            get { return averageTemperature; }
            set
            {
                if(averageTemperature != value)
                {
                    AverageTemperature = value;
                    OnPropertyChanged();
                }
            }
        }

        // Declare a PUBLIC average humidity to display
        public string AverageHumidity
        {
            get { return averageHumidity; }
            set
            {
                if (averageHumidity != value)
                {
                    averageHumidity = value;
                    OnPropertyChanged();
                }
            }
        }

        // Declare a PUBLIC average pressure to display
        public string AveragePressure
        {
            get { return averagePressure; }
            set
            {
                if (averagePressure != value)
                {
                    averagePressure = value;
                    OnPropertyChanged();
                }
            }
        }

        // Declare a PUBLIC average light to display
        public string AverageLight
        {
            get { return averageLight; }
            set
            {
                if (averageLight != value)
                {
                    averageLight = value;
                    OnPropertyChanged();
                }
            }
        }

        // Declare a PUBLIC average for Enschede
        public string AverageTempEnschede
        {
            get { return averageTempSaxion; }
            set
            {
                if(value != averageTempSaxion)
                {
                    value = averageTempSaxion;
                }
            }
        }

        // Declare a PUBLIC average for Enschede
        public string AveragePresEnschede
        {
            get { return averagePresSaxion; }
            set
            {
                if (value != averagePresSaxion)
                {
                    value = averagePresSaxion;
                }
            }
        }

        // Declare a PUBLIC average for Enschede
        public string AverageLightEnschede
        {
            get { return averageLightSaxion; }
            set
            {
                if (value != averageLightSaxion)
                {
                    value = averageLightSaxion;
                }
            }
        }

        // Declare a PUBLIC average for Wierden
        public string AverageTempWierden
        {
            get { return averageTempWierden; }
            set
            {
                if (value != averageTempWierden)
                {
                    value = averageTempWierden;
                }
            }
        }

        // Declare a PUBLIC average for Wierden
        public string AverageHumWierden
        {
            get { return averageHumWierden; }
            set
            {
                if (value != averageHumWierden)
                {
                    value = averageHumWierden;
                }
            }
        }

        // Declare a PUBLIC average for Wierden
        public string AverageLightWierden
        {
            get { return averageLightWierden; }
            set
            {
                if (value != averageLightWierden)
                {
                    value = averageLightWierden;
                }
            }
        }

        // Declare a PUBLIC average for Gronau
        public string AverageTempGronau
        {
            get { return averageTempGronau; }
            set
            {
                if (value != averageTempGronau)
                {
                    value = averageTempGronau;
                }
            }
        }

        // Declare a PUBLIC average for Gronau
        public string AverageHumGronau
        {
            get { return averageHumGronau; }
            set
            {
                if (value != averageHumGronau)
                {
                    value = averageHumGronau;
                }
            }
        }

        // Declare a PUBLIC average for Gronau
        public string AverageLightGronau
        {
            get { return averageLightGronau; }
            set
            {
                if (value != averageLightGronau)
                {
                    value = averageLightGronau;
                }
            }
        }

        // Declare a PUBLIC list of cities to display
        public List<string> Cities
        {
            get { return cities; }
            set
            {
                if(cities != value)
                {
                    cities = value;
                }
            }
        }

        // Declare a PUBLIC city title
        public string CityTitle
        {
            get { return cityTitle; }
            set
            {
                if(cityTitle != value)
                {
                    cityTitle = value;
                    OnPropertyChanged();

                    // TODO: Need to change respective city data
                }
            }
        }

        public ICommand OpenWebCommand { get; }

        public AboutViewModel()
        {
            SetupConnection();
            ComputeAllSensorAverage();
            SortSensorData();
            SetupImageSlider();
        }

        // Function to setup SSH connection
        private void SetupConnection()
        {
            var currentApp = Application.Current as App;

            pySensors = currentApp.SensorsPy;
            lhtSensors = currentApp.SensorsLht;
        }

        // Function to setup images for image sliders
        private void SetupImageSlider()
        {
            Title = "Home";
            cityTitle = "Enschede";

            cities = new List<string>
            {
                "https://www.netherlands-tourism.com/wp-content/uploads/2013/11/Enschede-Langestraat.jpg",
                "https://images0.persgroep.net/rcs/dbAwxzM_Ia_KQ8BZQmSpWOrvVG8/diocontent/100711003/_fill/1352/900/?appId=21791a8992982cd8da851550a453bd7f&quality=0.9",
                "https://www.dik.nl/wp-content/uploads/2019/04/Gronau_Altstadt_2-scaled.jpg"
            };
        }

        // Function to calculate daily average
        private void ComputeAllSensorAverage()
        {
            averageDay = new CalculateAverageDay();
            averageGraph = new List<GraphData>();
            sensorParser = new SensorParser();

            var currentApp = Application.Current as App;

            currentApp._parser.SortData_Py(pySensors);
            currentApp._parser.SortData_LHT(lhtSensors);

            var wierden_py = currentApp._parser.getWierenPy();
            var saxion_py = currentApp._parser.getSaxionPy();
            var wierden_lht = currentApp._parser.getWierenLHT();
            var gronau_lht = currentApp._parser.getGronauLHT();

            averageDay.CalculateAveragePy(wierden_py, saxion_py);
            averageDay.CalculateAverageLHT(wierden_lht, gronau_lht);
        }

        // Function to gather and sort data using the parsed sensor data
        private void SortSensorData()
        {
            // Py sensor data gathering
            var pyWierden = averageDay.getDayAverageWierdenPy();
            var pySaxion = averageDay.getDayAverageSaxionPy();

            var sumAvgTempWierdenPy = 0.0f;
            var sumAvgPresWierden = 0.0f;
            var sumAvgLightWierden = 0.0f;

            var sumAvgTempSaxion = 0.0f;
            var sumAvgPresSaxion = 0.0f;
            var sumAvgLightSaxion = 0.0f;

            foreach (var element in pyWierden)
            {
                sumAvgTempWierdenPy += element.average_temperature;
                sumAvgPresWierden += element.average_pressure;
                sumAvgLightWierden += element.average_light;
            }

            foreach (var element in pySaxion)
            {
                sumAvgTempSaxion += element.average_temperature;
                sumAvgPresSaxion += element.average_pressure;
                sumAvgLightSaxion += element.average_light;
            }

            // Lht-sensor data gathering
            var lhtWierden = averageDay.getDayAverageWierdenLHT();
            var lhtGronau = averageDay.getDayAverageGronauLHT();

            var sumAvgTempWierdenLht = 0.0f;
            var sumAvgHumWierden = 0.0f;
            var sumAvgLightWierdenLht = 0.0f;

            var sumAvgTempGronau = 0.0f;
            var sumAvgHumGronau = 0.0f;
            var sumAvgLightGronau = 0.0f;

            foreach (var element in lhtWierden)
            {
                sumAvgTempWierdenLht += element.average_temperature;
                sumAvgHumWierden += element.average_humidity;
                sumAvgLightWierdenLht += element.average_light;
            }

            foreach (var element in lhtGronau)
            {
                sumAvgTempGronau += element.average_temperature;
                sumAvgHumGronau += element.average_humidity;
                sumAvgLightGronau += element.average_light;
            }

            var AverageTempSaxion = sumAvgTempSaxion / lhtWierden.Count;
            var AveragePresSaxion = sumAvgPresSaxion / lhtWierden.Count;
            var AverageLightSaxion = sumAvgLightSaxion / lhtWierden.Count;

            averageTempSaxion = "Temperature: " + Math.Round(AverageTempSaxion, 2).ToString() + "°C (inside temperature)";
            averagePresSaxion = "Pressure: " + Math.Round(AveragePresSaxion, 2).ToString() + " Pa";
            averageLightSaxion = "Light: " + Math.Round(AverageLightSaxion, 2).ToString() + " Lumen";

            averageTempWierden = "Temperature: " + Math.Round(sumAvgTempWierdenLht / lhtWierden.Count, 2).ToString() + "°C";
            averageHumWierden = "Humidity: " + Math.Round(sumAvgHumWierden / lhtWierden.Count, 2).ToString() + "%";
            averageLightWierden = "Light: " + Math.Round(sumAvgLightWierden / lhtWierden.Count, 2).ToString() + " Lumen";

            averageTempGronau = "Temperature: " + Math.Round(sumAvgTempGronau / lhtWierden.Count, 2).ToString() + "°C";
            averageHumGronau = "Humidity: " + Math.Round(sumAvgHumGronau / lhtWierden.Count, 2).ToString() + "%";
            averageLightGronau = "Light: " + Math.Round(sumAvgLightGronau / lhtWierden.Count, 2).ToString() + " Lumen";

            // Create variable to store lht-sensor averages
            var averageTempLhtSensor = new List<float>();
            var averageHumLhtSensor = new List<float>();
            var averageLightLhtSensor = new List<float>();
            var averagePresPySensor = new List<float>();

            // Using py-sensor timings
            var lhtTimes = sensorParser.GetTimes(lhtWierden);
            lhtTimes = Enumerable.Reverse(lhtTimes).ToList();

            for (int i = 0; i < lhtWierden.Count; i++)
            {
                // Combine lht-sensors
                averageTempLhtSensor.Add((lhtWierden[i].average_temperature + lhtGronau[i].average_temperature / 2));
                averagePresPySensor.Add((pyWierden[i].average_pressure + pySaxion[i].average_pressure / 2));
                averageHumLhtSensor.Add((lhtWierden[i].average_humidity + lhtGronau[i].average_humidity / 2));
                averageLightLhtSensor.Add((lhtWierden[i].average_light + lhtGronau[i].average_light / 2));
            }

            for (int i = 0; i < lhtWierden.Count; i++)
            {
                // Gather all combined averages
                var allSensors = new GraphData(averageTempLhtSensor[i], averagePresPySensor[i], averageHumLhtSensor[i], averageLightLhtSensor[i], "Average_data_" + i, lhtTimes[i]);

                // Add to list of average graphs
                averageGraph.Add(allSensors);
            }

            var sumAvgTemp = 0.000f;
            var sumAvgPres = 0.000f;
            var sumAvgHum = 0.000f;
            var sumAvgLight = 0.000f;

            for(int i = 0; i < pyWierden.Count; i++)
            {
                sumAvgTemp += averageGraph[i].average_temperature;
                sumAvgPres += averageGraph[i].average_pressure;
                sumAvgHum += averageGraph[i].average_humidity;
                sumAvgLight += averageGraph[i].average_light;
            }

            var temperature = Math.Round(sumAvgTemp / averageGraph.Count, 2);
            var pressure = Math.Round(sumAvgPres / averageGraph.Count, 2);
            var humidity = Math.Round(sumAvgHum / averageGraph.Count, 2);
            var light = Math.Round(sumAvgLight / averageGraph.Count, 2);

            averageTemperature = double.IsNaN(temperature) ? "Temperature: " + "0" + "°C" : "Temperature: " + temperature.ToString() + "°C";
            averagePressure = double.IsNaN(pressure) ? "Pressure: " + "0" + " Pa" : "Pressure: " + pressure.ToString() + " Pa";
            averageHumidity = double.IsNaN(humidity) ? "Humidity: " + "0" + "%" : "Humidity: " + humidity.ToString() + "%";
            averageLight = double.IsNaN(pressure) ? "Light: " + "0" + " Lumen" : "Light: " + light.ToString() + " Lumen";
        }
    }
}