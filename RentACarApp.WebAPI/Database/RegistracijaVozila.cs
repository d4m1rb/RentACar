using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class RegistracijaVozila
    {
        public int RegistracijaVozilaId { get; set; }
        public int UposlenikId { get; set; }
        public int AutomobilId { get; set; }
        public string RegistarskeOznake { get; set; }
        public string Trajanje { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public DateTime VaziDo { get; set; }
        public bool Status { get; set; }
        public decimal UkupanIznos { get; set; }

        public Automobil Automobil { get; set; }
        public Korisnici Uposlenik { get; set; }
    }
}
