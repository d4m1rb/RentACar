using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
   public class RezervacijaRentanjaUpsertRequest
    {
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
        public decimal Iznos { get; set; }
        public decimal IznosSaPopustom { get; set; }
    }
}
