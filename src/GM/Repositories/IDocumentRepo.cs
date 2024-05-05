namespace GM.Repositories;

public interface IDocumentRepo
{
    Task<IEnumerable<Models.Document>> GetDocumentNames();
    Task<Data.Entities.Document> GetDocument(string documentName);
    Task InsertDocumentNameAsync(int userId, Models.Document document);
}
