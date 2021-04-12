
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Models
{
    public class Klijent
    {
        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public string Telefon { get; set; }
        public int GradId { get; set; }
        public string Adresa { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
        public bool Status { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }

    }
}
