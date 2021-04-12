using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class Racun
    {
        public Racun()
        {
            RezervacijaRentanja = new HashSet<RezervacijaRentanja>();
        }

        public int RacunId { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public decimal UkupanIznos { get; set; }

        public ICollection<RezervacijaRentanja> RezervacijaRentanja { get; set; }
    }
}
