using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class AutomobilSearchRequest
    {
        public int AutomobilId { get; set; }
        public int? ModelId { get; set; }

        public int? ProizvodjacId { get; set; }
        public int? KategorijaId { get; set; }
        public int? GodinaProizvodnje { get; set; }
        public bool? Dostupan { get; set; }
        public string RegistarskaOznaka { get; set; }
    }
}
