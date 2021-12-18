using GUI.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace GUI.ViewModels
{
    public class DailyTemperatureViewModel : BaseViewModel
    {
        // Declare a picker list
        private List<Picker_Item> items { get; set; }

        // Declare a picker selected item
        private Picker_Item selectedItem;

        // Declare a temperature microchart list
        private List<Microcharts.ChartEntry> chartEntries_temp { get; set; }

        // Declare a humidity microchart list
        private List<Microcharts.ChartEntry> chartEntries_hum { get; set; }

        // Declare a light microchart list
        private List<Microcharts.ChartEntry> chartEntries_light { get; set; }

        // Declare a selected chart
        private List<Microcharts.ChartEntry> selectedList { get; set; }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////
        /// </summary>

        // Declare a PUBLIC picker item 'Ilist' to display to the view
        public IList<Picker_Item> Items
        { get {return items;}
        }

        // Declare a PUBLIC chart entries 'Ilist'to display to the view
        public IList<Microcharts.ChartEntry> SelectedChartEntries
        {
            get { return selectedList; }
            set
            {
                if (selectedList != (List<ChartEntry>)value)
                {
                    selectedList = (List<ChartEntry>)value;
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
                    OnPropertyChanged();
                }
            }
        }
        
        // CONSTRUCTOR
        public DailyTemperatureViewModel()
        {
            items = new List<Picker_Item>();
            selectedItem = new Picker_Item {};
            items.Add(new Picker_Item { Name = "Temperature" });
            items.Add(new Picker_Item { Name = "Humidity" });
            items.Add(new Picker_Item { Name = "Light" });

            SetupGraph(0);
        }

        // Function to setup graph
        public void SetupGraph(int choice) // Later, we will pass parsed data. Arguments will be (int choice, list<string> data)
        {
            switch(choice){
                // Temperature
                case 0:
                    // TODO: Get parsed data and assign to 'temp_entries'

                    // Make a 'Microchart' chart for temperature
                    List<Microcharts.ChartEntry> temp_entries = new List<Microcharts.ChartEntry>
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

                    // Assign 'selectedList' with temperature entries
                    selectedList = temp_entries;
                    break;

                // Humidity
                case 1:
                    // TODO: Get parsed data and assign to 'temp_entries'

                    // Make a 'Microchart' chart for temperature
                    List<Microcharts.ChartEntry> hum_entries = new List<Microcharts.ChartEntry>
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

                    // Assign 'selectedList' with temperature entries
                    selectedList = hum_entries;
                    break;

                // Light
                case 2:
                    // TODO: Get parsed data and assign to 'temp_entries'

                    // Make a 'Microchart' chart for temperature
                    List<Microcharts.ChartEntry> light_entries = new List<Microcharts.ChartEntry>
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

                    // Assign 'selectedList' with temperature entries
                    selectedList = light_entries;
                    break;
            }
                
        }
    }
}
