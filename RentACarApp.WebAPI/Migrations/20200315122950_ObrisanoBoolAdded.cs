using Microsoft.EntityFrameworkCore.Migrations;

namespace RentACarApp.WebAPI.Migrations
{
    public partial class ObrisanoBoolAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ObrisanoKorisnik",
                table: "Poruka",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ObrisanoUposlenik",
                table: "Poruka",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObrisanoKorisnik",
                table: "Poruka");

            migrationBuilder.DropColumn(
                name: "ObrisanoUposlenik",
                table: "Poruka");
        }
    }
}
