using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDb_AddedAgentNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HospitalAgentNoteStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalAgentNoteStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HospitalAgentNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Note = table.Column<string>(type: "text", nullable: false),
                    HospitalResponseId = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalAgentNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalAgentNotes_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HospitalAgentNotes_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HospitalAgentNotes_HospitalAgentNoteStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "HospitalAgentNoteStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HospitalAgentNotes_HospitalResponses_HospitalResponseId",
                        column: x => x.HospitalResponseId,
                        principalTable: "HospitalResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HospitalAgentNotes_CreatorId",
                table: "HospitalAgentNotes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalAgentNotes_HospitalResponseId",
                table: "HospitalAgentNotes",
                column: "HospitalResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalAgentNotes_LastModifierId",
                table: "HospitalAgentNotes",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalAgentNotes_StatusId",
                table: "HospitalAgentNotes",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalAgentNotes");

            migrationBuilder.DropTable(
                name: "HospitalAgentNoteStatuses");
        }
    }
}
