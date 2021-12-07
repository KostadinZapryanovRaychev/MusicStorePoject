using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMusicStoreWebProject.Migrations
{
    public partial class NewAttendModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ApplicationUserId",
                table: "Attendances",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AspNetUsers_ApplicationUserId",
                table: "Attendances",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AspNetUsers_ApplicationUserId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_ApplicationUserId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Attendances");
        }
    }
}
