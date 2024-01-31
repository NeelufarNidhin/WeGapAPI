using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeGapApi.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedJobTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobSkill_JobSkillId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobType_JobTypeId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobSkillId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobTypeId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobSkillId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobTypeId",
                table: "Jobs");

            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "JobType",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "JobSkill",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobType_JobId",
                table: "JobType",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_JobId",
                table: "JobSkill",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_Jobs_JobId",
                table: "JobSkill",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobType_Jobs_JobId",
                table: "JobType",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_Jobs_JobId",
                table: "JobSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_JobType_Jobs_JobId",
                table: "JobType");

            migrationBuilder.DropIndex(
                name: "IX_JobType_JobId",
                table: "JobType");

            migrationBuilder.DropIndex(
                name: "IX_JobSkill_JobId",
                table: "JobSkill");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobType");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobSkill");

            migrationBuilder.AddColumn<int>(
                name: "JobSkillId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobTypeId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobSkillId",
                table: "Jobs",
                column: "JobSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobTypeId",
                table: "Jobs",
                column: "JobTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobSkill_JobSkillId",
                table: "Jobs",
                column: "JobSkillId",
                principalTable: "JobSkill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
