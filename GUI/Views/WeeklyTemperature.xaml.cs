using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GUI.ViewModels;

namespace GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeeklyTemperature : ContentPage
    {
        private WeeklyOverviewViewModels viewModel;

        public WeeklyTemperature()
        {
            InitializeComponent();
            viewModel = new WeeklyOverviewViewModels();
            BindingContext = viewModel;
        }
    }
}