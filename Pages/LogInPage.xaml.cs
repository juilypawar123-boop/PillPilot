using Microsoft.Maui.Controls;

namespace PillPilot.Pages
{
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = EmailEntry?.Text?.Trim() ?? string.Empty;
            string password = PasswordEntry?.Text ?? string.Empty;

            if (email == "admin@example.com" && password == "1234")
            {
                await DisplayAlert("Success", "Account created successfully.", "OK");
                await Navigation.PushAsync(new HealthOverviewPage());
            }
            else
            {
                await DisplayAlert("Login Failed","Incorrect email or password. Try again.", "OK");
            }
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private bool _isPasswordHidden = true;

        private void OnTogglePasswordClicked(object sender, EventArgs e)
        {
            _isPasswordHidden = !_isPasswordHidden;
            PasswordEntry.IsPassword = _isPasswordHidden;
            TogglePasswordButton.Source =
                _isPasswordHidden ? "eye_closed.png" : "eye_open.png";
        }
    }
}
