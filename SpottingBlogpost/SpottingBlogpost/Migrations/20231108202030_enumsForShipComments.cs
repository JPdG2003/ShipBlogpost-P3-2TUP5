using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpottingBlogpost.Migrations
{
    public partial class enumsForShipComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Ships");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Ships",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Flag",
                table: "Ships",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Ships",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Ships");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Ships",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Flag",
                table: "Ships",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Ships",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
