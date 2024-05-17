using GM.Stores;
using GM.Services;
using GM.Commands;
using GM.Commands.Document;

using System.Windows.Data;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GM.ViewModels.Documents;

public class DocumentsViewModel : ViewModelBase
{
    private readonly ObservableCollection<DocumentInfoViewModel> _documentInfos;

    public ICollectionView DocumentInfoCollectionView { get; }

    private string _vehicleFilter = string.Empty;
    public string VehicleFilter
    {
        get => _vehicleFilter;
        set
        {
            _vehicleFilter = value;
            OnPropertyChanged(nameof(VehicleFilter));
            DocumentInfoCollectionView.Refresh();
        }
    }

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
        DocumentInfoCollectionView = CollectionViewSource.GetDefaultView(_documentInfos);

        DocumentInfoCollectionView.Filter = FilterDocumentByVehicle;

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

    private bool FilterDocumentByVehicle(object obj)
    {
        if (obj is DocumentInfoViewModel documentInfoVM)
        {
            return documentInfoVM.VehicleCode.Contains(
                VehicleFilter, 
                StringComparison.InvariantCultureIgnoreCase) ||
                documentInfoVM.VehiclePlateNumber.Contains(
                    VehicleFilter, 
                    StringComparison.InvariantCultureIgnoreCase);
        }

        return false;
    }
}
