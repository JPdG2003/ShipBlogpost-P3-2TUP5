using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpottingBlogpost.Migrations
{
    public partial class RollBackFixAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Comments",
                newName: "PosterId");

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Ships",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_SpotterId",
                table: "Ships",
                column: "SpotterId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PosterId",
                table: "Comments",
                column: "PosterId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ShipId",
                table: "Comments",
                column: "ShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Ships_ShipId",
                table: "Comments",
                column: "ShipId",
                principalTable: "Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_PosterId",
                table: "Comments",
                column: "PosterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Users_SpotterId",
                table: "Ships",
                column: "SpotterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Ships_ShipId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_PosterId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Users_SpotterId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_SpotterId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PosterId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ShipId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Ships");

            migrationBuilder.RenameColumn(
                name: "PosterId",
                table: "Comments",
                newName: "CreatorId");
        }
    }
}
