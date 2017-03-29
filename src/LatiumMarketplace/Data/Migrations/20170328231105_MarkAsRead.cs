using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class MarkAsRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unread",
                table: "Message");

            migrationBuilder.AddColumn<bool>(
                name: "RecieverUnread",
                table: "Message",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SenderUnread",
                table: "Message",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecieverUnread",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SenderUnread",
                table: "Message");

            migrationBuilder.AddColumn<bool>(
                name: "Unread",
                table: "Message",
                nullable: false,
                defaultValue: false);
        }
    }
}
