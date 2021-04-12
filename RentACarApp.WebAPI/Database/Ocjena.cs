using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class Ocjena
    {
        public int OcjenaId { get; set; }
        public int RezervacijaRentanjaId { get; set; }
        public DateTime DatumEvidentiranja { get; set; }
        public int Ocjena1 { get; set; }
        public string Napomena { get; set; }

        public RezervacijaRentanja RezervacijaRentanja { get; set; }
    }
}
