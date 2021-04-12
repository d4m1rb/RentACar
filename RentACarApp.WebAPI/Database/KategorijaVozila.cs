using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class KategorijaVozila
    {
        public KategorijaVozila()
        {
            Automobil = new HashSet<Automobil>();
        }

        public int KategorijaId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }

        public ICollection<Automobil> Automobil { get; set; }
    }
}
