using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class PorukaDetaljiVM
    {
        public Poruka porukaKlijent { get; set; }
        public Poruka porukaUposlenik { get; set; }
        public int RezervacijaID { get; set; }
    }
}
