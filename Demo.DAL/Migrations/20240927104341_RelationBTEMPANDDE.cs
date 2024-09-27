using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.DAL.Migrations
{
    public partial class RelationBTEMPANDDE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Empolyee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empolyee_DepartmentId",
                table: "Empolyee",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empolyee_Department_DepartmentId",
                table: "Empolyee",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empolyee_Department_DepartmentId",
                table: "Empolyee");

            migrationBuilder.DropIndex(
                name: "IX_Empolyee_DepartmentId",
                table: "Empolyee");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Empolyee");
        }
    }
}
