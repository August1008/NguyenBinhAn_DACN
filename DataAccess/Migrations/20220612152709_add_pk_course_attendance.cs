using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class add_pk_course_attendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseClassOrder",
                table: "Attendances",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseOrder",
                table: "Attendances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_CourseClassOrder",
                table: "Attendances",
                column: "CourseClassOrder");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Course_Class_CourseClassOrder",
                table: "Attendances",
                column: "CourseClassOrder",
                principalTable: "Course_Class",
                principalColumn: "CourseClassOrder",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Course_Class_CourseClassOrder",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_CourseClassOrder",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CourseClassOrder",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CourseOrder",
                table: "Attendances");
        }
    }
}
