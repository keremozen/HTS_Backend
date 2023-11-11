using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDb_AddedDetailColumnsAboutTIKAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TikAssignmentDate",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TikReturnDate",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TikUserIdReturned",
                table: "Patients",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserIdAssignedToTik",
                table: "Patients",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_TikUserIdReturned",
                table: "Patients",
                column: "TikUserIdReturned");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserIdAssignedToTik",
                table: "Patients",
                column: "UserIdAssignedToTik");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AbpUsers_TikUserIdReturned",
                table: "Patients",
                column: "TikUserIdReturned",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AbpUsers_UserIdAssignedToTik",
                table: "Patients",
                column: "UserIdAssignedToTik",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AbpUsers_TikUserIdReturned",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AbpUsers_UserIdAssignedToTik",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_TikUserIdReturned",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_UserIdAssignedToTik",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "TikAssignmentDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "TikReturnDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "TikUserIdReturned",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "UserIdAssignedToTik",
                table: "Patients");
        }
    }
}
