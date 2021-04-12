using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class RezervacijeIndexVM
    {
        public int brojRezervacija { get; set; }
        public int uToku { get; set; }
        public int Zavrsene { get; set; }
        public decimal ukupno { get; set; }
        public ObservableCollection<RezervacijaRentanja> RezervacijeRetanjaList { get; set; }
        public List<int> ListaBrojPoruka { get; set; } = new List<int>();
        public List<int> ListaBrojPorukaZavrsene { get; set; } = new List<int>();
        public ObservableCollection<Ocjena> ListaOcjena { get; set; }
        public ObservableCollection<Ocjena> ListaOcjenaZavrsene { get; set; }
        public ObservableCollection<RezervacijaRentanja> RezervacijeRetanjaListZavrsene { get; set; }


    }
}
