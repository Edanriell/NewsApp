﻿using System.Net.Http.Json;
using News.Models;

namespace News.Services;

public class NewsService : INewsService, IDisposable
{
    private const string UriBase = "https://newsapi.org/v2";

    private readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri(UriBase),
        DefaultRequestHeaders = { { "user-agent", "maui-projects-news/1.0" } }
    };

    private bool disposedValue;

    private static string Headlines => $"{UriBase}/top-headlines?country=us&apiKey={Settings.NewsApiKey}";

    private static string Local => $"{UriBase}/everything?q=local&apiKey={Settings.NewsApiKey}";

    private static string Global => $"{UriBase}/everything?q=global&apiKey={Settings.NewsApiKey}";

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<NewsResult> GetNews(NewsScope scope)
    {
        NewsResult result;
        var url = GetUrl(scope);
        try { result = await httpClient.GetFromJsonAsync<NewsResult>(url); }
        catch (Exception ex)
        {
            result = new NewsResult
            {
                Articles = new List<Article>
                    { new() { Title = $"HTTP Get failed: {ex.Message}", PublishedAt = DateTime.Now } }
            };
        }

        return result;
    }

    private string GetUrl(NewsScope scope)
    {
        return scope switch
        {
            NewsScope.Headlines => Headlines,
            NewsScope.Global => Global,
            NewsScope.Local => Local,
            _ => throw new Exception("Undefined scope")
        };
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing) httpClient.Dispose();
            disposedValue = true;
        }
    }
}