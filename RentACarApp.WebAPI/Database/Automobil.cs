using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class Automobil
    {
        public Automobil()
        {
            RegistracijaVozila = new HashSet<RegistracijaVozila>();
            RezervacijaRentanja = new HashSet<RezervacijaRentanja>();
        }

        public int AutomobilId { get; set; }
        public int ModelId { get; set; }
        public int KategorijaId { get; set; }
        public int GodinaProizvodnje { get; set; }
        public string SnagaMotora { get; set; }
        public string Kubikaza { get; set; }
        public string Transmisija { get; set; }
        public string EmisioniStandard { get; set; }
        public string Gorivo { get; set; }
        public string Potrosnja { get; set; }
        public string Boja { get; set; }
        public string BrojSjedista { get; set; }
        public string BrojVrata { get; set; }
        public bool Dostupan { get; set; }
        public bool Novo { get; set; }
        public decimal CijenaIznajmljivanja { get; set; }
        public decimal CijenaKaskoOsiguranja { get; set; }

        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }

        public KategorijaVozila Kategorija { get; set; }
        public Model Model { get; set; }
        public ICollection<RegistracijaVozila> RegistracijaVozila { get; set; }
        public ICollection<RezervacijaRentanja> RezervacijaRentanja { get; set; }
    }
}
