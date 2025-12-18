using Microsoft.Maui.Controls;
using PillPilot.Services;

namespace PillPilot.Pages
{
    public partial class AlertsPage : ContentPage
    {
        public AlertsPage()
        {
            InitializeComponent();
            BindingContext = AlertsService.Instance; 
        }

        private async void OnSetAlertsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SetAlertsPage());
        }
    }
}
