using System.Diagnostics;
using System.Web;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using News.Models;
using News.Services;

namespace News.ViewModels;

public partial class HeadlinesViewModel : ViewModel
{
    private readonly INewsService newsService;

    [ObservableProperty] private NewsResult currentNews;
    private NewsScope currentScope = NewsScope.Headlines;
    [ObservableProperty] private string headerTitle = "Latest News";
    [ObservableProperty] private bool isInitialLoading;
    [ObservableProperty] private bool isRefreshing;

    public HeadlinesViewModel(INewsService newsService, INavigate navigation) : base(navigation)
    {
        this.newsService = newsService;
    }

    public async Task Initialize(string scope)
    {
        await Initialize(scope.ToLower() switch
        {
            "local" => NewsScope.Local,
            "global" => NewsScope.Global,
            "headlines" => NewsScope.Headlines,
            _ => NewsScope.Headlines
        });
    }

    public async Task Initialize(NewsScope scope)
    {
        currentScope = scope;
        UpdateHeaderTitle(scope);
        IsInitialLoading = true;

        try { CurrentNews = await newsService.GetNews(scope); }
        catch (Exception ex) { Debug.WriteLine($"Error loading news: {ex.Message}"); }
        finally { IsInitialLoading = false; }
    }

    private void UpdateHeaderTitle(NewsScope scope)
    {
        HeaderTitle = scope switch
        {
            NewsScope.Headlines => "Top Headlines",
            NewsScope.Local => "Local News",
            NewsScope.Global => "Global News",
            _ => "Latest News"
        };
    }

    [RelayCommand]
    public async Task ItemSelected(object selectedItem)
    {
        var selectedArticle = selectedItem as Article;
        var url = HttpUtility.UrlEncode(selectedArticle.Url);
        await Navigation.NavigateTo($"articleview?url={url}");
    }

    [RelayCommand]
    public async Task Refresh()
    {
        IsRefreshing = true;

        try { CurrentNews = await newsService.GetNews(currentScope); }
        catch (Exception ex) { Debug.WriteLine($"Error refreshing news: {ex.Message}"); }
        finally { IsRefreshing = false; }
    }
}