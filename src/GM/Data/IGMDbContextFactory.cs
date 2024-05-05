namespace GM.Data;

public interface IGMDbContextFactory
{
    GMDbContext CreateDbContext();
}
