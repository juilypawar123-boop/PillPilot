using Microsoft.Maui.Controls;
using PillPilot.Data;
using PillPilot.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PillPilot.Pages
{
    public partial class SymptomsTrackerPage : ContentPage
    {
        public SymptomsTrackerPage()
        {
            InitializeComponent();

            SeveritySlider.ValueChanged += (s, e) =>
            {
                SeverityValue.Text = ((int)e.NewValue).ToString();
            };

            RecentLogs.ItemsSource = SymptomsDataStore.Symptoms;

            BuildHeatmap();
            RefreshStats();
            UpdateStreakLabel();
        }

        private async void QuickTile_Tapped(object sender, EventArgs e)
        {
            var element = sender as VisualElement;
            string param = null;

       

            if (!string.IsNullOrWhiteSpace(param))
            {
                QuickSymptomName.Text = param;
                await Task.Delay(50);
                QuickSymptomName.Focus();
            }
        }

        private async void BodyMap_Tapped(object sender, EventArgs e)
        {
            string location = await DisplayPromptAsync("Body location", "Type the location (e.g., left temple):", initialValue: "");
            if (string.IsNullOrWhiteSpace(location)) return;

            QuickSymptomName.Text = "Pain";
            SeveritySlider.Value = 60;
            NotesEditor.Text = $"Location: {location}";
            QuickSymptomName.Focus();
        }

        private async void SaveSymptom_Clicked(object sender, EventArgs e)
        {
            string name = QuickSymptomName.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Missing", "Please enter a symptom name.", "OK");
                return;
            }

            var s = new SymptomModel
            {
                SymptomName = name,
                Severity = (int)SeveritySlider.Value,
                SymptomType = SymptomTypePicker.SelectedItem?.ToString() ?? string.Empty,
                Notes = NotesEditor.Text ?? string.Empty,
                Timestamp = DateTime.Now,
                Location = ExtractLocationFromNotes(NotesEditor.Text)
            };

            SymptomsDataStore.Add(s);

            QuickSymptomName.Text = string.Empty;
            NotesEditor.Text = string.Empty;
            SymptomTypePicker.SelectedIndex = -1;
            SeveritySlider.Value = 50;

            BuildHeatmap();
            RefreshStats();
            UpdateStreakLabel();

            RecentLogs.ItemsSource = null;
            RecentLogs.ItemsSource = SymptomsDataStore.Symptoms;

            await DisplayAlert("Saved", "Symptom logged.", "OK");
        }

        private string ExtractLocationFromNotes(string notes)
        {
            if (string.IsNullOrWhiteSpace(notes)) return string.Empty;

            var idx = notes.IndexOf("Location:", StringComparison.OrdinalIgnoreCase);
            if (idx >= 0)
            {
                return notes.Substring(idx + "Location:".Length).Trim();
            }
            return string.Empty;
        }

        private void BuildHeatmap()
        {
            HeatmapGrid.Children.Clear();
            HeatmapGrid.RowDefinitions.Clear();

            int cols = 7;
            int rows = 4;

            for (int r = 0; r < rows; r++)
                HeatmapGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var today = DateTime.Today;

            for (int i = 0; i < 28; i++)
            {
                var date = today.AddDays(i - 27); // oldest -> newest
                var dayItems = SymptomsDataStore.Symptoms.Where(x => x.Timestamp.Date == date.Date).ToList();
                double avg = dayItems.Any() ? dayItems.Average(x => x.Severity) : 0;

                Color tileColor = Color.FromRgb(18, 28, 36); // base
                if (avg > 0)
                {
   
                    int rcol = (int)(30 + (avg / 100.0) * 200);
                    int gcol = (int)(120 - (avg / 100.0) * 80);
                    int bcol = (int)(150 - (avg / 100.0) * 80);
                    rcol = Math.Clamp(rcol, 0, 255);
                    gcol = Math.Clamp(gcol, 0, 255);
                    bcol = Math.Clamp(bcol, 0, 255);
                    tileColor = Color.FromRgb(rcol, gcol, bcol);
                }

                var dayBox = new Frame
                {
                    BackgroundColor = tileColor,
                    CornerRadius = 6,
                    HeightRequest = 36,
                    WidthRequest = 36,
                    Padding = 0,
                    HasShadow = false,
                    Content = new Label
                    {
                        Text = date.Day.ToString(),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        FontSize = 10,
                        TextColor = avg > 60 ? Colors.White : Colors.WhiteSmoke,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    }
                };

                int col = i % cols;
                int row = i / cols;
                
            }
        }

        private void RefreshStats()
        {
            TopSymptomLabel.Text = SymptomsDataStore.MostCommonSymptomLastDays(28);
            var avg7 = SymptomsDataStore.AverageSeverityLastDays(7);
            AvgSeverityLabel.Text = $"{Math.Round(avg7)}";
        }

        private void UpdateStreakLabel()
        {
            var daysLogged = Enumerable.Range(0, 7)
                .Count(i => SymptomsDataStore.LastDays(7).Any(s => s.Timestamp.Date == DateTime.Today.AddDays(-i).Date));
            StreakLabel.Text = $"{daysLogged}d";
        }

        private async void ViewDetails_Clicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is SymptomModel s)
            {
                await DisplayAlert($"{s.SymptomName} details",
                    $"When: {s.Timestamp:dd MMM yyyy HH:mm}\nSeverity: {s.Severity}\nType: {s.SymptomType}\nLocation: {s.Location}\nNotes: {s.Notes}",
                    "OK");
            }
        }

        private async void Voice_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Voice", "Voice input is a planned feature.", "OK");
        }
    }
}
