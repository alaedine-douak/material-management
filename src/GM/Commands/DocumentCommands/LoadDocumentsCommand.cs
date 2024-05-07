using GM.Stores;
using GM.ViewModels.Documents;

namespace GM.Commands.Document;

public class LoadDocumentInfosCommand(
    DocumentsViewModel documentViewModel,
    InsertDocumentInfoViewModel insertDocumentViewModel,
    VehicleStore vehicleStore,
    DocumentStore documentStore,
    DocumentInfoStore documentInfoStore) : AsyncCommandBase
{
    private readonly DocumentsViewModel _documentsViewModel = documentViewModel;
    private readonly InsertDocumentInfoViewModel _insertDocumentViewModel = insertDocumentViewModel;

    private readonly VehicleStore _vehicleStore = vehicleStore;
    private readonly DocumentStore _documentStore = documentStore;
    private readonly DocumentInfoStore _documentInfoStore = documentInfoStore;

    public override async Task ExecuteAsync(object? parameter)
    {
        _documentsViewModel.IsLoading = true;

        try
        {
            await _documentInfoStore.Load();

            await _vehicleStore.LoadVehicles();
            await _documentStore.LoadDocumentNames();


            _insertDocumentViewModel.UpdateDocumentNames(_documentStore.Documents);
            _documentsViewModel.UpdateDocumentInfos(_documentInfoStore.DocumentInfos);
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
