using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionApp.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "EndTime",
                value: new DateTime(2023, 12, 30, 10, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "BidDbs",
                columns: new[] { "Id", "AuctionId", "BidAmount", "TimeOfBid" },
                values: new object[] { -2, -1, 200.0, new DateTime(2023, 12, 26, 9, 10, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "BidDbs",
                columns: new[] { "Id", "AuctionId", "BidAmount", "TimeOfBid" },
                values: new object[] { -1, -1, 150.0, new DateTime(2023, 12, 25, 12, 45, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "EndTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
