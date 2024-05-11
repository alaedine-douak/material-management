using GM.Models;
using GM.Stores;
using GM.Services;
using GM.Commands;
using GM.Commands.Document;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace GM.ViewModels.Documents;

public class DocumentsViewModel : ViewModelBase
{
    private readonly ObservableCollection<DocumentInfoViewModel> _documentInfos;

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set 
        {
            _isLoading = value; 
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public IEnumerable<DocumentInfoViewModel> DocumentInfos => _documentInfos;
    public bool HasDocumentInfos => _documentInfos.Any();

    public ICommand LoadDocumentInfosCommand { get; }
    public ICommand InsertDocumentInfoCommand { get; }

    public DocumentsViewModel(
        InsertDocumentInfoViewModel insertDocumentViewModel,
        VehicleStore vehicleStore,
        DocumentStore documentStore,
        DocumentInfoStore documentInfoStore,
        NavigationService<InsertDocumentInfoViewModel> insertDocumentInfoNavigationService)
    {
        _documentInfos = new ObservableCollection<DocumentInfoViewModel>();

        LoadDocumentInfosCommand = new LoadDocumentInfosCommand(this, insertDocumentViewModel, vehicleStore, documentStore, documentInfoStore);
        InsertDocumentInfoCommand = new NavigateCommand<InsertDocumentInfoViewModel>(insertDocumentInfoNavigationService);
    }

    public static DocumentsViewModel LoadViewModel(
        InsertDocumentInfoViewModel insertDocumentInfoViewModel,
        VehicleStore vehicleStore,
        DocumentStore documentStore, 
        DocumentInfoStore documentInfoStore,
        NavigationService<InsertDocumentInfoViewModel> insertDocumentInfoNavigationService)
    {
        DocumentsViewModel viewModel = new DocumentsViewModel(insertDocumentInfoViewModel, vehicleStore, documentStore, documentInfoStore, insertDocumentInfoNavigationService);

        viewModel.LoadDocumentInfosCommand.Execute(null);

        return viewModel;
    }

    public void UpdateDocumentInfos(IEnumerable<DocumentInfoViewModel> documentInfos)
    {
        _documentInfos.Clear();

        foreach (DocumentInfoViewModel docInfo in documentInfos)
        {
            _documentInfos.Add(docInfo);
        }
    }
}
