using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalizationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FinalizationDescription",
                table: "PatientTreatmentProcesses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FinalizationTypeId",
                table: "PatientTreatmentProcesses",
                type: "integer",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateTable(
                name: "FinalizationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalizationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalizationTypes_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalizationTypes_AbpUsers_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FinalizationTypes_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientTreatmentProcesses_FinalizationTypeId",
                table: "PatientTreatmentProcesses",
                column: "FinalizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalizationTypes_CreatorId",
                table: "FinalizationTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalizationTypes_DeleterId",
                table: "FinalizationTypes",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalizationTypes_LastModifierId",
                table: "FinalizationTypes",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientTreatmentProcesses_FinalizationTypes_FinalizationTyp~",
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

            migrationBuilder.DropTable(
                name: "FinalizationTypes");

            migrationBuilder.DropIndex(
                name: "IX_PatientTreatmentProcesses_FinalizationTypeId",
                table: "PatientTreatmentProcesses");

            migrationBuilder.DropColumn(
                name: "FinalizationDescription",
                table: "PatientTreatmentProcesses");

            migrationBuilder.DropColumn(
                name: "FinalizationTypeId",
                table: "PatientTreatmentProcesses");
        }
    }
}
