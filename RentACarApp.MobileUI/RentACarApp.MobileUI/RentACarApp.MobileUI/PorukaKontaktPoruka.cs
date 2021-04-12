using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.MobileUI
{
    public class PorukaKontaktPoruka
    {
        public int PorukaId { get; set; }
        public int RezervacijaRentanjaId { get; set; }
        public int KlijentId { get; set; }
        public int UposlenikId { get; set; }
        public string Sadrzaj { get; set; }
        public string Naslov { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public bool Procitano { get; set; }
        public bool NijeProcitano { get; set; }
        public string Posiljaoc { get; set; }
        public string PosiljaocInfo { get; set; }
        public string PrimaocInfo { get; set; }
        public byte[] PosiljaocSlikaThumb { get; set; }


        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Drzava { get; set; }

        public byte[] slikaThumb { get; set; }

    }
}
