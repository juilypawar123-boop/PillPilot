using System.Collections.ObjectModel;
using PillPilot.Models;

namespace PillPilot.Data
{
    public static class AlertsDataStore
    {
        public static ObservableCollection<AlertsModel> Alerts { get; set; }
            = new ObservableCollection<AlertsModel>();
    }
}