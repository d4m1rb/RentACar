using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class RegistracijaVozilaSearchRequest
    {
        public int RegistracijaVozilaId { get; set; }
        public int UposlenikId { get; set; }
        public int AutomobilId { get; set; }
        public string Trajanje { get; set; }
        public string RegistarskeOznake { get; set; }

        public DateTime DatumRegistracije { get; set; }
        public DateTime VaziDo { get; set; }

        public bool Status { get; set; }
    }
}
