using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class RezervacijeRezervisiVM
    {
        public Automobil _automobil { get; set; }
        public List<Automobil> _listaDostupnihVozila { get; set; }
        public string _poruka { get; set; }
        public bool _porukaBool { get; set; }
        public DateTime _datumRezervacijeOd { get; set; }
        public string _datumRezervacijeOdString { get; set; }
        public DateTime _datumRezervacijeDo { get; set; }
        public string _datumRezervacijeDoString { get; set; }
        public string _lokacijaPreuzimanja { get; set; }
        public bool _vracanjeUPoslovnicu { get; set; }
        public bool _kaskoOsiguranje { get; set; }
        public string _cijenaKaskoString { get; set; }
        public decimal _ukupnoCijena { get; set; }
        public decimal _ukupnoCijenaBezKaska { get; set; }
        public decimal _ukupnoCijenaSaKaskom { get; set; }
        public decimal _popust { get; set; }

        public int AutomobilId { get; set; }

    }
}
