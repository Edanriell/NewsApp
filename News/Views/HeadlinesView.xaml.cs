using System.Diagnostics;
using News.Models;
using News.ViewModels;

namespace News.Views;

public partial class HeadlinesView : ContentPage
{
    private readonly HeadlinesViewModel viewModel;
    private string lastInitializedScope = "";

    public HeadlinesView(HeadlinesViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var currentScope = DetermineCurrentScope();

        Debug.WriteLine($"HeadlinesView OnAppearing - Determined Scope: {currentScope}");
        Debug.WriteLine($"HeadlinesView OnAppearing - Last Initialized Scope: {lastInitializedScope}");

        // Always reinitialize if scope changed OR if this is the first time
        if (lastInitializedScope != currentScope)
        {
            lastInitializedScope = currentScope;
            Debug.WriteLine($"Initializing ViewModel with scope: {currentScope}");
            await viewModel.Initialize(currentScope);
        }
    }

    private string DetermineCurrentScope()
    {
        // Get the current route from Shell
        var fullRoute = Shell.Current.CurrentState.Location.ToString();
        Debug.WriteLine($"Full route: {fullRoute}");

        // Check route segments
        var routeSegments = fullRoute.Split('/');
        foreach (var segment in routeSegments)
        {
            Debug.WriteLine($"Route segment: {segment}");

            switch (segment.ToLower())
            {
                case "headlines":
                    return "headlines";
                case "local":
                    return "local";
                case "global":
                    return "global";
            }
        }

        // Fallback: check current page properties
        var currentPage = Shell.Current.CurrentPage;
        var pageTitle = currentPage?.Title?.ToLower();
        Debug.WriteLine($"Page title: {pageTitle}");

        switch (pageTitle)
        {
            case "local":
                return "local";
            case "global":
                return "global";
            case "headlines":
            default:
                return "headlines";
        }
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Article selectedArticle)
        {
            // Clear selection
            ((CollectionView)sender).SelectedItem = null;

            // Execute the selection command
            if (viewModel.ItemSelectedCommand.CanExecute(selectedArticle))
                await viewModel.ItemSelectedCommand.ExecuteAsync(selectedArticle);
        }
    }
}