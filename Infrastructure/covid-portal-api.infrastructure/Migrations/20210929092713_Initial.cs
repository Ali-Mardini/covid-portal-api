using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace covid_portal_api.infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewConfirmed = table.Column<long>(type: "bigint", nullable: false),
                    TotalConfirmed = table.Column<long>(type: "bigint", nullable: false),
                    NewDeaths = table.Column<long>(type: "bigint", nullable: false),
                    TotalDeaths = table.Column<long>(type: "bigint", nullable: false),
                    NewRecovered = table.Column<long>(type: "bigint", nullable: false),
                    TotalRecovered = table.Column<long>(type: "bigint", nullable: false),
                    CaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseId);
                    table.ForeignKey(
                        name: "FK_Cases_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CountryId",
                table: "Cases",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
