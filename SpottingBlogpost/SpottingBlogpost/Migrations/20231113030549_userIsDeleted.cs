using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpottingBlogpost.Migrations
{
    public partial class userIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Users",
                newName: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Users",
                newName: "State");
        }
    }
}
