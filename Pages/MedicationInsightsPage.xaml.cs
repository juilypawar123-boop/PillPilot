using Microsoft.Maui.Controls;
using PillPilot.Data;
using PillPilot.Models;
using System;

namespace PillPilot.Pages
{
    public partial class MedicationInsightsPage : ContentPage
    {
        public MedicationInsightsPage()
        {
            InitializeComponent();
            LoadMedications();
        }

        private void LoadMedications()
        {
            MedicationContainer.Children.Clear();

            foreach (var med in MedicationsDataStore.medications)
            {
                MedicationContainer.Children.Add(CreateMedicationCard(med));
            }
        }

        private Frame CreateMedicationCard(MedicationsModel med)
        {
            Color cardColor = GenerateColor(med.MedicationName);

            var name = new Label
            {
                Text = med.MedicationName,
                TextColor = Colors.White,
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center
            };

            var dosage = new Label
            {
                Text = $"Dosage: {med.DosageAmount}",
                TextColor = Colors.White,
                FontSize = 15
            };

            var purpose = new Label
            {
                Text = $"Purpose: {med.Purpose}",
                TextColor = Colors.White,
                FontSize = 14
            };

            var alarm = new Label
            {
                Text = $"Alarm: {med.AlarmTime:hh:mm tt}",
                TextColor = Colors.White,
                FontSize = 14
            };

            var pharmacy = new Label
            {
                Text = $"Pharmacy: {med.PharmacyName}",
                TextColor = Colors.White,
                FontSize = 13
            };

            var deleteBtn = new Button
            {
                Text = "Delete",
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
                CornerRadius = 12,
                HeightRequest = 40,
                FontSize = 14
            };
            deleteBtn.Clicked += async (s, e) =>
            {
                bool confirm = await DisplayAlert(
                    "Delete Medication",
                    $"Are you sure you want to delete {med.MedicationName}?",
                    "Yes", "No");

                if (confirm)
                {
                    MedicationsDataStore.medications.Remove(med);
                    LoadMedications();
                }
            };

            var stopBtn = new Button
            {
                Text = "Stop",
                BackgroundColor = Colors.Orange,
                TextColor = Colors.White,
                CornerRadius = 12,
                HeightRequest = 40,
                FontSize = 14
            };
            stopBtn.Clicked += (s, e) =>
            {
                DisplayAlert("Medication Stopped",
                    $"{med.MedicationName} has been stopped.",
                    "OK");
            };

            var pauseBtn = new Button
            {
                Text = "Pause",
                BackgroundColor = Colors.Yellow,
                TextColor = Colors.Black,
                CornerRadius = 12,
                HeightRequest = 40,
                FontSize = 14
            };
            pauseBtn.Clicked += (s, e) =>
            {
                DisplayAlert("Medication Paused",
                    $"{med.MedicationName} alerts have been paused.",
                    "OK");
            };

            return new Frame
            {
                WidthRequest = 240,
                HeightRequest = 360,
                Padding = 15,
                Margin = new Thickness(10),
                BackgroundColor = cardColor,
                BorderColor = Colors.Black,
                CornerRadius = 18,
                HasShadow = true,

                Content = new VerticalStackLayout
                {
                    Spacing = 8,
                    Children =
            {
                name,
                dosage,
                purpose,
                alarm,
                pharmacy,
                deleteBtn,
                stopBtn,
                pauseBtn
            }
                }
            };
        }

        private Color GenerateColor(string input)
        {
            int hash = input.GetHashCode();
            byte r = (byte)((hash >> 16) & 0xFF);
            byte g = (byte)((hash >> 8) & 0xFF);
            byte b = (byte)(hash & 0xFF);

            return Color.FromRgb(r, g, b);
        }
    }
}
