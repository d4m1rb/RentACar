using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class Drzava
    {
        public Drzava()
        {
            Grad = new HashSet<Grad>();
            Proizvodjac = new HashSet<Proizvodjac>();
        }

        public int DrzavaId { get; set; }
        public string Naziv { get; set; }

        public ICollection<Grad> Grad { get; set; }
        public ICollection<Proizvodjac> Proizvodjac { get; set; }
    }
}
