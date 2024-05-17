using System.Windows;
using GM.ViewModels.Documents;
using System.ComponentModel;
using GM.Repositories;
using GM.Exceptions;
using GM.Stores;
using GM.Extensions;

namespace GM.Commands.Documents;

public class InsertDocumentNameCommand : AsyncCommandBase
{
    private readonly InsertDocumentInfoViewModel _viewModel;

    private readonly IUserRepo _userRepo;
    private readonly IDocumentRepo _documentRepo;
    private readonly DocumentStore _documentStore;
    private readonly IDocumentConflictValidator _documentConflictValidation;

    public InsertDocumentNameCommand(
        InsertDocumentInfoViewModel viewModel,
        IUserRepo userRepo,
        IDocumentRepo documentRepo,
        DocumentStore documentStore,
        IDocumentConflictValidator documentConflictValidation)
    {
        _viewModel = viewModel;
        _userRepo = userRepo;
        _documentRepo = documentRepo;
        _documentStore = documentStore;
        _documentConflictValidation = documentConflictValidation;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InsertDocumentInfoViewModel.DocumentName) ||
            e.PropertyName == nameof(InsertDocumentInfoViewModel.AlartDuration))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_viewModel.DocumentName) && 
            !string.IsNullOrEmpty(_viewModel.AlartDuration) &&
            base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            var document = new Models.Document(
                _viewModel.DocumentName.CapitalizeEachWord(), 
                int.Parse(_viewModel.AlartDuration));

            _viewModel.DocumentName = string.Empty;
            _viewModel.AlartDuration = string.Empty;


            var conflictingDocument = await _documentConflictValidation.GetConflictingDocument(document);

            if (conflictingDocument != null)
            {
                throw new DocumentConflictException();
            }


            var user = await _userRepo.GetUser("gmadmin");

            if (user is null) throw new Exception("Il n'y a aucun utilisateur, confirmez auprès de l'administrateur de la base de données que l'utilisateur existe !");

            await _documentStore.InsertDocument(user!.Id, document);

            MessageBox.Show(
                "Le nom du document a été inséré",
                "Insérer un document",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch(DocumentConflictException)
        {
            MessageBox.Show(
                "Ce nom de document existe déjà",
                "Erreur d'insertion du nom du document",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"{ex.Message}",
                "Erreur d'insertion du nom du document",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

}
