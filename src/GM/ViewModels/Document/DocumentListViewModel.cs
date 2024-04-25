using GM.Stores;
using GM.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GM.Services;
using GM.Models;
using GM.Commands.Document;


namespace GM.ViewModels.Document;

public class DocumentListViewModel : ViewModelBase
{
    private readonly User _user;

    private readonly ObservableCollection<DocumentViewModel> _documents;
    public IEnumerable<DocumentViewModel> Documents => _documents;
    
    
    public ICommand AddDocumentCommand { get; }
    public ICommand LoadDocumentsCommand { get; }


    public DocumentListViewModel(User user, NavigationService<AddDocumentViewModel> navigationService)
    {
        _user = user;
        _documents = new ObservableCollection<DocumentViewModel>();

        AddDocumentCommand = new NavigateCommand<AddDocumentViewModel>(navigationService);
        LoadDocumentsCommand = new LoadDocumentsCommand(this);

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
