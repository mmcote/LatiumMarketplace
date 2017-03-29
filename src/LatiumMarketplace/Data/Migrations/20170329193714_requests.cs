using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class requests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    requestID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessoryListId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    accessory = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    duration = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    ownerID = table.Column<string>(nullable: true),
                    ownerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.requestID);
                    table.ForeignKey(
                        name: "FK_Request_AccessoryList_AccessoryListId",
                        column: x => x.AccessoryListId,
                        principalTable: "AccessoryList",
                        principalColumn: "AccessoryListId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Request_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<int>(
                name: "requestID",
                table: "MessageThread",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "requestID",
                table: "Bid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageThread_requestID",
                table: "MessageThread",
                column: "requestID");

            migrationBuilder.CreateIndex(
                name: "IX_Bid_requestID",
                table: "Bid",
                column: "requestID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_AccessoryListId",
                table: "Request",
                column: "AccessoryListId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_CityId",
                table: "Request",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Request_requestID",
                table: "Bid",
                column: "requestID",
                principalTable: "Request",
                principalColumn: "requestID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThread_Request_requestID",
                table: "MessageThread",
                column: "requestID",
                principalTable: "Request",
                principalColumn: "requestID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Request_requestID",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageThread_Request_requestID",
                table: "MessageThread");

            migrationBuilder.DropIndex(
                name: "IX_MessageThread_requestID",
                table: "MessageThread");

            migrationBuilder.DropIndex(
                name: "IX_Bid_requestID",
                table: "Bid");

            migrationBuilder.DropColumn(
                name: "requestID",
                table: "MessageThread");

            migrationBuilder.DropColumn(
                name: "requestID",
                table: "Bid");

            migrationBuilder.DropTable(
                name: "Request");
        }
    }
}
