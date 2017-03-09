using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Migrations
{
    public partial class assetCategory002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageGallery_Asset_AssetId",
                table: "ImageGallery");

            migrationBuilder.DropIndex(
                name: "IX_ImageGallery_AssetId",
                table: "ImageGallery");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "ImageGallery");

            migrationBuilder.AddColumn<int>(
                name: "ImageGalleryId",
                table: "Asset",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ImageGalleryId",
                table: "Asset",
                column: "ImageGalleryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_ImageGallery_ImageGalleryId",
                table: "Asset",
                column: "ImageGalleryId",
                principalTable: "ImageGallery",
                principalColumn: "ImageGalleryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_ImageGallery_ImageGalleryId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_ImageGalleryId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "ImageGalleryId",
                table: "Asset");

            migrationBuilder.AddColumn<int>(
                name: "AssetId",
                table: "ImageGallery",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ImageGallery_AssetId",
                table: "ImageGallery",
                column: "AssetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageGallery_Asset_AssetId",
                table: "ImageGallery",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "assetID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
