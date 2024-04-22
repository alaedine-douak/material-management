namespace GM.Models;

public class Document
{
    public string Name { get; }

    //private List<Vehicle> _vehicles;
    //private List<DocumentInfo> _documentInfos;

    public Document(string name)
    {
        Name = name;

        //_vehicles = new List<Vehicle>();
        //_documentInfos = new List<DocumentInfo>();
    }

    //public IEnumerable<Vehicle> GetAllVehicles() => _vehicles;

    //public void AddVehicle(Vehicle vehicle) => _vehicles.Add(vehicle);

    //public IEnumerable<DocumentInfo> GetAllDocumentsInfo()
    //{
    //    return _documentInfos;
    //}

    //public void AddDocumentInfo(DocumentInfo documentInfo)
    //{
    //    // TODO: Check dates validation

    //    foreach (DocumentInfo docInfo in _documentInfos)
    //    {
    //        if (docInfo.ConflictDocumentNumber(documentInfo))
    //        {
    //            throw new DocumentInfoException();
    //        }
    //    }

    //    _documentInfos.Add(documentInfo);
    //}

    public bool ConflictDocumentName(Document document)
    {
        if (document.Name.ToLower() !=  Name.ToLower()) return false;

        return true;
    }
}