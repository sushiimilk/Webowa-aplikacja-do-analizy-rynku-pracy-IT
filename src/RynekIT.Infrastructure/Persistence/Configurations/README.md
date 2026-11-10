# Persistence/Configurations – jak encje wyglądają w bazie

Encje w Domain są „czyste” – nie wiedzą nic o bazie. Tutaj mówimy Entity Frameworkowi, jak je zapisać w tabelach: maksymalne długości tekstów, indeksy, klucze złożone, wartości domyślne, relacje.

**Zasada: jedna encja = jeden plik** `NazwaEncjiConfiguration.cs`.

## Jak dodać konfigurację

```csharp
internal sealed class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(100);   // kolumna nvarchar(100)
        builder.HasIndex(x => x.Name).IsUnique();          // nazwa nie może się powtarzać
    }
}
```

Klasa jest wykrywana automatycznie. Po każdej zmianie tutaj **trzeba zrobić migrację** (patrz `Migrations/README.md`).
