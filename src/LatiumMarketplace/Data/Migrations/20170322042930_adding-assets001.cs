using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class addingassets001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location",
                table: "Asset");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Asset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
