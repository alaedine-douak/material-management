using GM.Repositories;

namespace GM.Stores;

public class DocumentInfoStore
{
    private readonly IDocumentInfoRepo _documentInfoRepo;
    private readonly List<Models.DocumentInfo> _docInfos;
    private Lazy<Task> _initializeLazy;

    public IEnumerable<Models.DocumentInfo> DocumentInfos => _docInfos;
    public DocumentInfoStore(IDocumentInfoRepo documentInfoRepo)
    {
        _documentInfoRepo = documentInfoRepo;
        _docInfos = new List<Models.DocumentInfo>();
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
    public async Task InsertDocumentInfo(int documentId, Models.DocumentInfo documentInfo)
    {
        await _documentInfoRepo.InsertDocumentInfo(documentId, documentInfo);

        _docInfos.Add(documentInfo);
    }

    private async Task Initialize()
    {
        //TODO: implement this method
        //IEnumerable<DocumentInfoModel> docInfos = await _user.GetAllDocumentInfos();

        //IEnumerable<DocumentInfo> docInfos = Enumerable.Empty<DocumentInfo>();

        IEnumerable<Models.DocumentInfo> docInfos = await _documentInfoRepo.GetAllDocumentInfos();

        _docInfos.Clear();
        _docInfos.AddRange(docInfos);

        //_docInfos.Clear();
        //_docInfos.AddRange(docInfos);
    }


}
