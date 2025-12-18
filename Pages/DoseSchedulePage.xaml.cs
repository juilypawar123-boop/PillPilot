using PillPilot.ViewModels;

namespace PillPilot.Pages
{
    public partial class DoseSchedulePage : ContentPage
    {
        public DoseSchedulePage()
        {
            InitializeComponent();
            BindingContext = new DoseScheduleViewModel();
        }
    }
}
