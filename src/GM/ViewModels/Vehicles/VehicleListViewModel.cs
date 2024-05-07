using GM.Commands;
using GM.Commands.VehicleCommands;
using GM.Repositories;
using GM.Services;
using GM.Stores;
using GM.ViewModels.Documents;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GM.ViewModels.Vehicles;

public class VehicleListViewModel : ViewModelBase
{
    private readonly ObservableCollection<Models.Vehicle> _vehicleObs;


    public IEnumerable<Models.Vehicle> Vehicles => _vehicleObs;

    public ICommand LoadVehiclesCommad { get; }
    public ICommand InsertVehicleNavigatorCommand { get; }

    public VehicleListViewModel(
        VehicleStore vehicleStore,
        InsertDocumentInfoViewModel insertDocumentInfoViewModel,
        NavigationService<InsertVehicleViewModel> insertVehicleNavigationService)
    {
        _vehicleObs = new ObservableCollection<Models.Vehicle>();



        LoadVehiclesCommad = new LoadVehiclesCommand(this, vehicleStore);
        InsertVehicleNavigatorCommand = new NavigateCommand<InsertVehicleViewModel>(insertVehicleNavigationService);

        //LoadVehiclesCommad.Execute(null);
    }


    public void UpdateVehicles(IEnumerable<Models.Vehicle> vehicles)
    {
        _vehicleObs.Clear();

        foreach (Models.Vehicle vehicle in vehicles)
        {
            _vehicleObs.Add(vehicle);
        }
    }
}
