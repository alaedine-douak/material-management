namespace GM.Repositories;

public interface IDocumentInfoRepo
{
    Task<IEnumerable<Models.DocumentInfo>> GetAllDocumentInfos();
    Task InsertDocumentInfo(int documentId, Models.DocumentInfo documentInfo);
}
