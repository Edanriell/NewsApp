using News.Services;

namespace News.Views;

public partial class SettingsView : ContentPage
{
    private readonly ISettingsService _settingsService;

    public SettingsView()
    {
        InitializeComponent();
        _settingsService = App.SettingsService;

        // Subscribe to font size changes
        _settingsService.FontSizeChanged += OnFontSizeChangedFromService;

        LoadSettings();
        UpdateFontSizes();
    }

    protected override void OnDisappearing()
    {
        // Unsubscribe when leaving the page
        _settingsService.FontSizeChanged -= OnFontSizeChangedFromService;
        base.OnDisappearing();
    }

    private void OnFontSizeChangedFromService(object sender, double fontSize) { UpdateFontSizes(); }

    private void UpdateFontSizes()
    {
        var fontSize = _settingsService.FontSize;

        // Update all labels with the new font size
        DarkModeLabel.FontSize = fontSize;
        FontSizeTextLabel.FontSize = fontSize;
        FontSizeLabel.FontSize = fontSize;
        PreviewLabel.FontSize = fontSize;
        NotificationsLabel.FontSize = fontSize;
        AutoRefreshLabel.FontSize = fontSize;
        RefreshIntervalLabel.FontSize = fontSize;
    }

    private void LoadSettings()
    {
        DarkModeSwitch.IsToggled = _settingsService.DarkMode;
        FontSizeSlider.Value = _settingsService.FontSize;
        FontSizeLabel.Text = _settingsService.FontSize.ToString("F0");
        NotificationsSwitch.IsToggled = _settingsService.NotificationsEnabled;
        AutoRefreshSwitch.IsToggled = _settingsService.AutoRefresh;
        RefreshIntervalPicker.SelectedIndex = GetIntervalIndex(_settingsService.RefreshInterval);
    }

    private int GetIntervalIndex(int minutes)
    {
        return minutes switch
        {
            1 => 0,
            5 => 1,
            10 => 2,
            30 => 3,
            60 => 4,
            _ => 1 // Default to 5 minutes
        };
    }

    private int GetIntervalFromIndex(int index)
    {
        return index switch
        {
            0 => 1,
            1 => 5,
            2 => 10,
            3 => 30,
            4 => 60,
            _ => 5 // Default to 5 minutes
        };
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e) { _settingsService.DarkMode = e.Value; }

    private void OnFontSizeChanged(object sender, ValueChangedEventArgs e)
    {
        var fontSize = Math.Round(e.NewValue);
        FontSizeLabel.Text = fontSize.ToString();
        _settingsService.FontSize = fontSize;
        UpdateFontSizes();
    }

    private void OnNotificationsToggled(object sender, ToggledEventArgs e)
    {
        _settingsService.NotificationsEnabled = e.Value;
    }

    private void OnAutoRefreshToggled(object sender, ToggledEventArgs e) { _settingsService.AutoRefresh = e.Value; }

    private void OnRefreshIntervalChanged(object sender, EventArgs e)
    {
        if (RefreshIntervalPicker.SelectedIndex >= 0)
            _settingsService.RefreshInterval = GetIntervalFromIndex(RefreshIntervalPicker.SelectedIndex);
    }

    private async void OnResetDefaultsClicked(object sender, EventArgs e)
    {
        var confirm = await DisplayAlert("Reset Settings",
            "Are you sure you want to reset all settings to their default values?",
            "Yes", "No");

        if (confirm)
        {
            _settingsService.ResetToDefaults();
            LoadSettings();
            UpdateFontSizes();
            await DisplayAlert("Reset Complete", "All settings have been reset to defaults.", "OK");
        }
    }
}