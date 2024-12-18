using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PocketCards.Application.Installers;
using PocketCards.Infrastructure.Installers;

namespace PocketCards.DataLoader;

internal class Program
{
    static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
                       .ConfigureAppConfiguration((context, app) =>
                       {
                           app.AddUserSecrets<Program>();
                       })
                       .ConfigureServices((context, services) =>
                       {
                           services.AddHostedService<App>();
                           services.AddApplication(context.Configuration);
                           services.AddInfrastructure(context.Configuration);
                       })
                       .Build();

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        services.SeedDatabase();

        await host.RunAsync();
    }
}
