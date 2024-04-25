using GM.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GM.Data;

public class GMDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<DocumentDetail> DocumentDetails => Set<DocumentDetail>(); 

    public GMDbContext(DbContextOptions<GMDbContext> options) : base(options)
    {
        
    }
}
