using GM.ViewModels;

namespace GM.Models;

public class Vehicle(
    string? code, 
    string? designation, 
    string? brand, 
    string? plateNumber) : ViewModelBase
{
    public string? Code { get; } = code;
    public string? Designation { get; } = designation;
    public string? Brand { get; } = brand;
    public string? PlateNumber { get; } = plateNumber;

    public string VehicleComboBoxText =>
        $"{Code} {Designation} {Brand} {PlateNumber}";
}
