using GM.Exceptions;
using GM.Repositories;
using GM.Repositories.VehicleRepos;
using GM.Services;
using GM.ViewModels.Vehicles;
using System.ComponentModel;
using System.Windows;

namespace GM.Commands.VehicleCommands;

public class InsertVehicleCommand : AsyncCommandBase
{
    private readonly IUserRepo _userRepo;
    private readonly IVehicleRepo _vehicleRepo;
    private readonly IVehicleConflictValidator _vehicleConflictValidator;
    private readonly InsertVehicleViewModel _insertVehicleViewModel;
    private readonly NavigationService<VehicleListViewModel> _vehicleListNavigationService;
   
    public InsertVehicleCommand(
        IUserRepo userRepo,
        IVehicleRepo vehicleRepo,
        IVehicleConflictValidator vehicleConflictValidator,
        InsertVehicleViewModel insertVehicleViewModel,
        NavigationService<VehicleListViewModel> vehicleListNavigationService)
    {
        _userRepo = userRepo;
        _vehicleRepo = vehicleRepo;
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
            var vehicleModel = new Models.Vehicle(
                _insertVehicleViewModel.Code!.ToUpper(),
                _insertVehicleViewModel.Designation!.ToUpper(),
                _insertVehicleViewModel.Brand!.ToUpper(),
                _insertVehicleViewModel.PlateNumber!);

            var vehicle = await _vehicleConflictValidator.GetConflictingVehicle(vehicleModel);

            if (vehicle != null) throw new VehicleConflictException();


            var user = await _userRepo.GetUser("gmadmin");
            await _vehicleRepo.InsertVehicle(user.Id, vehicleModel);

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
                "Vehicle Code Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        catch(Exception ex) 
        {
            MessageBox.Show($"Inserting vehicle error {ex.Message}",
                "Db Error",
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
