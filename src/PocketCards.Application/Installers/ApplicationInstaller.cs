using Microsoft.Extensions.DependencyInjection;
using PocketCards.Application.Services;
using PocketCards.Domain.Services;

namespace PocketCards.Application.Installers;

public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPocketCardService, PocketCardService>();
        services.AddScoped<IPocketPackService, PocketPackService>();

        return services;
    }
}
