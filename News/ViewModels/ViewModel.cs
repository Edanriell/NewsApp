using CommunityToolkit.Mvvm.ComponentModel;

namespace News.ViewModels;

[ObservableObject]
public abstract partial class ViewModel
{
    internal ViewModel(INavigate navigation) { Navigation = navigation; }

    public INavigate Navigation { get; init; }
}