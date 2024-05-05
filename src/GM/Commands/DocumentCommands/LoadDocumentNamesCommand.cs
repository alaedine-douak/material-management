using GM.Stores;
using GM.ViewModels.Documents;

namespace GM.Commands.DocumentCommands;

public class LoadDocumentNamesCommand : AsyncCommandBase
{
    private readonly InsertDocumentInfoViewModel _viewModel;
    private readonly DocumentStore _documentStore;

    public LoadDocumentNamesCommand(
        InsertDocumentInfoViewModel vm,
        DocumentStore documentStore)
    {
        _viewModel = vm;
        _documentStore = documentStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            await _documentStore.LoadDocumentNames();

            _viewModel.UpdateDocumentNames(_documentStore.Documents);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
