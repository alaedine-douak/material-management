using GM.Repositories;
using GM.ViewModels.Documents;

namespace GM.Stores;

public class DocumentInfoStore
{
    private readonly IDocumentInfoRepo _documentInfoRepo;
    private readonly List<DocumentInfoViewModel> _docInfos;
    private Lazy<Task> _initializeLazy;

    public IEnumerable<DocumentInfoViewModel> DocumentInfos => _docInfos;
    public DocumentInfoStore(IDocumentInfoRepo documentInfoRepo)
    {
        _documentInfoRepo = documentInfoRepo;
        _docInfos = new List<DocumentInfoViewModel>();
        _initializeLazy = new Lazy<Task>(Initialize);
        
    }

    public async Task Load()
    {
        try
        {
            await _initializeLazy.Value;
        }
        catch (Exception)
        {
            _initializeLazy = new Lazy<Task>(Initialize);
            throw;
        }
    }

    public async Task InsertDocumentInfo(int documentId, int vehicleId, DocumentInfoViewModel documentInfo)
    {
        await _documentInfoRepo.InsertDocumentInfo(documentId, vehicleId, documentInfo);

        _docInfos.Add(documentInfo);
    }

    private async Task Initialize()
    {
        IEnumerable<DocumentInfoViewModel> docInfos = await _documentInfoRepo.GetAllDocumentInfos();

        _docInfos.Clear();
        _docInfos.AddRange(docInfos);
    }


}
