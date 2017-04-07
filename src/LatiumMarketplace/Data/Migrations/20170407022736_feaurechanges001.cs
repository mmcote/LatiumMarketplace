using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class feaurechanges001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cab",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "EngineHours",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "FluelTankCapicity",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "IsForWheelDrive",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "NumberOfAxels",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "Odometer",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Feature");

            migrationBuilder.AddColumn<string>(
                name: "FeatureName",
                table: "Feature",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Feature",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureName",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Feature");

            migrationBuilder.AddColumn<string>(
                name: "Cab",
                table: "Feature",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EngineHours",
                table: "Feature",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FluelTankCapicity",
                table: "Feature",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsForWheelDrive",
                table: "Feature",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAxels",
                table: "Feature",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Odometer",
                table: "Feature",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Feature",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Feature",
                nullable: false,
                defaultValue: 0);
        }
    }
}
