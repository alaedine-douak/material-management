using GM.Data;
using GM.Models;
using GM.Stores;
using GM.Services;
using GM.ViewModels;
using GM.Extensions;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GM;

public partial class App : Application
{
    private readonly IHost _host;
    public App() => _host = CreateHostBuilder().Build();

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        //using (var serviceScope = _host.Services.CreateScope())
        //{
        //    var context = serviceScope.ServiceProvider.GetRequiredService<GMDbContext>();
        //    context.Database.Migrate();
        //}


        var navigationService = _host.Services.GetRequiredService<NavigationService<HomeViewModel>>();
        navigationService.Navigate();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host.Dispose();

        base.OnExit(e);
    }

    private static IHostBuilder CreateHostBuilder()
        => Host
            .CreateDefaultBuilder()
            .AddViewModels()
            .ConfigureServices((hostContext, services) =>
            {
                var connectionString = hostContext.Configuration.GetConnectionString("GMConnectionString");

                services.AddDbContext<GMDbContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                    options.UseLazyLoadingProxies();
                });

                services.AddSingleton<NavigationStore>();

                services.AddSingleton(s => new User("gmadmin"));


                services.AddSingleton(s => new MainWindow
                {
                    DataContext = s.GetRequiredService<MainWindowViewModel>()
                });
            });
}

