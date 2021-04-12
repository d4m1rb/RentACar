using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
   public class OcjenaUpsertRequest
    {
        public int OcjenaId { get; set; }
        public int RezervacijaRentanjaId { get; set; }
        public DateTime DatumEvidentiranja { get; set; }
        public int Ocjena1 { get; set; }
        public string Napomena { get; set; }
    }
}
