using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class RezervacijaRentanjaSearchRequest
    {
        public int RezervacijaRentanjaId { get; set; }
        public int RacunId { get; set; }
        public int AutomobilId { get; set; }
        public int KlijentId { get; set; }
        public DateTime? DatumKreiranja { get; set; }
        public bool? VracanjeUposlovnicu { get; set; }
        public DateTime? RezervacijaOd { get; set; }
        public DateTime? RezervacijaDo { get; set; }
        public bool? KaskoOsiguranje { get; set; }
        public bool? StatusAktivna { get; set; }
        public bool? Otkazana { get; set; }

        public string Username { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string RegistarskaOznaka { get; set; }
        public int ModelId { get; set; }
        public int ProizvodjacId { get; set; }


    }
}
