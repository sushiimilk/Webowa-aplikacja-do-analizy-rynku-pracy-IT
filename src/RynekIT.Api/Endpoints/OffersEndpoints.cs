using RynekIT.Application.Offers.SearchOffers;
using RynekIT.Domain.Offers;

namespace RynekIT.Api.Endpoints;

public static class OffersEndpoints
{
    public static IEndpointRouteBuilder MapOffersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/offers").WithTags("Offers");

        group.MapGet("/", SearchOffers).WithName("SearchOffers");

        return app;
    }

    private static async Task<IResult> SearchOffers(
        string? phrase,
        WorkMode? workMode,
        int? page,
        int? pageSize,
        SearchOffersHandler handler,
        CancellationToken cancellationToken)
    {
        var query = new SearchOffersQuery(phrase, workMode, page ?? 1, pageSize ?? 20);
        var result = await handler.HandleAsync(query, cancellationToken);

        return TypedResults.Ok(result);
    }
}
