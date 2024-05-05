using GM.Services;
using GM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GM.ViewModels.Documents;
using GM.Stores;
using GM.Repositories;
using GM.ViewModels.Vehicles;

namespace GM.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddTransient<HomeViewModel>();
            services.AddSingleton<NavigationService<HomeViewModel>>();
            services.AddSingleton<Func<HomeViewModel>>(s => () => s.GetRequiredService<HomeViewModel>());

            services.AddTransient(s => InitializeDocumentsListViewModel(s));
            services.AddSingleton<NavigationService<DocumentsViewModel>>();
            services.AddSingleton<Func<DocumentsViewModel>>(s => () => s.GetRequiredService<DocumentsViewModel>());

            services.AddTransient(s => InitializeInsertDocumentInfoViewModel(s));
            services.AddSingleton<NavigationService<InsertDocumentInfoViewModel>>();
            services.AddSingleton<Func<InsertDocumentInfoViewModel>>(s => () => s.GetRequiredService<InsertDocumentInfoViewModel>());

            /** Vehicle Views */
            services.AddTransient<VehicleListViewModel>();
            services.AddSingleton<NavigationService<VehicleListViewModel>>();
            services.AddSingleton<Func<VehicleListViewModel>>(s => () => s.GetRequiredService<VehicleListViewModel>());

            services.AddTransient<InsertVehicleViewModel>();
            services.AddSingleton<NavigationService<InsertVehicleViewModel>>();
            services.AddSingleton<Func<InsertVehicleViewModel>>(s => () => s.GetRequiredService<InsertVehicleViewModel>());

            //services.AddTransient<DocumentListViewModel>();
            //services.AddSingleton<NavigationService<DocumentListViewModel>>();
            //services.AddSingleton<Func<DocumentListViewModel>>(s => () => s.GetRequiredService<DocumentListViewModel>());

            //services.AddTransient<AddDocumentViewModel>();
            //services.AddSingleton<NavigationService<AddDocumentViewModel>>();
            //services.AddSingleton<Func<AddDocumentViewModel>>((s) => () => s.GetRequiredService<AddDocumentViewModel>());

            services.AddSingleton<MainWindowViewModel>();
        });

        return hostBuilder;
    }

    private static InsertDocumentInfoViewModel InitializeInsertDocumentInfoViewModel(IServiceProvider service)
        => InsertDocumentInfoViewModel.LoadViewModel(
            service.GetRequiredService<IUserRepo>(),
            service.GetRequiredService<IDocumentRepo>(),
            service.GetRequiredService<IDocumentInfoRepo>(),
            service.GetRequiredService<IDocumentConflictValidator>(),
            service.GetRequiredService<DocumentStore>(),
            service.GetRequiredService<DocumentInfoStore>(),
            service.GetRequiredService<NavigationService<DocumentsViewModel>>());

    private static DocumentsViewModel InitializeDocumentsListViewModel(IServiceProvider service) 
        => DocumentsViewModel.LoadViewModel(
            service.GetRequiredService<DocumentStore>(),
            service.GetRequiredService<DocumentInfoStore>(),
            service.GetRequiredService<InsertDocumentInfoViewModel>(),
            service.GetRequiredService<NavigationService<InsertDocumentInfoViewModel>>());
}
