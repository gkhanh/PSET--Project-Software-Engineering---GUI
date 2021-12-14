using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeeklyTemperature : ContentPage
    {
        private readonly ChartEntry[] entries = new[]
        {
            new ChartEntry(10)
            {
                Label = "Monday",
                ValueLabel = "10",
                Color = SKColor.Parse("#3FBCCA"),
                ValueLabelColor = SKColor.Parse("#FFFFFF")
            },
            new ChartEntry(12)
            {
                Label = "Tuesday",
                ValueLabel = "12",
                Color = SKColor.Parse("#3FBCCA"),
                ValueLabelColor = SKColor.Parse("#FFFFFF")
            },
            new ChartEntry(10)
            {
                Label = "Wednesday",
                ValueLabel = "10",
                Color = SKColor.Parse("#3FBCCA"),
                ValueLabelColor = SKColor.Parse("#FFFFFF")
            },
            new ChartEntry(14)
            {
                Label = "Thursday",
                ValueLabel = "14",
                Color = SKColor.Parse("#3FBCCA"),
                ValueLabelColor = SKColor.Parse("#FFFFFF")
            },
             new ChartEntry(9)
            {
                Label = "Friday",
                ValueLabel = "9",
                Color = SKColor.Parse("#3FBCCA"),
                ValueLabelColor = SKColor.Parse("#FFFFFF")
            },
              new ChartEntry(12)
            {
                Label = "Saturday",
                ValueLabel = "12",
                Color = SKColor.Parse("#3FBCCA"),
                ValueLabelColor = SKColor.Parse("#FFFFFF")
            },
              new ChartEntry(8)
            {
                Label = "Sunday",
                ValueLabel = "8",
                Color = SKColor.Parse("#3FBCCA"),
                ValueLabelColor = SKColor.Parse("#FFFFFF")
            }
        };
        public WeeklyTemperature()
        {
            InitializeComponent();
            TemperatureWeek.Chart = new LineChart {  Entries = entries, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent, LabelColor = SKColor.Parse("#FFFFFF") };
            TemperatureWeek1.Chart = new LineChart {  Entries = entries, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent, LabelColor = SKColor.Parse("#FFFFFF") };
            TemperatureWeek2.Chart = new LineChart {  Entries = entries, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent, LabelColor = SKColor.Parse("#FFFFFF") };
            HumidityDay.Chart = new LineChart {  Entries = entries, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent, LabelColor = SKColor.Parse("#FFFFFF") };
            //chartViewLine.Chart = new LineChart { Entries = entries, LineMode = LineMode.Straight 
        }
    }
}