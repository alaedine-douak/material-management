using GM.Stores;
using GM.Commands;
using GM.Services;
using GM.Repositories;
using System.Collections;
using System.Windows.Input;
using System.ComponentModel;
using GM.Commands.VehicleCommands;
using System.Text.RegularExpressions;

namespace GM.ViewModels.Vehicles;

public class InsertVehicleViewModel : ViewModelBase, INotifyDataErrorInfo
{
    private string? _code;
    public string? Code
    {
        get => _code;
        set
        {
            _code = value;
            OnPropertyChanged(nameof(Code));
            OnPropertyChanged(nameof(CanInsertVehicle));
        }
    }

    
    private string? _designation;
    public string? Designation
    {
        get => _designation;
        set
        {
            _designation = value;
            OnPropertyChanged(nameof(Designation));
            OnPropertyChanged(nameof(CanInsertVehicle));
        }
    }

    
    private string? _brand;
    public string? Brand
    {
        get => _brand;
        set
        {
            _brand = value;
            OnPropertyChanged(nameof(Brand));
            OnPropertyChanged(nameof(CanInsertVehicle));
        }
    }

    
    private string? _plateNumber;
    public string? PlateNumber
    {
        get => _plateNumber;
        set
        {
            _plateNumber = value;
            OnPropertyChanged(nameof(PlateNumber));

            ClearErrors(nameof(PlateNumber));

            if (!Regex.IsMatch(_plateNumber!, @"((^| )(\d{1,6}\.\d{3}\.\d{2})|-)+$") )
            {
                AddError(nameof(PlateNumber), "invalid format");
            }

            OnPropertyChanged(nameof(CanInsertVehicle));
        }
    }

    private bool HasVehicleCode => !string.IsNullOrEmpty(Code);
    private bool HasDesignation => !string.IsNullOrEmpty(Designation);
    private bool HasBrand => !string.IsNullOrEmpty(Brand);
    private bool HasPlateNumber => !string.IsNullOrEmpty(PlateNumber);


    private readonly Dictionary<string, List<string>> _propertyErrors;

    public bool CanInsertVehicle =>
        HasVehicleCode &&
        HasDesignation &&
        HasBrand &&
        HasPlateNumber &&
        !HasErrors;

    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public bool HasErrors => _propertyErrors.Any();

    public InsertVehicleViewModel(
        IUserRepo userRepo,
        VehicleStore vehicleStore,
        NavigationService<VehicleListViewModel> vehicleListNavigationService)
    {
        _propertyErrors = new();


        SubmitCommand = new InsertVehicleCommand(
            userRepo, 
            vehicleStore, 
            this, 
            vehicleListNavigationService);

        CancelCommand = new NavigateCommand<VehicleListViewModel>(
            vehicleListNavigationService);
    }

    public IEnumerable GetErrors(string? propertyName)
    {
        return _propertyErrors.GetValueOrDefault(propertyName!, new List<string>());
    }

    public void AddError(string propertyName, string errorMessage)
    {
        if (!_propertyErrors.ContainsKey(propertyName))
        {
            _propertyErrors.Add(propertyName, new List<string>());
        }

        _propertyErrors[propertyName]?.Add(errorMessage);

        OnErrorChanged(propertyName);
    }

    public void ClearErrors(string propertyName)
    {
        _propertyErrors.Remove(propertyName); 
        
        OnErrorChanged(propertyName);
    }

    private void OnErrorChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
