using GM.Data;
using GM.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GM.Repositories;

public class UserRepo(IGMDbContextFactory dbContextFactory) : IUserRepo
{
    private readonly IGMDbContextFactory _dbContextFactory = dbContextFactory;

    public async Task<User> GetUser(string username)
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            return await dbContext.Users
               .FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower())
               ?? new();
        }
    }
}
