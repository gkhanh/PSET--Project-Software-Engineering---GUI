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

        // Declare a list of parsed py-temperature data
        private List<float> pyTemperatures { get; set; }

        // Declare a list of parsed py-pressure data
        private List<float> pyPressure { get; set; }

        // Declare a list of parsed py-light data
        private List<int> pyLight { get; set; }

        // Declare a parsed lht-sensor data list
        private List<LhtSensor> lhtSensors { get; set; }

        // Declare a list of parsed lht-temperature data
        private List<float> lhtTemperatures { get; set; }

        // Declare a list of parsed lht-pressure data
        private List<float> lhtHumidity { get; set; }

        // Declare a list of parsed lht-light data
        private List<int> lhtLight { get; set; }

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
            SortSensorData();
            CalculateAverages();
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

        // Function to gather and sort data using the parsed sensor data
        private void SortSensorData()
        {
            // Py sensor data gathering
            var temperature = sensorParser.GetTemperatures(pySensors);
            var pressure = sensorParser.GetPressure(pySensors);
            var light = sensorParser.GetLight(pySensors);

            pyTemperatures = Enumerable.Reverse(temperature).ToList();
            pyPressure = Enumerable.Reverse(pressure).ToList();
            pyLight = Enumerable.Reverse(light).ToList();

            // Lht sensor data gathering
            temperature = sensorParser.GetTemperatures(lhtSensors);
            var humidity = sensorParser.GetHumidity(lhtSensors);
            light = sensorParser.GetLight(lhtSensors);

            lhtTemperatures = Enumerable.Reverse(temperature).ToList();
            lhtHumidity = Enumerable.Reverse(humidity).ToList();
            lhtLight = Enumerable.Reverse(light).ToList();
        }

        // TEMPORARY: Function to calculate averages of data
        private void CalculateAverages()
        {
            // TODO: TEMPORARY FIX
            var sumTemp = 0.0f;
            var sumHum = 0.0f;
            var sumPres = 0.0f;
            var sumLight = 0;

            var avgTempList = new List<float>();
            var avgLightList = new List<int>();

            for (int i = 0; i < pyTemperatures.Count; i++)
            {
                avgTempList.Add(pyTemperatures[i]);
                avgTempList.Add(lhtTemperatures[i]);
                avgLightList.Add(pyLight[i]);
                avgLightList.Add(lhtLight[i]);
            }

            foreach (var element in avgTempList)
            {
                sumTemp += element;
            }

            foreach (var element in lhtHumidity)
            {
                sumHum += element;
            }

            foreach (var element in pyPressure)
            {
                sumPres += element;
            }

            foreach (var element in avgLightList)
            {
                sumLight += element;
            }

            averageTemperature = "Temperature: " + Math.Round(sumTemp / avgTempList.Count, 0).ToString() + "°C";
            averageHumidity = "Humidity: " + Math.Round(sumHum / avgTempList.Count, 0).ToString() + "%";
            averagePressure = "Pressure: " + Math.Round(sumPres / avgTempList.Count, 0).ToString() + " Pa";
            averageLight = "Light: " + (sumLight / avgTempList.Count).ToString() + " Lumen";
        }
    }
}