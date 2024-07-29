using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class PtpFinalizationTypeClm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientTreatmentProcesses_FinalizationTypes_FinalizationTyp~",
                table: "PatientTreatmentProcesses");

            migrationBuilder.AlterColumn<int>(
                name: "FinalizationTypeId",
                table: "PatientTreatmentProcesses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinalized",
                table: "PatientTreatmentProcesses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientTreatmentProcesses_FinalizationTypes_FinalizationTypeId",
                table: "PatientTreatmentProcesses",
                column: "FinalizationTypeId",
                principalTable: "FinalizationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientTreatmentProcesses_FinalizationTypes_FinalizationTyp~",
                table: "PatientTreatmentProcesses");

            migrationBuilder.DropColumn(
                name: "IsFinalized",
                table: "PatientTreatmentProcesses");

            migrationBuilder.AlterColumn<int>(
                name: "FinalizationTypeId",
                table: "PatientTreatmentProcesses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientTreatmentProcesses_FinalizationTypes_FinalizationTyp~",
                table: "PatientTreatmentProcesses",
                column: "FinalizationTypeId",
                principalTable: "FinalizationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
