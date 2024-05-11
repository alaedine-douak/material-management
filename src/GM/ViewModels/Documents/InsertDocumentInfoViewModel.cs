using GM.Stores;
using GM.Commands;
using GM.Services;
using GM.Repositories;
using System.Windows.Data;
using System.Windows.Input;
using GM.Commands.Documents;
using GM.Commands.DocumentCommands;
using System.Collections.ObjectModel;

namespace GM.ViewModels.Documents;

public class InsertDocumentInfoViewModel : ViewModelBase
{
    private readonly VehicleStore _vehicleStore;
    private readonly DocumentStore _documentStore;
    private readonly ObservableCollection<Models.Document> _docsObs;
    private readonly ObservableCollection<Models.Vehicle> _vehiclesObs;

    public CollectionView Vehicles { get; }
    public IEnumerable<Models.Document> Documents => _docsObs;


    private string? _documentNumber;
    public string? DocumentNumber
    {
        get => _documentNumber;
        set
        {
            _documentNumber = value;
            OnPropertyChanged(nameof(DocumentNumber));
        }
    }

    private DateTime _issuedDate = DateTime.Today;
    public DateTime IssuedDate
    {
        get => _issuedDate;
        set
        {
            _issuedDate = value;
            OnPropertyChanged(nameof(IssuedDate));
        }
    }

    private DateTime _endDate = DateTime.Today;
    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }

    #region Document
    private string _documentName = string.Empty;
    public string DocumentName
    {
        get => _documentName;
        set
        {
            _documentName = value;
            OnPropertyChanged(nameof(DocumentName));
        }
    }

    private string _alartedDuration = string.Empty ;
    public string AlartedDuration
    {
        get => _alartedDuration;
        set
        {
            _alartedDuration = value;
            OnPropertyChanged(nameof(AlartedDuration));
        }
    }
    #endregion

    private Models.Document? _selectedDocument;
    public Models.Document? SelectedDocument
    {
        get => _selectedDocument; 
        set 
        {
            _selectedDocument = value;
            OnPropertyChanged(nameof(SelectedDocument));
        }
    }

    private Models.Vehicle? _selectedVehicle;
    public Models.Vehicle? SelectedVehicle
    {
        get => _selectedVehicle;
        set
        {
            if (value is not null)
            {
                _selectedVehicle = value;
                OnPropertyChanged(nameof(SelectedVehicle));
                SearchVehicleText = SelectedVehicle?.VehicleComboBoxText;
            }
        }
    }

    private string? _searchVehicleText;
    public string? SearchVehicleText
    {
        get => _searchVehicleText;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _searchVehicleText = value;
                OnPropertyChanged(nameof(SearchVehicleText));

                if (SearchVehicleText != SelectedVehicle?.VehicleComboBoxText) Vehicles.Refresh();
            }
            else
            {
                SelectedVehicle = null;
                OnPropertyChanged(nameof(SelectedVehicle));

                Vehicles.Refresh();
            }
        }
    }


    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand LoadDocumentNamesCommand { get; }
    public ICommand InsertDocumentNameCommand { get; }

    public InsertDocumentInfoViewModel(
        IUserRepo userRepo,
        IDocumentRepo documentRepo,
        IDocumentInfoRepo documentInfoRepo,
        IDocumentConflictValidator documentConflictValidation,
        VehicleStore vehicleStore,
        DocumentStore documentStore,
        DocumentInfoStore documentInfoStore,
        NavigationService<DocumentsViewModel> documentsViewNavigationService)
    {
        _vehicleStore = vehicleStore;
        _documentStore = documentStore;

        _docsObs = new ObservableCollection<Models.Document>();
        _vehiclesObs = new ObservableCollection<Models.Vehicle>();

        SubmitCommand = new SubmitDocumentInfoCommand(this, documentInfoRepo, documentRepo, documentInfoStore, documentsViewNavigationService);
        CancelCommand = new NavigateCommand<DocumentsViewModel>(documentsViewNavigationService);
        LoadDocumentNamesCommand = new LoadDocumentNamesCommand(this, documentStore); 
        InsertDocumentNameCommand = new InsertDocumentNameCommand(this, userRepo, documentRepo, documentStore, documentConflictValidation);

        UpdateVehicles();
        Vehicles = (CollectionView)new CollectionViewSource { Source = _vehiclesObs }.View;
        Vehicles.Filter = VehicleFilter;

        documentStore.DocumentInserted += OnDocumentInserted;
    }

    public override void Dispose()
    {
        _documentStore.DocumentInserted -= OnDocumentInserted;
        base.Dispose();
    }

    public static InsertDocumentInfoViewModel LoadViewModel(
        IUserRepo userRepo,
        IDocumentRepo documentRepo,
        IDocumentInfoRepo documentInfoRepo,
        IDocumentConflictValidator documentConflictValidation,
        VehicleStore vehicleStore,
        DocumentStore documentStore,
        DocumentInfoStore documentInfoStore,
        NavigationService<DocumentsViewModel> documentsViewNavigationService)
    {
        InsertDocumentInfoViewModel insertDocumentInfoViewModel = new(
            userRepo, 
            documentRepo, 
            documentInfoRepo, 
            documentConflictValidation, 
            vehicleStore,
            documentStore, 
            documentInfoStore, 
            documentsViewNavigationService);

        insertDocumentInfoViewModel.LoadDocumentNamesCommand.Execute(null);
        return insertDocumentInfoViewModel;
    }

    public void UpdateDocumentNames(IEnumerable<Models.Document> documents)
    {
        _docsObs.Clear();

        foreach (Models.Document doc in documents)
        {
            _docsObs.Add(doc);
        }
    }

    private void UpdateVehicles()
    {
        _vehiclesObs.Clear();

        foreach (Models.Vehicle vehicle in _vehicleStore.Vehicles)
        {
            _vehiclesObs.Add(vehicle);
        }
    }

    private void OnDocumentInserted(Models.Document document)
    {
        _docsObs.Add(document);
    }

    private bool VehicleFilter(object obj)
    {
        var vehicle = obj as Models.Vehicle;

        if (vehicle == null) return false;

        if (string.IsNullOrEmpty(SearchVehicleText)) return true;

        return vehicle.Code!.ToLower().Contains(SearchVehicleText.ToLower()) ||
            vehicle.PlateNumber!.ToLower().Contains(SearchVehicleText.ToLower());
    }
}
