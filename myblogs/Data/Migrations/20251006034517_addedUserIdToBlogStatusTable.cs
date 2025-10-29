using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myblogs.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedUserIdToBlogStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPublishStatusOptions");

            migrationBuilder.CreateTable(
                name: "BlogStats",
                columns: table => new
                {
                    BlogStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogStatusText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogStats", x => x.BlogStatusId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogStats");

            migrationBuilder.CreateTable(
                name: "BlogPublishStatusOptions",
                columns: table => new
                {
                    BlogPublishStatusOptionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogStatusText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPublishStatusOptions", x => x.BlogPublishStatusOptionsId);
                });
        }
    }
}
