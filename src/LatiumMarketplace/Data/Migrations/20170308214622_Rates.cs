using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class Rates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "priceDaily",
                table: "Asset",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "priceMonthly",
                table: "Asset",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "priceWeekly",
                table: "Asset",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "request",
                table: "Asset",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "priceDaily",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "priceMonthly",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "priceWeekly",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "request",
                table: "Asset");
        }
    }
}
