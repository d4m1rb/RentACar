using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Models
{
    public class KorisniciUloge
    {
        public int KorisniciUlogeId { get; set; }
        public int KorisnikId { get; set; }
        public int UlogaId { get; set; }
        public DateTime DatumIzmjene { get; set; }

        public Korisnici Korisnik { get; set; }
        public Uloge Uloga { get; set; }
    }
}
