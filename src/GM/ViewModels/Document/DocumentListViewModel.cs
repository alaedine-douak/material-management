using GM.Stores;
using GM.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GM.Services;
using GM.Models;


namespace GM.ViewModels.Document;

public class DocumentListViewModel : ViewModelBase
{
    private readonly User _user;

    private readonly ObservableCollection<DocumentViewModel> _documents;
    public IEnumerable<DocumentViewModel> Documents => _documents;
    public ICommand AddDocumentCommand { get; }

    public DocumentListViewModel(User user, NavigationService navigationService)
    {
        _user = user;
        _documents = new ObservableCollection<DocumentViewModel>();

        AddDocumentCommand = new NavigateCommand(navigationService);

        UpdateDocuments();
    }


    private void UpdateDocuments()
    {
        _documents.Clear();

        foreach (DocumentDetail document in _user.GetAllDocumentDetails())
        {
            DocumentViewModel vm = new(document);

            _documents.Add(vm);
        }
    }
}
