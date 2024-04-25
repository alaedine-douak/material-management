using GM.Data;
using GM.Data.Entities;

namespace GM.Services.UserRepositories;

public class UserRepository : IUserRepository
{
    private readonly GMDbContext _dbContext;
    public UserRepository(GMDbContext dbContext) => _dbContext = dbContext;

    public User GetUsername(string username)
    {
        return _dbContext.Users.First(x => x.Username == username);
    }
}
