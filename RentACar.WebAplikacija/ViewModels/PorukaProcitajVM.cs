using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class PorukaProcitajVM
    {
        public ObservableCollection<Poruka> listaPoruka { get; set; } = new ObservableCollection<Poruka>();
        public ObservableCollection<KontaktPoruka> kontaktPodaci { get; set; } = new ObservableCollection<KontaktPoruka>();
    }
}
