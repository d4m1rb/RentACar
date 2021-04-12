using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class KorisniciUlogeSearchRequest
    {
        public int KorisnikId { get; set; }
        public int UlogaId { get; set; }
        public string NazivUloge { get; set; }
    }
}
