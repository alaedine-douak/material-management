using GM.Exceptions;
using GM.Repositories;
using GM.Repositories.VehicleRepos;
using GM.Services;
using GM.Stores;
using GM.ViewModels.Vehicles;
using System.ComponentModel;
using System.Windows;

namespace GM.Commands.VehicleCommands;

public class InsertVehicleCommand : AsyncCommandBase
{
    private readonly IUserRepo _userRepo;
    private readonly VehicleStore _vehicleStore;
    private readonly IVehicleConflictValidator _vehicleConflictValidator;
    private readonly InsertVehicleViewModel _insertVehicleViewModel;
    private readonly NavigationService<VehicleListViewModel> _vehicleListNavigationService;
   
    public InsertVehicleCommand(
        IUserRepo userRepo,
        VehicleStore vehicleStore,
        IVehicleConflictValidator vehicleConflictValidator,
        InsertVehicleViewModel insertVehicleViewModel,
        NavigationService<VehicleListViewModel> vehicleListNavigationService)
    {
        _userRepo = userRepo;
        _vehicleStore = vehicleStore;
        _vehicleConflictValidator = vehicleConflictValidator;
        _insertVehicleViewModel = insertVehicleViewModel;
        _vehicleListNavigationService = vehicleListNavigationService;

        _insertVehicleViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return _insertVehicleViewModel.CanInsertVehicle && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            Models.Vehicle vehicleModel = new(
                _insertVehicleViewModel.Code,
                _insertVehicleViewModel.Designation,
                _insertVehicleViewModel.Brand,
                _insertVehicleViewModel.PlateNumber);

            var vehicle = await _vehicleConflictValidator.GetConflictingVehicle(vehicleModel);

            if (vehicle != null) throw new VehicleConflictException();

            var user = await _userRepo.GetUser("gmadmin");

            if (user is null) throw new Exception("There is no user");


            await _vehicleStore.InsertVehicle(user.Id, vehicleModel);

            MessageBox.Show("Vehicle has inserted successfully",
                "Insert vehicle",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            _vehicleListNavigationService.Navigate();
        }
        catch(VehicleConflictException)
        {
            MessageBox.Show(
                "Vehicle code is already taken, please try another code.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        catch(Exception ex) 
        {
            MessageBox.Show($"[Inserting Vehicle]: {ex.Message}",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_insertVehicleViewModel.CanInsertVehicle))
        {
            OnCanExecutedChanged();
        }
    }
}
