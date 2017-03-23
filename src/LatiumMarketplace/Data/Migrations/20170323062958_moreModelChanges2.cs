using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class moreModelChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ownerID",
                table: "Asset",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.RenameColumn(
                name: "duration",
                table: "Asset",
                newName: "Duration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ownerID",
                table: "Asset");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Asset",
                newName: "duration");
        }
    }
}
