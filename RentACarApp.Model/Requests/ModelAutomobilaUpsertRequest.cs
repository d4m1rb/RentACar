using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class ModelAutomobilaUpsertRequest
    {
        public int ModelId { get; set; }
        public int ProizvodjacId { get; set; }
        public string Naziv { get; set; }
    }
}
