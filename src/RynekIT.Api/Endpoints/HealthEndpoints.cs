namespace RynekIT.Api.Endpoints;

public static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/health", () => TypedResults.Ok(new { status = "ok", time = DateTime.UtcNow }))
            .WithTags("Health")
            .WithName("GetHealth");

        return app;
    }
}
