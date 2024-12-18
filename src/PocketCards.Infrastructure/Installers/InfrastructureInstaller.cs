using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PocketCards.Application.Repositories;
using PocketCards.Infrastructure.Contexts;
using PocketCards.Infrastructure.Repositories;

namespace PocketCards.Infrastructure.Installers;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PocketCards") ?? throw new InvalidOperationException("Connection string 'PocketCards' not found");

        var userSecrets = new Dictionary<string, string?>
        {
            { "<database-server>", configuration["<database-server>"] },
            { "<database-user>", configuration["<database-user>"] },
            { "<database-user-password>", configuration["<database-user-password>"] }
        };
        connectionString = connectionString.ReplaceUserSecrets(userSecrets);

        services.AddDbContext<PocketCardsDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IPocketCardRepository, PocketCardRepository>();
        services.AddScoped<IPocketPackRepository, PocketPackRepository>();

        return services;
    }

    public static IServiceProvider SeedDatabase(this IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<PocketCardsDbContext>();
        context.Database.Migrate();

        return serviceProvider;
    }

    private static string ReplaceUserSecrets(this string source, IDictionary<string, string?> secrets)
    {
        foreach (var (key, value) in secrets)
        {
            source = value != null ? source.Replace(key, value) : source;
        }

        return source;
    }
}
