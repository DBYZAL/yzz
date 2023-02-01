using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WesternInnzilunyouxianzhihan.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewGuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Booking_BookingID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Booking_TheBookingsID",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_TheBookingsID",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Booking_BookingID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "TheBookingsID",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "BookingID",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Booking",
                newName: "GuestEmail");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Booking",
                newName: "Cost");

            migrationBuilder.RenameColumn(
                name: "BedCount",
                table: "Booking",
                newName: "RoomID");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckIn",
                table: "Booking",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOut",
                table: "Booking",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Surname = table.Column<string>(type: "TEXT", nullable: false),
                    GivenName = table.Column<string>(type: "TEXT", nullable: false),
                    PostCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.Email);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_GuestEmail",
                table: "Booking",
                column: "GuestEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_RoomID",
                table: "Booking",
                column: "RoomID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Guest_GuestEmail",
                table: "Booking",
                column: "GuestEmail",
                principalTable: "Guest",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Room_RoomID",
                table: "Booking",
                column: "RoomID",
                principalTable: "Room",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Guest_GuestEmail",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Room_RoomID",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropIndex(
                name: "IX_Booking_GuestEmail",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_RoomID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "RoomID",
                table: "Booking",
                newName: "BedCount");

            migrationBuilder.RenameColumn(
                name: "GuestEmail",
                table: "Booking",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Booking",
                newName: "Level");

            migrationBuilder.AddColumn<int>(
                name: "TheBookingsID",
                table: "Room",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookingID",
                table: "Booking",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_TheBookingsID",
                table: "Room",
                column: "TheBookingsID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingID",
                table: "Booking",
                column: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Booking_BookingID",
                table: "Booking",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Booking_TheBookingsID",
                table: "Room",
                column: "TheBookingsID",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
