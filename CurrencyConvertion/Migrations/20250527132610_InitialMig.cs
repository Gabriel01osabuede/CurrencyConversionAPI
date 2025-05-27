using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyConvertion.Migrations
{
    /// <inheritdoc />
    public partial class InitialMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchnageRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchnageRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalExchnageRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalExchnageRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExchnageRatesDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExchangeRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchnageRatesDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchnageRatesDetails_ExchnageRates_ExchangeRateId",
                        column: x => x.ExchangeRateId,
                        principalTable: "ExchnageRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricalExchnageRateDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HistoricalExchangeRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricalExchnageRateDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricalExchnageRateDetails_HistoricalExchnageRates_HistoricalExchangeRateId",
                        column: x => x.HistoricalExchangeRateId,
                        principalTable: "HistoricalExchnageRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchnageRatesDetails_ExchangeRateId",
                table: "ExchnageRatesDetails",
                column: "ExchangeRateId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricalExchnageRateDetails_HistoricalExchangeRateId",
                table: "HistoricalExchnageRateDetails",
                column: "HistoricalExchangeRateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchnageRatesDetails");

            migrationBuilder.DropTable(
                name: "HistoricalExchnageRateDetails");

            migrationBuilder.DropTable(
                name: "ExchnageRates");

            migrationBuilder.DropTable(
                name: "HistoricalExchnageRates");
        }
    }
}
