using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Models
{
    public class Poruka
    {
        public int PorukaId { get; set; }
        public int RezervacijaRentanjaId { get; set; }
        public int KlijentId { get; set; }
        public int UposlenikId { get; set; }
        public string Sadrzaj { get; set; }
        public string Naslov { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public bool Procitano { get; set; }
        public bool ObrisanoUposlenik { get; set; }
        public bool ObrisanoKlijent { get; set; }
        public string Posiljaoc { get; set; }
        public string PosiljaocInfo { get; set; }
        public string PrimaocInfo { get; set; }
        public byte[] PosiljaocSlikaThumb { get; set; }


    }
}
