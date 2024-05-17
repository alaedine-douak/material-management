using GM.Data;
using GM.Stores;
using GM.Models;
using GM.Services;
using GM.ViewModels;
using GM.Extensions;
using System.Windows;
using GM.Repositories;
using GM.ViewModels.Documents;
using Microsoft.Extensions.Hosting;
using GM.Repositories.VehicleRepos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GM;

public partial class App : Application
{
    private static IHost? _hosting;
    private const string CONNECTION_STRING = "Host=localhost;Port=5432;Username=douak;Password=6772AlA@;Database=gestion_materiel";

    public static IHost Hosting => _hosting ??= CreateHostBuilder().Build();

    public static IHostBuilder CreateHostBuilder() => Host
        .CreateDefaultBuilder()
        .AddViewModels()
        .ConfigureServices(ConfigureServices);

    private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
    {
        services.AddSingleton<IGMDbContextFactory>(new GMDbContextFactory(CONNECTION_STRING));

        services.AddSingleton<NavigationStore>();

        services.AddSingleton<VehicleStore>();
        services.AddSingleton<DocumentStore>();
        services.AddSingleton<DocumentInfoStore>();

        services.AddSingleton<IUserRepo, UserRepo>();
        services.AddSingleton<IDocumentRepo, DocumentRepo>();
        services.AddSingleton<IDocumentInfoRepo, DocumentInfoRepo>();
        services.AddSingleton<IDocumentConflictValidator, DocumentConflictValidator>();

        services.AddSingleton<IVehicleRepo, VehicleRepo>();
        services.AddSingleton<IVehicleConflictValidator, VehicleConflictValidator>();

        services.AddSingleton(s => new User("gmadmin"));

        services.AddSingleton(s => new MainWindow
        {
            DataContext = s.GetRequiredService<MainWindowViewModel>()
        });
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var host = Hosting;

        try
        {
            var dbContextFactory = host.Services.GetRequiredService<IGMDbContextFactory>();
            using (GMDbContext dbContext = dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }


            var navigationService = host.Services.GetRequiredService<NavigationService<DocumentsViewModel>>();
            navigationService.Navigate();

            MainWindow = host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
            await host.StartAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message}!",
                "Erreur de connexion à la base de données",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            Current.Shutdown();
        }
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        using var host = Hosting;
        await host.StopAsync();
    }
}

