using Microsoft.EntityFrameworkCore;

namespace GM.Data;

public class GMDbContextFactory : IGMDbContextFactory
{
    private readonly string? _connectionString;

    public GMDbContextFactory(string? connectionString) => _connectionString = connectionString;

    public GMDbContext CreateDbContext()
    {
        DbContextOptions options = new DbContextOptionsBuilder()
            .UseNpgsql(_connectionString)
            .Options;

        return new GMDbContext(options);
    }
}