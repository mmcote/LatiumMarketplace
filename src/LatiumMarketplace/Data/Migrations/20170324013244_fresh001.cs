using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class fresh001 : Migration
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
                name: "Transaction",
                columns: table => new
                {
                    transactionId = table.Column<Guid>(nullable: false),
                    end = table.Column<DateTime>(nullable: false),
                    price = table.Column<int>(nullable: false),
                    start = table.Column<DateTime>(nullable: false),
                    transactionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.transactionId);
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

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    assetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    ImageGalleryId = table.Column<int>(nullable: true),
                    MakeId = table.Column<int>(nullable: false),
                    accessory = table.Column<string>(nullable: true),
                    addDate = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    featuredItem = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
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
                        name: "FK_Asset_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asset_ImageGallery_ImageGalleryId",
                        column: x => x.ImageGalleryId,
                        principalTable: "ImageGallery",
                        principalColumn: "ImageGalleryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asset_Make_MakeId",
                        column: x => x.MakeId,
                        principalTable: "Make",
                        principalColumn: "MakeId",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    chosen = table.Column<bool>(nullable: false),
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
                name: "MessageThread",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    Assetid = table.Column<int>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    RecieverEmail = table.Column<string>(nullable: true),
                    RecieverId = table.Column<string>(nullable: false),
                    SenderEmail = table.Column<string>(nullable: true),
                    SenderId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageThread", x => x.id);
                    table.ForeignKey(
                        name: "FK_MessageThread_Asset_Assetid",
                        column: x => x.Assetid,
                        principalTable: "Asset",
                        principalColumn: "assetID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    MessageThreadid = table.Column<Guid>(nullable: true),
                    SendDate = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.id);
                    table.ForeignKey(
                        name: "FK_Message_MessageThread_MessageThreadid",
                        column: x => x.MessageThreadid,
                        principalTable: "MessageThread",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<bool>(
                name: "banned",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accessory_AssetId",
                table: "Accessory",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_CityId",
                table: "Asset",
                column: "CityId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Message_MessageThreadid",
                table: "Message",
                column: "MessageThreadid");

            migrationBuilder.CreateIndex(
                name: "IX_MessageThread_Assetid",
                table: "MessageThread",
                column: "Assetid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "banned",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "description",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "firstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "lastName",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Accessory");

            migrationBuilder.DropTable(
                name: "AssetCategory");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Bid");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "MessageThread");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "ImageGallery");

            migrationBuilder.DropTable(
                name: "Make");
        }
    }
}
