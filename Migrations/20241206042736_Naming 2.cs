using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cafe_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Naming2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Qrdata",
                table: "Tables",
                newName: "QrData");

            migrationBuilder.AddColumn<string>(
                name: "TablesTableId",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TablesTableId",
                table: "Orders",
                column: "TablesTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Tables_TablesTableId",
                table: "Orders",
                column: "TablesTableId",
                principalTable: "Tables",
                principalColumn: "TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Tables_TablesTableId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TablesTableId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TablesTableId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "QrData",
                table: "Tables",
                newName: "Qrdata");
        }
    }
}
