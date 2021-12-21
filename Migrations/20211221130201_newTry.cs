using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMusicStoreWebProject.Migrations
{
    public partial class newTry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "NonWorkingDays");

            migrationBuilder.AddColumn<DateTime>(
                name: "Holiday",
                table: "NonWorkingDays",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Attendances",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Holiday",
                table: "NonWorkingDays");

            migrationBuilder.AddColumn<DateTime>(
                name: "Day",
                table: "NonWorkingDays",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Attendances",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
