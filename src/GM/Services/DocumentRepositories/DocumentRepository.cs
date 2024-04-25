//using GM.Data;
//using GM.Models;
//using Microsoft.EntityFrameworkCore;

//namespace GM.Services.DocumentRepositories;

//public class DocumentRepository : IDocumentRepository
//{
//    private readonly GMDbContext _dbContext;

//    public DocumentRepository(GMDbContext dbContext) => _dbContext = dbContext;
    

//    public async Task<IEnumerable<Document>> GetAllDocuments()
//    {
//        IEnumerable<GM.Data.Entities.Document> documents = await _dbContext.Documents.ToListAsync();

//        return documents.Select(d => MapToDocumentModel(d));
//    }

//    public async Task CreateDocument(Document documentModel)
//    {
//        _dbContext.Documents.Add(new GM.Data.Entities.Document { Name = documentModel.Name });
//        await _dbContext.SaveChangesAsync();
//    }

//    // TODO: create generic funtion for mapping between entities and models
//    static Document MapToDocumentModel(Data.Entities.Document documentEntity)
//        => new Document(documentEntity.Name);

//}
