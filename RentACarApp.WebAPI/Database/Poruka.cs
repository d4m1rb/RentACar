using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class Poruka
    {
        public int PorukaId { get; set; }
        public int RezervacijaRentanjaId { get; set; }
        public int KlijentId { get; set; }
        public int UposlenikId { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public bool Procitano { get; set; }
        public string Posiljaoc { get; set; }
        public bool ObrisanoUposlenik { get; set; }
        public bool ObrisanoKlijent { get; set; }
        public Klijent Klijent { get; set; }
        public RezervacijaRentanja RezervacijaRentanja { get; set; }
        public Korisnici Uposlenik { get; set; }
    }
}
