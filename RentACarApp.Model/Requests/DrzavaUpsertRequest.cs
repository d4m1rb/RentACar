using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class DrzavaUpsertRequest
    {
        public int DrzavaId { get; set; }
        public string Naziv { get; set; }
    }
}
