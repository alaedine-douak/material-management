using GM.Exceptions;

namespace GM.Models;

public class User
{
    public string Username { get; }

    private readonly List<Vehicle> _vehicles;
    private readonly List<Document> _documents;
    private readonly List<DocumentDetail> _docDetails;

    public User(string username)
    {
        Username = username;

        _vehicles = new List<Vehicle>();
        _documents = new List<Document>();
        _docDetails = new List<DocumentDetail>();
    }


    public IEnumerable<Document> GetAllDocuments()
    {
        return _documents;
    }

    public void AddDocument(Document document)
    {
        foreach (Document existingDoc in _documents)
        {
            if (existingDoc.ConflictDocumentName(document))
            {
                throw new DocumentException();
            }
        }

        _documents.Add(document);
    }

    public IEnumerable<DocumentDetail> GetAllDocumentDetails()
    {
        return _docDetails;
    }

    internal void AddDocumentDetail(DocumentDetail docDetail)
    {
        _docDetails.Add(docDetail);
    }

    public IEnumerable<Vehicle> GetAllVehicles()
    {
        return _vehicles;
    }

    public void AddVehicle(Vehicle vehicle)
    {
        foreach (Vehicle existingVehicle in _vehicles)
        {
            if (existingVehicle.ConflictVehicleCode(vehicle))
            {
                throw new VehicleException();
            }
        }

        _vehicles.Add(vehicle);
    }
}
