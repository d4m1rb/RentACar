using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACarApp.WebAPI.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    DrzavaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.DrzavaID);
                });

            migrationBuilder.CreateTable(
                name: "KategorijaVozila",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijaVozila", x => x.KategorijaID);
                });

            migrationBuilder.CreateTable(
                name: "Racun",
                columns: table => new
                {
                    RacunID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatumIzdavanja = table.Column<DateTime>(type: "datetime", nullable: false),
                    UkupanIznos = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racun", x => x.RacunID);
                });

            migrationBuilder.CreateTable(
                name: "Uloge",
                columns: table => new
                {
                    UlogaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloge", x => x.UlogaID);
                });

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    GradID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DrzavaID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    PostanskiBroj = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.GradID);
                    table.ForeignKey(
                        name: "FK_Grad_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzava",
                        principalColumn: "DrzavaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodjac",
                columns: table => new
                {
                    ProizvodjacID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DrzavaID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    Slika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodjac", x => x.ProizvodjacID);
                    table.ForeignKey(
                        name: "FK_Proizvodjac_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzava",
                        principalColumn: "DrzavaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Klijent",
                columns: table => new
                {
                    KlijentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(maxLength: 50, nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime", nullable: false),
                    DatumRegistracije = table.Column<DateTime>(type: "datetime", nullable: false),
                    Telefon = table.Column<string>(maxLength: 20, nullable: true),
                    GradID = table.Column<int>(nullable: false),
                    Adresa = table.Column<string>(maxLength: 100, nullable: false),
                    LozinkaHash = table.Column<string>(maxLength: 50, nullable: false),
                    LozinkaSalt = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Slika = table.Column<byte[]>(nullable: true),
                    SlikaThumb = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijent", x => x.KlijentID);
                    table.ForeignKey(
                        name: "FK_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(maxLength: 50, nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    DatumRodjenja = table.Column<DateTime>(nullable: true),
                    DatumRegistracije = table.Column<DateTime>(nullable: false),
                    Telefon = table.Column<string>(maxLength: 20, nullable: true),
                    GradID = table.Column<int>(nullable: false),
                    Adresa = table.Column<string>(maxLength: 100, nullable: true),
                    LozinkaHash = table.Column<string>(maxLength: 50, nullable: false),
                    LozinkaSalt = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Slika = table.Column<byte[]>(nullable: true),
                    SlikaThumb = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikID);
                    table.ForeignKey(
                        name: "FK_User_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    ModelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProizvodjacID = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.ModelID);
                    table.ForeignKey(
                        name: "FK_Model_ProizvodjacID",
                        column: x => x.ProizvodjacID,
                        principalTable: "Proizvodjac",
                        principalColumn: "ProizvodjacID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KorisniciUloge",
                columns: table => new
                {
                    KorisniciUlogeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KorisnikID = table.Column<int>(nullable: false),
                    UlogaID = table.Column<int>(nullable: false),
                    DatumIzmjene = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisniciUloge", x => x.KorisniciUlogeID);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserID",
                        column: x => x.KorisnikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleID",
                        column: x => x.UlogaID,
                        principalTable: "Uloge",
                        principalColumn: "UlogaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Automobil",
                columns: table => new
                {
                    AutomobilID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModelID = table.Column<int>(nullable: false),
                    KategorijaID = table.Column<int>(nullable: false),
                    GodinaProizvodnje = table.Column<int>(nullable: false),
                    SnagaMotora = table.Column<string>(maxLength: 50, nullable: false),
                    Kubikaza = table.Column<string>(maxLength: 20, nullable: false),
                    Transmisija = table.Column<string>(maxLength: 20, nullable: false),
                    EmisioniStandard = table.Column<string>(maxLength: 20, nullable: false),
                    Gorivo = table.Column<string>(maxLength: 50, nullable: false),
                    Potrosnja = table.Column<string>(maxLength: 50, nullable: true),
                    Boja = table.Column<string>(maxLength: 20, nullable: false),
                    BrojSjedista = table.Column<string>(maxLength: 10, nullable: false),
                    BrojVrata = table.Column<string>(maxLength: 10, nullable: false),
                    Dostupan = table.Column<bool>(nullable: false),
                    Novo = table.Column<bool>(nullable: false),
                    CijenaIznajmljivanja = table.Column<decimal>(nullable: false),
                    CijenaKaskoOsiguranja = table.Column<decimal>(nullable: false),
                    Slika = table.Column<byte[]>(nullable: true),
                    SlikaThumb = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobil", x => x.AutomobilID);
                    table.ForeignKey(
                        name: "FK_Automobil_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "KategorijaVozila",
                        principalColumn: "KategorijaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Automobil_ModelID",
                        column: x => x.ModelID,
                        principalTable: "Model",
                        principalColumn: "ModelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistracijaVozila",
                columns: table => new
                {
                    RegistracijaVozilaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UposlenikID = table.Column<int>(nullable: false),
                    AutomobilID = table.Column<int>(nullable: false),
                    RegistarskeOznake = table.Column<string>(maxLength: 20, nullable: false),
                    Trajanje = table.Column<string>(maxLength: 50, nullable: false),
                    DatumRegistracije = table.Column<DateTime>(type: "datetime", nullable: false),
                    VaziDo = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    UkupanIznos = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistracijaVozila", x => x.RegistracijaVozilaID);
                    table.ForeignKey(
                        name: "FK_Registracija_AutomobilID",
                        column: x => x.AutomobilID,
                        principalTable: "Automobil",
                        principalColumn: "AutomobilID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistracijaVozila_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijaRentanja",
                columns: table => new
                {
                    RezervacijaRentanjaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RacunID = table.Column<int>(nullable: false),
                    AutomobilID = table.Column<int>(nullable: false),
                    KlijentID = table.Column<int>(nullable: false),
                    DatumKreiranja = table.Column<DateTime>(type: "datetime", nullable: false),
                    LokacijaPreuzimanja = table.Column<string>(maxLength: 100, nullable: true),
                    VracanjeUPoslovnicu = table.Column<bool>(nullable: false),
                    Opis = table.Column<string>(nullable: true),
                    RezervacijaOd = table.Column<DateTime>(type: "datetime", nullable: false),
                    RezervacijaDo = table.Column<DateTime>(type: "datetime", nullable: false),
                    KaskoOsiguranje = table.Column<bool>(nullable: false),
                    Otkazana = table.Column<bool>(nullable: false),
                    Popust = table.Column<double>(nullable: false),
                    IznosSaPopustom = table.Column<decimal>(nullable: true),
                    Iznos = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijaRentanja", x => x.RezervacijaRentanjaID);
                    table.ForeignKey(
                        name: "FK_RezervacijaRentanja_AutomobilID",
                        column: x => x.AutomobilID,
                        principalTable: "Automobil",
                        principalColumn: "AutomobilID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaRentanja_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijent",
                        principalColumn: "KlijentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RezervacijaRentanja_RacunID",
                        column: x => x.RacunID,
                        principalTable: "Racun",
                        principalColumn: "RacunID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ocjena",
                columns: table => new
                {
                    OcjenaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RezervacijaRentanjaID = table.Column<int>(nullable: false),
                    DatumEvidentiranja = table.Column<DateTime>(type: "datetime", nullable: false),
                    Ocjena = table.Column<int>(nullable: false),
                    Napomena = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocjena", x => x.OcjenaID);
                    table.ForeignKey(
                        name: "FK_Ocjena_RezervacijaRentanja_RezervacijaRentanjaID",
                        column: x => x.RezervacijaRentanjaID,
                        principalTable: "RezervacijaRentanja",
                        principalColumn: "RezervacijaRentanjaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poruka",
                columns: table => new
                {
                    PorukaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RezervacijaRentanjaID = table.Column<int>(nullable: false),
                    KlijentID = table.Column<int>(nullable: false),
                    UposlenikID = table.Column<int>(nullable: false),
                    Naslov = table.Column<string>(maxLength: 100, nullable: false),
                    Sadrzaj = table.Column<string>(nullable: false),
                    DatumVrijeme = table.Column<DateTime>(type: "datetime", nullable: false),
                    Procitano = table.Column<bool>(nullable: false),
                    Posiljaoc = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poruka", x => x.PorukaID);
                    table.ForeignKey(
                        name: "FK_Poruka_Klijent_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijent",
                        principalColumn: "KlijentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poruka_RezervacijaRentanjaID",
                        column: x => x.RezervacijaRentanjaID,
                        principalTable: "RezervacijaRentanja",
                        principalColumn: "RezervacijaRentanjaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Poruka_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automobil_KategorijaID",
                table: "Automobil",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Automobil_ModelID",
                table: "Automobil",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Grad_DrzavaID",
                table: "Grad",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_Klijent_GradID",
                table: "Klijent",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_GradID",
                table: "Korisnici",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciUloge_KorisnikID",
                table: "KorisniciUloge",
                column: "KorisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciUloge_UlogaID",
                table: "KorisniciUloge",
                column: "UlogaID");

            migrationBuilder.CreateIndex(
                name: "IX_Model_ProizvodjacID",
                table: "Model",
                column: "ProizvodjacID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjena_RezervacijaRentanjaID",
                table: "Ocjena",
                column: "RezervacijaRentanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Poruka_KlijentID",
                table: "Poruka",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_Poruka_RezervacijaRentanjaID",
                table: "Poruka",
                column: "RezervacijaRentanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Poruka_UposlenikID",
                table: "Poruka",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvodjac_DrzavaID",
                table: "Proizvodjac",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_RegistracijaVozila_AutomobilID",
                table: "RegistracijaVozila",
                column: "AutomobilID");

            migrationBuilder.CreateIndex(
                name: "IX_RegistracijaVozila_UposlenikID",
                table: "RegistracijaVozila",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaRentanja_AutomobilID",
                table: "RezervacijaRentanja",
                column: "AutomobilID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaRentanja_KlijentID",
                table: "RezervacijaRentanja",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijaRentanja_RacunID",
                table: "RezervacijaRentanja",
                column: "RacunID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisniciUloge");

            migrationBuilder.DropTable(
                name: "Ocjena");

            migrationBuilder.DropTable(
                name: "Poruka");

            migrationBuilder.DropTable(
                name: "RegistracijaVozila");

            migrationBuilder.DropTable(
                name: "Uloge");

            migrationBuilder.DropTable(
                name: "RezervacijaRentanja");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Automobil");

            migrationBuilder.DropTable(
                name: "Klijent");

            migrationBuilder.DropTable(
                name: "Racun");

            migrationBuilder.DropTable(
                name: "KategorijaVozila");

            migrationBuilder.DropTable(
                name: "Model");

            migrationBuilder.DropTable(
                name: "Grad");

            migrationBuilder.DropTable(
                name: "Proizvodjac");

            migrationBuilder.DropTable(
                name: "Drzava");
        }
    }
}
