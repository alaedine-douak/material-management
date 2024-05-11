namespace GM.Repositories;

public interface IDocumentRepo
{
    Task<IEnumerable<Models.Document>> GetAllDocuments();
    Task<Data.Entities.Document?> GetDocumentByName(string name);
    Task InsertDocumentAsync(int userId, Models.Document document);
}
