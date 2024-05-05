using GM.Commands;
using GM.Services;
using GM.Repositories;
using System.Windows.Input;
using GM.Commands.Documents;
using GM.Commands.DocumentCommands;
using System.Collections.ObjectModel;
using GM.Stores;
using GM.Models;
using System.Windows.Data;

namespace GM.ViewModels.Documents;

public class InsertDocumentInfoViewModel : ViewModelBase
{
    private readonly DocumentStore _documentStore;
    private readonly ObservableCollection<Models.Document> _docsObs;
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


    private Models.Document _selectedDocument = new("");
    public Models.Document SelectedDocument
    {
        get => _selectedDocument; 
        set 
        {
            _selectedDocument = value;
            OnPropertyChanged(nameof(SelectedDocument));
        }
    }

    // Testing
    private readonly ObservableCollection<Models.Vehicle> _vehicleObs;
    public CollectionView Vehicles { get; }



    private Vehicle? _selectedVehicle;
    public Vehicle? SelectedVehicle
    {
        get => _selectedVehicle;
        set
        {
            if (value != null)
            {
                _selectedVehicle = value;
                OnPropertyChanged(nameof(SelectedVehicle));
                //SearchVehicle = _selectedVehicle.Code ?? _selectedVehicle.PlateNumber;
            }
        }
    }

    //private Vehicle? _searchVehicle;
    //public Vehicle? SearchVehicle
    //{
    //    get => _searchVehicle;
    //    set
    //    {
    //        if (value != null)
    //        {
    //            _searchVehicle = value;
    //            OnPropertyChanged(nameof(SearchVehicle));

    //            //Vehicles.Refresh();

    //            //if (_searchVehicle != SelectedVehicle?.Code ||
    //            //    _searchVehicle != SelectedVehicle?.PlateNumber)
    //            //{
    //            //    Vehicles.Refresh();
    //            //}
    //        }
    //    }
    //}


    private string? _searchVehicle;
    public string? SearchVehicle
    {
        get => _searchVehicle;
        set
        {
            if (value != null)
            {
                _searchVehicle = value;
                OnPropertyChanged(nameof(SearchVehicle));

                Vehicles.Refresh();

                if (_searchVehicle != SelectedVehicle?.Code ||
                    _searchVehicle != SelectedVehicle?.PlateNumber)
                {
                    Vehicles.Refresh();
                }
            }
        }
    }




    //private string? _selectedDocumentName;
    //public string? SelectedDocumentName
    //{
    //    get => _selectedDocumentName;
    //    set
    //    {
    //        _selectedDocumentName = value;
    //        OnPropertyChanged(nameof(SelectedDocumentName));
    //    }
    //}


    //private ObservableCollection<DocumentViewModel> _documentNames;

    //public IEnumerable<DocumentViewModel> DocumentNames
    //{
    //    get
    //    {
    //        var documentNamesList = new List<DocumentViewModel>();

    //        foreach (var documentName in _documentRepo.GetDocuments()) 
    //        {
    //            documentNamesList.Add(new DocumentViewModel(documentName));
    //        }

    //        return documentNamesList;
    //    }
    //}



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



        _vehicleObs = GetVehicles();
        Vehicles = (CollectionView) new CollectionViewSource { Source = _vehicleObs }.View;
        Vehicles.Filter = DropDownFilter;



        SubmitCommand = new SubmitDocumentInfoCommand(this, documentInfoRepo, documentRepo, documentInfoStore, documentsViewNavigationService);
        CancelCommand = new NavigateCommand<DocumentsViewModel>(documentsViewNavigationService);
        LoadDocumentNamesCommand = new LoadDocumentNamesCommand(this, documentStore);
        InsertDocumentNameCommand = new InsertDocumentNameCommand(this, userRepo, documentRepo, documentStore, documentConflictValidation);


        documentStore.DocumentInserted += OnDocumentInserted;
    }

    private bool DropDownFilter(object obj)
    {
        Vehicle? vehicle = obj as Vehicle;

        if (vehicle is null) return false;

        if (string.IsNullOrEmpty(SearchVehicle)) return true;

        return vehicle.Code.ToLower().Contains(SearchVehicle.ToLower()) ||
            vehicle.PlateNumber.Contains(SearchVehicle.ToLower());

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

    private void OnDocumentInserted(Document document)
    {
        _docsObs.Add(document);
    }




    // testing
    //private bool DropDownFilter()

    private static ObservableCollection<Vehicle> GetVehicles()
    {
        return new ObservableCollection<Vehicle>
        {
            new Vehicle("F43211","MINI BUS", "NISSAN", "000021.321.16"),
            new Vehicle("F09821","PICK UP", "FIAT", "1221.121.16"),
            new Vehicle("F98301","c/ STATION G", "RENAULT", "32134.311.16"),
            new Vehicle("F98401","BREAK T.T", "MERCEDES", "8392.119.16"),
            new Vehicle("F09841","C/ CITERNE A GASOIL", "IVECO", "23211.115.16"),
            new Vehicle("F89401","VIT", "MERCEDES", "98311.222.16"),
            new Vehicle("F09418","MINI BUS", "TOYOTA", "0931112.211.16"),
        };
    }
}
