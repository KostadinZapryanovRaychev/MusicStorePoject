using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcMusicStoreWebProject.Migrations
{
    public partial class TryIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Disciplines_DisciplineId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Programs_ProgramsId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_DisciplineId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_ProgramsId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "DisciplineId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "ProgramsId",
                table: "Attendances");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Programs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Discipline",
                table: "Attendances",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Programs",
                table: "Attendances",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discipline",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "Programs",
                table: "Attendances");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Programs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "DisciplineId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramsId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_DisciplineId",
                table: "Attendances",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ProgramsId",
                table: "Attendances",
                column: "ProgramsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Disciplines_DisciplineId",
                table: "Attendances",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Programs_ProgramsId",
                table: "Attendances",
                column: "ProgramsId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
