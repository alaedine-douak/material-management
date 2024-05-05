using GM.Repositories;
using GM.ViewModels.Vehicles;
using System.Windows;

namespace GM.Commands.VehicleCommands;

public class LoadVehiclesCommand(
    IVehicleRepo vehicleRepo,
    VehicleListViewModel vehicleListViewModel) : AsyncCommandBase
{
    private readonly VehicleListViewModel _vehicleListViewModel = vehicleListViewModel;
    private readonly IVehicleRepo _vehicleRepo = vehicleRepo;

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            var vehicles = await _vehicleRepo.GetAllVehiclesAsync();

            _vehicleListViewModel.UpdateVehicles(vehicles);
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
