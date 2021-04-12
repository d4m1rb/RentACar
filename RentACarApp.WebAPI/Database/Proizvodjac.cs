using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class Proizvodjac
    {
        public Proizvodjac()
        {
            Model = new HashSet<Model>();
        }

        public int ProizvodjacId { get; set; }
        public int DrzavaId { get; set; }
        public string Naziv { get; set; }
        public byte[] Slika { get; set; }

        public Drzava Drzava { get; set; }
        public ICollection<Model> Model { get; set; }
    }
}
