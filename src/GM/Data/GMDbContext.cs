using GM.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GM.Data;

public class GMDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<DocumentInfo> DocumentInfos => Set<DocumentInfo>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    public GMDbContext(DbContextOptions options) : base(options)
    {
        
    }
}
