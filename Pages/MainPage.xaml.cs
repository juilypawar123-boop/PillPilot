using Microsoft.Maui.Controls;
using PillPilot.Pages;

namespace PillPilot
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new LogInPage());
        }
    }
}
