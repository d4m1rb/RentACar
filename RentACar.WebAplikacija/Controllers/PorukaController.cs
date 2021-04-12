using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.Web;
using RentACar.WebAplikacija.Helper;
using RentACar.WebAplikacija.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace RentACar.WebAplikacija.Controllers
{
    public class PorukaController : Controller
    {
        private readonly APIService _porukeService = new APIService("Poruka");
        private readonly APIService _korisnikService = new APIService("Korisnik");
        private readonly APIService _drzavaService = new APIService("Drzava");
        private readonly APIService _gradService = new APIService("Grad");
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Posalji(PorukaOdgovoriVM model)
        {
            var poruke=await _porukeService.Get<List<Poruka>>(new PorukaSearchRequest { PorukaId = model.PorukaId });
            var poruka = poruke.FirstOrDefault();
            poruka.Procitano = true;

            await _porukeService.Update<Poruka>(model.PorukaId, poruka);

            Poruka p = new Poruka()
            {
                UposlenikId = model.UposlenikId,
                KlijentId = model.KlijentId,
                Sadrzaj = model.Sadrzaj,
                Naslov = model.Naslov,
                RezervacijaRentanjaId = model.RezervacijaRentanjaId,
                DatumVrijeme = DateTime.Now,
                Procitano=false,
                Posiljaoc="Klijent"
            };

            await _porukeService.Insert<Poruka>(p);


            
            return View();
        }
        public async Task<IActionResult> Odgovori(int PorukaID)
        {
            PorukaOdgovoriVM model = new PorukaOdgovoriVM();
            var korisnik = await HttpContext.GetLogiraniKorisnik();
            if (korisnik.KlijentId > 0)
            {
                PorukaSearchRequest searchRequest = new PorukaSearchRequest();
                searchRequest.PorukaId = PorukaID;

                var list = await _porukeService.Get<IEnumerable<Poruka>>(searchRequest);
                var poruka = list.FirstOrDefault();


                model.KlijentId = poruka.KlijentId;
                model.Naslov = poruka.Naslov;
                model.PosiljaocInfo = poruka.PosiljaocInfo;
                model.PrimaocInfo = poruka.PrimaocInfo;
                model.RezervacijaRentanjaId = poruka.RezervacijaRentanjaId;
                model.Sadrzaj = "";
                model.UposlenikId = poruka.UposlenikId;
                model.Procitano = false;
                model.PorukaId = poruka.PorukaId;
               

            }
            return View(model);
        }

        public async Task<IActionResult> Detalji(int PorukaID, int RezervacijaID)
        {
            PorukaDetaljiVM model = new PorukaDetaljiVM();
            var korisnik = await HttpContext.GetLogiraniKorisnik();
            if (korisnik.KlijentId > 0)
            {
                PorukaSearchRequest searchRequest = new PorukaSearchRequest();
                searchRequest.PorukaId = PorukaID;
               

                var list = await _porukeService.Get<IEnumerable<Poruka>>(searchRequest);
                var poruka = list.FirstOrDefault();

                PorukaSearchRequest uposlenikPor = new PorukaSearchRequest();
                uposlenikPor.KlijentId = poruka.KlijentId;
                uposlenikPor.UposlenikId = poruka.UposlenikId;
                uposlenikPor.Naslov = poruka.Naslov;
                uposlenikPor.Posiljaoc = "Uposlenik";
                var listuposlenikPor = await _porukeService.Get<IEnumerable<Poruka>>(uposlenikPor);
                var porukaUp = list.FirstOrDefault();


                PorukaSearchRequest klijentPor = new PorukaSearchRequest();
                klijentPor.KlijentId = poruka.KlijentId;
                klijentPor.UposlenikId = poruka.UposlenikId;
                klijentPor.Naslov = poruka.Naslov;
                klijentPor.Posiljaoc = "Klijent";
                var listklijentPor = await _porukeService.Get<IEnumerable<Poruka>>(klijentPor);
                var porukaKl = listklijentPor.FirstOrDefault();

                model.porukaKlijent = porukaKl;
                model.porukaUposlenik = porukaUp;
                model.RezervacijaID = RezervacijaID;
               
            }
            return View(model);
        }

        public async Task<IActionResult> Obrisi(int PorukaID, int RezervacijaID)
        {
            var poruka = await _porukeService.GetById<Poruka>(PorukaID);

            PorukaUpsertRequest request = new PorukaUpsertRequest()
            {
                DatumVrijeme = poruka.DatumVrijeme,
                PorukaId = poruka.PorukaId,
                KlijentId = poruka.KlijentId,
                Naslov = poruka.Naslov,
                Posiljaoc = poruka.Posiljaoc,
                Procitano = poruka.Procitano,
                RezervacijaRentanjaId = poruka.RezervacijaRentanjaId,
                Sadrzaj = poruka.Sadrzaj,
                UposlenikId = poruka.UposlenikId,
                ObrisanoKlijent = true,
                ObrisanoUposlenik=poruka.ObrisanoUposlenik
                
            };

            await _porukeService.Update<Poruka>(PorukaID, request);

            return RedirectToAction("Procitaj",RezervacijaID);
        }

        public async Task<IActionResult> Procitaj(int rezervacijaID)
        {
            PorukaProcitajVM model = new PorukaProcitajVM();
            var korisnik = await HttpContext.GetLogiraniKorisnik();
            if ( korisnik.KlijentId> 0)
            {
                PorukaSearchRequest searchRequest = new PorukaSearchRequest();
                searchRequest.KlijentId = korisnik.KlijentId;
                searchRequest.RezervacijaRentanjaId = rezervacijaID;
                searchRequest.Posiljaoc = "Uposlenik";
                searchRequest.ObrisanoKlijent = false;

                var list = await _porukeService.Get<IEnumerable<Poruka>>(searchRequest);

              
                model.listaPoruka.Clear();
                model.kontaktPodaci.Clear();
                foreach (var item in list)
                {
                    KorisniciSearchRequest searchKorisnici = new KorisniciSearchRequest();
                    searchKorisnici.KorisnikId = item.UposlenikId;
                    searchKorisnici.Status = true;
                    var listaKorisnik = await _korisnikService.Get<IEnumerable<Korisnici>>(searchKorisnici);
                    var k = listaKorisnik.FirstOrDefault();
                    var slika = k.SlikaThumb;


                    GradSearchRequest searchGrad = new GradSearchRequest();
                    searchGrad.GradId = k.GradId;
                    var listaGrad=await _gradService.Get<IEnumerable<Grad>>(searchGrad);
                    int drzavaId = listaGrad.FirstOrDefault().DrzavaId;

                    DrzavaSearchRequest searchDrzava = new DrzavaSearchRequest();
                    searchDrzava.DrzavaId = drzavaId;
                    var listaDrzava = await _drzavaService.Get<IEnumerable<Drzava>>(searchDrzava);
                    var nazivDrzave = listaDrzava.FirstOrDefault().Naziv;


                    model.listaPoruka.Add(item);
                    model.kontaktPodaci.Add(new KontaktPoruka() { Drzava =nazivDrzave, Email =k.Email, Telefon =k.Telefon, slikaThumb=slika});
                }

            }

            return View(model);
        }
    }
}