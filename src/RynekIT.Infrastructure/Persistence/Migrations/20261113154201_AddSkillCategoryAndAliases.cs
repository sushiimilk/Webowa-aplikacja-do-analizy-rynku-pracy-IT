using Microsoft.EntityFrameworkCore.Migrations;
using RynekIT.Infrastructure.Persistence.Sql;

#nullable disable

namespace RynekIT.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    /// <remarks>
    /// Kroki #2-#5 to recznie dopisany SQL (seed slownika, aliasy, scalenie duplikatow, procedury).
    /// Przy ponownym generowaniu migracji przez dotnet ef trzeba je przeniesc recznie.
    /// Scalenie duplikatow jest nieodwracalne: Down() nie przywroci usunietych skilli
    /// ani ich pierwotnych powiazan z ofertami i CV.
    /// </remarks>
    public partial class AddSkillCategoryAndAliases : Migration
    {
        /// <summary>Slownik znanych skilli z kategoriami. Jedyne zrodlo listy dla Up i Down.</summary>
        private const string SeedValues = @"
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
    ('lead', 'Level'), ('intern', 'Level')";

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

            // #2. slownik znanych skilli: kategorie dla istniejacych, brakujace dodane
            migrationBuilder.Sql($@"
CREATE TABLE #seed (Name nvarchar(100) NOT NULL PRIMARY KEY, Category nvarchar(20) NOT NULL);
INSERT INTO #seed (Name, Category) VALUES {SeedValues};

UPDATE s SET Category = v.Category
FROM dbo.Skills s
JOIN #seed v ON v.Name = s.Name;

INSERT INTO dbo.Skills (Name, Category)
SELECT v.Name, v.Category
FROM #seed v
WHERE NOT EXISTS (SELECT 1 FROM dbo.Skills s WHERE s.Name = v.Name);

DROP TABLE #seed;
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

            // #4. procedura scalajaca duplikaty (oferty + CV) i jej pierwsze uruchomienie
            //     (ETL wola ja przy kazdym przebiegu, wiec aliasy dodane pozniej tez sie scala)
            migrationBuilder.Sql(SqlScripts.Load(SqlScripts.MergeSkillAliases));
            migrationBuilder.Sql("EXEC dbo.usp_MergeSkillAliases;");

            // #5. procedura ETL z obsluga aliasow
            migrationBuilder.Sql(SqlScripts.Load(SqlScripts.ProcessRawOffersV2));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlScripts.Load(SqlScripts.ProcessRawOffersV1));
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.usp_MergeSkillAliases;");

            migrationBuilder.DropTable(
                name: "SkillAliases");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Skills");

            // usuniecie skilli ze slownika, ktorych nic nie uzywa (dodanych przez seed)
            migrationBuilder.Sql($@"
DELETE s
FROM dbo.Skills s
JOIN (VALUES {SeedValues}) v (Name, Category) ON v.Name = s.Name
WHERE NOT EXISTS (SELECT 1 FROM dbo.OfferSkills os WHERE os.SkillId = s.Id)
  AND NOT EXISTS (SELECT 1 FROM dbo.CvSkills cs WHERE cs.SkillId = s.Id);
");
        }
    }
}
