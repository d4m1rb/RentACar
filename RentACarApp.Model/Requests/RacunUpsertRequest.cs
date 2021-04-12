using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class RacunUpsertRequest
    {
        public int RacunId { get; set; }
        public DateTime DatumIzdavanja { get; set; }
        public decimal UkupanIznos { get; set; }

    }
}
