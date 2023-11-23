using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpottingBlogpost.Migrations
{
    public partial class adminLogicalDeletions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Ships",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "Comments",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "Comments");
        }
    }
}
