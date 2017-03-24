using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class FeatureAccessory001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessory_Asset_AssetId",
                table: "Accessory");

            migrationBuilder.DropIndex(
                name: "IX_Accessory_AssetId",
                table: "Accessory");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Accessory");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accessory");

            migrationBuilder.CreateTable(
                name: "AccessoryList",
                columns: table => new
                {
                    AccessoryListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Price = table.Column<decimal>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessoryList", x => x.AccessoryListId);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    FeatureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cab = table.Column<string>(nullable: true),
                    EngineHours = table.Column<decimal>(nullable: false),
                    FluelTankCapicity = table.Column<decimal>(nullable: false),
                    IsForWheelDrive = table.Column<bool>(nullable: false),
                    NumberOfAxels = table.Column<int>(nullable: false),
                    Odometer = table.Column<int>(nullable: false),
                    Seats = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.FeatureId);
                });

            migrationBuilder.CreateTable(
                name: "AssetFeature",
                columns: table => new
                {
                    AssetId = table.Column<int>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetFeature", x => new { x.AssetId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_AssetFeature_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "assetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetFeature_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<int>(
                name: "AccessoryListId",
                table: "Asset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessoryListId",
                table: "Accessory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AccessoryListId",
                table: "Asset",
                column: "AccessoryListId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accessory_AccessoryListId",
                table: "Accessory",
                column: "AccessoryListId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetFeature_AssetId",
                table: "AssetFeature",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetFeature_FeatureId",
                table: "AssetFeature",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessory_AccessoryList_AccessoryListId",
                table: "Accessory",
                column: "AccessoryListId",
                principalTable: "AccessoryList",
                principalColumn: "AccessoryListId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_AccessoryList_AccessoryListId",
                table: "Asset",
                column: "AccessoryListId",
                principalTable: "AccessoryList",
                principalColumn: "AccessoryListId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessory_AccessoryList_AccessoryListId",
                table: "Accessory");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_AccessoryList_AccessoryListId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_AccessoryListId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Accessory_AccessoryListId",
                table: "Accessory");

            migrationBuilder.DropColumn(
                name: "AccessoryListId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "AccessoryListId",
                table: "Accessory");

            migrationBuilder.DropTable(
                name: "AccessoryList");

            migrationBuilder.DropTable(
                name: "AssetFeature");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.AddColumn<int>(
                name: "AssetId",
                table: "Accessory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accessory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accessory_AssetId",
                table: "Accessory",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessory_Asset_AssetId",
                table: "Accessory",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "assetID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
