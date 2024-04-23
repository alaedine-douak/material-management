using GM.Models;
using GM.Commands;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GM.Stores;

namespace GM.ViewModels.Document;

public class AddDocumentViewModel : ViewModelBase
{
    private string _documentNumber = "";
    public string DocumentNumber
    {
        get => _documentNumber;
        set
        {
            _documentNumber = value;
            OnPropertyChanged(nameof(DocumentNumber));
        }
    }

    private DateTime _issuedDate = DateTime.Now;
    public DateTime IssuedDate
    {
        get => _issuedDate;
        set
        {
            _issuedDate = value;
            OnPropertyChanged(nameof(IssuedDate));
        }
    }

    private DateTime _endDate = DateTime.Now;
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }

    private string _documentName = "";
    public string DocumentName
    {
        get => _documentName;
        set
        {
            _documentName = value;
            OnPropertyChanged(nameof(DocumentName));
        }
    }

    private string _selectedDocumentName = "";
    public string SelectedDocumentName
    {
        get => _selectedDocumentName;
        set
        {
            _selectedDocumentName = value;
            OnPropertyChanged(nameof(SelectedDocumentName));
        }
    }
    
    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand SubmitNewDocumentNameCommand { get; }
    //public ICommand DocumentNameSelectionChangedCommand { get; }

    private readonly ObservableCollection<string> _documents;
    public IEnumerable<string> Documents => _documents;


    public AddDocumentViewModel(
        User user, 
        NavigationStore navigationStore,
        Func<DocumentListViewModel> documentListViewModel)
    {
        _documents = new ObservableCollection<string> {"D1", "D2", "D3", "D4" };


        SubmitCommand = new SubmitDocumentDetailCommand(this, user);
        CancelCommand = new NavigateCommand(navigationStore, documentListViewModel);
        SubmitNewDocumentNameCommand = null!;
        //DocumentNameSelectionChangedCommand = null!;
    }


}
