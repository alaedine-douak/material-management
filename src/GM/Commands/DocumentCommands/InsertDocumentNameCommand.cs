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
            e.PropertyName == nameof(InsertDocumentInfoViewModel.AlartedDuration))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_viewModel.DocumentName) && 
            !string.IsNullOrEmpty(_viewModel.AlartedDuration) &&
            base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        try
        {
            var document = new Models.Document(
                _viewModel.DocumentName.CapitalizeEachWord(), 
                int.Parse(_viewModel.AlartedDuration));

            _viewModel.DocumentName = string.Empty;
            _viewModel.AlartedDuration = string.Empty;


            var conflictingDocument = await _documentConflictValidation.GetConflictingDocument(document);

            if (conflictingDocument != null)
            {
                throw new DocumentConflictException();
            }


            var user = await _userRepo.GetUser("gmadmin");

            if (user is null) throw new Exception("There is no user");

            await _documentStore.InsertDocument(user!.Id, document);

            MessageBox.Show(
                "Le nom du document a été ajouté avec succès",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch(DocumentConflictException)
        {
            MessageBox.Show(
                "Ce nom de document existe déjà",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        catch (Exception)
        {
            MessageBox.Show(
                "Quelque chose d'inattendu s'est produit lors de l'insertion du nom du document, réessayez plus tard ou contactez l'administrateur.",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

}
