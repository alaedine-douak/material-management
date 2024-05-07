using GM.Repositories;
using System.Windows.Controls;

namespace GM.Stores;

public class VehicleStore
{
    private readonly IVehicleRepo _vehicleRepo;
    private readonly List<Models.Vehicle> _vehicles;

    private Lazy<Task> _initializeLazy;

    public IEnumerable<Models.Vehicle> Vehicles => _vehicles;

    public VehicleStore(IVehicleRepo vehicleRepo)
    {
        _vehicleRepo = vehicleRepo;
        _vehicles = new List<Models.Vehicle>();

        _initializeLazy = new Lazy<Task>(Initialize);
    }

    public async Task LoadVehicles()
    {
        try
        {
            await _initializeLazy.Value;
        }
        catch(Exception)
        {
            _initializeLazy = new Lazy<Task>(Initialize);
            throw;
        }
    }

    private async Task Initialize()
    {
        IEnumerable<Models.Vehicle> vehicles = await _vehicleRepo.GetAllVehiclesAsync();

        _vehicles.Clear();
        _vehicles.AddRange(vehicles);
    }
}
