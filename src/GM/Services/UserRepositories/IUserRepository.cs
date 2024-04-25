using GM.Data.Entities;

namespace GM.Services.UserRepositories;

public interface IUserRepository
{
    User GetUsername(string username);
}
