using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDb_AddedContentTypeColumntoPatientDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "PatientDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "PatientDocuments");
        }
    }
}
