using Microsoft.Maui.Controls;
using PillPilot.Models;
using PillPilot.Services;

namespace PillPilot.Pages
{
    public partial class SetAlertsPage : ContentPage
    {
        public SetAlertsPage()
        {
            InitializeComponent();
        }

        private async void OnSaveAlertClicked(object sender, EventArgs e)
        {
            var alert = new AlertsModel
            {
                MedicationName = MedicationNameEntry.Text?.Trim(),
                Purpose = PurposeEntry.Text?.Trim(),
                Instructions = InstructionsEntry.Text?.Trim(),
                StartDate = StartDatePicker.Date,
                EndDate = EndDatePicker.Date,
                AlertTime = AlertTimePicker.Time
            };

            if (string.IsNullOrEmpty(alert.MedicationName))
            {
                await DisplayAlert("Error", "Medication name cannot be empty.", "OK");
                return;
            }

            AlertsService.Instance.AddAlert(alert);

            MedicationNameEntry.Text = PurposeEntry.Text = InstructionsEntry.Text = string.Empty;

            await DisplayAlert("Success", "Alert added successfully!", "OK");

            await Navigation.PopAsync();
        }
    }
}
