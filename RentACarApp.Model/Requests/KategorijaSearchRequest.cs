using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class KategorijaSearchRequest
    {
        public int KategorijaId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
    }
}
