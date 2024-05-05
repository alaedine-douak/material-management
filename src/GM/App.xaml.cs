using GM.Data;
using GM.Stores;
using GM.Models;
using GM.Services;
using GM.ViewModels;
using GM.Extensions;
using System.Windows;
using GM.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GM.Repositories.VehicleRepos;

namespace GM;

public partial class App : Application
{
    //private readonly IHost _host;
    //public App() => _host = CreateHostBuilder().Build();

    private static IHost? __Hosting;

    public static IHost Hosting => __Hosting ??= CreateHostBuilder().Build();

    public static IHostBuilder CreateHostBuilder() => Host
        .CreateDefaultBuilder()
        .AddViewModels()
        .ConfigureServices(ConfigureServices);

    private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
    {
        string? connectionString = hostContext.Configuration.GetConnectionString("GMConnectionString");
        services.AddSingleton<IGMDbContextFactory>(new GMDbContextFactory(connectionString));

        services.AddSingleton<NavigationStore>();

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

        var dbContextFactory = host.Services.GetRequiredService<IGMDbContextFactory>();
        using (GMDbContext dbContext = dbContextFactory.CreateDbContext())
        {
            dbContext.Database.Migrate();
        }

        //using (var serviceScope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //{
        //    serviceScope.ServiceProvider.GetRequiredService<GMDbContext>().Database.Migrate();
        //}


        var navigationService = host.Services.GetRequiredService<NavigationService<HomeViewModel>>();
        navigationService.Navigate();

        MainWindow = host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
        await host.StartAsync();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        using var host = Hosting;
        await host.StopAsync();
    }

    //private static IHostBuilder CreateHostBuilder()
    //    => Host
    //        .CreateDefaultBuilder()
    //        .AddViewModels()
    //        .ConfigureServices((hostContext, services) =>
    //        {
    //            var connectionString = hostContext.Configuration.GetConnectionString("GMConnectionString");

    //            services.AddDbContext<GMDbContext>(options =>
    //            {
    //                options.UseNpgsql(connectionString);
    //                options.UseLazyLoadingProxies();
    //            });

    //            services.AddScoped<NavigationStore>();
    //            services.AddScoped<DocumentStore>();
    //            services.AddScoped<DocumentInfoStore>();

    //            services.AddScoped<IUserRepository, UserRepository>();
    //            services.AddScoped<IDocumentRepo, DocumentRepo>();
    //            services.AddScoped<IDocumentInfoRepo, DocumentInfoRepo>();
    //            services.AddScoped<IDocumentConflictValidator, DocumentConflictValidator>();


    //            services.AddSingleton(s => new User("gmadmin"));


    //            services.AddSingleton(s => new MainWindow
    //            {
    //                DataContext = s.GetRequiredService<MainWindowViewModel>()
    //            });
    //        });
}

