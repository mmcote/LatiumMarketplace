using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class vinsonGaylordMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Message",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    RecieverId = table.Column<string>(nullable: true),
                    SendDate = table.Column<DateTime>(nullable: false),
                    SenderId = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    assetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageGalleryId = table.Column<int>(nullable: false),
                    addDate = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    ownerID = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false),
                    priceDaily = table.Column<decimal>(nullable: false),
                    priceMonthly = table.Column<decimal>(nullable: false),
                    priceWeekly = table.Column<decimal>(nullable: false),
                    request = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.assetID);
                    table.ForeignKey(
                        name: "FK_Asset_ImageGallery_ImageGalleryId",
                        column: x => x.ImageGalleryId,
                        principalTable: "ImageGallery",
                        principalColumn: "ImageGalleryId",
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
                    Title = table.Column<string>(nullable: true),
                    isMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Image_ImageGallery_ImageGalleryId",
                        column: x => x.ImageGalleryId,
                        principalTable: "ImageGallery",
                        principalColumn: "ImageGalleryId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ImageGalleryId",
                table: "Asset",
                column: "ImageGalleryId",
                unique: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AssetCategory");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ImageGallery");
        }
    }
}
