using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class VoziloAdmNoviProizvodjacVM
    {
        public int AutomobilId { get; set; }
        public string NazivProizvodjaca { get; set; }
        public byte[] Slika { get; set; }
        public int DrzavaId { get; set; }
        public List<SelectListItem> listaDrzava { get; set; } = new List<SelectListItem>();
        public IFormFile slikaProizvodjaca { get; set; }

    }
}
