using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RynekIT.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillCategoryAndAliases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // #1. schemat wygenerowany przez EF
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Skills",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Other");

            migrationBuilder.CreateTable(
                name: "SkillAliases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillAliases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillAliases_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillAliases_Alias",
                table: "SkillAliases",
                column: "Alias",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillAliases_SkillId",
                table: "SkillAliases",
                column: "SkillId");

            // #2. slownik znanych skilli z kategoriami (dodane tylko jesli ich brak)
            migrationBuilder.Sql(@"
INSERT INTO dbo.Skills (Name, Category)
SELECT v.Name, v.Category
FROM (VALUES
    ('golang', 'Technology'), ('python', 'Technology'), ('sql', 'Technology'),
    ('excel', 'Technology'), ('javascript', 'Technology'), ('typescript', 'Technology'),
    ('node', 'Technology'), ('react', 'Technology'), ('html', 'Technology'),
    ('postgres', 'Technology'), ('docker', 'Technology'), ('git', 'Technology'),
    ('c', 'Technology'), ('microsoft', 'Technology'), ('api', 'Technology'),
    ('cloud', 'Technology'), ('ai', 'Technology'), ('kubernetes', 'Technology'),
    ('power bi', 'Technology'),
    ('exec', 'Role'), ('customer support', 'Role'), ('engineer', 'Role'),
    ('dev', 'Role'), ('ops', 'Role'), ('marketing', 'Role'), ('design', 'Role'),
    ('technical', 'Role'), ('sys admin', 'Role'), ('recruiter', 'Role'),
    ('sales', 'Role'), ('testing', 'Role'), ('infosec', 'Role'), ('backend', 'Role'),
    ('front end', 'Role'), ('full stack', 'Role'), ('mobile', 'Role'),
    ('quality assurance', 'Role'), ('product manager', 'Role'),
    ('content writing', 'Role'), ('admin', 'Role'), ('coordinator', 'Role'),
    ('devops', 'Role'), ('virtual assistant', 'Role'), ('analyst', 'Role'), ('hr', 'Role'),
    ('senior', 'Level'), ('junior', 'Level'), ('mid', 'Level'),
    ('lead', 'Level'), ('intern', 'Level')
) v (Name, Category)
WHERE NOT EXISTS (SELECT 1 FROM dbo.Skills s WHERE s.Name = v.Name);
");

            // #3. aliasy: wariant zapisu -> skill docelowy
            migrationBuilder.Sql(@"
INSERT INTO dbo.SkillAliases (Alias, SkillId)
SELECT v.Alias, s.Id
FROM (VALUES
    ('node.js',   'node'),
    ('nodejs',    'node'),
    ('postgresql','postgres'),
    ('js',        'javascript'),
    ('ts',        'typescript'),
    ('go',        'golang'),
    ('react.js',  'react'),
    ('reactjs',   'react'),
    ('k8s',       'kubernetes'),
    ('power-bi',  'power bi'),
    ('powerbi',   'power bi'),
    ('frontend',  'front end'),
    ('front-end', 'front end'),
    ('back end',  'backend'),
    ('back-end',  'backend'),
    ('fullstack', 'full stack'),
    ('full-stack','full stack'),
    ('qa',        'quality assurance'),
    ('sysadmin',  'sys admin')
) v (Alias, Canonical)
JOIN dbo.Skills s ON s.Name = v.Canonical
WHERE NOT EXISTS (SELECT 1 FROM dbo.SkillAliases a WHERE a.Alias = v.Alias);
");

            // #4. scalenie duplikatow, ktore juz sa w bazie (np. node.js -> node)
            migrationBuilder.Sql(@"
INSERT INTO dbo.OfferSkills (OfferId, SkillId)
SELECT DISTINCT os.OfferId, a.SkillId
FROM dbo.OfferSkills os
JOIN dbo.Skills dup     ON dup.Id = os.SkillId
JOIN dbo.SkillAliases a ON a.Alias = dup.Name
WHERE dup.Id <> a.SkillId
  AND NOT EXISTS (SELECT 1 FROM dbo.OfferSkills x
                  WHERE x.OfferId = os.OfferId AND x.SkillId = a.SkillId);

DELETE os
FROM dbo.OfferSkills os
JOIN dbo.Skills dup     ON dup.Id = os.SkillId
JOIN dbo.SkillAliases a ON a.Alias = dup.Name
WHERE dup.Id <> a.SkillId;

DELETE dup
FROM dbo.Skills dup
JOIN dbo.SkillAliases a ON a.Alias = dup.Name
WHERE dup.Id <> a.SkillId
  AND NOT EXISTS (SELECT 1 FROM dbo.CvSkills cs WHERE cs.SkillId = dup.Id);
");

            // #5. klasyfikacja istniejacych tagow (reszta zostaje jako Other)
            migrationBuilder.Sql(@"
UPDATE dbo.Skills SET Category = 'Technology'
WHERE Name IN ('golang','python','sql','excel','javascript','typescript','node','react',
               'html','postgres','docker','git','c','microsoft','api','cloud','ai',
               'kubernetes','power bi');

UPDATE dbo.Skills SET Category = 'Role'
WHERE Name IN ('exec','customer support','engineer','dev','ops','marketing','design',
               'technical','sys admin','recruiter','sales','testing','infosec','backend',
               'front end','full stack','mobile','quality assurance','product manager',
               'content writing','admin','coordinator','devops','virtual assistant',
               'analyst','hr');

UPDATE dbo.Skills SET Category = 'Level'
WHERE Name IN ('senior','junior','mid','lead','intern');
");

            // #6. procedura ETL z obsluga aliasow
            migrationBuilder.Sql(Procedure(TagsWithAliases));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(Procedure(TagsWithoutAliases));

            migrationBuilder.DropTable(
                name: "SkillAliases");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Skills");
        }

        private static string Procedure(string tagsInsert) =>
            ProcedureTemplate.Replace("/*TAGS*/", tagsInsert);

        private const string TagsWithAliases = @"
    INSERT INTO #tags (RawId, SkillName)
    SELECT DISTINCT x.RawId, COALESCE(s.Name, x.Tag)
    FROM (
        SELECT b.RawId, LEFT(LOWER(TRIM(t.value)), 100) AS Tag
        FROM #batch b
        CROSS APPLY OPENJSON(b.Tags) t
        WHERE NULLIF(TRIM(t.value), '') IS NOT NULL
    ) x
    LEFT JOIN dbo.SkillAliases a ON a.Alias = x.Tag
    LEFT JOIN dbo.Skills s       ON s.Id = a.SkillId;";

        private const string TagsWithoutAliases = @"
    INSERT INTO #tags (RawId, SkillName)
    SELECT DISTINCT b.RawId, LEFT(LOWER(TRIM(t.value)), 100)
    FROM #batch b
    CROSS APPLY OPENJSON(b.Tags) t
    WHERE NULLIF(TRIM(t.value), '') IS NOT NULL;";

        private const string ProcedureTemplate = @"
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

    -- #6. tagi na osobne wiersze (z zamiana aliasow na skill docelowy)
    CREATE TABLE #tags (
        RawId     bigint        NOT NULL,
        SkillName nvarchar(100) NOT NULL
    );
/*TAGS*/

    -- #7. mapowanie wiersz stagingu -> Id oferty
    DECLARE @map TABLE (RawId bigint, OfferId int, Akcja nvarchar(10));

    BEGIN TRY
        BEGIN TRANSACTION;

        -- #8. nowe firmy
        INSERT INTO dbo.Companies (Name)
        SELECT DISTINCT b.Company
        FROM #batch b
        WHERE NOT EXISTS (SELECT 1 FROM dbo.Companies c WHERE c.Name = b.Company);

        -- #9. insert nowych i update istniejacych ofert
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

        -- #10. nowe technologie do slownika (Category = Other z domyslnej wartosci)
        INSERT INTO dbo.Skills (Name)
        SELECT DISTINCT tg.SkillName
        FROM #tags tg
        WHERE NOT EXISTS (SELECT 1 FROM dbo.Skills s WHERE s.Name = tg.SkillName);

        -- #11. odswiezenie powiazan oferta-technologia
        DELETE os
        FROM dbo.OfferSkills os
        JOIN @map m ON m.OfferId = os.OfferId;

        INSERT INTO dbo.OfferSkills (OfferId, SkillId)
        SELECT DISTINCT m.OfferId, s.Id
        FROM #tags tg
        JOIN @map m       ON m.RawId = tg.RawId
        JOIN dbo.Skills s ON s.Name  = tg.SkillName;

        -- #12. oznaczenie wierszy stagingu jako przetworzone
        UPDATE r
        SET ProcessedAt = SYSUTCDATETIME()
        FROM stg.RawOffers r
        JOIN @map m ON m.RawId = r.Id;

        -- #13. zatwierdzenie transakcji
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
";
    }
}
