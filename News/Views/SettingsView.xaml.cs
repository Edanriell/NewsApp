namespace News.Views;

public partial class SettingsView : ContentPage
{
    public SettingsView()
    {
        InitializeComponent();
        LoadSettings();
    }

    private void LoadSettings()
    {
        // Load saved settings from preferences
        DarkModeSwitch.IsToggled = Preferences.Get("dark_mode", false);
        FontSizeStepper.Value = Preferences.Get("font_size", 16.0);
        NotificationsSwitch.IsToggled = Preferences.Get("notifications", true);
        AutoRefreshSwitch.IsToggled = Preferences.Get("auto_refresh", true);
        RefreshIntervalPicker.SelectedIndex = Preferences.Get("refresh_interval", 1);
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e)
    {
        Preferences.Set("dark_mode", e.Value);

        // Apply theme change
        if (e.Value)
            Application.Current!.UserAppTheme = AppTheme.Dark;
        else
            Application.Current!.UserAppTheme = AppTheme.Light;

        DisplayAlert("Theme Changed", "The theme has been updated.", "OK");
    }

    private void OnResetDefaultsClicked(object sender, EventArgs e)
    {
        // Reset all settings to default
        DarkModeSwitch.IsToggled = false;
        FontSizeStepper.Value = 16;
        NotificationsSwitch.IsToggled = true;
        AutoRefreshSwitch.IsToggled = true;
        RefreshIntervalPicker.SelectedIndex = 1;

        // Clear preferences
        Preferences.Clear();

        DisplayAlert("Reset Complete", "All settings have been reset to defaults.", "OK");
    }

    private async void OnSendFeedbackClicked(object sender, EventArgs e)
    {
        try
        {
            var email = new EmailMessage
            {
                Subject = "News App Feedback",
                Body = "Please share your feedback about the News App:",
                To = new List<string> { "support@newsapp.com" }
            };

            await Email.ComposeAsync(email);
        }
        catch (Exception ex) { await DisplayAlert("Error", "Unable to open email client.", "OK"); }
    }
}