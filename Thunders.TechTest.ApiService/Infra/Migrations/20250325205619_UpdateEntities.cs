using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thunders.TechTest.ApiService.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hour",
                table: "HourlyRevenueByCity");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "TopEarningTollPlazas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "TopEarningTollPlazas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "HourlyRevenueByCity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "TopEarningTollPlazas");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "TopEarningTollPlazas");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "HourlyRevenueByCity");

            migrationBuilder.AddColumn<DateTime>(
                name: "Hour",
                table: "HourlyRevenueByCity",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
