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
    public partial class DailyHumidity : ContentPage
    {
        List<Microcharts.ChartEntry> entries = new List<Microcharts.ChartEntry>
        {
            new ChartEntry(20)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "12:00",
                ValueLabel = "20",
            },
            new ChartEntry(6)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "13:00",
                ValueLabel = "20",
            },
            new ChartEntry(17)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "14:00",
                ValueLabel = "20",
            },
            new ChartEntry(12)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "15:00",
                ValueLabel = "20",
            },
            new ChartEntry(19)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "16:00",
                ValueLabel = "20",
            },
            new ChartEntry(4)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "17:00",
                ValueLabel = "20",
            },
            new ChartEntry(12)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "18:00",
                ValueLabel = "20",
            },
            new ChartEntry(12)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "19:00",
                ValueLabel = "20",
            },
            new ChartEntry(12)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "20:00",
                ValueLabel = "20",
            },
            new ChartEntry(14)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "21:00",
                ValueLabel = "20",
            },
            new ChartEntry(8)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "22:00",
                ValueLabel = "20",
            },
            new ChartEntry(9)
            {
                Color = SKColor.Parse("#FF1493"),
                Label = "23:00",
                ValueLabel = "20",
            }
        };
        public DailyHumidity()
        {
            InitializeComponent();
            HumidityDaily.Chart = new LineChart { Entries = entries, LineMode = LineMode.Straight, BackgroundColor = SKColors.Transparent };
        }
    }
}