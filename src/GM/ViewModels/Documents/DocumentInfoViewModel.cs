using GM.Models;

namespace GM.ViewModels.Documents;

public class DocumentInfoViewModel(
    DocumentInfo documentInfo,
    Vehicle vehicle) : ViewModelBase
{
    public string DocumentNo => documentInfo.DocumentNumber;
    public string DocumentName => documentInfo.DocumentName;
    public DateTime DocumentIssuedDate => documentInfo.IssuedDate;
    public DateTime DocumentEndDate => documentInfo.EndDate;
    public TimeSpan RemainingDays  => DocumentEndDate.Subtract(DocumentIssuedDate);
    public string VehicleCode => vehicle.Code;
    public string VehiclePlateNumber => vehicle.PlateNumber;
    public string VehicleDesignation => vehicle.Designation;
    public string vehicleBrand => vehicle.Brand;

}
