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
                fonts.AddFont("OpenSans-Regular.ttf",
                    "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterAppTypes();
        return builder.Build();
    }

    public static MauiAppBuilder RegisterAppTypes(
        this
            MauiAppBuilder mauiAppBuilder)
    {
        // Services
        mauiAppBuilder.Services.AddSingleton<INewsService>(serviceProvider => new NewsService());

        // ViewModels
        mauiAppBuilder.Services.AddTransient<HeadlinesViewModel>();

        // Views
        mauiAppBuilder.Services.AddTransient<AboutView>();
        mauiAppBuilder.Services.AddTransient<ArticleView>();
        mauiAppBuilder.Services.AddTransient<HeadlinesView>();
        return mauiAppBuilder;
    }
}