using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACarApp.WebAPI.Migrations
{
    public partial class ObrisanoKlijentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ObrisanoKorisnik",
                table: "Poruka",
                newName: "ObrisanoKlijent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ObrisanoKlijent",
                table: "Poruka",
                newName: "ObrisanoKorisnik");
        }
    }
}
