using GM.Models;
using GM.Commands;
using GM.Services;
using System.Windows.Input;
using System.Collections.ObjectModel;


namespace GM.ViewModels.Document;

public class AddDocumentViewModel : ViewModelBase
{
    private string _documentNumber = string.Empty;
    
    private DateTime _issuedDate = DateTime.Now;
    
    private DateTime _endDate = DateTime.Now;

    private string _documentName = string.Empty;

    private string _selectedDocumentName = string.Empty;

    private readonly ObservableCollection<string> _documents;


    public string DocumentNumber
    {
        get => _documentNumber;
        set
        {
            _documentNumber = value;
            OnPropertyChanged(nameof(DocumentNumber));
        }
    }

    public DateTime IssuedDate
    {
        get => _issuedDate;
        set
        {
            _issuedDate = value;
            OnPropertyChanged(nameof(IssuedDate));
        }
    }

    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }

    public string DocumentName
    {
        get => _documentName;
        set
        {
            _documentName = value;
            OnPropertyChanged(nameof(DocumentName));
        }
    }

    public string SelectedDocumentName
    {
        get => _selectedDocumentName;
        set
        {
            _selectedDocumentName = value;
            OnPropertyChanged(nameof(SelectedDocumentName));
        }
    }
    
    public IEnumerable<string> Documents => _documents;

    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand SubmitNewDocumentNameCommand { get; }


    public AddDocumentViewModel(User user, NavigationService<DocumentListViewModel> navigationService)
    {
        _documents = new ObservableCollection<string> {"D1", "D2", "D3", "D4" };


        SubmitCommand = new SubmitDocumentDetailCommand(this, user, navigationService);
        CancelCommand = new NavigateCommand<DocumentListViewModel>(navigationService);
        SubmitNewDocumentNameCommand = null!;
    }


}
