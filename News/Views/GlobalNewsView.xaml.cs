using News.Models;
using News.Services;
using News.ViewModels;

namespace News.Views;

public partial class GlobalNewsView : ContentPage
{
    private readonly HeadlinesViewModel viewModel;

    public GlobalNewsView(HeadlinesViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.Initialize(NewsScope.Global);
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Article selectedArticle)
        {
            ((CollectionView)sender).SelectedItem = null;

            if (viewModel.ItemSelectedCommand.CanExecute(selectedArticle))
                await viewModel.ItemSelectedCommand.ExecuteAsync(selectedArticle);
        }
    }
}