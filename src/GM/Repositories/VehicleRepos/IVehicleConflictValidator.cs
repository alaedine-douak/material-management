namespace GM.Repositories.VehicleRepos;

public interface IVehicleConflictValidator
{
    Task<Models.Vehicle?> GetConflictingVehicle(Models.Vehicle vehicle);
}
