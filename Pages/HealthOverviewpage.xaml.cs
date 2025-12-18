using Microsoft.Maui.Controls;
using System;

namespace PillPilot.Pages
{
    public partial class HealthOverviewPage : ContentPage
    {
        public HealthOverviewPage()
        {
            InitializeComponent();
        }

        #region Navigation Button Handlers

        private async void NavigateToPage(Page page)
        {
            if (page != null)
                await Navigation.PushAsync(page);
        }

        private void Overview_Clicked(object sender, EventArgs e) =>
            NavigateToPage(new HealthOverviewPage());

        private void AddMedications_Clicked(object sender, EventArgs e) =>
            NavigateToPage(new AddMedicationsPage());

        private void MedicationInsights_Clicked(object sender, EventArgs e) =>
            NavigateToPage(new MedicationInsightsPage());

        private void DoseSchedule_Clicked(object sender, EventArgs e) =>
            NavigateToPage(new DoseSchedulePage());

        private void Alerts_Clicked(object sender, EventArgs e) =>
            NavigateToPage(new AlertsPage());

        private void Settings_Clicked(object sender, EventArgs e) =>
            NavigateToPage(new SettingsPage());

        private void SymtomsTracker_Clicked(object sender, EventArgs e) =>
            NavigateToPage(new SymptomsTrackerPage());

        private void Analytics_Clicked(object sender, EventArgs e) =>
            NavigateToPage(new AnalyticsPage());

        private void LogOut_Clicked(object sender, EventArgs e) =>
            NavigateToPage(new LogOutPage());

        #endregion
    }
}
