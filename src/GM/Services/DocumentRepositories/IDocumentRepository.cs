using GM.Models;

namespace GM.Services.DocumentRepositories;

public interface IDocumentRepository
{
    Task<IEnumerable<Document>> GetAllDocuments(); 
    Task CreateDocument(Document document);
}
