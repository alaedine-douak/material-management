using GM.Data;
using Microsoft.EntityFrameworkCore;

namespace GM.Repositories;

public class DocumentRepo(IGMDbContextFactory dbContextFactory) : IDocumentRepo
{
    private readonly IGMDbContextFactory _dbContextFactory = dbContextFactory;

    public async Task<IEnumerable<Models.Document>> GetAllDocuments()
    {
        using (GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            return await dbContext
                .Documents
                .Select(x => new Models.Document(x.Name, null))
                .ToListAsync();
        }
    }

    public async Task<Data.Entities.Document?> GetDocumentByName(string name)
    {
        using (GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            return await dbContext
                .Documents
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }

    public async Task InsertDocumentAsync(int userId, Models.Document document)
    {
        using (GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            var documentEntity = new Data.Entities.Document
            {
                UserId = userId,
                Name = document.Name,
                AlertDuration = (int)document.AlartDuration!
            };


            dbContext.Documents.Add(documentEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
