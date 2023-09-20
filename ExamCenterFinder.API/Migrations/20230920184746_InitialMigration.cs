using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamCenterFinder.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamCenters",
                columns: table => new
                {
                    ExamCenterId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    MaximumSeatingCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCenters", x => x.ExamCenterId);
                });

            migrationBuilder.CreateTable(
                name: "ExamCenterSlots",
                columns: table => new
                {
                    SlotId = table.Column<int>(type: "int", nullable: false),
                    ExamCenterId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFilled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCenterSlots", x => x.SlotId);
                    table.ForeignKey(
                        name: "FK_ExamCenterSlots_ExamCenters_ExamCenterId",
                        column: x => x.ExamCenterId,
                        principalTable: "ExamCenters",
                        principalColumn: "ExamCenterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamCenterSlotBookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    SlotId = table.Column<int>(type: "int", nullable: false),
                    SlotBookedOn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCenterSlotBookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_ExamCenterSlotBookings_ExamCenterSlots_SlotId",
                        column: x => x.SlotId,
                        principalTable: "ExamCenterSlots",
                        principalColumn: "SlotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamCenterSlotBookings_SlotId",
                table: "ExamCenterSlotBookings",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCenterSlots_ExamCenterId",
                table: "ExamCenterSlots",
                column: "ExamCenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamCenterSlotBookings");

            migrationBuilder.DropTable(
                name: "ExamCenterSlots");

            migrationBuilder.DropTable(
                name: "ExamCenters");
        }
    }
}
