using GM.Data;
using GM.Models;
using Microsoft.EntityFrameworkCore;

namespace GM.Repositories;

public class DocumentInfoRepo(IGMDbContextFactory dbContextFactory) : IDocumentInfoRepo
{
    private readonly IGMDbContextFactory _dbContextFactory = dbContextFactory;

    public async Task<IEnumerable<DocumentInfo>> GetAllDocumentInfos()
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            return await dbContext
                .DocumentInfos
                .Include(x => x.Document)
                .Select(doc => new Models.DocumentInfo(
                    doc.Document.Name,
                    doc.DocumentNumber,
                    doc.IssuedDate,
                    doc.EndDate))
                .ToListAsync();

        }
    }


    //public async Task<IQueryable<Models.DocumentInfo>> GetAllDocumentInfos()
    //{
    //    using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
    //    {
    //        IQueryable<Data.Entities.DocumentInfo> documentInfos = dbContext.DocumentInfos.AsQueryable();


    //        await Task.Delay(1150);


    //        return documentInfos.Select(x => new Models.DocumentInfo(
    //            x.Document.Name,
    //            x.DocumentNumber,
    //            x.IssuedDate,
    //            x.EndDate)).AsQueryable();
    //    }

    //}

    public async Task InsertDocumentInfo(int documentId, Models.DocumentInfo documentInfo)
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            var docInfo = new Data.Entities.DocumentInfo
            {
                DocumentNumber = documentInfo.DocumentNumber!,
                IssuedDate = documentInfo.IssuedDate,
                EndDate = documentInfo.EndDate,
                DocumentId = documentId
            };

            dbContext.DocumentInfos.Add(docInfo);
            await dbContext.SaveChangesAsync();
        }
    }


}
