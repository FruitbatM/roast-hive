using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoastHiveMvc.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Product",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Product");
        }
    }
}
