using PillPilot.ViewModels;

namespace PillPilot.Pages
{
    public partial class AnalyticsPage : ContentPage
    {
        public AnalyticsPage()
        {
            InitializeComponent();
            BindingContext = new AnalyticsViewModel();
        }
    }
}
