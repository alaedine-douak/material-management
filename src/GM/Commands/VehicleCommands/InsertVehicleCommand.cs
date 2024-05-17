using GM.Stores;
using GM.Services;
using System.Windows;
using GM.Repositories;
using System.ComponentModel;
using GM.ViewModels.Vehicles;

namespace GM.Commands.VehicleCommands;

public class InsertVehicleCommand : AsyncCommandBase
{
    private readonly IUserRepo _userRepo;
    private readonly VehicleStore _vehicleStore;
    private readonly InsertVehicleViewModel _viewModel;
    private readonly NavigationService<VehicleListViewModel> _vehicleListNavigationService;
   
    public InsertVehicleCommand(
        IUserRepo userRepo,
        VehicleStore vehicleStore,
        InsertVehicleViewModel viewModel,
        NavigationService<VehicleListViewModel> vehicleListNavigationService)
    {
        _userRepo = userRepo;
        _vehicleStore = vehicleStore;
        _viewModel = viewModel;
        _vehicleListNavigationService = vehicleListNavigationService;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override bool CanExecute(object? parameter)
    {
        return _viewModel.CanInsertVehicle && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            Models.Vehicle vehicleModel = new(
                _viewModel.Code?.ToUpper()!,
                _viewModel.Designation?.ToUpper()!,
                _viewModel.Brand?.ToUpper()!,
                _viewModel.PlateNumber?.ToUpper()!);

            var user = await _userRepo.GetUser("gmadmin");

            if (user is null) throw new Exception("Il n'y a aucun utilisateur, confirmez auprès de l'administrateur de la base de données que l'utilisateur existe !");


            await _vehicleStore.InsertVehicle(user.Id, vehicleModel);

            MessageBox.Show("Le véhicule a inséré",
                "Insérer un véhicule",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            _vehicleListNavigationService.Navigate();
        }
        catch(Exception ex) 
        {
            MessageBox.Show($"{ex.Message}",
                "Insérer un véhicule",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.CanInsertVehicle))
        {
            OnCanExecutedChanged();
        }
    }
}
