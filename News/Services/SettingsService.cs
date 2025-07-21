namespace News.Services;

public class SettingsService : ISettingsService
{
    private bool _autoRefresh = true;
    private bool _darkMode;

    private double _fontSize = 16;
    private bool _notificationsEnabled = true;
    private int _refreshInterval = 5;

    public event EventHandler<double> FontSizeChanged;
    public event EventHandler<bool> DarkModeChanged;
    public event EventHandler<bool> AutoRefreshChanged;

    public double FontSize
    {
        get => _fontSize;
        set
        {
            if (_fontSize != value)
            {
                _fontSize = value;
                Preferences.Set("font_size", value);
                FontSizeChanged?.Invoke(this, value);
            }
        }
    }

    public bool DarkMode
    {
        get => _darkMode;
        set
        {
            if (_darkMode != value)
            {
                _darkMode = value;
                Preferences.Set("dark_mode", value);
                Application.Current.UserAppTheme = value ? AppTheme.Dark : AppTheme.Light;
                DarkModeChanged?.Invoke(this, value);
            }
        }
    }

    public bool NotificationsEnabled
    {
        get => _notificationsEnabled;
        set
        {
            _notificationsEnabled = value;
            Preferences.Set("notifications", value);
        }
    }

    public bool AutoRefresh
    {
        get => _autoRefresh;
        set
        {
            if (_autoRefresh != value)
            {
                _autoRefresh = value;
                Preferences.Set("auto_refresh", value);
                AutoRefreshChanged?.Invoke(this, value);
            }
        }
    }

    public int RefreshInterval
    {
        get => _refreshInterval;
        set
        {
            _refreshInterval = value;
            Preferences.Set("refresh_interval", value);
        }
    }

    public void LoadSettings()
    {
        _fontSize = Preferences.Get("font_size", 16.0);
        _darkMode = Preferences.Get("dark_mode", false);
        _notificationsEnabled = Preferences.Get("notifications", true);
        _autoRefresh = Preferences.Get("auto_refresh", true);
        _refreshInterval = Preferences.Get("refresh_interval", 5);

        // Apply dark mode on startup
        Application.Current.UserAppTheme = _darkMode ? AppTheme.Dark : AppTheme.Light;
    }

    public void SaveSettings()
    {
        Preferences.Set("font_size", _fontSize);
        Preferences.Set("dark_mode", _darkMode);
        Preferences.Set("notifications", _notificationsEnabled);
        Preferences.Set("auto_refresh", _autoRefresh);
        Preferences.Set("refresh_interval", _refreshInterval);
    }

    public void ResetToDefaults()
    {
        FontSize = 16;
        DarkMode = false;
        NotificationsEnabled = true;
        AutoRefresh = true;
        RefreshInterval = 5;
        Preferences.Clear();
    }
}