using Xamarin.Forms;
using GUI.ViewModels;

namespace GUI.Views
{
    public partial class AboutPage : ContentPage
    {
        private AboutViewModel viewModel;

        public AboutPage()
        {
            InitializeComponent();
            viewModel = new AboutViewModel();
            BindingContext = viewModel;

            MainImageView.ItemsSource = viewModel.Cities;
        }

        private void MainImageView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            var text = e.CurrentItem as string;

            if(text == viewModel.Cities[0])
            {
                viewModel.CityTitle = "Enschede";
            }

            if (text == viewModel.Cities[1])
            {
                viewModel.CityTitle = "Wierden";
            }

            if (text == viewModel.Cities[2])
            {
                viewModel.CityTitle = "Gronau";
            }
        }
    }
}