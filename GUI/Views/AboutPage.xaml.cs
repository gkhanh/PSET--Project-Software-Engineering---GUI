using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text;
using System.Windows;
using System.Diagnostics;



using GUI.Models;

namespace GUI.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            //TemperatureMainPage.Text = "Temperature: 10 Degrees";
            //LightMainPage.Text = "Light: 4500 Lumen";
            //HumidityMainPage.Text = "Humidity: 84%";
            
            Receive_connection receive_Connection = new Receive_connection();
            Console.WriteLine("Test");
            receive_Connection.RetrieveData();

        }
    }
}