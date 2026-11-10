using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RynekIT.Application.Abstractions;
using RynekIT.Infrastructure.Persistence;

namespace RynekIT.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment)
    {
        var connectionString = BuildConnectionString(configuration);

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);

            if (isDevelopment)
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

        return services;
    }

    private static string BuildConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Brak ConnectionStrings:Default. Konfiguracja bazy jest opisana w README.md w sekcji \"Baza danych\".");
        }

        var password = configuration["Database:Password"];
        if (string.IsNullOrEmpty(password))
        {
            return connectionString;
        }

        return new SqlConnectionStringBuilder(connectionString) { Password = password }.ConnectionString;
    }
}
