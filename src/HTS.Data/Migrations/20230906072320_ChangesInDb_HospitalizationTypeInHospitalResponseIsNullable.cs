using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDb_HospitalizationTypeInHospitalResponseIsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalResponses_HospitalizationTypes_HospitalizationTypeId",
                table: "HospitalResponses");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalizationTypeId",
                table: "HospitalResponses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalResponses_HospitalizationTypes_HospitalizationTypeId",
                table: "HospitalResponses",
                column: "HospitalizationTypeId",
                principalTable: "HospitalizationTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HospitalResponses_HospitalizationTypes_HospitalizationTypeId",
                table: "HospitalResponses");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalizationTypeId",
                table: "HospitalResponses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HospitalResponses_HospitalizationTypes_HospitalizationTypeId",
                table: "HospitalResponses",
                column: "HospitalizationTypeId",
                principalTable: "HospitalizationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
