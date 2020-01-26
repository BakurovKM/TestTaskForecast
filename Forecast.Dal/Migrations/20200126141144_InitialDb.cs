using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Forecast.Dal.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forecasts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forecasts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityForecasts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ForecastId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityForecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityForecasts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityForecasts_Forecasts_ForecastId",
                        column: x => x.ForecastId,
                        principalTable: "Forecasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DayForecasts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    CityForecastId = table.Column<int>(nullable: true),
                    Describe = table.Column<string>(maxLength: 128, nullable: true),
                    TemperatureMax = table.Column<int>(nullable: true),
                    TemperatureMin = table.Column<int>(nullable: true),
                    WindSpeedMs = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Precipitation = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayForecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayForecasts_CityForecasts_CityForecastId",
                        column: x => x.CityForecastId,
                        principalTable: "CityForecasts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityForecasts_CityId",
                table: "CityForecasts",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CityForecasts_ForecastId",
                table: "CityForecasts",
                column: "ForecastId");

            migrationBuilder.CreateIndex(
                name: "IX_DayForecasts_CityForecastId",
                table: "DayForecasts",
                column: "CityForecastId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayForecasts");

            migrationBuilder.DropTable(
                name: "CityForecasts");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Forecasts");
        }
    }
}
