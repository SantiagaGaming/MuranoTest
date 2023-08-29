using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MuranoTest.Migrations
{
    /// <inheritdoc />
    public partial class EditUrlObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlId",
                table: "Urls",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Urls",
                newName: "UrlId");
        }
    }
}
