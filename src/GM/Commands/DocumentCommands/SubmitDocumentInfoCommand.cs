using GM.Repositories;
using GM.Services;
using GM.Stores;
using GM.ViewModels.Documents;
using System.ComponentModel;
using System.Windows;

namespace GM.Commands.DocumentCommands;

public class SubmitDocumentInfoCommand : AsyncCommandBase
{
    private readonly InsertDocumentInfoViewModel _viewModel;
    private readonly IDocumentRepo _documentRepo;
    private readonly DocumentInfoStore _documentInfoStore;
    private readonly NavigationService<DocumentsViewModel> _navigationService;

    public SubmitDocumentInfoCommand(
        InsertDocumentInfoViewModel viewModel,
        IDocumentRepo documentRepo,
        DocumentInfoStore documentInfoStore,
        NavigationService<DocumentsViewModel> navigationService)
    {
        _viewModel = viewModel;
        _documentRepo = documentRepo;
        _documentInfoStore = documentInfoStore;
        _navigationService = navigationService;
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InsertDocumentInfoViewModel.SelectedDocument) ||
            e.PropertyName == nameof(InsertDocumentInfoViewModel.DocumentNumber) ||
            e.PropertyName == nameof(InsertDocumentInfoViewModel.SelectedVehicle))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_viewModel.SelectedDocument?.Name) && 
            !string.IsNullOrEmpty(_viewModel.DocumentNumber) &&
            _viewModel.SelectedVehicle is not null &&
            base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {

        var docInfoVM = new DocumentInfoViewModel(
            new Models.DocumentInfo(
                _viewModel.SelectedDocument?.Name!,
                _viewModel.DocumentNumber!,
                _viewModel.IssuedDate,
                _viewModel.EndDate), 
            new Models.Vehicle(
                _viewModel.SelectedVehicle!.Code, 
                _viewModel.SelectedVehicle.Designation,
                _viewModel.SelectedVehicle.Brand,
                _viewModel.SelectedVehicle.PlateNumber));

        try
        {
            var vehicleId = int.Parse(_viewModel.SelectedVehicle?.VehicleId!);

            var doc = await _documentRepo.GetDocumentByName(_viewModel.SelectedDocument?.Name!);

            await _documentInfoStore.InsertDocumentInfo(doc!.Id, vehicleId, docInfoVM);

            MessageBox.Show("Les informations sur le document ont été insérées !",
                "Insérer des informations sur le document",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            _navigationService.Navigate();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"{ex.Message}",
                "Insérer des informations sur le document",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
