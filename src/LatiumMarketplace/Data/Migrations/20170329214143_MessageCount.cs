using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class MessageCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecieverUnreadMessageCount",
                table: "MessageThread",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SenderUnreadMessageCount",
                table: "MessageThread",
                nullable: false,
                defaultValue: 0);

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
                name: "RecieverUnreadMessageCount",
                table: "MessageThread");

            migrationBuilder.DropColumn(
                name: "SenderUnreadMessageCount",
                table: "MessageThread");

            migrationBuilder.DropColumn(
                name: "RecieverUnread",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SenderUnread",
                table: "Message");
        }
    }
}
