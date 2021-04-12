using RentACarApp.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class KorisniciAdmDodajVM
    {
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public string DatumRodjenjaString { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public string DatumRegistracijeString { get; set; }
        public string Telefon { get; set; }
        public int GradId { get; set; }
        public string Adresa { get; set; }
        public string Password { get; set; }
        public string PasswordPotvrda { get; set; }
        public bool Status { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public string ImePrezime { get; set; }
        public ICollection<KorisniciUloge> KorisniciUloge { get; set; }

        public List<SelectListItem> listaGradova { get; set; } = new List<SelectListItem>();
        public string listaUlogaString { get; set; }
        public List<Uloge> listaUlogaKorisnik { get; set; } = new List<Uloge>();
        public List<Uloge> listaUlogaAll { get; set; } = new List<Uloge>();

        public List<UlogeCustomModel> listaSelektovanihUloga { get; set; } = new List<UlogeCustomModel>();
        public string NazivGrada { get; set; }
        public List<string> listaUserName { get; set; } = new List<string>();

        public IFormFile slikaProfila { get; set; }
        public List<bool> listaUlogaBool { get; set; }

    }
}
