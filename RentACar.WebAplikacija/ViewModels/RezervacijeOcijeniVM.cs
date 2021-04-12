using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class RezervacijeOcijeniVM
    {        
        public int RezervacijaRentanjaId { get; set; }
        public int Ocjena { get; set; }
        public DateTime DatumEvidentiranja { get; set; }
        public string Napomena { get; set; }             

    }
}
