using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractedInstitutionStaffs_Nationalities_PhoneCountryCodeId",
                table: "ContractedInstitutionStaffs");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneCountryCodeId",
                table: "ContractedInstitutionStaffs",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractedInstitutionStaffs_Nationalities_PhoneCountryCodeId",
                table: "ContractedInstitutionStaffs",
                column: "PhoneCountryCodeId",
                principalTable: "Nationalities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractedInstitutionStaffs_Nationalities_PhoneCountryCodeId",
                table: "ContractedInstitutionStaffs");

            migrationBuilder.AlterColumn<int>(
                name: "PhoneCountryCodeId",
                table: "ContractedInstitutionStaffs",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractedInstitutionStaffs_Nationalities_PhoneCountryCodeId",
                table: "ContractedInstitutionStaffs",
                column: "PhoneCountryCodeId",
                principalTable: "Nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
