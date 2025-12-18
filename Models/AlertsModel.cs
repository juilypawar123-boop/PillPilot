namespace PillPilot.Models
{
    public class AlertsModel
    {
        public string MedicationName { get; set; }
        public string Purpose { get; set; }
        public string Instructions { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan AlertTime { get; set; }
    }
}
