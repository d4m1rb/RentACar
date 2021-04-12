using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class KlijentiAdmVM
    {
        public class Row
        {
            public int KlijentId { get; set; }
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
            public string ImePrezime { get; set; }
            
            public string NazivGrada { get; set; }
        }
        public List<Row> listaKlijenata { get; set; } = new List<Row>();
    }
}
