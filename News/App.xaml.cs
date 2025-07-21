using News.Services;

namespace News;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        SettingsService = new SettingsService();
        SettingsService.LoadSettings();

        MainPage = new AppShell();
    }

    public static ISettingsService SettingsService { get; private set; }
}