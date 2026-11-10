using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AplikacjaDoAnalizyRynkuPracy.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class SalaryChecksAndFetchedAtDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FetchedAt",
                table: "Offers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSUTCDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Offers_SalaryNormalizedRange",
                table: "Offers",
                sql: "[SalaryMinNormalized] IS NULL OR [SalaryMaxNormalized] IS NULL OR [SalaryMinNormalized] <= [SalaryMaxNormalized]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Offers_SalaryRange",
                table: "Offers",
                sql: "[SalaryMin] IS NULL OR [SalaryMax] IS NULL OR [SalaryMin] <= [SalaryMax]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Offers_SalaryNormalizedRange",
                table: "Offers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Offers_SalaryRange",
                table: "Offers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FetchedAt",
                table: "Offers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "SYSUTCDATETIME()");
        }
    }
}
