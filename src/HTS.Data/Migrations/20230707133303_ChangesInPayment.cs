using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcessingUserNameSurname",
                table: "Payments",
                newName: "ProformaNumber");

            migrationBuilder.RenameColumn(
                name: "PaidNameSurname",
                table: "Payments",
                newName: "PayerNameSurname");

            migrationBuilder.AlterColumn<int>(
                name: "RowNumber",
                table: "Payments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CollectorNameSurname",
                table: "Payments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GeneratedRowNumber",
                table: "Payments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "PaymentItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectorNameSurname",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "GeneratedRowNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "PaymentItems");

            migrationBuilder.RenameColumn(
                name: "ProformaNumber",
                table: "Payments",
                newName: "ProcessingUserNameSurname");

            migrationBuilder.RenameColumn(
                name: "PayerNameSurname",
                table: "Payments",
                newName: "PaidNameSurname");

            migrationBuilder.AlterColumn<string>(
                name: "RowNumber",
                table: "Payments",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
