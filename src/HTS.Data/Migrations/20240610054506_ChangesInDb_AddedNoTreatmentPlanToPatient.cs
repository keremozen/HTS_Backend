using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDb_AddedNoTreatmentPlanToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NoTreatmentPlan",
                table: "Patients",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoTreatmentPlan",
                table: "Patients");
        }
    }
}
