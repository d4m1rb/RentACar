using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class HomeIndexVM
    {
        public DateTime? DatumZadnjeRezervacije { get; set; }
        public int BrojIznajmljivanja { get; set; }
        public int BrojVozila { get; set; }
        public int BrojKategorija { get; set; }
    }
}
