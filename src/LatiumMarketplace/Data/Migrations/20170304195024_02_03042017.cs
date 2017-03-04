using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LatiumMarketplace.Data.Migrations
{
    public partial class _02_03042017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Message",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RecieverId",
                table: "Message",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Message",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "RecieverId",
                table: "Message",
                nullable: false);
        }
    }
}
