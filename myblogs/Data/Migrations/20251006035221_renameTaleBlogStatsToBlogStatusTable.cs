using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myblogs.Data.Migrations
{
    /// <inheritdoc />
    public partial class renameTaleBlogStatsToBlogStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogStats",
                table: "BlogStats");

            migrationBuilder.RenameTable(
                name: "BlogStats",
                newName: "BlogStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogStatus",
                table: "BlogStatus",
                column: "BlogStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogStatus",
                table: "BlogStatus");

            migrationBuilder.RenameTable(
                name: "BlogStatus",
                newName: "BlogStats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogStats",
                table: "BlogStats",
                column: "BlogStatusId");
        }
    }
}
