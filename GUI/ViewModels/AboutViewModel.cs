using GUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
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

            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        // Function to setup SSH connection
        private void SetupConnection()
        {
            sensorParser = new SensorParser();
            var unparsedList = DatabaseConnection.Connect();
            var parsedPySensor = sensorParser.Parse(unparsedList.First);
            var parsedLhtSensor = sensorParser.Parse(unparsedList.Second);

            pySensors = parsedPySensor.First;
            lhtSensors = parsedLhtSensor.Second;
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

            sensorParser.SortData_Py(pySensors);
            sensorParser.SortData_LHT(lhtSensors);

            averageDay.CalculateAveragePy(sensorParser.getWierenPy(), sensorParser.getSaxionPy());
            averageDay.CalculateAverageLHT(sensorParser.getWierenLHT(), sensorParser.getGronauLHT());
        }

        // Function to gather and sort data using the parsed sensor data
        private void SortSensorData()
        {
            // Py sensor data gathering
            var pyWierden = averageDay.getDayAverageWierdenPy();
            var pySaxion = averageDay.getDayAverageSaxionPy();

            // Lht-sensor data gathering
            var lhtWierden = averageDay.getDayAverageWierdenLHT();
            var lhtGronau = averageDay.getDayAverageWierdenLHT();

            // Create variable to store lht-sensor averages
            var averageTempLhtSensor = new List<float>();
            var averageHumLhtSensor = new List<float>();
            var averageLightLhtSensor = new List<float>();
            var averagePresPySensor = new List<float>();

            /*// Create variable to store averages of all sensors combined
            var averageTempAllSensor = new List<float>();
            var averageHumAllSensor = new List<float>();
            var averagePresAllSensor = new List<float>();
            var averageLightAllSensor = new List<float>();*/

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

                /*// Combine all sensors
                averageTempAllSensor.Add((pyWierden[i].average_temperature + pySaxion[i].average_temperature + lhtWierden[i].average_temperature + lhtGronau[i].average_temperature / 4));
                averageLightAllSensor.Add((pyWierden[i].average_light + pySaxion[i].average_light + lhtWierden[i].average_light + lhtGronau[i].average_light / 4));
                averageHumAllSensor.Add((lhtWierden[i].average_light + lhtGronau[i].average_light / 2));
                averagePresAllSensor.Add((pyWierden[i].average_pressure + pySaxion[i].average_pressure / 2));*/
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

            averageTemperature = "Temperature: " + Math.Round(sumAvgTemp / averageGraph.Count, 2).ToString() + "°C";
            averagePressure = "Pressure: " + Math.Round(sumAvgPres / averageGraph.Count, 2).ToString() + " Pa";
            averageHumidity = "Humidity: " + Math.Round(sumAvgHum / averageGraph.Count, 2).ToString() + "%";
            averageLight = "Light: " + Math.Round(sumAvgLight / averageGraph.Count, 2).ToString() + " Lumen";
        }
    }
}