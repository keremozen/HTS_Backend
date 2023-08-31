using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDb_RelationBetweenHospitalConsultationDocumentAndConsultationMadeNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalConsultationDocuments_DocumentTypes_DocumentTypeId",
                table: "HospitalConsultationDocuments");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentTypeId",
                table: "HospitalConsultationDocuments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalConsultationDocuments_DocumentTypes_DocumentTypeId",
                table: "HospitalConsultationDocuments",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalConsultationDocuments_DocumentTypes_DocumentTypeId",
                table: "HospitalConsultationDocuments");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentTypeId",
                table: "HospitalConsultationDocuments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalConsultationDocuments_DocumentTypes_DocumentTypeId",
                table: "HospitalConsultationDocuments",
                column: "DocumentTypeId",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
