using System.Windows;
using GM.ViewModels.Documents;
using System.ComponentModel;
using GM.Repositories;
using GM.Exceptions;
using GM.Stores;

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
        if (e.PropertyName == nameof(InsertDocumentInfoViewModel.DocumentName))
        {
            OnCanExecutedChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_viewModel.DocumentName) && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object? parameter)
    {

        Models.Document document = new(_viewModel.DocumentName!);
        _viewModel.DocumentName = string.Empty;


        try
        {
            var conflictingDocument = await _documentConflictValidation.GetConflictingDocument(document);

            if (conflictingDocument != null)
            {
                throw new DocumentConflictException();
            }


            var user = await _userRepo.GetUser("gmadmin");

            await _documentStore.InsertDocument(user.Id, document);

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
