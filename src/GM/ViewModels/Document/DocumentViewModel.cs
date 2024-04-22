using GM.Models;

namespace GM.ViewModels.Document;

public class DocumentViewModel : ViewModelBase
{
    public string DocumentNumber => _docInfo.DocumentNumber;
    public string DocumentName => _docInfo.Document.Name;
    public DateTime IssuedDate => _docInfo.IssuedDate;
    public DateTime EndDate => _docInfo.EndDate;
    

    private readonly DocumentInfo _docInfo;

    public DocumentViewModel(DocumentInfo docInfo)
    {
        _docInfo = docInfo;
    }
}
