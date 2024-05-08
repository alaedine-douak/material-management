using GM.Data;
using Microsoft.EntityFrameworkCore;

namespace GM.Repositories.VehicleRepos;

public class VehicleConflictValidator(IGMDbContextFactory dbContextFactory) : IVehicleConflictValidator
{
    private readonly IGMDbContextFactory _dbContextFactory = dbContextFactory;

    public async Task<Models.Vehicle?> GetConflictingVehicle(Models.Vehicle vehicle)
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            var existingVehicle = await dbContext
                .Vehicles
                .Where(v => v.Code == vehicle.Code)
                .FirstOrDefaultAsync();

            if (existingVehicle is null) return null;

            return new Models.Vehicle(
                existingVehicle.Code,
                existingVehicle.Designation,
                existingVehicle.Brand,
                existingVehicle.PlateNumber);

        }
    }
}
