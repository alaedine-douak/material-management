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

    private string? _documentName;
    public string? DocumentName
    {
        get => _documentName;
        set
        {
            _documentName = value;
            OnPropertyChanged(nameof(DocumentName));
        }
    }

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
        DocumentStore documentStore,
        DocumentInfoStore documentInfoStore,
        NavigationService<DocumentsViewModel> documentsViewNavigationService)
    {
        _documentStore = documentStore;
        _docsObs = new ObservableCollection<Models.Document>();

        _vehiclesObs = GetVehicles();
        Vehicles = (CollectionView)new CollectionViewSource { Source = _vehiclesObs }.View;
        Vehicles.Filter = VehicleFilter;

        SubmitCommand = new SubmitDocumentInfoCommand(this, documentInfoRepo, documentRepo, documentInfoStore, documentsViewNavigationService);
        CancelCommand = new NavigateCommand<DocumentsViewModel>(documentsViewNavigationService);
        LoadDocumentNamesCommand = new LoadDocumentNamesCommand(this, documentStore);
        InsertDocumentNameCommand = new InsertDocumentNameCommand(this, userRepo, documentRepo, documentStore, documentConflictValidation);

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
        DocumentStore documentStore,
        DocumentInfoStore documentInfoStore,
        NavigationService<DocumentsViewModel> documentsViewNavigationService)
    {
        InsertDocumentInfoViewModel viewModel = new(
            userRepo, 
            documentRepo, 
            documentInfoRepo, 
            documentConflictValidation, 
            documentStore, 
            documentInfoStore, 
            documentsViewNavigationService);

        viewModel.LoadDocumentNamesCommand.Execute(null);
        return viewModel;
    }

    public void UpdateDocumentNames(IEnumerable<Models.Document> documents)
    {
        _docsObs.Clear();

        foreach (Models.Document doc in documents)
        {
            _docsObs.Add(doc);
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

        return vehicle.Code.ToLower().Contains(SearchVehicleText.ToLower()) ||
            vehicle.PlateNumber.ToLower().Contains(SearchVehicleText.ToLower());
    }


    // testing
    //private bool DropDownFilter()

    private static ObservableCollection<Models.Vehicle> GetVehicles()
    {
        return new ObservableCollection<Models.Vehicle>
        {
            new Models.Vehicle("F43211","MINI BUS", "NISSAN", "000021.321.16"),
            new Models.Vehicle("F09821","PICK UP", "FIAT", "1221.121.16"),
            new Models.Vehicle("F98301","c/ STATION G", "RENAULT", "32134.311.16"),
            new Models.Vehicle("F98401","BREAK T.T", "MERCEDES", "8392.119.16"),
            new Models.Vehicle("F09841","C/ CITERNE A GASOIL", "IVECO", "23211.115.16"),
            new Models.Vehicle("F89401","VIT", "MERCEDES", "98311.222.16"),
            new Models.Vehicle("F09418","MINI BUS", "TOYOTA", "0931112.211.16"),
        };
    }
}
