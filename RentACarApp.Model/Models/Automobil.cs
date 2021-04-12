using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Models
{
    public class Automobil
    {
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
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public DateTime? RegistrovanDo { get; set; }
        public string RegistarskaOznaka { get; set; }
        public string ProizvodjacModel { get; set; }
        public string DostupanTekst { get; set; }
        public decimal CijenaIznajmljivanja { get; set; }
        public decimal CijenaKaskoOsiguranja { get; set; }
        
    }
}
