using GM.Commands;
using GM.Commands.VehicleCommands;
using GM.Repositories;
using GM.Services;
using GM.Stores;
using GM.ViewModels.Documents;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace GM.ViewModels.Vehicles;

public class VehicleListViewModel : ViewModelBase
{
    private readonly ObservableCollection<Models.Vehicle> _vehicleObs;

    public ICollectionView VehiclesCollectionView { get; }

    private string _vehicleFilter = string.Empty;
    public string VehicleFilter
    {
        get => _vehicleFilter;
        set
        {
            _vehicleFilter = value;
            OnPropertyChanged(nameof(VehicleFilter));
            VehiclesCollectionView.Refresh();
        }
    }

    public ICommand LoadVehiclesCommad { get; }
    public ICommand InsertVehicleNavigatorCommand { get; }

    public VehicleListViewModel(
        VehicleStore vehicleStore,
        InsertDocumentInfoViewModel insertDocumentInfoViewModel,
        NavigationService<InsertVehicleViewModel> insertVehicleNavigationService)
    {
        _vehicleObs = new ObservableCollection<Models.Vehicle>();

        VehiclesCollectionView = CollectionViewSource.GetDefaultView(_vehicleObs);
        VehiclesCollectionView.Filter = FilterVehivle;



        LoadVehiclesCommad = new LoadVehiclesCommand(this, vehicleStore);
        InsertVehicleNavigatorCommand = new NavigateCommand<InsertVehicleViewModel>(insertVehicleNavigationService);

        LoadVehiclesCommad.Execute(null);
    }

    public void UpdateVehicles(IEnumerable<Models.Vehicle> vehicles)
    {
        _vehicleObs.Clear();

        foreach (Models.Vehicle vehicle in vehicles)
        {
            _vehicleObs.Add(vehicle);
        }
    }

    private bool FilterVehivle(object obj)
    {
        if (obj is Models.Vehicle  mVehicle)
        {
            return mVehicle.Code.Contains(
                VehicleFilter,
                StringComparison.InvariantCultureIgnoreCase) ||
                mVehicle.PlateNumber.Contains(
                    VehicleFilter,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        return false;
    }
}
