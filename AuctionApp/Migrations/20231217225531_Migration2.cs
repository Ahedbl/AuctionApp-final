using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionApp.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "AuctionDbs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "Owner",
                value: "test@email.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "AuctionDbs");
        }
    }
}
