using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GUI.ViewModels;

namespace GUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyTemperature : ContentPage
    {
        private DailyTemperatureViewModel viewModel;

        public DailyTemperature()
        {
            InitializeComponent();
            viewModel = new DailyTemperatureViewModel();
            BindingContext = viewModel;
        }
    }
}