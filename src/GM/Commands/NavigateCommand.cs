using GM.Stores;
using GM.ViewModels.Document;

namespace GM.Commands;

public class NavigateCommand : CommandBase
{
    private readonly NavigationStore _navigationStore;

    public NavigateCommand(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }

    public override void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = new AddDocumentViewModel(new Models.User("Alaedine"));
    }
}
