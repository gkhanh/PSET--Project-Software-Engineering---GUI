using GUI.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GUI.ViewModels
{
    public class DailyTemperatureViewModel : BaseViewModel
    {
        /// <summary>
        /// //////////////////////////////////////////////////////////////////
        /// </summary>

        // Declare a picker title name
        private string pickerTitle { get; set; }

        // Declare a picker list
        private List<Picker_Item> items { get; set; }

        // Declare a picker selected item
        private Picker_Item selectedItem;

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

        /// <summary>
        /// //////////////////////////////////////////////////////////////////
        /// </summary>

        // Declare a PUBLIC picker title to display the items on the picker
        public string PickerTitle
        {
            get { return pickerTitle; }
        }

        // Declare a PUBLIC picker item 'Ilist' to display to the view
        public IList<Picker_Item> Items
        { get {return items;}
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

        // Declare a PUBLIC selected item to display the selected item on the picker
        public Picker_Item SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if(selectedItem != value)
                {
                    selectedItem = value;
                    ChangeGraph(selectedItem.Name);
                }
            }
        }

        // Declare a PUBLIC selected chart to display
        public LineChart Chart
        {
            get { return chart; }
            set { if(chart != value)
                {
                    chart = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////
        /// </summary>

        // CONSTRUCTOR
        public DailyTemperatureViewModel()
        {
            SetupPicker();
            SetupGraphs();

            // Set chart with temperature data as default entries
            chart = new LineChart { Entries = chartEntries_temp, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent };
        }

        // Function to setup picker control
        private void SetupPicker()
        {
            items = new List<Picker_Item>();
            selectedItem = new Picker_Item { };
            pickerTitle = "Select graph data:";

            items.Add(new Picker_Item { Name = "Temperature" });
            items.Add(new Picker_Item { Name = "Humidity" });
            items.Add(new Picker_Item { Name = "Light" });
        }

        // Function to setup graph
        private void SetupGraphs()
        {
            // Temperature chart entries
            chartEntries_temp = new List<Microcharts.ChartEntry>
                    {
                            new ChartEntry(20)
                            {
                                Color = SKColor.Parse("#FF1E90FF"),
                                Label = "12:00",
                                TextColor = SKColor.Parse("FF000000"),
                                ValueLabel = "20",
                            },
                        new ChartEntry(6)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "13:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "6",
                        },
                        new ChartEntry(17)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "14:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "17",
                        },
                        new ChartEntry(12)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "15:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "12",
                        },
                        new ChartEntry(19)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "16:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "19",
                        },
                        new ChartEntry(4)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "17:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "4",
                        },
                    };

            // Humidity chart entries
            chartEntries_hum = new List<Microcharts.ChartEntry>
                    {
                        new ChartEntry(10)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "12:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "10",
                        },
                        new ChartEntry(6)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "13:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "6",
                        },
                        new ChartEntry(17)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "14:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "17",
                        },
                        new ChartEntry(12)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "15:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "12",
                        },
                        new ChartEntry(21)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "16:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "21",
                        },
                        new ChartEntry(7)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "17:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "7",
                        },
                    };

            // Light chart entries
            chartEntries_light = new List<Microcharts.ChartEntry>
                    {
                        new ChartEntry(8)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "12:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "8",
                        },
                        new ChartEntry(6)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "13:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "6",
                        },
                        new ChartEntry(17)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "14:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "17",
                        },
                        new ChartEntry(12)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "15:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "12",
                        },
                        new ChartEntry(21)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "16:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "21",
                        },
                        new ChartEntry(20)
                        {
                            Color = SKColor.Parse("#FF1E90FF"),
                            Label = "17:00",
                            TextColor = SKColor.Parse("FF000000"),
                            ValueLabel = "20",
                        },
                    };
        }

        // Function that handles changing of graph from picker data
        private void ChangeGraph(string name)
        {
            if (name == items[0].Name)
            {
                // TEMPERATURE:
                Chart = new LineChart { Entries = chartEntries_temp, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent };
            }

            if (name == items[1].Name)
            {
                // HUMIDITY:
                Chart = new LineChart { Entries = chartEntries_hum, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent };
            }

            if (name == items[2].Name)
            {
                // LIGHT:
                Chart = new LineChart { Entries = chartEntries_light, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent };
            }
        }
    }
}
