using RentACarApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class HomeAdministratorVM
    {
        public int brojVozila { get; set; }
        public int slobodnihVozila { get; set; }
        public int zauzetihVozila { get; set; }
        public int brojRezervacija { get; set; }
        public int rezervacijeOvogMjeseca { get; set; }
        public int rezervacijeOveGodine { get; set; }
        public decimal ukupnaZarada { get; set; }
        public decimal zaradaOvogMjeseca { get; set; }
        public decimal zaradaOveGodine { get; set; }
        public decimal zaradaProteklih7dana { get; set; }
        public int[] zaradaPoMjesecima { get; set; }
        public string zaradaPoMjesecimaString { get; set; }
        public int brojKlijenata { get; set; }
        public int brojKlijenataM { get; set; }
        public int brojKlijenataG { get; set; }

        public int ukupnoVozila { get; set; }
        public string KategorijeVozilaString { get; set; }
        public string KategorijeVozilaBrojString { get; set; }
        public string[] nizBoja { get; set; }
        public List<KategorijaVozila> listaKatVoz { get; set; }
        public int postotakPutnicko { get; set; }
        public int postotakKombi { get; set; }
    }
}
