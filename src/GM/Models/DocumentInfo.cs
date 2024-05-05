namespace GM.Models;

public class DocumentInfo(
    string documentName,
    string documentNumber,
    DateTime issuedDate,
    DateTime endDate)
{
    public string DocumentName { get; } = documentName;
    public string DocumentNumber { get; } = documentNumber;
    public DateTime EndDate { get; } = endDate;
    public DateTime IssuedDate { get; } = issuedDate;
    public TimeSpan RemainingDays => EndDate.Subtract(IssuedDate);

    //public bool Conflicts(DocumentInfo documentInfo)
    //{
    //    return documentInfo.IssuedDate < EndDate && documentInfo.EndDate > IssuedDate;
    //}
}
