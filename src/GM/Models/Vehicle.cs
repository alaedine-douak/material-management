using System.Xml.Linq;

namespace GM.Models;

public class Vehicle
{
    public string Code { get; }
    public string VehicleCode { get; }
    public string Brand { get; }
    public string Model { get; }

    private readonly List<DocumentDetail> _docInfos;

    public Vehicle(string code, string vehicleCode, string brand, string model)
    {
        Code = code;
        VehicleCode = vehicleCode;
        Brand = brand;
        Model = model;

        _docInfos = new List<DocumentDetail>();
    }

    public IEnumerable<DocumentDetail> GetAllDocumentInfos() => _docInfos;

    public void AddDocumentInfo(DocumentDetail doctInfo) => _docInfos.Add(doctInfo);

    public bool ConflictVehicleCode(Vehicle vehicle)
    {
        if (vehicle.Code.ToLower() != Code.ToLower()) return false;

        return true;
    }
}
