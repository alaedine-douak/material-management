namespace GM.Repositories;

public interface IVehicleRepo
{
    Task<int> InsertVehicle(int userId, Models.Vehicle vehicle);
    Task<IEnumerable<Models.Vehicle>> GetAllVehiclesAsync();
}
