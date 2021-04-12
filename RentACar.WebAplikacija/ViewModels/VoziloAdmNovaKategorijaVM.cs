using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class VoziloAdmNovaKategorijaVM
    {
        public int AutomobilId { get; set; }
        public string NazivKategorije { get; set; }
        public string Opis { get; set; }       

    }
}
