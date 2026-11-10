using Microsoft.Extensions.DependencyInjection;
using RynekIT.Application.Offers.SearchOffers;

namespace RynekIT.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<SearchOffersHandler>();

        return services;
    }
}
