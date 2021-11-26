using GUI.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace GUI.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}