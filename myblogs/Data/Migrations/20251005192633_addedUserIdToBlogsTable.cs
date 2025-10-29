using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myblogs.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedUserIdToBlogsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Blogs");
        }
    }
}
