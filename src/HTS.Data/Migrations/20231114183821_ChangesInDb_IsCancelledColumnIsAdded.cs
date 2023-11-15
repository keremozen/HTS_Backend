using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDb_IsCancelledColumnIsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "ENabizProcesses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProcessId",
                table: "ENabizProcesses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ENabizProcesses_ProcessId",
                table: "ENabizProcesses",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_ENabizProcesses_Processes_ProcessId",
                table: "ENabizProcesses",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ENabizProcesses_Processes_ProcessId",
                table: "ENabizProcesses");

            migrationBuilder.DropIndex(
                name: "IX_ENabizProcesses_ProcessId",
                table: "ENabizProcesses");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "ENabizProcesses");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "ENabizProcesses");
        }
    }
}
