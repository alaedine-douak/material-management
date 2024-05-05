using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GM.Data;

public class GMDesignTimeDbContextFactory : IDesignTimeDbContextFactory<GMDbContext>
{
    public GMDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder()
            .UseNpgsql("Host=localhost;Port=5432;Username=douak;Password=6772AlA@;Database=gestion_materiel");

        return new GMDbContext(optionsBuilder.Options);
    }
}
