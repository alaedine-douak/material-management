using GM.Commands;
using GM.Services;
using GM.Stores;
using GM.ViewModels.Documents;
using System.Windows.Input;

namespace GM.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    public ICommand HomeNavigationCommand { get; }
    public ICommand DocumentNavigationCommand { get; }

    public MainWindowViewModel(
        NavigationStore navigationStore,
        NavigationService<HomeViewModel> homeNavigationService,
        NavigationService<DocumentsListViewModel> documentsListNavigationService)
    {
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

        HomeNavigationCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
        DocumentNavigationCommand = new NavigateCommand<DocumentsListViewModel>(documentsListNavigationService);
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}
