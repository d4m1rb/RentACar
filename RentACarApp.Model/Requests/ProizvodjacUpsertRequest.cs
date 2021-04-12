using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class ProizvodjacUpsertRequest
    {
        public int DrzavaId { get; set; }
        public string Naziv { get; set; }
        public byte[] Slika{ get; set; }
    }
}
