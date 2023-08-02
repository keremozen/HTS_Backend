using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedContractedInstitutionTypeAndKind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KindId",
                table: "ContractedInstitutions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "ContractedInstitutions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContractedInstitutionKinds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_ContractedInstitutionKinds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractedInstitutionKinds_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractedInstitutionKinds_AbpUsers_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractedInstitutionKinds_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractedInstitutionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_ContractedInstitutionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractedInstitutionTypes_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractedInstitutionTypes_AbpUsers_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractedInstitutionTypes_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractedInstitutions_KindId",
                table: "ContractedInstitutions",
                column: "KindId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractedInstitutions_TypeId",
                table: "ContractedInstitutions",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractedInstitutionKinds_CreatorId",
                table: "ContractedInstitutionKinds",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractedInstitutionKinds_DeleterId",
                table: "ContractedInstitutionKinds",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractedInstitutionKinds_LastModifierId",
                table: "ContractedInstitutionKinds",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractedInstitutionTypes_CreatorId",
                table: "ContractedInstitutionTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractedInstitutionTypes_DeleterId",
                table: "ContractedInstitutionTypes",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractedInstitutionTypes_LastModifierId",
                table: "ContractedInstitutionTypes",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractedInstitutions_ContractedInstitutionKinds_KindId",
                table: "ContractedInstitutions",
                column: "KindId",
                principalTable: "ContractedInstitutionKinds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractedInstitutions_ContractedInstitutionTypes_TypeId",
                table: "ContractedInstitutions",
                column: "TypeId",
                principalTable: "ContractedInstitutionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractedInstitutions_ContractedInstitutionKinds_KindId",
                table: "ContractedInstitutions");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractedInstitutions_ContractedInstitutionTypes_TypeId",
                table: "ContractedInstitutions");

            migrationBuilder.DropTable(
                name: "ContractedInstitutionKinds");

            migrationBuilder.DropTable(
                name: "ContractedInstitutionTypes");

            migrationBuilder.DropIndex(
                name: "IX_ContractedInstitutions_KindId",
                table: "ContractedInstitutions");

            migrationBuilder.DropIndex(
                name: "IX_ContractedInstitutions_TypeId",
                table: "ContractedInstitutions");

            migrationBuilder.DropColumn(
                name: "KindId",
                table: "ContractedInstitutions");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "ContractedInstitutions");
        }
    }
}
