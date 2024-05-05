namespace GM.Repositories;

public interface IVehicleRepo
{
    Task InsertVehicle(int userId, Models.Vehicle vehicle);
    Task<IEnumerable<Models.Vehicle>> GetAllVehiclesAsync();
}
