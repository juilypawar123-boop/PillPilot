using Microsoft.Maui.Controls;

namespace PillPilot.Pages
{
    public partial class SignUpPage : ContentPage
    {
        private bool _isPasswordHidden = true;
        private bool _isConfirmPasswordHidden = true;

        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            string name = NameEntry?.Text?.Trim() ?? string.Empty;
            string email = EmailEntry?.Text?.Trim() ?? string.Empty;
            string password = PasswordEntry?.Text?.Trim() ?? string.Empty;
            string confirmPassword = ConfirmPasswordEntry?.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill all fields.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            await DisplayAlert("Success", "Account created successfully.", "OK");

            await Navigation.PushAsync(new HealthOverviewPage());
        }

        private void OnTogglePasswordClicked(object sender, EventArgs e)
        {
            _isPasswordHidden = !_isPasswordHidden;
            PasswordEntry.IsPassword = _isPasswordHidden;
            TogglePasswordButton.Source = _isPasswordHidden ? "eye_closed.png" : "eye_open.png";
        }

        private void OnToggleConfirmPasswordClicked(object sender, EventArgs e)
        {
            _isConfirmPasswordHidden = !_isConfirmPasswordHidden;
            ConfirmPasswordEntry.IsPassword = _isConfirmPasswordHidden;
            ToggleConfirmPasswordButton.Source = _isConfirmPasswordHidden ? "eye_closed.png" : "eye_open.png";
        }
    }
}
