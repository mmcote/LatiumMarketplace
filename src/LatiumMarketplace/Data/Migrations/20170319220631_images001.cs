using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class images001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Bid",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageGuid",
                table: "Image",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Image_ImageGuid",
                table: "Image",
                column: "ImageGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Image_ImageGuid",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Bid");

            migrationBuilder.DropColumn(
                name: "ImageGuid",
                table: "Image");
        }
    }
}
