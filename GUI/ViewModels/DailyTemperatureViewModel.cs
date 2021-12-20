using GUI.Models;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;

/************* IMPORTANT MESSAGE (PLEASE READ ME) *************/

/* TODO: Use this class as template for a ViewModel. This ViewModel is equipped with
         the necessary variables and functions that is used in a View within this
         application. (See 'Daily overview temperature' page in application)

         Please follow the structure of this class, beginning declarations of private
         and public member variables. Continuing with the constructor and function
         implementations for this ViewModel.

         Remember, comments are always appreciated.

         Of course, dont hesitate to ask if you need help clarifying what all this means.
         Just let me know...

         Arief.K
*/

namespace GUI.ViewModels
{
    public class DailyTemperatureViewModel : BaseViewModel
    {
        /// PRIVATE MEMBER VARIABLES FOR LOCAL HANDLING
        /// //////////////////////////////////////////////////////////////////
        /// Below are the required private member variables to handle the declaration
        /// variables to display to the view

        // Declare a graph picker title name
        private string graphPickerTitle { get; set; }

        // Declare a graph picker list
        private List<Picker_Item> graphPickerItems { get; set; }

        // Declare a sensor picker title name
        private string sensorPickerTitle { get; set; }

        // Declare a sensor picker list
        private List<Picker_Item> sensorPickerItems { get; set; }

        // Declare a graph title
        private string graphTitle { get; set; }

        // Declare a graph picker selected item
        private Picker_Item graphPickerSelectedItem;

        // Declare a sensor picker selected item
        private Picker_Item sensorPickerSelectedItem;

        // Declare a temperature microchart list
        private List<ChartEntry> chartEntries_temp { get; set; }

        // Declare a humidity microchart list
        private List<ChartEntry> chartEntries_hum { get; set; }

        // Declare a light microchart list
        private List<ChartEntry> chartEntries_light { get; set; }

        // Declare a selected list of chart entries
        private List<ChartEntry> selectedChartEntries { get; set; }

        // Declare a line chart
        private LineChart chart { get; set; }

        // Declare a sensor parser
        private SensorParser sensorParser { get; set; }

        // Declare a list of parsed py-sensor data
        private List<PySensor> pySensors { get; set; }

        // Declare a list of parsed py-temperature data
        private List<float> pyTemperatures { get; set; }

        // Declare a list of parsed py-pressure data
        private List<float> pyPressure { get; set; }

        // Declare a list of parsed py-light data
        private List<int> pyLight { get; set; }

        // Declare a list of parsed lht-sensor data
        private List<LhtSensor> lhtSensors { get; set; }

        // Declare a list of parsed lht-temperature data
        private List<float> lhtTemperatures { get; set; }

        // Declare a list of parsed lht-pressure data
        private List<float> lhtHumidity { get; set; }

        // Declare a list of parsed lht-light data
        private List<int> lhtLight { get; set; }

        /// PUBLIC MEMBER VARIABLES FOR VIEW HANDLING
        /// //////////////////////////////////////////////////////////////////
        /// Below are the required public member varibles to handle the implementation
        /// and displaying to the view

        // Declare a PUBLIC graph picker title to display the items on the picker
        public string GraphPickerTitle
        {
            get { return graphPickerTitle; }
        }

        // Declare a PUBLIC sensor picker title to display the items on the picker
        public string SensorPickerTitle
        {
            get { return sensorPickerTitle; }
        }

        // Declare a PUBLIC graph picker item 'Ilist' to display to the view
        public IList<Picker_Item> GraphPickerItems
        { get { return graphPickerItems; }
        }

        // Declare a PUBLIC sensor picker item 'Ilist' to display to the view
        public IList<Picker_Item> SensorPickerItems
        {
            get { return sensorPickerItems; }
        }

        // Declare a PUBLIC chart entries 'Ilist'to display to the view
        public IList<Microcharts.ChartEntry> SelectedChartEntries
        {
            get { return selectedChartEntries; }
            set
            {
                if (selectedChartEntries != (List<ChartEntry>)value)
                {
                    selectedChartEntries = (List<ChartEntry>)value;
                    OnPropertyChanged();
                }
            }
        }

        // Declare a PUBLIC selected graph item to display the selected item on the picker
        public Picker_Item SelectedGraphItem
        {
            get { return graphPickerSelectedItem; }
            set
            {
                if (graphPickerSelectedItem != value)
                {
                    graphPickerSelectedItem = value;

                    // Change graph name when 'selectedItem' of graph 'picker' has changed
                    ChangeName(graphPickerSelectedItem.Name);
                    
                    // Change graph when 'selectedItem' of graph 'picker' has changed
                    ChangeGraph(graphPickerSelectedItem.Name);
                }
            }
        }

        // Declare a PUBLIC selected item to display the selected item on the picker
        public Picker_Item SelectedSensorItem
        {
            get { return sensorPickerSelectedItem; }
            set
            {
                if (sensorPickerSelectedItem != value)
                {
                    sensorPickerSelectedItem = value;

                    // Change graph entry source when sensor source changed
                    ChangeGraphSource(sensorPickerSelectedItem.Name);

                    // Change graph when 'selectedItem' of graph 'picker' has changed
                    ChangeGraph(graphPickerSelectedItem.Name);
                }
            }
        }

        // Declare a PUBLIC selected chart to display
        public LineChart Chart
        {
            get { return chart; }
            set { if (chart != value)
                {
                    chart = value;
                    OnPropertyChanged();
                }
            }
        }

        // Declare a PUBLIC graph title to display
        public string GraphTitle
        {
            get {return graphTitle;}
            set
            {
                if(graphTitle != value)
                {
                    graphTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        /// ViewModel constructor
        /// //////////////////////////////////////////////////////////////////
        /// In this constructor, the initialisation process is performed.

        // CONSTRUCTOR
        public DailyTemperatureViewModel()
        {
            Title = "Hour overview";

            // Set up connection and view elements
            SetupConnection();
            SortSensorData();
            SetupPicker();
            SetupGraphs();
            graphTitle = "Hourly Temperature";
           
            // Set chart with temperature data as default entries
            chart = new LineChart { Entries = chartEntries_temp, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent };
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

        // Function to change graph name from picker data
        private void ChangeName(string name)
        {
            if(name == graphPickerItems[0].Name)
            {
                GraphTitle = "Hourly Temperature";
            }
            if (name == graphPickerItems[1].Name)
            {
                GraphTitle = "Hourly Humidity";
            }
            if (name == graphPickerItems[2].Name)
            {
                GraphTitle = "Hourly Light";
            }
        }

        // Function to setup picker control
        private void SetupPicker()
        {
            graphPickerItems = new List<Picker_Item>();
            graphPickerSelectedItem = new Picker_Item { };
            graphPickerTitle = "Select graph data:";

            graphPickerItems.Add(new Picker_Item { Name = "Temperature" });
            graphPickerItems.Add(new Picker_Item { Name = "Humidity" });
            graphPickerItems.Add(new Picker_Item { Name = "Light" });

            graphPickerSelectedItem = graphPickerItems[0];

            sensorPickerItems = new List<Picker_Item>();
            sensorPickerSelectedItem = new Picker_Item { };
            sensorPickerTitle = "Select sensor data:";

            sensorPickerItems.Add(new Picker_Item { Name = "Py-sensor" });
            sensorPickerItems.Add(new Picker_Item { Name = "Lht-sensor" });

            sensorPickerSelectedItem = sensorPickerItems[0];
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

        // Function to setup graph
        private void SetupGraphs()
        {
            // Chart entry initialisation
            chartEntries_temp = new List<ChartEntry>();
            chartEntries_hum = new List<ChartEntry>();
            chartEntries_light = new List<ChartEntry>();

            var pyTimes = sensorParser.GetTimes(pySensors);
            var lhtTimes = sensorParser.GetTimes(lhtSensors);

            // Times are reversed to correctly show data
            pyTimes = Enumerable.Reverse(pyTimes).ToList();
            lhtTimes = Enumerable.Reverse(lhtTimes).ToList();

            var pyTimeIndex = 0;
            var lhtTimeIndex = 0;

            // By default, display data of Py-sensors
            foreach (var element in pyTemperatures)
            {
                var entry = new ChartEntry(element)
                {
                    Color = SKColor.Parse("#FF1E90FF"),
                    Label = pyTimes[pyTimeIndex].Hour.ToString() + ':' + pyTimes[pyTimeIndex].Minute.ToString(),
                    TextColor = SKColor.Parse("FF000000"),
                    ValueLabel = element.ToString()
                };
                chartEntries_temp.Add(entry);
                pyTimeIndex++;
            }

            // Reset py time index
            pyTimeIndex = 0;

            foreach (var element in lhtHumidity)
            {
                var entry = new ChartEntry(element)
                {
                    Color = SKColor.Parse("#FF1E90FF"),
                    Label = lhtTimes[lhtTimeIndex].Hour.ToString() + ':' + lhtTimes[lhtTimeIndex].Minute.ToString(),
                    TextColor = SKColor.Parse("FF000000"),
                    ValueLabel = element.ToString()
                };
                chartEntries_hum.Add(entry);
                lhtTimeIndex++;
            }

            foreach(var element in pyLight)
            {
                var entry = new ChartEntry(element)
                {
                    Color = SKColor.Parse("#FF1E90FF"),
                    Label = pyTimes[pyTimeIndex].Hour.ToString() + ':' + pyTimes[pyTimeIndex].Minute.ToString(),
                    TextColor = SKColor.Parse("FF000000"),
                    ValueLabel = element.ToString()
                };
                chartEntries_light.Add(entry);
                pyTimeIndex++;
            }
        }

        // Function that handles changing of graph entries source
        private void ChangeGraphSource(string name)
        {
            // Clear current entries
            chartEntries_temp = new List<ChartEntry>();
            chartEntries_hum = new List<ChartEntry>();
            chartEntries_light = new List<ChartEntry>();

            // Get sensor times
            var pyTimes = sensorParser.GetTimes(pySensors);
            var lhtTimes = sensorParser.GetTimes(lhtSensors);

            // Times are reversed to correctly show data
            pyTimes = Enumerable.Reverse(pyTimes).ToList();
            lhtTimes = Enumerable.Reverse(lhtTimes).ToList();

            var pyTimeIndex = 0;
            var lhtTimeIndex = 0;

            // Py-sensor selected
            if (name == sensorPickerItems[0].Name)
            {
                // Py sensor selected
                foreach (var element in pyTemperatures)
                {
                    var entry = new ChartEntry(element)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = pyTimes[pyTimeIndex].Hour.ToString() + ':' + pyTimes[pyTimeIndex].Minute.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.ToString()
                    };
                    chartEntries_temp.Add(entry);
                    pyTimeIndex++;
                }

                // Reset py time index
                pyTimeIndex = 0;

                foreach (var element in lhtHumidity)
                {
                    var entry = new ChartEntry(element)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = lhtTimes[lhtTimeIndex].Hour.ToString() + ':' + lhtTimes[lhtTimeIndex].Minute.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.ToString()
                    };
                    chartEntries_hum.Add(entry);
                    lhtTimeIndex++;
                }

                foreach (var element in pyLight)
                {
                    var entry = new ChartEntry(element)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = pyTimes[pyTimeIndex].Hour.ToString() + ':' + pyTimes[pyTimeIndex].Minute.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.ToString()
                    };
                    chartEntries_light.Add(entry);
                    pyTimeIndex++;
                }
            }

            // Lht-sensor selected
            if (name == sensorPickerItems[1].Name)
            {
                foreach (var element in lhtTemperatures)
                {
                    var entry = new ChartEntry(element)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = lhtTimes[lhtTimeIndex].Hour.ToString() + ':' + lhtTimes[lhtTimeIndex].Minute.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.ToString()
                    };
                    chartEntries_temp.Add(entry);
                    lhtTimeIndex++;
                }

                // Reset lht time index
                lhtTimeIndex = 0;

                foreach (var element in lhtHumidity)
                {
                    var entry = new ChartEntry(element)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = lhtTimes[lhtTimeIndex].Hour.ToString() + ':' + lhtTimes[lhtTimeIndex].Minute.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.ToString()
                    };
                    chartEntries_hum.Add(entry);
                    lhtTimeIndex++;
                }

                // Reset lht time index
                lhtTimeIndex = 0;

                foreach (var element in lhtLight)
                {
                    var entry = new ChartEntry(element)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = lhtTimes[lhtTimeIndex].Hour.ToString() + ':' + lhtTimes[lhtTimeIndex].Minute.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.ToString()
                    };
                    chartEntries_light.Add(entry);
                    lhtTimeIndex++;
                }
            }
        }

        // Function that handles changing of graph from picker data
        private void ChangeGraph(string name)
        {
            if (name == graphPickerItems[0].Name)
            {
                // TEMPERATURE:
                Chart = new LineChart { Entries = chartEntries_temp, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent };
            }

            if (name == graphPickerItems[1].Name)
            {
                // HUMIDITY:
                Chart = new LineChart { Entries = chartEntries_hum, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent };
            }

            if (name == graphPickerItems[2].Name)
            {
                // LIGHT:
                Chart = new LineChart { Entries = chartEntries_light, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent };
            }
        }
    }
}
