using Microsoft.Extensions.Logging;
using News.Services;
using News.ViewModels;
using News.Views;

namespace News;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("FontAwesome.otf", "FontAwesome");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterAppTypes();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterAppTypes(this MauiAppBuilder mauiAppBuilder)
    {
        // Services
        mauiAppBuilder.Services.AddSingleton<INewsService>(serviceProvider => new NewsService());
        mauiAppBuilder.Services.AddSingleton<INavigate>(serviceProvider => new Navigator());
        mauiAppBuilder.Services.AddSingleton<ISettingsService>(serviceProvider => new SettingsService());

        // ViewModels
        mauiAppBuilder.Services.AddTransient<HeadlinesViewModel>();

        //Views
        mauiAppBuilder.Services.AddTransient<AboutView>();
        mauiAppBuilder.Services.AddTransient<ArticleView>();
        mauiAppBuilder.Services.AddTransient<HeadlinesView>();
        mauiAppBuilder.Services.AddTransient<SettingsView>();

        return mauiAppBuilder;
    }
}