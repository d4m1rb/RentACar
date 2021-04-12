using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class Grad
    {
        public Grad()
        {
            Klijent = new HashSet<Klijent>();
            Korisnici = new HashSet<Korisnici>();
        }

        public int GradId { get; set; }
        public int DrzavaId { get; set; }
        public string Naziv { get; set; }
        public string PostanskiBroj { get; set; }

        public Drzava Drzava { get; set; }
        public ICollection<Klijent> Klijent { get; set; }
        public ICollection<Korisnici> Korisnici { get; set; }
    }
}
