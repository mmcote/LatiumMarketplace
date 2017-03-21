using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class mergeMigration030917 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bid",
                columns: table => new
                {
                    bidId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssetId = table.Column<int>(nullable: true),
                    bidPrice = table.Column<decimal>(nullable: false),
                    bidder = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    endDate = table.Column<DateTime>(nullable: false),
                    startDate = table.Column<DateTime>(nullable: false),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bid", x => x.bidId);
                    table.ForeignKey(
                        name: "FK_Bid_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "assetID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<string>(
                name: "accessory",
                table: "Asset",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bid_AssetId",
                table: "Bid",
                column: "AssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accessory",
                table: "Asset");

            migrationBuilder.DropTable(
                name: "Bid");
        }
    }
}
