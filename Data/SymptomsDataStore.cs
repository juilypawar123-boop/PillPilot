using System;
using System.Collections.ObjectModel;
using System.Linq;
using PillPilot.Models;

namespace PillPilot.Data
{
    public static class SymptomsDataStore
    {
        public static ObservableCollection<SymptomModel> Symptoms { get; } = new();

        public static void Add(SymptomModel s) => Symptoms.Insert(0, s); 

        public static void Remove(SymptomModel s) => Symptoms.Remove(s);

        public static IEnumerable<SymptomModel> LastDays(int days)
        {
            var since = DateTime.Today.AddDays(-days + 1);
            return Symptoms.Where(x => x.Timestamp.Date >= since.Date);
        }

        public static double AverageSeverityLastDays(int days)
        {
            var items = LastDays(days).ToList();
            return items.Any() ? items.Average(x => x.Severity) : 0.0;
        }

        public static string MostCommonSymptomLastDays(int days)
        {
            var items = LastDays(days).GroupBy(x => x.SymptomName)
                .OrderByDescending(g => g.Count()).FirstOrDefault();
            return items?.Key ?? "—";
        }
    }
}
