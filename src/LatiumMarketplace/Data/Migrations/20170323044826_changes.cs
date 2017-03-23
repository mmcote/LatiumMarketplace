using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location",
                table: "Asset");

            migrationBuilder.AddColumn<string>(
                name: "assetName",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bidder",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "poster",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Asset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assetName",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "bidder",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "poster",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Asset");

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "Asset",
                nullable: true);
        }
    }
}
