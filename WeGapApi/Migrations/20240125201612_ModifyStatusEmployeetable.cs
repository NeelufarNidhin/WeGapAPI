using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGapApi.Migrations
{
    /// <inheritdoc />
    public partial class ModifyStatusEmployeetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Education_Employees_EmployeeId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Experience_Employees_EmployeeId",
                table: "Experience");

            migrationBuilder.DropIndex(
                name: "IX_Experience_EmployeeId",
                table: "Experience");

            migrationBuilder.DropIndex(
                name: "IX_Education_EmployeeId",
                table: "Education");

            migrationBuilder.AddColumn<bool>(
                name: "CreatedStatus",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedStatus",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_EmployeeId",
                table: "Experience",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_EmployeeId",
                table: "Education",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Employees_EmployeeId",
                table: "Education",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experience_Employees_EmployeeId",
                table: "Experience",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
