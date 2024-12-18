using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PocketCards.Application.Options;
using PocketCards.Application.Services;
using PocketCards.Domain.Services;

namespace PocketCards.Application.Installers;

public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CdnOptions>(configuration.GetSection(nameof(CdnOptions)));

        services.AddScoped<IPocketCardService, PocketCardService>();
        services.AddScoped<IPocketPackService, PocketPackService>();

        return services;
    }
}
