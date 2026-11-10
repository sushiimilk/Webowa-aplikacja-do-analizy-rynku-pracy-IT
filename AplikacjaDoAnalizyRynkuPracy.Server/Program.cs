using AplikacjaDoAnalizyRynkuPracy.Server.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = BuildConnectionString(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();

static string BuildConnectionString(IConfiguration configuration)
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
