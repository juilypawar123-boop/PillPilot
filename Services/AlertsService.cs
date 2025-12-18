using System.Collections.ObjectModel;
using PillPilot.Models;

namespace PillPilot.Services
{
    public class AlertsService
    {
        private static AlertsService _instance;
        public static AlertsService Instance => _instance ??= new AlertsService();

        public ObservableCollection<AlertsModel> Alerts { get; private set; }

        private AlertsService()
        {
            Alerts = new ObservableCollection<AlertsModel>();
        }

        public void AddAlert(AlertsModel alert)
        {
            Alerts.Add(alert);
        }
    }
}
