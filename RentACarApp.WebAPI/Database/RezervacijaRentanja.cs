using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class RezervacijaRentanja
    {
        public RezervacijaRentanja()
        {
            Ocjena = new HashSet<Ocjena>();
            Poruka = new HashSet<Poruka>();
        }

        public int RezervacijaRentanjaId { get; set; }
        public int RacunId { get; set; }
        public int AutomobilId { get; set; }
        public int KlijentId { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string LokacijaPreuzimanja { get; set; }
        public bool VracanjeUposlovnicu { get; set; }
        public string Opis { get; set; }
        public DateTime RezervacijaOd { get; set; }
        public DateTime RezervacijaDo { get; set; }
        public bool KaskoOsiguranje { get; set; }
        public bool Otkazana { get; set; }
        public double Popust { get; set; }
        public decimal? IznosSaPopustom { get; set; }
        public decimal Iznos { get; set; }

        public Automobil Automobil { get; set; }
        public Klijent Klijent { get; set; }
        public Racun Racun { get; set; }
        public ICollection<Ocjena> Ocjena { get; set; }
        public ICollection<Poruka> Poruka { get; set; }
    }
}
