using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class VoziloAdmNoviModelVM
    {
        public int AutomobilId { get; set; }
        public string NazivModela { get; set; }
        public int ProizvodjacId { get; set; }
        public List<SelectListItem> listaProizvodjaca { get; set; } = new List<SelectListItem>();

    }
}
