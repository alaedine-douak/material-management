using GM.Services;
using GM.ViewModels.Document;
using GM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GM.ViewModels.Documents;

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

            services.AddTransient<DocumentsListViewModel>();
            services.AddSingleton<NavigationService<DocumentsListViewModel>>();
            services.AddSingleton<Func<DocumentsListViewModel>>(s => () => s.GetRequiredService<DocumentsListViewModel>());

            services.AddTransient<DocumentListViewModel>();
            services.AddSingleton<NavigationService<DocumentListViewModel>>();
            services.AddSingleton<Func<DocumentListViewModel>>(s => () => s.GetRequiredService<DocumentListViewModel>());

            services.AddTransient<AddDocumentViewModel>();
            services.AddSingleton<NavigationService<AddDocumentViewModel>>();
            services.AddSingleton<Func<AddDocumentViewModel>>((s) => () => s.GetRequiredService<AddDocumentViewModel>());

            services.AddSingleton<MainWindowViewModel>();
        });

        return hostBuilder;
    }
}
