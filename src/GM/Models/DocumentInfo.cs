using GM.ViewModels;

namespace GM.Models;

public class DocumentInfo(
    string documentName,
    string documentNumber,
    DateTime issuedDate,
    DateTime endDate) : ViewModelBase
{
    public string DocumentName { get; } = documentName;
    public string DocumentNumber { get; } = documentNumber;
    public DateTime EndDate { get; } = endDate;
    public DateTime IssuedDate { get; } = issuedDate;
}
