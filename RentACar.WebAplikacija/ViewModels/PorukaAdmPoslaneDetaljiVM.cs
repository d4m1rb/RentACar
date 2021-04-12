using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class PorukaAdmPoslaneDetaljiVM
    {
       
        public int PorukaId { get; set; }
        public int RezervacijaRentanjaId { get; set; }
        public int KlijentId { get; set; }
        public int UposlenikId { get; set; }
        public string Sadrzaj { get; set; }
        public string Naslov { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public bool Procitano { get; set; }
        public string Posiljaoc { get; set; }
        public string PosiljaocInfo { get; set; }
        public string PrimaocInfo { get; set; }
        public byte[] PosiljaocSlikaThumb { get; set; }
       
    }
}
