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
    private readonly IDocumentInfoRepo _documentInfoRepo;
    private readonly IDocumentRepo _documentRepo;
    private readonly DocumentInfoStore _documentInfoStore;
    private readonly NavigationService<DocumentsViewModel> _navigationService;

    public SubmitDocumentInfoCommand(
        InsertDocumentInfoViewModel viewModel,
        IDocumentInfoRepo documentInfoRepo,
        IDocumentRepo documentRepo,
        DocumentInfoStore documentInfoStore,
        NavigationService<DocumentsViewModel> navigationService)
    {
        _viewModel = viewModel;
        _documentInfoRepo = documentInfoRepo;
        _documentRepo = documentRepo;
        _documentInfoStore = documentInfoStore;
        _navigationService = navigationService;
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InsertDocumentInfoViewModel.SelectedDocument) ||
            e.PropertyName == nameof(InsertDocumentInfoViewModel.DocumentNumber))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_viewModel.SelectedDocument.Name) && 
            !string.IsNullOrEmpty(_viewModel.DocumentNumber) &&
            base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        var documentInfo = new Models.DocumentInfo(
            _viewModel.SelectedDocument.Name,
            _viewModel.DocumentNumber,
            _viewModel.IssuedDate,
            _viewModel.EndDate);

        try
        {
            var doc = await _documentRepo.GetDocument(_viewModel.SelectedDocument.Name);

            
            await _documentInfoStore.InsertDocumentInfo(doc.Id, documentInfo);

            MessageBox.Show("Successfully insert document information",
                "Insert document information",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            _navigationService.Navigate();
        }
        catch (Exception)
        {
            MessageBox.Show(
                "Error when submitting a new document",
                "Submitting document error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
