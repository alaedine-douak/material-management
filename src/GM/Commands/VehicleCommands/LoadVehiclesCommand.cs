using GM.Stores;
using GM.ViewModels.Vehicles;
using System.Windows;

namespace GM.Commands.VehicleCommands;

public class LoadVehiclesCommand(
    VehicleListViewModel vehicleListViewModel,
    VehicleStore vehicleStore) : AsyncCommandBase
{
    private readonly VehicleListViewModel _vehicleListViewModel = vehicleListViewModel;
    private readonly VehicleStore _vehicleStore = vehicleStore;

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _vehicleStore.LoadVehicles();

            _vehicleListViewModel.UpdateVehicles(_vehicleStore.Vehicles);
        }
        catch(Exception ex)
        {
            MessageBox.Show($"Loading Vehicles: {ex.Message}",
                "Loading Vehicles Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

    }
}
