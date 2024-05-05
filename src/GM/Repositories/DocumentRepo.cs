using GM.Data;
using Microsoft.EntityFrameworkCore;

namespace GM.Repositories;

public class DocumentRepo(IGMDbContextFactory dbContextFactory) : IDocumentRepo
{
    private readonly IGMDbContextFactory _dbContextFactory = dbContextFactory;

    public async Task<Data.Entities.Document> GetDocument(string documentName)
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            return await dbContext.Documents
                .FirstOrDefaultAsync(x => x.Name.ToLower() == documentName.ToLower()) ?? new Data.Entities.Document();
        }
    }

    public async Task<IEnumerable<Models.Document>> GetDocumentNames()
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            return await dbContext.Documents
                .Select(doc => new Models.Document(doc.Name))
                .ToListAsync();
        }
    }

    public async Task InsertDocumentNameAsync(int userId, Models.Document document)
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            var newDocument = new Data.Entities.Document { Name = document.Name, UserId = userId };

            dbContext.Documents.Add(newDocument);
            await dbContext.SaveChangesAsync();
        }
    }


}
