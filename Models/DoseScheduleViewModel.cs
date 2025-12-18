using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PillPilot.ViewModels
{
    public class DoseScheduleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // Next Dose
        public string NextDoseText => NextDose?.Name ?? "No upcoming doses";
        public string NextDoseTime => NextDose?.Time ?? "--:--";

        private DoseItem _nextDose;
        public DoseItem NextDose
        {
            get => _nextDose;
            set { _nextDose = value; OnPropertyChanged(); OnPropertyChanged(nameof(NextDoseText)); OnPropertyChanged(nameof(NextDoseTime)); }
        }

        // Daily Progress
        public double DailyProgress
        {
            get
            {
                if (TodayDoses.Count == 0) return 0;
                double taken = 0;
                foreach (var dose in TodayDoses)
                    if (dose.Status == "Taken") taken++;
                return taken / TodayDoses.Count;
            }
        }

        public string DailyProgressText
        {
            get
            {
                if (TodayDoses.Count == 0)
                    return "0/0 taken";

                int takenCount = 0;
                foreach (var dose in TodayDoses)
                    if (dose.Status == "Taken")
                        takenCount++;

                return $"{takenCount}/{TodayDoses.Count} taken";
            }
        }

        // Dose List
        public ObservableCollection<DoseItem> TodayDoses { get; set; }

        // Commands
        public ICommand TakeDoseCommand { get; }
        public ICommand SnoozeDoseCommand { get; }
        public ICommand SkipDoseCommand { get; }

        public DoseScheduleViewModel()
        {
            // Sample data
            TodayDoses = new ObservableCollection<DoseItem>
            {
                new DoseItem { Name = "Metformin 500mg", Time = "08:00 AM", Status = "Upcoming" },
                new DoseItem { Name = "Atorvastatin 20mg", Time = "12:00 PM", Status = "Upcoming" },
                new DoseItem { Name = "Lisinopril 10mg", Time = "04:00 PM", Status = "Upcoming" }
            };

            UpdateNextDose();

            // Commands
            TakeDoseCommand = new Command<DoseItem>(TakeDose);
            SnoozeDoseCommand = new Command<DoseItem>(SnoozeDose);
            SkipDoseCommand = new Command<DoseItem>(SkipDose);
        }

        private void TakeDose(DoseItem dose)
        {
            dose.Status = "Taken";
            OnPropertyChanged(nameof(TodayDoses));
            OnPropertyChanged(nameof(DailyProgress));
            OnPropertyChanged(nameof(DailyProgressText));
            UpdateNextDose();
        }

        private void SnoozeDose(DoseItem dose)
        {
            dose.Status = "Snoozed";
            OnPropertyChanged(nameof(TodayDoses));
            UpdateNextDose();
        }

        private void SkipDose(DoseItem dose)
        {
            dose.Status = "Skipped";
            OnPropertyChanged(nameof(TodayDoses));
            UpdateNextDose();
        }

        private void UpdateNextDose()
        {
            foreach (var dose in TodayDoses)
            {
                if (dose.Status == "Upcoming" || dose.Status == "Snoozed")
                {
                    NextDose = dose;
                    return;
                }
            }
            NextDose = null;
        }
    }

    public class DoseItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        private string _time;
        public string Time { get => _time; set { _time = value; OnPropertyChanged(); } }

        private string _status = "Upcoming"; // Upcoming, Taken, Snoozed, Skipped
        public string Status { get => _status; set { _status = value; OnPropertyChanged(); } }
    }
}
