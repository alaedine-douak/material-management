using GM.ViewModels.Documents;

namespace GM.Repositories;

public interface IDocumentInfoRepo
{
    Task<IEnumerable<DocumentInfoViewModel>> GetAllDocumentInfos();
    Task InsertDocumentInfo(int documentId, int vehicleId, DocumentInfoViewModel documentInfo);
}
