using GM.Data;
using GM.Models;
using Microsoft.EntityFrameworkCore;

namespace GM.Repositories;

public class DocumentConflictValidator(IGMDbContextFactory dbContextFactory) : IDocumentConflictValidator
{
    private readonly IGMDbContextFactory _dbContextFactory = dbContextFactory;

    public async Task<Document?> GetConflictingDocument(Document doc)
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            var document = await dbContext.Documents
                .Where(x => x.Name == doc.Name)
                .FirstOrDefaultAsync();

            if (document == null)
            {
                return null;
            }

            return new Models.Document(document.Name, null);
        }
    }
}
