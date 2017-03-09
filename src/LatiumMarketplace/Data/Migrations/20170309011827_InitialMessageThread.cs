using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class InitialMessageThread : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecieverId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Message");

            migrationBuilder.CreateTable(
                name: "MessageThread",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    RecieverId = table.Column<string>(nullable: false),
                    SenderId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageThread", x => x.id);
                });

            migrationBuilder.AddColumn<Guid>(
                name: "messageThreadId",
                table: "Message",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "messageThreadId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "MessageThread");

            migrationBuilder.AddColumn<string>(
                name: "RecieverId",
                table: "Message",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Message",
                nullable: true);
        }
    }
}
