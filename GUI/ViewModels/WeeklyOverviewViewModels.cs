using System.Collections.Generic;
using GUI.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Linq;
using Xamarin.Forms;

namespace GUI.ViewModels
{
    public class WeeklyOverviewViewModels : BaseViewModel
    {
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

        // Declare a pressure microchart list
        private List<ChartEntry> chartEntries_pressure { get; set; }

        // Declare a selected list of chart entries
        private List<ChartEntry> selectedChartEntries { get; set; }

        // Declare a line chart
        private LineChart chart { get; set; }

        // Declare a sensor parser
        private SensorParser sensorParser { get; set; }

        // Declare an daily average parser
        private CalculateAverageWeek averageWeek { get; set; }

        // Declare a list of parsed py-sensor data
        private List<PySensor> pySensors { get; set; }

        // Declare a list of parsed lht-sensor data
        private List<LhtSensor> lhtSensors { get; set; }

        private List<GraphData> averageGraph { get; set; }

        private List<GraphData> averagePyGraph { get; set; }

        private List<GraphData> averageLhtGraph { get; set; }

        /// PUBLIC MEMBER VARIABLES FOR VIEW HANDLING
        /// //////////////////////////////////////////////////////////////////
        /// Below are the required public member varibles to handle the implementation
        /// and displaying to the view

        // Declare a PUBLIC picker title to display the items on the picker
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
        {
            get { return graphPickerItems; }
        }

        // Declare a PUBLIC sensor picker item 'Ilist' to display to the view
        public IList<Picker_Item> SensorPickerItems
        {
            get { return sensorPickerItems; }
        }

        // Declare a PUBLIC chart entries 'Ilist'to display to the view
        public IList<ChartEntry> SelectedChartEntries
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

        // Declare a PUBLIC selected chart to display
        public LineChart Chart
        {
            get { return chart; }
            set
            {
                if (chart != value)
                {
                    chart = value;
                    OnPropertyChanged();
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

        public string GraphTitle
        {
            get { return graphTitle; }
            set
            {
                if (graphTitle != value)
                {
                    graphTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        /// ViewModel constructor
        /// //////////////////////////////////////////////////////////////////
        /// In this constructor, the initialisation process is performed

        // CONSTRUCTOR
        public WeeklyOverviewViewModels()
        {
            Title = "Weekly overview";
            SetupConnection();
            ComputeWeeklyAverage();
            SortSensorData();
            SetupPicker();
            SetupGraphs();
            graphTitle = "Daily Temperature";

            // Set chart with temperature data as default entries
            chart = new LineChart { Entries = chartEntries_temp, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, YAxisPosition = Position.Left };
        }
        private void SetupConnection()
        {
            var currentApp = Application.Current as App;

            pySensors = currentApp.SensorsPy;
            lhtSensors = currentApp.SensorsLht;
        }

        // Function to calculate the weekly average
        private void ComputeWeeklyAverage()
        {
            averageWeek = new CalculateAverageWeek();
            sensorParser = new SensorParser();

            sensorParser.SortData_Py(pySensors);
            sensorParser.SortData_LHT(lhtSensors);

            averageWeek.CalculateAveragePy(sensorParser.getWierenPy(), sensorParser.getSaxionPy());
            averageWeek.CalculateAverageLHT(sensorParser.getWierenLHT(), sensorParser.getGronauLHT());
        }

        // Function to gather and sort data using the parsed sensor data
        private void SortSensorData()
        {
            // Initialise graph
            averagePyGraph = new List<GraphData>();
            averageLhtGraph = new List<GraphData>();

            // Py sensor data gathering
            var pyWierden = averageWeek.getWeekAverageWierdenPy();
            var pySaxion = averageWeek.getWeekAverageSaxionPy();

            // Lht-sensor data gathering
            var lhtWierden = averageWeek.getWeekAverageWierdenLHT();
            var lhtGronau = averageWeek.getWeekAverageWierdenLHT();

            // Create variable to store py-sensor averages
            var averageTempPySensor = new List<float>();
            var averagePresPySensor = new List<float>();
            var averageLightPySensor = new List<float>();

            // Create variable to store lht-sensor averages
            var averageTempLhtSensor = new List<float>();
            var averageHumLhtSensor = new List<float>();
            var averageLightLhtSensor = new List<float>();

            var pyTimes = new List<string>();

            // Using py-sensor timings
            for (int i = 0; i < 7; i++)
            {
                pyTimes.Add(DateTime.Today.AddDays(-i).ToString(""));
            }

            pyTimes = Enumerable.Reverse(pyTimes).ToList();

            var AvgTemp = 0.00;
            var AvgPres = 0.00;
            var AvgHum = 0.00;
            var AvgLight = 0.00;

            for (int i = 0; i < pyWierden.Count; i++)
            {
                AvgTemp = Math.Round(pyWierden[i].average_temperature + pySaxion[i].average_temperature / 2, 1);
                AvgPres = Math.Round(pyWierden[i].average_pressure + pySaxion[i].average_pressure / 2, 1);
                AvgLight = Math.Round(pyWierden[i].average_light + pySaxion[i].average_light / 2, 1);

                // Combine py-sensors
                averageTempPySensor.Add(!double.IsNaN(AvgTemp) ? (float)AvgTemp : 0.0f);
                averagePresPySensor.Add(!double.IsNaN(AvgPres) ? (float)AvgPres : 0.0f);
                averageLightPySensor.Add(!double.IsNaN(AvgLight) ? (float)AvgLight : 0.0f);

                // Reset variables
                AvgTemp = 0.00;
                AvgPres = 0.00;
                AvgLight = 0.00;
                AvgHum = 0.00;

                AvgTemp = Math.Round(lhtWierden[i].average_temperature + lhtGronau[i].average_temperature / 2, 1);
                AvgHum = Math.Round(lhtWierden[i].average_humidity + lhtGronau[i].average_humidity / 2, 1);
                AvgLight = Math.Round(lhtWierden[i].average_light + lhtGronau[i].average_light / 2, 1);

                // Combine lht-sensors
                averageTempLhtSensor.Add(!double.IsNaN(AvgTemp) ? (float)AvgTemp : 0.0f);
                averageHumLhtSensor.Add(!double.IsNaN(AvgHum) ? (float)AvgHum : 0.0f);
                averageLightLhtSensor.Add(!double.IsNaN(AvgLight) ? (float)AvgLight : 0.0f);
            }

            for (int i = 0; i < pyWierden.Count; i++)
            {
                // Add to list of average graphs
                averagePyGraph.Add(new GraphData(averageTempPySensor[i], averagePresPySensor[i], 0.0f, averageLightPySensor[i], "Average_data_" + i, pyTimes[i]));
                averageLhtGraph.Add(new GraphData(averageTempLhtSensor[i], 0.0f, averageHumLhtSensor[i], averageLightLhtSensor[i], "Average_data_" + i, pyTimes[i]));
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

        // Function to change graph name
        public void ChangeName(string name)
        {
            if (name == graphPickerItems[0].Name)
            {
                GraphTitle = "Daily Temperature";
            }
            if (name == graphPickerItems[1].Name)
            {
                GraphTitle = "Daily Humidity";
            }
            if (name == graphPickerItems[2].Name)
            {
                GraphTitle = "Daily Light";
            }
        }

        // Function to setup graph
        private void SetupGraphs()
        {
            chartEntries_temp = new List<ChartEntry>();
            chartEntries_hum = new List<ChartEntry>();
            chartEntries_light = new List<ChartEntry>();
          
            foreach (var element in averagePyGraph)
            {
                var entry = new ChartEntry(element.average_temperature)
                {
                    Color = SKColor.Parse("#FF1E90FF"),
                    Label = element.date.ToString(),
                    TextColor = SKColor.Parse("FF000000"),
                    ValueLabel = element.average_temperature.ToString(),
                };
                chartEntries_temp.Add(entry);
            }

            foreach (var element in averageLhtGraph)
            {
                var entry = new ChartEntry(element.average_humidity)
                {
                    Color = SKColor.Parse("#FF1E90FF"),
                    Label = element.date.ToString(),
                    TextColor = SKColor.Parse("FF000000"),
                    ValueLabel = element.average_humidity.ToString()
                };
                chartEntries_hum.Add(entry);
            }

            foreach (var element in averagePyGraph)
            {
                var entry = new ChartEntry(element.average_light)
                {
                    Color = SKColor.Parse("#FF1E90FF"),
                    Label = element.date.ToString(),
                    TextColor = SKColor.Parse("FF000000"),
                    ValueLabel = element.average_light.ToString()
                };
                chartEntries_light.Add(entry);
            }

        }

        // Function that handles changing of graph entries source
        private void ChangeGraphSource(string name)
        {
            // Clear current entries
            chartEntries_temp = new List<ChartEntry>();
            chartEntries_hum = new List<ChartEntry>();
            chartEntries_light = new List<ChartEntry>();
            chartEntries_pressure = new List<ChartEntry>();

            // Py-sensor selected
            if (name == sensorPickerItems[0].Name)
            {
                // Py sensor selected
                foreach (var element in averagePyGraph)
                {
                    var entry = new ChartEntry(element.average_temperature)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = element.date.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.average_temperature.ToString()
                    };
                    chartEntries_temp.Add(entry);
                }

                foreach (var element in averagePyGraph)
                {
                    var entry = new ChartEntry(element.average_pressure)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = element.date.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.average_pressure.ToString()
                    };
                    chartEntries_pressure.Add(entry);
                }

                foreach (var element in averagePyGraph)
                {
                    var entry = new ChartEntry(element.average_light)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = element.date.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.average_light.ToString()
                    };
                    chartEntries_light.Add(entry);
                }
            }

            // Lht-sensor selected
            if (name == sensorPickerItems[1].Name)
            {
                foreach (var element in averageLhtGraph)
                {
                    var entry = new ChartEntry(element.average_temperature)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = element.date.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.average_temperature.ToString()
                    };
                    chartEntries_temp.Add(entry);
                }

                foreach (var element in averageLhtGraph)
                {
                    var entry = new ChartEntry(element.average_humidity)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = element.date.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.average_humidity.ToString()
                    };
                    chartEntries_hum.Add(entry);
                }

                foreach (var element in averageLhtGraph)
                {
                    var entry = new ChartEntry(element.average_light)
                    {
                        Color = SKColor.Parse("#FF1E90FF"),
                        Label = element.date.ToString(),
                        TextColor = SKColor.Parse("FF000000"),
                        ValueLabel = element.average_light.ToString()
                    };
                    chartEntries_light.Add(entry);
                }
            }
        }

        // Function that handles changing of graph from picker data
        private void ChangeGraph(string name)
        {
            if (name == graphPickerItems[0].Name)
            {
                // TEMPERATURE:
                Chart = new LineChart { Entries = chartEntries_temp, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, YAxisPosition = Position.Left };
            }

            if (name == graphPickerItems[1].Name)
            {
                // HUMIDITY:
                Chart = new LineChart { Entries = chartEntries_hum, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, YAxisPosition = Position.Left };
            }

            if (name == graphPickerItems[2].Name)
            {
                // LIGHT:
                Chart = new LineChart { Entries = chartEntries_light, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent, ValueLabelOrientation = Orientation.Horizontal, LabelOrientation = Orientation.Horizontal, YAxisPosition = Position.Left };
            }
        }
    }
}