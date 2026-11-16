using System.Reflection;

namespace RynekIT.Infrastructure.Persistence.Sql;

/// <summary>
/// Odczyt skryptow SQL (procedury itp.) osadzonych w assembly jako EmbeddedResource.
/// Pliki *.sql w tym folderze sa wersjonowane (v1, v2, ...) i nigdy nie zmieniane po wypchnieciu,
/// zeby Up/Down kazdej migracji zawsze odtwarzaly dokladnie ten sam tekst.
/// </summary>
internal static class SqlScripts
{
    public const string ProcessRawOffersV1 = "usp_ProcessRawOffers.v1.sql";
    public const string ProcessRawOffersV2 = "usp_ProcessRawOffers.v2.sql";
    public const string MergeSkillAliases = "usp_MergeSkillAliases.sql";

    public static string Load(string fileName)
    {
        var assembly = typeof(SqlScripts).Assembly;
        var resource = assembly.GetManifestResourceNames()
            .SingleOrDefault(n => n.EndsWith(".Persistence.Sql." + fileName, StringComparison.Ordinal))
            ?? throw new InvalidOperationException($"Brak osadzonego skryptu SQL: {fileName}");

        using var stream = assembly.GetManifestResourceStream(resource)!;
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
