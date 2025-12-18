using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using PillPilot.Data;
using PillPilot.Models;
using System;
using System.Collections.ObjectModel;

namespace PillPilot.Pages
{
    public partial class AddMedicationsPage : ContentPage
    {
        public ObservableCollection<UploadFile> FilesList { get; set; }
            = new ObservableCollection<UploadFile>();

        public AddMedicationsPage()
        {
            InitializeComponent();
            FilesCollectionView.ItemsSource = FilesList;
        }

        private async void OnUploadFileClicked(object sender, EventArgs e)
        {
            // Validate medication name
            if (string.IsNullOrWhiteSpace(MedicationNameEntry.Text))
            {
                await DisplayAlert("Error", "Please enter a medication name.", "OK");
                return;
            }

            // Pick file
            var file = await FilePicker.PickAsync();

            if (file == null)
            {
                SelectedFileLabel.Text = "No file selected";
                return;
            }

            SelectedFileLabel.Text = $"Selected: {file.FileName}";

            // Add to list
            FilesList.Add(new UploadFile
            {
                MedicationName = MedicationNameEntry.Text,
                FileName = file.FileName
            });

            await DisplayAlert("Success", "Your file has been selected!", "OK");
        }

        private async void OnSaveMedication_Clicked(object sender, EventArgs e)
        {
            var medication = new MedicationsModel
            {
                MedicationName = MedicationNameEntry.Text,
                Purpose = Purpose.Text,
                DosageAmount = DosageAmount.Text,
                Instructions = Instructions.Text,
                LowStockWarning = LowStockWarning.Text,
                RepeatPrescriptionNeeded = RepeatPrescriptionNeeded.Text,
                PrescribingDoctorName = PrescribingDoctorName.Text,
                PharmacyName = PharmacyName.Text,
                MedicationDescription = MedicationDescription.Text
            };

            MedicationsDataStore.medications.Add(medication);

            // Clear fields
            MedicationNameEntry.Text = string.Empty;
            Purpose.Text = string.Empty;
            DosageAmount.Text = string.Empty;
            Instructions.Text = string.Empty;
            LowStockWarning.Text = string.Empty;
            RepeatPrescriptionNeeded.Text = string.Empty;
            PrescribingDoctorName.Text = string.Empty;
            PharmacyName.Text = string.Empty;
            MedicationDescription.Text = string.Empty;

            await DisplayAlert("Success", "Medication has been added successfully!", "OK");
            await Navigation.PopAsync();
        }
    }
}
