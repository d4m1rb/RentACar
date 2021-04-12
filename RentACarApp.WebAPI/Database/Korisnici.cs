using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class Korisnici
    {
        public Korisnici()
        {
            KorisniciUloge = new List<KorisniciUloge>();
            Poruka = new HashSet<Poruka>();
            RegistracijaVozila = new HashSet<RegistracijaVozila>();
        }

        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public string Telefon { get; set; }
        public int GradId { get; set; }
        public string Adresa { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
        public bool Status { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }

        public Grad Grad { get; set; }
        public ICollection<KorisniciUloge> KorisniciUloge { get; set; }
        public ICollection<Poruka> Poruka { get; set; }
        public ICollection<RegistracijaVozila> RegistracijaVozila { get; set; }
    }
}
