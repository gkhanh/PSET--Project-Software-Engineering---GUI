using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using GUI.ViewModels;
using System.Collections.ObjectModel;
using GUI.Models;

namespace GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyTemperature : ContentPage
    {
        // Member variables of daily temperature
        /*List<Microcharts.ChartEntry> entries1 = new List<Microcharts.ChartEntry>
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
        };*/

        private DailyTemperatureViewModel viewModel;

        public DailyTemperature()
        {
            InitializeComponent();
            viewModel = new DailyTemperatureViewModel();
            BindingContext = viewModel;
            TemperatureDaily.Chart = new LineChart { Entries = viewModel.SelectedChartEntries, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent };

            // Defining a picker
            Picker picker = new Picker { Title = "Charts", TitleColor = Color.Yellow };
            picker.SetBinding(Picker.ItemsSourceProperty, "Items");
            picker.SetBinding(Picker.SelectedItemProperty, "SelectedItem");
            picker.ItemDisplayBinding = new Binding("Name");
        }
    }
}