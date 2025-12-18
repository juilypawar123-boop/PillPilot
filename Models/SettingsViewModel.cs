using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PillPilot.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private bool _notificationsEnabled = true;
        public bool NotificationsEnabled
        {
            get => _notificationsEnabled;
            set { _notificationsEnabled = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> ReminderSounds { get; set; } =
            new ObservableCollection<string> { "Default", "Soft Chime", "Beep", "Alert Tone" };

        private string _selectedReminderSound = "Default";
        public string SelectedReminderSound
        {
            get => _selectedReminderSound;
            set { _selectedReminderSound = value; OnPropertyChanged(); }
        }

        private bool _dailySummaryEnabled = false;
        public bool DailySummaryEnabled
        {
            get => _dailySummaryEnabled;
            set { _dailySummaryEnabled = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> SnoozeOptions { get; set; } =
            new ObservableCollection<string> { "5 minutes", "10 minutes", "30 minutes" };

        private string _selectedSnoozeTime = "10 minutes";
        public string SelectedSnoozeTime
        {
            get => _selectedSnoozeTime;
            set { _selectedSnoozeTime = value; OnPropertyChanged(); }
        }

        private TimeSpan _summaryTime = new TimeSpan(9, 0, 0); // Default 9:00 AM
        public TimeSpan SummaryTime
        {
            get => _summaryTime;
            set { _summaryTime = value; OnPropertyChanged(); }
        }
    }
}
