using System.Windows.Input;
using System.Collections.ObjectModel;

namespace GM.ViewModels.Document;

public class DocumentListViewModel : ViewModelBase
{
    private readonly ObservableCollection<DocumentViewModel> _documents;

    public IEnumerable<DocumentViewModel> Documents => _documents;
    public ICommand AddDocumentCommand { get; }

    public DocumentListViewModel()
    {
        _documents = new ObservableCollection<DocumentViewModel>();
        _documents.Add(new DocumentViewModel(new Models.DocumentInfo(
            new Models.Document("D1"), "3874", DateTime.Now, DateTime.Now)));
        _documents.Add(new DocumentViewModel(new Models.DocumentInfo(
            new Models.Document("D2"), "3174", DateTime.Now, DateTime.Now)));
        _documents.Add(new DocumentViewModel(new Models.DocumentInfo(
            new Models.Document("D3"), "3374", DateTime.Now, DateTime.Now)));
        _documents.Add(new DocumentViewModel(new Models.DocumentInfo(
            new Models.Document("D4"), "3474", DateTime.Now, DateTime.Now)));


        AddDocumentCommand = null!;
    }
}
