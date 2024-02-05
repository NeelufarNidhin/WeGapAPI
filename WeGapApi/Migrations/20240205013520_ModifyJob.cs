using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGapApi.Migrations
{
    /// <inheritdoc />
    public partial class ModifyJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobType_JobTypeId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobTypeId",
                table: "Jobs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobTypeId",
                table: "Jobs",
                column: "JobTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobType_JobTypeId",
                table: "Jobs",
                column: "JobTypeId",
                principalTable: "JobType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
