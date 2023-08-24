using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDb_AddedContentTypeToDocumentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "PaymentDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "HospitalConsultationDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "PaymentDocuments");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "HospitalConsultationDocuments");
        }
    }
}
