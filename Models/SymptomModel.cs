using System;

namespace PillPilot.Models
{
    public class SymptomModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SymptomName { get; set; } = string.Empty;    
        public int Severity { get; set; }                         
        public string SymptomType { get; set; } = string.Empty;    
        public string Notes { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Location { get; set; } = string.Empty;       
    }
}
