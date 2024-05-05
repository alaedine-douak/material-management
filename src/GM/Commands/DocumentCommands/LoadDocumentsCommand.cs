using GM.Stores;
using GM.ViewModels.Documents;

namespace GM.Commands.Document;

public class LoadDocumentInfosCommand : AsyncCommandBase
{
    private readonly DocumentsViewModel _documentsViewModel;
    private readonly InsertDocumentInfoViewModel _insertDocumentViewModel;

    private readonly DocumentStore _documentStore;
    private readonly DocumentInfoStore _documentInfoStore;

    public LoadDocumentInfosCommand(
        DocumentsViewModel documentViewModel,
        InsertDocumentInfoViewModel insertDocumentViewModel,
        DocumentInfoStore documentInfoStore, 
        DocumentStore documentStore)
    {
        _documentsViewModel = documentViewModel;
        _insertDocumentViewModel = insertDocumentViewModel;
        _documentStore = documentStore;
        _documentInfoStore = documentInfoStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        _documentsViewModel.IsLoading = true;

        try
        {
            await _documentInfoStore.Load();
            await _documentStore.LoadDocumentNames();

            _documentsViewModel.UpdateDocumentInfos(_documentInfoStore.DocumentInfos);
            _insertDocumentViewModel.UpdateDocumentNames(_documentStore.Documents);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _documentsViewModel.IsLoading = false;
        }
    }
}
