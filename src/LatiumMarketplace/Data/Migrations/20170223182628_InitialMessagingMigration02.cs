using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class InitialMessagingMigration02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageID = table.Column<Guid>(nullable: false),
                    DateRead = table.Column<DateTime>(nullable: false),
                    DateSent = table.Column<DateTime>(nullable: false),
                    MessageContent = table.Column<string>(nullable: false),
                    MessageThreadID = table.Column<int>(nullable: false),
                    Read = table.Column<bool>(nullable: false),
                    RecipientID = table.Column<string>(nullable: false),
                    SenderID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
