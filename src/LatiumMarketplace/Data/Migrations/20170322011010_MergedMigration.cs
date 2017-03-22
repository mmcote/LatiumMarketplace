using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class MergedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accessory",
                columns: table => new
                {
                    AccessoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssetId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessory", x => x.AccessoryId);
                    table.ForeignKey(
                        name: "FK_Accessory_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "assetID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true),
                    ParentCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Category",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageGallery",
                columns: table => new
                {
                    ImageGalleryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageGallery", x => x.ImageGalleryId);
                });

            migrationBuilder.CreateTable(
                name: "Make",
                columns: table => new
                {
                    MakeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Make", x => x.MakeId);
                });

            migrationBuilder.CreateTable(
                name: "Bid",
                columns: table => new
                {
                    bidId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssetId = table.Column<int>(nullable: true),
                    asset_id_model = table.Column<int>(nullable: false),
                    asset_name = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "AssetCategory",
                columns: table => new
                {
                    AssetId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCategory", x => new { x.AssetId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_AssetCategory_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "assetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileLink = table.Column<string>(nullable: false),
                    ImageGalleryId = table.Column<int>(nullable: false),
                    ImageGuid = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    isMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.UniqueConstraint("AK_Image_ImageGuid", x => x.ImageGuid);
                    table.ForeignKey(
                        name: "FK_Image_ImageGallery_ImageGalleryId",
                        column: x => x.ImageGalleryId,
                        principalTable: "ImageGallery",
                        principalColumn: "ImageGalleryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "MessageThread",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RecieverEmail",
                table: "MessageThread",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderEmail",
                table: "MessageThread",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageGalleryId",
                table: "Asset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MakeId",
                table: "Asset",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "accessory",
                table: "Asset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "banned",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ImageGalleryId",
                table: "Asset",
                column: "ImageGalleryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_MakeId",
                table: "Asset",
                column: "MakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accessory_AssetId",
                table: "Accessory",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCategory_AssetId",
                table: "AssetCategory",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCategory_CategoryId",
                table: "AssetCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ImageGalleryId",
                table: "Image",
                column: "ImageGalleryId");

            migrationBuilder.CreateIndex(
                name: "IX_Bid_AssetId",
                table: "Bid",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_ImageGallery_ImageGalleryId",
                table: "Asset",
                column: "ImageGalleryId",
                principalTable: "ImageGallery",
                principalColumn: "ImageGalleryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Make_MakeId",
                table: "Asset",
                column: "MakeId",
                principalTable: "Make",
                principalColumn: "MakeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_ImageGallery_ImageGalleryId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Make_MakeId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_ImageGalleryId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_MakeId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "MessageThread");

            migrationBuilder.DropColumn(
                name: "RecieverEmail",
                table: "MessageThread");

            migrationBuilder.DropColumn(
                name: "SenderEmail",
                table: "MessageThread");

            migrationBuilder.DropColumn(
                name: "ImageGalleryId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "MakeId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "accessory",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "banned",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Accessory");

            migrationBuilder.DropTable(
                name: "AssetCategory");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Make");

            migrationBuilder.DropTable(
                name: "Bid");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ImageGallery");
        }
    }
}
