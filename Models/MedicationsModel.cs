namespace PillPilot.Models
{
    public class MedicationsModel
    {
        public string MedicationName { get; set; }
        public string Purpose { get; set; }
        public string DosageAmount { get; set; }
        public string Instructions { get; set; }
        public string LowStockWarning { get; set; }
        public string RepeatPrescriptionNeeded { get; set; }
        public string PrescribingDoctorName { get; set; }
        public string PharmacyName { get; set; }
        public string MedicationDescription { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AlarmTime { get; set; }
    }
}
