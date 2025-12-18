using System.Collections.ObjectModel;

namespace PillPilot.ViewModels
{
    public class AnalyticsViewModel
    {
        public string AdherenceRate { get; set; } = "86%";
        public int MissedDoses { get; set; } = 2;
        public string AverageDelay { get; set; } = "+12 min";

        public ObservableCollection<MedicationScore> MedicationScores { get; set; }
        public ObservableCollection<RefillItem> RefillItems { get; set; }

        public string AlertSummary { get; set; } =
            "2 side-effect logs • 1 interaction warning in the last 30 days";

        public AnalyticsViewModel()
        {
            MedicationScores = new ObservableCollection<MedicationScore>
            {
                new MedicationScore { Name = "Atorvastatin", Score = "92%" },
                new MedicationScore { Name = "Metformin", Score = "88%" },
                new MedicationScore { Name = "Lisinopril", Score = "79%" }
            };

            RefillItems = new ObservableCollection<RefillItem>
            {
                new RefillItem { Name = "Metformin", DaysLeft = "5 days left" },
                new RefillItem { Name = "Lisinopril", DaysLeft = "12 days left" }
            };
        }
    }

    public class MedicationScore
    {
        public string Name { get; set; }
        public string Score { get; set; }
    }

    public class RefillItem
    {
        public string Name { get; set; }
        public string DaysLeft { get; set; }
    }
}
