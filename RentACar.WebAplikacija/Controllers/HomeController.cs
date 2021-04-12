using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentACar.WebAplikacija.Models;
using Microsoft.AspNetCore.Authorization;
using RentACar.WebAplikacija.Autorizacija;
using RentACar.WebAplikacija.Helper;
using RentACarApp.Model.Requests;
using RentACarApp.Model.Models;
using RentACar.WebAplikacija.ViewModels;

namespace RentACarApp.Web.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly APIService _rezervacijeService = new APIService("RezervacijaRentanja");
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _kategorijaVozilaService = new APIService("KategorijaVozila");
        private readonly APIService _klijentService = new APIService("Klijent");
       

        [Autorizacija(klijent: true, administrator: false)]
        public async Task<IActionResult> Index()
        {
           
            var logiraniKlijent = await HttpContext.GetLogiraniKorisnik();
            DateTime? datumZadnjeRezervacije=null;
            int brojIznajmljivanja, brojVozila, brojKategorija;

            HomeIndexVM model = new HomeIndexVM();           

            AutomobilSearchRequest vozilaSearch = new AutomobilSearchRequest()
            {
                Dostupan = true
            };
            var vozilaSlobodna = await _vozilaService.Get<List<Automobil>>(vozilaSearch);
            brojVozila = vozilaSlobodna.Count();

            var kategorije = await _kategorijaVozilaService.Get<List<KategorijaVozila>>(null);
            brojKategorija = kategorije.Count();

            var rezervacije = await _rezervacijeService.Get<List<RezervacijaRentanja>>(new RezervacijaRentanjaSearchRequest() { KlijentId = logiraniKlijent.KlijentId });
            rezervacije = rezervacije.OrderByDescending(x => x.DatumKreiranja).ToList();
            brojIznajmljivanja = rezervacije.Count();
            
            if(brojIznajmljivanja!=0)
                datumZadnjeRezervacije = rezervacije.FirstOrDefault().DatumKreiranja;

            model.BrojVozila = brojVozila;
            model.DatumZadnjeRezervacije = datumZadnjeRezervacije;
            model.BrojKategorija = brojKategorija;
            model.BrojIznajmljivanja = brojIznajmljivanja;
                        
            return View(model);
        }

        [Autorizacija(klijent: false, administrator: true)]
        public async Task<IActionResult> Administrator()
        {
            var logiraniKlijent = await HttpContext.GetLogiraniKorisnik();

            HomeAdministratorVM model = new HomeAdministratorVM();

            AutomobilSearchRequest vozilaSearch = new AutomobilSearchRequest()
            {
                Dostupan = true
            };
            var vozilaSlobodna = await _vozilaService.Get<List<Automobil>>(vozilaSearch);
            vozilaSearch.Dostupan = false;
            var vozilaZauzeta = await _vozilaService.Get<List<Automobil>>(vozilaSearch);


            RezervacijaRentanjaSearchRequest rezervacijeSearch = new RezervacijaRentanjaSearchRequest()
            {
                Otkazana = false,
                RezervacijaOd = DateTime.Now
            };
            var rezervacijeUToku = await _rezervacijeService.Get<List<RezervacijaRentanja>>(rezervacijeSearch);
            rezervacijeSearch.RezervacijaDo = DateTime.Now.AddDays(-1);
            rezervacijeSearch.RezervacijaOd = null;
            var rezervacijeZavrsene = await _rezervacijeService.Get<List<RezervacijaRentanja>>(rezervacijeSearch);

            decimal zaradaOvajMjesec = 0, zaradaOveGodine = 0, zarada = 0, zaradaProteklih7dana = 0;
            int rezervacijaOvajMjesec = 0, rezervacijaOveGodine = 0;
            int[] zaradaPoMjesecima = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            string zaradaPoMjesecimaString;


            foreach (var r in rezervacijeZavrsene)
            {
                TimeSpan razlikaDatum = DateTime.Today - r.RezervacijaOd;

                if (r.RezervacijaOd.Year == DateTime.Now.Year)
                {
                    zaradaPoMjesecima[(r.RezervacijaOd.Month) - 1] += (int)r.IznosSaPopustom;


                    //Mjesecna zarada i broj rezervacija
                    if (r.RezervacijaOd.Month == DateTime.Now.Month)
                    {
                        zaradaOvajMjesec += r.IznosSaPopustom;
                        rezervacijaOvajMjesec += 1;

                        if (razlikaDatum.Days <= 7)
                        {
                            zaradaProteklih7dana += r.IznosSaPopustom;
                        }
                    }
                }

                //Godisnja zarada i broj rezervacija
                if (r.RezervacijaOd.Year == DateTime.Now.Year)
                {
                    zaradaOveGodine += r.IznosSaPopustom;
                    rezervacijaOveGodine += 1;
                }
                zarada += r.IznosSaPopustom;
            }

            KlijentSearchRequest klijentSearch = new KlijentSearchRequest()
            {
                Status = true
            };
            var klijentiAktivni = await _klijentService.Get<List<Klijent>>(klijentSearch);
            klijentSearch.Status = false;
            var klijentiNeaktivni = await _klijentService.Get<List<Klijent>>(klijentSearch);

            int brKlijenataM = 0, brKlijenataG = 0;

            //Broj klijenata aktivnih mjesečno/godišnje
            foreach (var k in klijentiAktivni)
            {
                //Mjesecna zarada i broj rezervacija
                if (k.DatumRegistracije.Month == DateTime.Now.Month)
                {
                    brKlijenataM++;
                }
                //Mjesecna zarada i broj rezervacija
                if (k.DatumRegistracije.Year == DateTime.Now.Year)
                {
                    brKlijenataG++;
                }

            }

            //Broj klijenata nekativnih mjesečno/godišnje
            foreach (var k in klijentiNeaktivni)
            {

                if (k.DatumRegistracije.Month == DateTime.Now.Month)
                {
                    brKlijenataM++;
                }
                if (k.DatumRegistracije.Year == DateTime.Now.Year)
                {
                    brKlijenataG++;
                }

            }


            model.brojVozila = (vozilaSlobodna.Count + vozilaZauzeta.Count);
            model.zauzetihVozila = vozilaZauzeta.Count;
            model.slobodnihVozila = vozilaSlobodna.Count;

            model.brojKlijenata = (klijentiAktivni.Count + klijentiNeaktivni.Count);

            model.ukupnaZarada = zarada;
            model.zaradaOvogMjeseca = zaradaOvajMjesec;
            model.zaradaOveGodine = zaradaOveGodine;

            model.brojRezervacija = (rezervacijeUToku.Count + rezervacijeZavrsene.Count);
            model.rezervacijeOvogMjeseca = rezervacijaOvajMjesec;
            model.rezervacijeOveGodine = rezervacijaOveGodine;
            model.brojKlijenataM = brKlijenataM;
            model.brojKlijenataG = brKlijenataG;
            model.zaradaPoMjesecima = zaradaPoMjesecima;
            zaradaPoMjesecimaString = "";

            for (int i = 0; i < zaradaPoMjesecima.Length; i++)
            {
                if (i == zaradaPoMjesecima.Length - 1)
                {
                    zaradaPoMjesecimaString += zaradaPoMjesecima[i];
                }
                else
                {
                    zaradaPoMjesecimaString += zaradaPoMjesecima[i] + ", ";
                }
            }
            model.zaradaPoMjesecimaString = zaradaPoMjesecimaString;
            model.zaradaProteklih7dana = zaradaProteklih7dana;



            var listaKatVoz = await _kategorijaVozilaService.Get<List<KategorijaVozila>>(null);
            var svaVozila = await _vozilaService.Get<List<Automobil>>(new AutomobilSearchRequest() { Dostupan = true });
            int brojUkupnoVozila = svaVozila.Count();
            int brojKategorijaVozila = listaKatVoz.Count();

            model.listaKatVoz = listaKatVoz;


            string[] nizBoja = { "success", "danger", "brand", "dark", "warning", "primary", "fill-info", "fill-light" };
            int postotakPutnicka, postotakKombi, brojPutnicka = 0, brojKombi = 0;

            var brojacKat = 0;
            string kategorijeVozilaString = ""; string kategorijeVozilaBrojString = "";

            foreach (var kategorija in listaKatVoz)
            {
                int brojAutaKat = 0;
                if (brojacKat == listaKatVoz.Count() - 1)
                {
                    kategorijeVozilaString += "'" + kategorija.Naziv + "'";
                }
                else
                {
                    kategorijeVozilaString += "'" + kategorija.Naziv + "', ";
                }

                foreach (var auto in svaVozila)
                {
                    if (auto.KategorijaId == kategorija.KategorijaId && kategorija.Naziv == "Putnicko")
                    {
                        brojAutaKat++;
                        brojPutnicka++;
                    }

                    if (auto.KategorijaId == kategorija.KategorijaId && kategorija.Naziv == "Kombi")
                    {
                        brojAutaKat++;
                        brojKombi++;
                    }
                }


                if (brojacKat == listaKatVoz.Count() - 1)
                {
                    kategorijeVozilaBrojString += brojAutaKat.ToString();
                }
                else
                {
                    kategorijeVozilaBrojString += brojAutaKat.ToString() + ", ";
                }

                brojacKat++;
            }
            postotakPutnicka = (int)((double)brojPutnicka / brojUkupnoVozila * 100);
            postotakKombi = (int)((double)brojKombi / brojUkupnoVozila * 100);

            model.nizBoja = nizBoja;
            model.KategorijeVozilaString = kategorijeVozilaString;
            model.KategorijeVozilaBrojString = kategorijeVozilaBrojString;
            model.postotakKombi = postotakKombi;
            model.postotakPutnicko = postotakPutnicka;
            model.ukupnoVozila = brojUkupnoVozila;

            return View(model);
        }
        public IActionResult Template()
        {
            return View();
        }

        [Autorizacija(klijent: true, administrator: true)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
