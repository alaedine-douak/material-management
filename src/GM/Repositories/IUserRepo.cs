using GM.Data.Entities;

namespace GM.Repositories;

public interface IUserRepo
{
    Task<User?> GetUser(string username);
}
