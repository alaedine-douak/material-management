using System.Windows.Input;

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

    private DateTime _issuedDate;
    public DateTime IssuedDate
    {
        get => _issuedDate;
        set
        {
            _issuedDate = value;
            OnPropertyChanged(nameof(IssuedDate));
        }
    }

    private DateTime _endDate;
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }

    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    public AddDocumentViewModel()
    {
        SubmitCommand = null!; 
        CancelCommand = null!;
    }

}
