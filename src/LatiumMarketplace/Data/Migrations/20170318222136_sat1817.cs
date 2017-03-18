using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class sat1817 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Asset_Assetid",
                table: "Bid");

            migrationBuilder.AlterColumn<Guid>(
                name: "bidId",
                table: "Bid",
                nullable: false);

            migrationBuilder.Sql(@"ALTER TABLE dbo.Bid DROP CONSTRAINT PK_Bid");


            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Asset_AssetId",
                table: "Bid",
                column: "Assetid",
                principalTable: "Asset",
                principalColumn: "assetID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "Assetid",
                table: "Bid",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_Assetid",
                table: "Bid",
                newName: "IX_Bid_AssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Asset_AssetId",
                table: "Bid");

            migrationBuilder.AlterColumn<int>(
                name: "bidId",
                table: "Bid",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Asset_Assetid",
                table: "Bid",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "assetID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "Bid",
                newName: "Assetid");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_AssetId",
                table: "Bid",
                newName: "IX_Bid_Assetid");
        }
    }
}
