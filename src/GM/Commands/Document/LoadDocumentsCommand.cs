
using GM.ViewModels.Document;

namespace GM.Commands.Document;

public class LoadDocumentsCommand : AsyncCommandBase
{
    public LoadDocumentsCommand(DocumentListViewModel vm)
    {
        
    }

    public override Task ExecuteAsync(object? parameter)
    {
        throw new NotImplementedException();
    }
}
