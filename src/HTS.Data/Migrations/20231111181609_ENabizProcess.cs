using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HTS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ENabizProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ENabizProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TreatmentCode = table.Column<string>(type: "text", nullable: false),
                    SysTrackingNumber = table.Column<string>(type: "text", nullable: false),
                    GERCEKLESME_ZAMANI = table.Column<string>(type: "text", nullable: true),
                    ISLEM_TURU = table.Column<string>(type: "text", nullable: true),
                    ISLEM_KODU = table.Column<string>(type: "text", nullable: true),
                    ISLEM_ADI = table.Column<string>(type: "text", nullable: true),
                    ISLEM_ZAMANI = table.Column<string>(type: "text", nullable: true),
                    ADET = table.Column<string>(type: "text", nullable: true),
                    HASTA_TUTARI = table.Column<string>(type: "text", nullable: true),
                    KURUM_TUTARI = table.Column<string>(type: "text", nullable: true),
                    RANDEVU_ZAMANI = table.Column<string>(type: "text", nullable: true),
                    KULLANICI_KIMLIK_NUMARASI = table.Column<string>(type: "text", nullable: true),
                    CIHAZ_NUMARASI = table.Column<string>(type: "text", nullable: true),
                    ISLEM_REFERANS_NUMARASI = table.Column<string>(type: "text", nullable: true),
                    GIRISIMSEL_ISLEM_KODU = table.Column<string>(type: "text", nullable: true),
                    KLINIK_KODU = table.Column<string>(type: "text", nullable: true),
                    ISLEM_PUAN_BILGISI = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_ENabizProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ENabizProcesses_AbpUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ENabizProcesses_AbpUsers_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ENabizProcesses_AbpUsers_LastModifierId",
                        column: x => x.LastModifierId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ENabizProcesses_CreatorId",
                table: "ENabizProcesses",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ENabizProcesses_DeleterId",
                table: "ENabizProcesses",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_ENabizProcesses_LastModifierId",
                table: "ENabizProcesses",
                column: "LastModifierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ENabizProcesses");
        }
    }
}
