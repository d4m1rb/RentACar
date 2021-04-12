using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class VoziloAdmProduziRegistracijuVM
    {
        public int AutomobilId { get; set; }
        public string ProizvodjacModel { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public DateTime? RegistrovanDo { get; set; }
        public string RegistrovanDoStr { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public string DatumRegistracijeStr { get; set; }
        public string RegistarskaOznaka { get; set; }
        public string Trajanje { get; set; }
        public decimal IznosRegistracije { get; set; }

        public List<SelectListItem> listaTrajanje { get; set; } = new List<SelectListItem>();
    }
}
