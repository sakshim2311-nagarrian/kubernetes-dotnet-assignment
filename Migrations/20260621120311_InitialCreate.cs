using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceApiTier.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Records",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 6, 11, 12, 3, 10, 951, DateTimeKind.Utc).AddTicks(8540), "Customs import declaration for electronics", "Import Declaration" },
                    { 2, new DateTime(2026, 6, 12, 12, 3, 10, 951, DateTimeKind.Utc).AddTicks(8549), "Customs export declaration for machinery", "Export Declaration" },
                    { 3, new DateTime(2026, 6, 13, 12, 3, 10, 951, DateTimeKind.Utc).AddTicks(8550), "Transit documentation for goods in transit", "Transit Document" },
                    { 4, new DateTime(2026, 6, 14, 12, 3, 10, 951, DateTimeKind.Utc).AddTicks(8551), "Temporary storage authorization", "Temporary Storage" },
                    { 5, new DateTime(2026, 6, 15, 12, 3, 10, 951, DateTimeKind.Utc).AddTicks(8552), "Clearance certificate for imported goods", "Customs Clearance" },
                    { 6, new DateTime(2026, 6, 16, 12, 3, 10, 951, DateTimeKind.Utc).AddTicks(8553), "Commercial invoice validation record", "Invoice Validation" },
                    { 7, new DateTime(2026, 6, 17, 12, 3, 10, 951, DateTimeKind.Utc).AddTicks(8554), "Certificate of origin documentation", "Certificate of Origin" },
                    { 8, new DateTime(2026, 6, 18, 12, 3, 10, 951, DateTimeKind.Utc).AddTicks(8555), "Customs duty payment confirmation", "Duty Payment" },
                    { 9, new DateTime(2026, 6, 19, 12, 3, 10, 951, DateTimeKind.Utc).AddTicks(8556), "Goods inspection report", "Inspection Report" },
                    { 10, new DateTime(2026, 6, 20, 12, 3, 10, 951, DateTimeKind.Utc).AddTicks(8557), "Final release order for goods", "Release Order" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");
        }
    }
}
