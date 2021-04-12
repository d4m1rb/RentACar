using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class RezervacijePretragaDatumVM
    {
        public DateTime RezervacijaOd { get; set; }
        public string RezervacijaOdString { get; set; }
        public DateTime RezervacijaDo { get; set; }
        public string RezervacijaDoString { get; set; }

        public int kategorijaVozilaId { get; set; } = 0;
    }
}
