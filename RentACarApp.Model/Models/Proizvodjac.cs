using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.Model.Models
{
    public class Proizvodjac
    {
        public int ProizvodjacId { get; set; }
        public int DrzavaId { get; set; }
        public string Naziv { get; set; }
        public byte[] Slika { get; set; }
    }
}
