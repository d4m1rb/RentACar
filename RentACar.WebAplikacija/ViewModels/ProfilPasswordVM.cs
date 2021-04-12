using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RentACar.WebAplikacija.ViewModels
{
    public class ProfilPasswordVM
    {
        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string StariPassword { get; set; }
        public string Password { get; set; }
        public string PasswordPotvrda { get; set; }
        public bool Status { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public string Poruka { get; set; }


    }
}
