using System;
using System.Collections.Generic;

namespace RentACarApp.WebAPI.Database
{
    public partial class Model
    {
        public Model()
        {
            Automobil = new HashSet<Automobil>();
        }

        public int ModelId { get; set; }
        public int ProizvodjacId { get; set; }
        public string Naziv { get; set; }

        public Proizvodjac Proizvodjac { get; set; }
        public ICollection<Automobil> Automobil { get; set; }
    }
}
