using GM.Stores;
using GM.Services;
using GM.Commands;
using System.Windows.Input;
using GM.ViewModels.Vehicles;
using GM.ViewModels.Documents;

namespace GM.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

    public ICommand HomeNavigationCommand { get; }
    public ICommand DocumentNavigationCommand { get; }
    public ICommand VehicleNavigatorCommand { get; }

    public MainWindowViewModel(
        NavigationStore navigationStore,
        NavigationService<HomeViewModel> homeNavigationService,
        NavigationService<DocumentsViewModel> documentsListNavigationService,
        NavigationService<VehicleListViewModel> vehicleListNavigationService)
    {
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

        HomeNavigationCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
        DocumentNavigationCommand = new NavigateCommand<DocumentsViewModel>(documentsListNavigationService);
        VehicleNavigatorCommand = new NavigateCommand<VehicleListViewModel>(vehicleListNavigationService);
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}
