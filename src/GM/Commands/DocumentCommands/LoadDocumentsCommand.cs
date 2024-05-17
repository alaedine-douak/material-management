using GM.Services;
using GM.Stores;
using GM.ViewModels.Documents;
using System.Windows;

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
        try
        {
            await _documentInfoStore.Load();

            await _vehicleStore.LoadVehicles();
            await _documentStore.LoadDocumentNames();

            _insertDocumentViewModel.UpdateDocumentNames(_documentStore.Documents);
            _documentsViewModel.UpdateDocumentInfos(_documentInfoStore.DocumentInfos);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"{ex.Message}",
                "Erreur de chargement des informations sur le document",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
