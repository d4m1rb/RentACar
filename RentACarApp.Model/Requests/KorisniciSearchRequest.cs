using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class KorisniciSearchRequest
    {
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LozinkaHash { get; set; }        
        public DateTime? DatumRegistracijeOd { get; set; }
        public DateTime? DatumRegistracijeDo { get; set; }
        public int? GradId { get; set; }
        public bool Status { get; set; }
        public List<string> uloge { get; set; }
    }
}
