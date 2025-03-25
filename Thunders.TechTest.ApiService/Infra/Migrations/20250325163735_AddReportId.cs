﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Thunders.TechTest.ApiService.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddReportId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReportId",
                table: "VehicleCountByTollPlaza",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReportId",
                table: "TopEarningTollPlazas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReportId",
                table: "HourlyRevenueByCity",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "VehicleCountByTollPlaza");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "TopEarningTollPlazas");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "HourlyRevenueByCity");
        }
    }
}
