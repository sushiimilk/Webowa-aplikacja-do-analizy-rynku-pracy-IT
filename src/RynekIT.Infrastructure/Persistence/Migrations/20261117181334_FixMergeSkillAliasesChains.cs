using Microsoft.EntityFrameworkCore.Migrations;
using RynekIT.Infrastructure.Persistence.Sql;

#nullable disable

namespace RynekIT.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    /// <remarks>
    /// Ta migracja nie zmienia schematu, tylko podmienia dbo.usp_MergeSkillAliases na v2.
    /// Stara wersja przy lancuchu albo cyklu aliasow po cichu kasowala powiazania ofert i CV
    /// (FK do Skills maja ON DELETE CASCADE, wiec nie bylo zadnego bledu).
    /// SQL jest dopisany recznie, wiec po ponownym wygenerowaniu migracji trzeba go przeniesc.
    /// </remarks>
    public partial class FixMergeSkillAliasesChains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlScripts.Load(SqlScripts.MergeSkillAliasesV2));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlScripts.Load(SqlScripts.MergeSkillAliases));
        }
    }
}
