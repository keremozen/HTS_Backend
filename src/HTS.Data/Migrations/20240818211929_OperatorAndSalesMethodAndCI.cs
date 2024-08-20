using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class OperatorAndSalesMethodAndCI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_TreatmentTypes_TreatmentTypeId",
                table: "Operations");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentTypes_AbpUsers_CreatorId",
                table: "TreatmentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentTypes_AbpUsers_DeleterId",
                table: "TreatmentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TreatmentTypes_AbpUsers_LastModifierId",
                table: "TreatmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_TreatmentTypes_CreatorId",
                table: "TreatmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_TreatmentTypes_DeleterId",
                table: "TreatmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_TreatmentTypes_LastModifierId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "TreatmentTypes");

            migrationBuilder.DropColumn(
                name: "AnyInvitationLetter",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "TravelDateToTurkey",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "TreatmentDate",
                table: "Operations");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TreatmentTypes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "AdvancePaymentRequested",
                table: "SalesMethodAndCompanionInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AnyInvitationLetter",
                table: "SalesMethodAndCompanionInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AnyTravelPlan",
                table: "SalesMethodAndCompanionInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "AppointedInterpreterId",
                table: "SalesMethodAndCompanionInfos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDocumentTranslationRequired",
                table: "SalesMethodAndCompanionInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "TravelDateToTurkey",
                table: "SalesMethodAndCompanionInfos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TravelDescription",
                table: "SalesMethodAndCompanionInfos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TreatmentDate",
                table: "SalesMethodAndCompanionInfos",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TurkeyDestination",
                table: "SalesMethodAndCompanionInfos",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "PatientTreatmentProcesses",
                type: "boolean",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TreatmentTypeId",
                table: "Operations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "InterpreterAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalesMethodAndCompanionInfoId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    BranchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterpreterAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterpreterAppointments_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InterpreterAppointments_SalesMethodAndCompanionInfos_SalesM~",
                        column: x => x.SalesMethodAndCompanionInfoId,
                        principalTable: "SalesMethodAndCompanionInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterpreterAppointments_BranchId",
                table: "InterpreterAppointments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InterpreterAppointments_SalesMethodAndCompanionInfoId",
                table: "InterpreterAppointments",
                column: "SalesMethodAndCompanionInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_TreatmentTypes_TreatmentTypeId",
                table: "Operations",
                column: "TreatmentTypeId",
                principalTable: "TreatmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operations_TreatmentTypes_TreatmentTypeId",
                table: "Operations");

            migrationBuilder.DropTable(
                name: "InterpreterAppointments");

            migrationBuilder.DropColumn(
                name: "AdvancePaymentRequested",
                table: "SalesMethodAndCompanionInfos");

            migrationBuilder.DropColumn(
                name: "AnyInvitationLetter",
                table: "SalesMethodAndCompanionInfos");

            migrationBuilder.DropColumn(
                name: "AnyTravelPlan",
                table: "SalesMethodAndCompanionInfos");

            migrationBuilder.DropColumn(
                name: "AppointedInterpreterId",
                table: "SalesMethodAndCompanionInfos");

            migrationBuilder.DropColumn(
                name: "IsDocumentTranslationRequired",
                table: "SalesMethodAndCompanionInfos");

            migrationBuilder.DropColumn(
                name: "TravelDateToTurkey",
                table: "SalesMethodAndCompanionInfos");

            migrationBuilder.DropColumn(
                name: "TravelDescription",
                table: "SalesMethodAndCompanionInfos");

            migrationBuilder.DropColumn(
                name: "TreatmentDate",
                table: "SalesMethodAndCompanionInfos");

            migrationBuilder.DropColumn(
                name: "TurkeyDestination",
                table: "SalesMethodAndCompanionInfos");

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "PatientTreatmentProcesses");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TreatmentTypes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "TreatmentTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "TreatmentTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "TreatmentTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TreatmentTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TreatmentTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "TreatmentTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "TreatmentTypes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TreatmentTypeId",
                table: "Operations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "AnyInvitationLetter",
                table: "Operations",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TravelDateToTurkey",
                table: "Operations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TreatmentDate",
                table: "Operations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentTypes_CreatorId",
                table: "TreatmentTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentTypes_DeleterId",
                table: "TreatmentTypes",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentTypes_LastModifierId",
                table: "TreatmentTypes",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operations_TreatmentTypes_TreatmentTypeId",
                table: "Operations",
                column: "TreatmentTypeId",
                principalTable: "TreatmentTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentTypes_AbpUsers_CreatorId",
                table: "TreatmentTypes",
                column: "CreatorId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentTypes_AbpUsers_DeleterId",
                table: "TreatmentTypes",
                column: "DeleterId",
                principalTable: "AbpUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TreatmentTypes_AbpUsers_LastModifierId",
                table: "TreatmentTypes",
                column: "LastModifierId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
