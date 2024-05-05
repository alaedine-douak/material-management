using GM.Repositories;

namespace GM.Stores;

public class DocumentStore
{
    private readonly IDocumentRepo _documentRepo;
    private readonly List<Models.Document> _docs;

    private Lazy<Task> _initializeLazy;

    public event Action<Models.Document>? DocumentInserted;

    public IEnumerable<Models.Document> Documents => _docs;

    public DocumentStore(IDocumentRepo documentRepo)
    {
        _documentRepo = documentRepo;
        _docs = new List<Models.Document>();

        _initializeLazy = new Lazy<Task>(Initialize);
    }

    public async Task InsertDocument(int userId, Models.Document document)
    {
        await _documentRepo.InsertDocumentNameAsync(userId, document);

        _docs.Add(document);

        OnDocumentInserted(document);
    }

    private void OnDocumentInserted(Models.Document doucment)
    {
        DocumentInserted?.Invoke(doucment);
    }

    public async Task LoadDocumentNames()
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

    private async Task Initialize()
    {
        IEnumerable<Models.Document> docs = await _documentRepo.GetDocumentNames();
    
        _docs.Clear();
        _docs.AddRange(docs);
    }
}
