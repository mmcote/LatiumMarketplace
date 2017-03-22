using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class city001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                });

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Asset",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_CityId",
                table: "Asset",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_City_CityId",
                table: "Asset",
                column: "CityId",
                principalTable: "City",
                principalColumn: "CityId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_City_CityId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_CityId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Asset");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
