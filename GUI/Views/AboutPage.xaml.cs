using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GUI.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            TemperatureMainPage.Text = "Temperature: 10 Degrees";
            LightMainPage.Text = "Light: 4500 Lumen";
            HumidityMainPage.Text = "Humidity: 84%";

        }
    }
}