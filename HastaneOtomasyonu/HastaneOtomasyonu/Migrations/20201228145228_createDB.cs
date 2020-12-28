using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HastaneOtomasyonu.Migrations
{
    public partial class createDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ilac",
                columns: table => new
                {
                    ilacKodu = table.Column<string>(nullable: false),
                    ilacAdi = table.Column<string>(nullable: false),
                    ilacMarkasi = table.Column<string>(nullable: true),
                    fiyat = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilac", x => x.ilacKodu);
                });

            migrationBuilder.CreateTable(
                name: "KanGrubu",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    adi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanGrubu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Klinik",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    adi = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klinik", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    adi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ulke",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    adi = table.Column<string>(maxLength: 50, nullable: false),
                    ulkeKodu = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ulke", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sehir",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    adi = table.Column<string>(maxLength: 50, nullable: false),
                    plakaKodu = table.Column<int>(nullable: true),
                    telefonKodu = table.Column<int>(nullable: true),
                    ulkeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sehir", x => x.id);
                    table.ForeignKey(
                        name: "FK_Sehir_Ulke_ulkeId",
                        column: x => x.ulkeId,
                        principalTable: "Ulke",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ilce",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    adi = table.Column<string>(nullable: true),
                    telefonKodu = table.Column<int>(nullable: true),
                    sehirId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilce", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ilce_Sehir_sehirId",
                        column: x => x.sehirId,
                        principalTable: "Sehir",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kisi",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    adi = table.Column<string>(nullable: true),
                    soyadi = table.Column<string>(nullable: true),
                    adres = table.Column<string>(nullable: true),
                    cepNo = table.Column<string>(nullable: true),
                    evNo = table.Column<string>(nullable: true),
                    isNo = table.Column<string>(nullable: true),
                    TCNo = table.Column<string>(nullable: true),
                    dogumTarihi = table.Column<DateTime>(nullable: false),
                    cinsiyet = table.Column<int>(nullable: false),
                    medeniDurum = table.Column<int>(nullable: false),
                    sehirId = table.Column<int>(nullable: false),
                    kanGrubuId = table.Column<int>(nullable: false),
                    kisiTuru = table.Column<char>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kisi", x => x.id);
                    table.ForeignKey(
                        name: "FK_Kisi_KanGrubu_kanGrubuId",
                        column: x => x.kanGrubuId,
                        principalTable: "KanGrubu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kisi_Sehir_sehirId",
                        column: x => x.sehirId,
                        principalTable: "Sehir",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hastane",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    adi = table.Column<string>(nullable: false),
                    telefonNo = table.Column<string>(nullable: true),
                    ilceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hastane", x => x.id);
                    table.ForeignKey(
                        name: "FK_Hastane_Ilce_ilceId",
                        column: x => x.ilceId,
                        principalTable: "Ilce",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hasta",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hasta", x => x.id);
                    table.ForeignKey(
                        name: "FK_Hasta_Kisi_id",
                        column: x => x.id,
                        principalTable: "Kisi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HastaneKlinik",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    hastaneId = table.Column<int>(nullable: false),
                    klinikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HastaneKlinik", x => x.id);
                    table.ForeignKey(
                        name: "FK_HastaneKlinik_Hastane_hastaneId",
                        column: x => x.hastaneId,
                        principalTable: "Hastane",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HastaneKlinik_Klinik_klinikId",
                        column: x => x.klinikId,
                        principalTable: "Klinik",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doktor",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    unvan = table.Column<string>(nullable: true),
                    hastaneKlinikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doktor", x => x.id);
                    table.ForeignKey(
                        name: "FK_Doktor_HastaneKlinik_hastaneKlinikId",
                        column: x => x.hastaneKlinikId,
                        principalTable: "HastaneKlinik",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doktor_Kisi_id",
                        column: x => x.id,
                        principalTable: "Kisi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personel",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    pozisyon = table.Column<string>(nullable: true),
                    hastaneKlinikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personel", x => x.id);
                    table.ForeignKey(
                        name: "FK_Personel_HastaneKlinik_hastaneKlinikId",
                        column: x => x.hastaneKlinikId,
                        principalTable: "HastaneKlinik",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personel_Kisi_id",
                        column: x => x.id,
                        principalTable: "Kisi",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevu",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    saat = table.Column<DateTime>(nullable: false),
                    hastaneKlinikId = table.Column<int>(nullable: false),
                    hastaId = table.Column<int>(nullable: false),
                    doktorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevu", x => x.id);
                    table.ForeignKey(
                        name: "FK_Randevu_Doktor_doktorId",
                        column: x => x.doktorId,
                        principalTable: "Doktor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevu_Hasta_hastaId",
                        column: x => x.hastaId,
                        principalTable: "Hasta",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevu_HastaneKlinik_hastaneKlinikId",
                        column: x => x.hastaneKlinikId,
                        principalTable: "HastaneKlinik",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Muayene",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    tani = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muayene", x => x.id);
                    table.ForeignKey(
                        name: "FK_Muayene_Randevu_id",
                        column: x => x.id,
                        principalTable: "Randevu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MuayeneTest",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    muayeneId = table.Column<int>(nullable: false),
                    testId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuayeneTest", x => x.id);
                    table.ForeignKey(
                        name: "FK_MuayeneTest_Muayene_muayeneId",
                        column: x => x.muayeneId,
                        principalTable: "Muayene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuayeneTest_Test_testId",
                        column: x => x.testId,
                        principalTable: "Test",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recete",
                columns: table => new
                {
                    receteNo = table.Column<string>(nullable: false),
                    tarih = table.Column<DateTime>(nullable: false),
                    muayeneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recete", x => x.receteNo);
                    table.ForeignKey(
                        name: "FK_Recete_Muayene_muayeneId",
                        column: x => x.muayeneId,
                        principalTable: "Muayene",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceteIlac",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    receteNo = table.Column<string>(nullable: true),
                    ilacKodu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceteIlac", x => x.id);
                    table.ForeignKey(
                        name: "FK_ReceteIlac_Ilac_ilacKodu",
                        column: x => x.ilacKodu,
                        principalTable: "Ilac",
                        principalColumn: "ilacKodu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceteIlac_Recete_receteNo",
                        column: x => x.receteNo,
                        principalTable: "Recete",
                        principalColumn: "receteNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doktor_hastaneKlinikId",
                table: "Doktor",
                column: "hastaneKlinikId");

            migrationBuilder.CreateIndex(
                name: "IX_Hastane_ilceId",
                table: "Hastane",
                column: "ilceId");

            migrationBuilder.CreateIndex(
                name: "IX_HastaneKlinik_hastaneId",
                table: "HastaneKlinik",
                column: "hastaneId");

            migrationBuilder.CreateIndex(
                name: "IX_HastaneKlinik_klinikId",
                table: "HastaneKlinik",
                column: "klinikId");

            migrationBuilder.CreateIndex(
                name: "IX_Ilce_sehirId",
                table: "Ilce",
                column: "sehirId");

            migrationBuilder.CreateIndex(
                name: "IX_Kisi_kanGrubuId",
                table: "Kisi",
                column: "kanGrubuId");

            migrationBuilder.CreateIndex(
                name: "IX_Kisi_sehirId",
                table: "Kisi",
                column: "sehirId");

            migrationBuilder.CreateIndex(
                name: "IX_MuayeneTest_muayeneId",
                table: "MuayeneTest",
                column: "muayeneId");

            migrationBuilder.CreateIndex(
                name: "IX_MuayeneTest_testId",
                table: "MuayeneTest",
                column: "testId");

            migrationBuilder.CreateIndex(
                name: "IX_Personel_hastaneKlinikId",
                table: "Personel",
                column: "hastaneKlinikId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_doktorId",
                table: "Randevu",
                column: "doktorId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_hastaId",
                table: "Randevu",
                column: "hastaId");

            migrationBuilder.CreateIndex(
                name: "IX_Randevu_hastaneKlinikId",
                table: "Randevu",
                column: "hastaneKlinikId");

            migrationBuilder.CreateIndex(
                name: "IX_Recete_muayeneId",
                table: "Recete",
                column: "muayeneId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceteIlac_ilacKodu",
                table: "ReceteIlac",
                column: "ilacKodu");

            migrationBuilder.CreateIndex(
                name: "IX_ReceteIlac_receteNo",
                table: "ReceteIlac",
                column: "receteNo");

            migrationBuilder.CreateIndex(
                name: "IX_Sehir_ulkeId",
                table: "Sehir",
                column: "ulkeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MuayeneTest");

            migrationBuilder.DropTable(
                name: "Personel");

            migrationBuilder.DropTable(
                name: "ReceteIlac");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Ilac");

            migrationBuilder.DropTable(
                name: "Recete");

            migrationBuilder.DropTable(
                name: "Muayene");

            migrationBuilder.DropTable(
                name: "Randevu");

            migrationBuilder.DropTable(
                name: "Doktor");

            migrationBuilder.DropTable(
                name: "Hasta");

            migrationBuilder.DropTable(
                name: "HastaneKlinik");

            migrationBuilder.DropTable(
                name: "Kisi");

            migrationBuilder.DropTable(
                name: "Hastane");

            migrationBuilder.DropTable(
                name: "Klinik");

            migrationBuilder.DropTable(
                name: "KanGrubu");

            migrationBuilder.DropTable(
                name: "Ilce");

            migrationBuilder.DropTable(
                name: "Sehir");

            migrationBuilder.DropTable(
                name: "Ulke");
        }
    }
}
