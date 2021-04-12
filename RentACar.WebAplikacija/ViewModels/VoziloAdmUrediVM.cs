using RentACarApp.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class VoziloAdmUrediVM
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
        public string RegistrovanDoStr { get; set; }
        public string RegistarskaOznaka { get; set; }
        public string ProizvodjacModel { get; set; }
        public int ProizvodjacId{ get; set; }
        public string DostupanTekst { get; set; }
        public decimal CijenaIznajmljivanja { get; set; }
        public decimal CijenaKaskoOsiguranja { get; set; }
        public List<SelectListItem> listaProizvodjac { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> listaModel { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> listaKategorija { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> listaTransmisija { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> listaEmisioniStandard { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> listaGorivo { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> listaBrojVrata { get; set; } = new List<SelectListItem>();
        public IFormFile slikaVozila { get; set; }

        #region Dio modela koji se koristi za dodavanje vozila
        public DateTime DatumRegistracije { get; set; }
        public string DatumRegistracijeStr { get; set; }
        public List<SelectListItem> listaTrajanje { get; set; } = new List<SelectListItem>();
        public string Trajanje { get; set; }
        public decimal IznosRegistracije { get; set; }
        #endregion
    }
}
