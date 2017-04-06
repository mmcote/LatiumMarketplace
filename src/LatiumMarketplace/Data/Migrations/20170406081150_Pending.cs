using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class Pending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "assetOwnerNotificationPending",
                table: "Bid",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "bidderNotificationPending",
                table: "Bid",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Asset",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assetOwnerNotificationPending",
                table: "Bid");

            migrationBuilder.DropColumn(
                name: "bidderNotificationPending",
                table: "Bid");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Asset",
                nullable: true);
        }
    }
}
