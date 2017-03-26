using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class MessageThreadUpdate02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnreadMessageCount",
                table: "MessageThread");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecieverUnreadMessageCount",
                table: "MessageThread");

            migrationBuilder.DropColumn(
                name: "SenderUnreadMessageCount",
                table: "MessageThread");

            migrationBuilder.AddColumn<int>(
                name: "UnreadMessageCount",
                table: "MessageThread",
                nullable: false,
                defaultValue: 0);
        }
    }
}
