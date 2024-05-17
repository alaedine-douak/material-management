using GM.Data;
using GM.Models;
using GM.ViewModels.Documents;
using Microsoft.EntityFrameworkCore;

namespace GM.Repositories;

public class DocumentInfoRepo(
    IGMDbContextFactory dbContextFactory) : IDocumentInfoRepo
{
    private readonly IGMDbContextFactory _dbContextFactory = dbContextFactory;

    public async Task<IEnumerable<DocumentInfoViewModel>> GetAllDocumentInfos()
    {
        using (GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            return await dbContext
                .DocumentInfos
                .Include(x => x.Document)
                .Include(x => x.Vehicle)
                .Where(x => !x.IsArchived)
                .Select(x => new DocumentInfoViewModel(
                    new DocumentInfo(
                        x.Document!.Name,
                        x.DocumentNumber,
                        x.IssuedDate,
                        x.EndDate),
                    new Vehicle(x.Vehicle!.Code,
                    x.Vehicle.Designation,
                    x.Vehicle.Brand,
                    x.Vehicle.PlateNumber)))
                .ToListAsync();
        }
    }

    public async Task InsertDocumentInfo(int documentId, int vehicleId, DocumentInfoViewModel documentInfo)
    {
        using(GMDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            var documentInfoEntity = new Data.Entities.DocumentInfo
            {
                DocumentId = documentId,
                VehicleId = vehicleId,
                DocumentNumber = documentInfo.DocumentNo,
                IssuedDate = documentInfo.DocumentIssuedDate,
                EndDate = documentInfo.DocumentEndDate,
                IsArchived = false,
            };

            dbContext.DocumentInfos.Add(documentInfoEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
