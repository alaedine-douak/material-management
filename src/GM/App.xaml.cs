using GM.Models;
using GM.Stores;
using GM.ViewModels;
using GM.ViewModels.Document;
using System.Windows;

namespace GM;

public partial class App : Application
{
    //private readonly IHost _host;
    private readonly User _user;
    private readonly NavigationStore _navigationStore;

    public App()
    {
        _user = new User("Alaedine");
        _navigationStore = new NavigationStore();

        //_host = Host
        //    .CreateDefaultBuilder()
        //    .ConfigureServices(service =>
        //    {

        //        service.AddTransient<MainWindowViewModel>();

        //        service.AddSingleton(s => new MainWindow
        //        {
        //            DataContext = s.GetRequiredService<MainWindowViewModel>()
        //        });
        //    })
        //    .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        //User user = new("Alaedine");
        //Document d1 = new("D1");
        //DocumentInfo docInfo = new(d1, "342", DateTime.Now, DateTime.Now);
        //DocumentInfo docInfo2 = new(d1, "341", DateTime.Now, DateTime.Now);
        //DocumentInfo docInfo3 = new(d1, "34a", DateTime.Now, DateTime.Now);
        //Vehicle vcl = new("E0302722", "019600.314.16", "Nissan", "PICK-UP");

        //try
        //{
        //    user.AddDocument(d1);
        //    user.AddDocumentInfo(docInfo);

        //    user.AddVehicle(vcl);
        //    vcl.AddDocumentInfo(docInfo);
        //    vcl.AddDocumentInfo(docInfo2);
        //    vcl.AddDocumentInfo(docInfo3);



        //}
        //catch (DocumentException) { }
        //catch (DocumentInfoException) { }
        //catch (VehicleException) { }

        //IEnumerable<Vehicle> vcls = user.GetAllVehicles();

        //_host.Start();

        //MainWindow = _host.Services.GetRequiredService<MainWindow>();    
        //MainWindow.Show();

        _navigationStore.CurrentViewModel = CreateDocumentListViewModel();

        MainWindow = new MainWindow
        {
            DataContext = new MainViewModel(_navigationStore)
        };

        MainWindow.Show();

        base.OnStartup(e);
    }

    private AddDocumentViewModel createAddDocumentViewModel()
        => new AddDocumentViewModel(
            _user,
            _navigationStore,
            CreateDocumentListViewModel);

    private DocumentListViewModel CreateDocumentListViewModel()
        => new DocumentListViewModel(
            _navigationStore,
            createAddDocumentViewModel);
}

