using RentACarApp.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class RezervacijePretragaVM
    {
        public class Row
        {
            public int AutomobilId { get; set; }
            public int ModelId { get; set; }
            public int KategorijaId { get; set; }
            public int GodinaProizvodnje { get; set; }
            public string SnagaMotora { get; set; }
            public string Kubikaza { get; set; }
            public string KubikazaString { get; set; }
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
            public decimal ProsjecnaOcjena { get; set; }
        }

        public int kategorijaVozilaId { get; set; }
        public List<SelectListItem> listaKategorija { get; set; } = new List<SelectListItem>();
        public List<Row> listaAutomobila { get; set; } = new List<Row>();
        public string _porukaDostupnaVozila { get; set; }
        public bool _porukaDostupnaVozilaBool { get; set; }
        public DateTime DatumRezervacijaOd { get; set; }
        public DateTime DatumRezervacijaDo { get; set; }

    }
}
