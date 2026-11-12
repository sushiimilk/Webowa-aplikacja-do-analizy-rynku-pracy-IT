using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RynekIT.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessRawOffersProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE OR ALTER PROCEDURE stg.usp_ProcessRawOffers
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- #1. tabela tymczasowa
    CREATE TABLE #batch (
        RawId          bigint        NOT NULL PRIMARY KEY,
        Source         nvarchar(50)  NOT NULL,
        SourceOfferId  nvarchar(100) NOT NULL,
        SourceUrl      nvarchar(500) NOT NULL,
        Company        nvarchar(200) NULL,
        Title          nvarchar(300) NULL,
        Location       nvarchar(200) NULL,
        WorkMode       nvarchar(20)  NOT NULL,
        SalaryMin      decimal(12,2) NULL,
        SalaryMax      decimal(12,2) NULL,
        SalaryCurrency nchar(3)      NULL,
        SalaryPeriod   nvarchar(10)  NULL,
        PublishedAt    datetime2     NULL,
        Description    nvarchar(max) NULL,
        FetchedAt      datetime2     NOT NULL,
        Tags           nvarchar(max) NULL
    );

    -- #2. wyciaganie pol z RawJson
    INSERT INTO #batch (RawId, Source, SourceOfferId, SourceUrl, Company, Title, Location,
                        WorkMode, SalaryMin, SalaryMax, PublishedAt, Description, FetchedAt, Tags)
    SELECT
        r.Id, r.Source, r.SourceOfferId, r.SourceUrl,
        LEFT(NULLIF(TRIM(j.Company),  ''), 200),
        LEFT(NULLIF(TRIM(j.Position), ''), 300),
        LEFT(NULLIF(TRIM(j.Location), ''), 200),
        N'Remote',
        NULLIF(j.SalaryMin, 0),
        NULLIF(j.SalaryMax, 0),
        CONVERT(datetime2, SWITCHOFFSET(TRY_CONVERT(datetimeoffset, j.PublishedAt), '+00:00')),
        j.Description,
        r.FetchedAt,
        j.Tags
    FROM stg.RawOffers r
    CROSS APPLY OPENJSON(r.RawJson)
    WITH (
        Company     nvarchar(400)  '$.company',
        Position    nvarchar(600)  '$.position',
        Location    nvarchar(400)  '$.location',
        SalaryMin   decimal(12,2)  '$.salary_min',
        SalaryMax   decimal(12,2)  '$.salary_max',
        PublishedAt nvarchar(50)   '$.date',
        Description nvarchar(max)  '$.description',
        Tags        nvarchar(max)  '$.tags' AS JSON
    ) j
    WHERE r.ProcessedAt IS NULL
      AND r.Source = N'remoteok'
      AND ISJSON(r.RawJson) = 1;

    -- #3. odwrocone widelki
    UPDATE #batch
    SET SalaryMin = SalaryMax, SalaryMax = SalaryMin
    WHERE SalaryMin > SalaryMax;

    -- #4. USD i year
    UPDATE #batch
    SET SalaryCurrency = N'USD', SalaryPeriod = N'Year'
    WHERE SalaryMin IS NOT NULL OR SalaryMax IS NOT NULL;

    -- #5. usuwanie ofert bez firmy lub bez tytulu
    DECLARE @skipped int;
    DELETE FROM #batch WHERE Company IS NULL OR Title IS NULL;
    SET @skipped = @@ROWCOUNT;

    -- #6. tagi na osoben wiersze
    CREATE TABLE #tags (
        RawId     bigint        NOT NULL,
        SkillName nvarchar(100) NOT NULL
    );

    INSERT INTO #tags (RawId, SkillName)
    SELECT DISTINCT b.RawId, LEFT(LOWER(TRIM(t.value)), 100)
    FROM #batch b
    CROSS APPLY OPENJSON(b.Tags) t
    WHERE NULLIF(TRIM(t.value), '') IS NOT NULL;

    -- #7.mapowanie wiersz stagingu-> Id oferty
    DECLARE @map TABLE (RawId bigint, OfferId int, Akcja nvarchar(10));

    BEGIN TRY
        BEGIN TRANSACTION;

        -- #8. poczatek tranzakcji
        INSERT INTO dbo.Companies (Name)
        SELECT DISTINCT b.Company
        FROM #batch b
        WHERE NOT EXISTS (SELECT 1 FROM dbo.Companies c WHERE c.Name = b.Company);

        -- #9.insert nowych i update instniejacych ofert
        MERGE dbo.Offers AS t
        USING (
            SELECT b.*, c.Id AS CompanyId
            FROM #batch b
            JOIN dbo.Companies c ON c.Name = b.Company
        ) AS s
        ON t.Source = s.Source AND t.SourceOfferId = s.SourceOfferId
        WHEN MATCHED THEN UPDATE SET
            t.CompanyId      = s.CompanyId,
            t.SourceUrl      = s.SourceUrl,
            t.Title          = s.Title,
            t.Description    = s.Description,
            t.Location       = s.Location,
            t.WorkMode       = s.WorkMode,
            t.SalaryMin      = s.SalaryMin,
            t.SalaryMax      = s.SalaryMax,
            t.SalaryCurrency = s.SalaryCurrency,
            t.SalaryPeriod   = s.SalaryPeriod,
            t.PublishedAt    = s.PublishedAt,
            t.FetchedAt      = s.FetchedAt
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (CompanyId, Source, SourceOfferId, SourceUrl, Title, Description, Location,
                    WorkMode, SalaryMin, SalaryMax, SalaryCurrency, SalaryPeriod, PublishedAt, FetchedAt)
            VALUES (s.CompanyId, s.Source, s.SourceOfferId, s.SourceUrl, s.Title, s.Description, s.Location,
                    s.WorkMode, s.SalaryMin, s.SalaryMax, s.SalaryCurrency, s.SalaryPeriod, s.PublishedAt, s.FetchedAt)
        OUTPUT s.RawId, inserted.Id, $action INTO @map;

        -- #10. dodanie tech skill do slownika
        INSERT INTO dbo.Skills (Name)
        SELECT DISTINCT tg.SkillName
        FROM #tags tg
        WHERE NOT EXISTS (SELECT 1 FROM dbo.Skills s WHERE s.Name = tg.SkillName);

        -- #11. odswiezenie powiazan oferta-tech
        DELETE os
        FROM dbo.OfferSkills os
        JOIN @map m ON m.OfferId = os.OfferId;

        INSERT INTO dbo.OfferSkills (OfferId, SkillId)
        SELECT DISTINCT m.OfferId, s.Id
        FROM #tags tg
        JOIN @map m       ON m.RawId = tg.RawId
        JOIN dbo.Skills s ON s.Name  = tg.SkillName;

        -- #12. oznaczanie wierszy stagingu jako przetworzone
        UPDATE r
        SET ProcessedAt = SYSUTCDATETIME()
        FROM stg.RawOffers r
        JOIN @map m ON m.RawId = r.Id;

        -- #13. zatwiredzneie lub odrzucenie trans
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;

    -- #14. podsumowanie 
    SELECT
        ISNULL(SUM(CASE WHEN Akcja = 'INSERT' THEN 1 ELSE 0 END), 0) AS NoweOferty,
        ISNULL(SUM(CASE WHEN Akcja = 'UPDATE' THEN 1 ELSE 0 END), 0) AS Zaktualizowane,
        @skipped                                                     AS Pominiete
    FROM @map;
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS stg.usp_ProcessRawOffers;");
        }
    }
}
