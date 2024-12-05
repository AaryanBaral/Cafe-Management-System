using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Namingchanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Qrdata",
                table: "Tables",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RatingValue",
                table: "Ratings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qrdata",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "RatingValue",
                table: "Ratings");
        }
    }
}
