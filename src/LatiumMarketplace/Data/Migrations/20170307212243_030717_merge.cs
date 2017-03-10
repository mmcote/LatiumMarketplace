using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class _030717_merge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    assetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    addDate = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    ownerID = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.assetID);
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

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
