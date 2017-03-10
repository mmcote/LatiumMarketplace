using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class FKeyMessageThreadToAssetId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Assetid",
                table: "MessageThread",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageThread_Assetid",
                table: "MessageThread",
                column: "Assetid");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThread_Asset_Assetid",
                table: "MessageThread",
                column: "Assetid",
                principalTable: "Asset",
                principalColumn: "assetID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageThread_Asset_Assetid",
                table: "MessageThread");

            migrationBuilder.DropIndex(
                name: "IX_MessageThread_Assetid",
                table: "MessageThread");

            migrationBuilder.DropColumn(
                name: "Assetid",
                table: "MessageThread");
        }
    }
}
