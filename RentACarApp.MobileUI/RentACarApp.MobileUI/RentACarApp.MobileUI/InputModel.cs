using RentACarApp.MobileUI.ViewModels.Vozila;
using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentACarApp.MobileUI
{
    public class InputModel
    {
      
            public Automobil _automobil { get; set; }
            public List<AutomobilVM> _listaDostupnihVozila { get; set; }
            public string _porukaDostupnaVozila { get; set; }
            public bool _porukaDostupnaVozilaBool { get; set; }
            public DateTime _datumRezervacijeOd { get; set; }
            public DateTime _datumRezervacijeDo { get; set; }
            public string _lokacijaPreuzimanja { get; set; }
            public bool _vracanjeUPoslovnicu { get; set; }
        public bool _kaskoOsiguranje { get; set; }
        public string _cijenaKaskoString { get; set; }
        public decimal _ukupnoCijena { get; set; }
        public string _ukupnoCijenaString { get; set; }
        public decimal _popust { get; set; }
        public string _popustString { get; set; }

    }
}
