using System.Collections.ObjectModel;
using PillPilot.Models;

namespace PillPilot.Data
{
    public static class MedicationsDataStore
    {
        public static ObservableCollection<MedicationsModel> medications { get; set; }
            = new ObservableCollection<MedicationsModel>();
    }
}