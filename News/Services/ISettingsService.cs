namespace News.Services;

public interface ISettingsService
{
    double FontSize { get; set; }
    bool DarkMode { get; set; }
    bool NotificationsEnabled { get; set; }
    bool AutoRefresh { get; set; }
    int RefreshInterval { get; set; }

    event EventHandler<double> FontSizeChanged;
    event EventHandler<bool> DarkModeChanged;
    event EventHandler<bool> AutoRefreshChanged;

    void LoadSettings();
    void SaveSettings();
    void ResetToDefaults();
}