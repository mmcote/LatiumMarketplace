using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class accessorychange001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "AccessoryList");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AccessoryList");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Accessory",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Accessory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Accessory");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Accessory");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "AccessoryList",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AccessoryList",
                nullable: true);
        }
    }
}
