using GM.Models;

namespace GM.ViewModels.Document;

public class DocumentViewModel : ViewModelBase
{
    public string DocumentNumber => _docInfo.DocumentNumber;
    public string DocumentName => _docInfo.Document.Name;
    public string IssuedDate => _docInfo.IssuedDate.ToString("d"); // TODO: modify datetime to string type and use .tostring("d") to format only date
    public string EndDate => _docInfo.EndDate.ToString("d");
    

    private readonly DocumentDetail _docInfo;

    public DocumentViewModel(DocumentDetail docInfo)
    {
        _docInfo = docInfo;
    }
}
