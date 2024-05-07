using GM.Data;
using GM.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GM.Repositories;

public class VehicleRepo(IGMDbContextFactory dbContextFactory) : IVehicleRepo
{
    private readonly IGMDbContextFactory _dbContextFactory = dbContextFactory;

    public async Task InsertVehicle(int userId, Models.Vehicle vehicle)
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            var newVehicle = new Vehicle
            {
                UserId = userId,
                Code = vehicle.Code,
                Designation = vehicle.Designation,
                Brand = vehicle.Brand,
                PlateNumber = vehicle.PlateNumber,
            };

            dbContext.Vehicles.Add(newVehicle);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Models.Vehicle>> GetAllVehiclesAsync()
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            return await dbContext
                .Vehicles
                .Select(v => new Models.Vehicle(
                    v.Code,
                    v.Designation,
                    v.Brand,
                    v.PlateNumber))
                .ToListAsync();
        }
    }

    //public async Task<IQueryable<Models.Vehicle>> GetVehicles()
    //{
    //    using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
    //    {
    //        return await dbContext.Vehicles.Select(x => new Models.Vehicle(
    //            x.Code,
    //            x.Designation,
    //            x.Brand,
    //            x.PlateNumber))
    //            .AsQueryable();
    //    }
    //}


    //private Models.Vehicle MapToVehicleModel(Vehicle vehicle)
    //    => new Models.Vehicle(
    //        vehicle.Code,
    //        vehicle.Designation, 
    //        vehicle.Brand, 
    //        vehicle.PlateNumber,
    //        vehicle.UserId);
}
