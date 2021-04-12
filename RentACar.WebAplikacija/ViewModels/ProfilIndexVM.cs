using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RentACar.WebAplikacija.ViewModels
{
    public class ProfilIndexVM
    {
        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public string DatumRodjenjaStr { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public string DatumRegistracijeStr { get; set; }
        public string Telefon { get; set; }
        public int GradId { get; set; }
        public string Adresa { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
        public bool Status { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public string SlikaPath { get; set; }
        public string Poruka { get; set; }

        public List<SelectListItem> listaGradova { get; set; }
    }
}
