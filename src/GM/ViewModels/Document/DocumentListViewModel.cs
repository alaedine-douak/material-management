using GM.Stores;
using GM.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;


namespace GM.ViewModels.Document;

public class DocumentListViewModel : ViewModelBase
{
    private readonly ObservableCollection<DocumentViewModel> _documents;
    public IEnumerable<DocumentViewModel> Documents => _documents;
    public ICommand AddDocumentCommand { get; }

    public DocumentListViewModel(NavigationStore navigationStore)
    {
        _documents = new ObservableCollection<DocumentViewModel>
        {
            new DocumentViewModel(new Models.DocumentDetail(new Models.Document("D1"), "3873", DateTime.Now, DateTime.Now)),
            new DocumentViewModel(new Models.DocumentDetail(new Models.Document("D2"), "3874", DateTime.Now, DateTime.Now)),
            new DocumentViewModel(new Models.DocumentDetail(new Models.Document("D3"), "3875", DateTime.Now, DateTime.Now)),
            new DocumentViewModel(new Models.DocumentDetail(new Models.Document("D4"), "3876", DateTime.Now, DateTime.Now)),
            new DocumentViewModel(new Models.DocumentDetail(new Models.Document("D5"), "3877", DateTime.Now, DateTime.Now)),
        };

        AddDocumentCommand = new NavigateCommand(navigationStore);
    }
}
