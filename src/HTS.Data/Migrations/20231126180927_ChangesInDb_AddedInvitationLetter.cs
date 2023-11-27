using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInDb_AddedInvitationLetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvitationLetterDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalesMethodAndCompanionInfoId = table.Column<int>(type: "integer", nullable: false),
                    FileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitationLetterDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvitationLetterDocuments_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvitationLetterDocuments_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvitationLetterDocuments_SalesMethodAndCompanionInfos_Sale~",
                        column: x => x.SalesMethodAndCompanionInfoId,
                        principalTable: "SalesMethodAndCompanionInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvitationLetterDocuments_CreatorId",
                table: "InvitationLetterDocuments",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationLetterDocuments_LastModifierId",
                table: "InvitationLetterDocuments",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationLetterDocuments_SalesMethodAndCompanionInfoId",
                table: "InvitationLetterDocuments",
                column: "SalesMethodAndCompanionInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvitationLetterDocuments");
        }
    }
}
