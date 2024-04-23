namespace GM.Models;

public class DocumentDetail
{
    public Document Document { get; }
    public string DocumentNumber { get; }
    public DateTime EndDate { get; }
    public DateTime IssuedDate { get; }


    public DocumentDetail(
        Document document,
        string documentNumber, 
        DateTime issuedDate, 
        DateTime endDate)
    {
        Document = document;
        DocumentNumber = documentNumber;
        IssuedDate = issuedDate;
        EndDate = endDate;
    }

    //public bool ConflictDocumentNumber(DocumentInfo documentInfo)
    //{
    //    if (documentInfo.DocumentNumber != DocumentNumber)
    //    {
    //        return false;
    //    }

    //    return true;
    //}
}
