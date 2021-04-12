using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.Web;
using RentACar.WebAplikacija.Helper;
using RentACar.WebAplikacija.ViewModels;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentACar.WebAplikacija.Controllers
{
    public class RezervacijaAdmController : Controller
    {
        private readonly APIService _voziloService = new APIService("Automobil");
        private readonly APIService _klijentiService = new APIService("Klijent");
        private readonly APIService _korisniciUlogaService = new APIService("KorisniciUloga");
        private readonly APIService _ulogaService = new APIService("Uloga");
        private readonly APIService _drzavaService = new APIService("Drzava");
        private readonly APIService _gradService = new APIService("Grad");
        private readonly APIService _PorukaService = new APIService("Poruka");
        private readonly APIService _modeliService = new APIService("Model");
        private readonly APIService _proizvodjaciService = new APIService("Proizvodjac");
        private readonly APIService _kategorijaService = new APIService("KategorijaVozila");
        private readonly APIService _registracijaService = new APIService("RegistracijaVozila");
        private readonly APIService _rezervacijaService = new APIService("RezervacijaRentanja");
        public async Task<IActionResult> Index()
        {
            var logiranikorisnik = HttpContext.GetLogiraniKorisnikAdm();
            RezervacijeAdmVM model = new RezervacijeAdmVM();
            var result = await _rezervacijaService.Get<List<RezervacijaRentanja>>(new RezervacijaRentanjaSearchRequest());
           

            foreach (var x in result)
            {
                RezervacijeAdmVM.Row rezervacija = new RezervacijeAdmVM.Row()
                {
                    AutomobilId = x.AutomobilId,
                    DatumKreiranja = x.DatumKreiranja,
                    Iznos = x.Iznos,
                    IznosSaPopustom = x.IznosSaPopustom,
                    Klijent = x.Klijent,
                    KlijentId = x.KlijentId,
                    LokacijaPreuzimanja = x.LokacijaPreuzimanja,
                    Opis = x.Opis,
                    RacunId = x.RacunId,
                    RezervacijaDo = x.RezervacijaDo,
                    RezervacijaInformacije = x.RezervacijaInformacije,
                    RezervacijaOd = x.RezervacijaOd,
                    RezervacijaOdDo = x.RezervacijaOdDo,
                    RezervacijaRentanjaId=x.RezervacijaRentanjaId,
                    VoziloInformacije=x.VoziloInformacije,
                    VoziloProizvodjacModel=x.VoziloProizvodjacModel,
                    VracanjeUposlovnicu=x.VracanjeUposlovnicu,
                    KaskoOsiguranje=x.KaskoOsiguranje
                };

                if(x.RezervacijaDo<DateTime.Today)
                {
                    rezervacija.Zavrsena = true;
                }
                else
                {
                    rezervacija.Zavrsena = false;
                }                                         
                            
                                
                model.listaRezervacija.Add(rezervacija);
                
            }

            return View(model);
        }
        public async Task<IActionResult> Uredi(int RezervacijaID)
        {

            var rezervacija = await _rezervacijaService.GetById<RezervacijaRentanja>(RezervacijaID);

            RezervacijaAdmUrediVM model = new RezervacijaAdmUrediVM()
            {
               AutomobilId=rezervacija.AutomobilId,
               CijenaIznajmljivanja=rezervacija.CijenaIznajmljivanja,
               DatumKreiranja=rezervacija.DatumKreiranja,
               IsOcjenjena=rezervacija.IsOcjenjena,
               Iznos=rezervacija.Iznos,
               IznosSaPopustom=rezervacija.IznosSaPopustom,
               KaskoOsiguranje=rezervacija.KaskoOsiguranje,
               Klijent=rezervacija.Klijent,
               KlijentId=rezervacija.KlijentId,
               LokacijaPreuzimanja=rezervacija.LokacijaPreuzimanja,
               Ocjena=rezervacija.Ocjena,
               Opis=rezervacija.Opis,
               Otkazana=rezervacija.Otkazana,
               Popust=rezervacija.Popust,
               RacunId=rezervacija.RacunId,
               RezervacijaBrojDana=rezervacija.RezervacijaBrojDana,
               RezervacijaDo=rezervacija.RezervacijaDo,
               RezervacijaInformacije=rezervacija.RezervacijaInformacije,
               RezervacijaOd=rezervacija.RezervacijaOd,
               RezervacijaOdDo=rezervacija.RezervacijaOdDo,
               RezervacijaRentanjaId=rezervacija.RezervacijaRentanjaId,
               SlikaThumb=rezervacija.SlikaThumb,
               VoziloInformacije=rezervacija.VoziloInformacije,
               VoziloProizvodjacModel=rezervacija.VoziloProizvodjacModel,
               VracanjeUposlovnicu=rezervacija.VracanjeUposlovnicu
            };

            var registracija = await _registracijaService.Get<List<RegistracijaVozila>>(new RegistracijaVozilaSearchRequest() { AutomobilId = rezervacija.AutomobilId, Status = true });
            model.RegistarskaOznaka = registracija.FirstOrDefault().RegistarskeOznake;
            return View(model);
        }

        public async Task<IActionResult> Obrisi(int RezervacijaID)
        {

            var rezervacija = await _rezervacijaService.GetById<RezervacijaRentanja>(RezervacijaID);

            await _rezervacijaService.Delete<RezervacijaRentanja>(RezervacijaID);
           
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PosaljiPoruku(int RezervacijaID)
        {
            var rezervacija = await _rezervacijaService.GetById<RezervacijaRentanja>(RezervacijaID);
            var logiraniKorisnik = await HttpContext.GetLogiraniKorisnikAdm();
            var klijent = await _klijentiService.GetById<Klijent>(rezervacija.KlijentId);
            RezervacijaAdmPosaljiPorukuVM model = new RezervacijaAdmPosaljiPorukuVM()
            {
                DatumVrijeme = DateTime.Now,
                KlijentId = rezervacija.KlijentId,
                Naslov = "",
                ObrisanoKlijent = false,
                ObrisanoUposlenik = false,
                UposlenikId = logiraniKorisnik.KorisnikId,
                PosiljaocSlikaThumb = logiraniKorisnik.SlikaThumb,
                Procitano = false,
                RezervacijaRentanjaId = RezervacijaID,
                Sadrzaj = "",
                PosiljaocInfo = logiraniKorisnik.Ime + " " + logiraniKorisnik.Prezime,
                PrimaocInfo=klijent.Ime+" "+klijent.Prezime
            };


            return View(model);
        }
        public async Task<IActionResult> PosaljiPorukuSnimi(RezervacijaAdmPosaljiPorukuVM model)
        {            
            var logiraniKorisnik = await HttpContext.GetLogiraniKorisnikAdm();

            PorukaUpsertRequest request = new PorukaUpsertRequest()
            {
                DatumVrijeme = DateTime.Now,
                KlijentId = model.KlijentId,
                Naslov = model.Naslov,
                ObrisanoKlijent = false,
                ObrisanoUposlenik = false,
                Posiljaoc = "Uposlenik",
                Procitano = false,
                RezervacijaRentanjaId = model.RezervacijaRentanjaId,
                Sadrzaj = model.Sadrzaj,
                UposlenikId = logiraniKorisnik.KorisnikId
            };

            await _PorukaService.Insert<Poruka>(request);

            TempData["poruka"] = "Poruka je uspješno poslana!";
            return RedirectToAction("Uredi", new { RezervacijaID=model.RezervacijaRentanjaId});
        }

        public async Task<IActionResult> Snimi(RezervacijaAdmUrediVM model)
        {
            string dan = "", mjesec = "", godina = "";
            DateTime datumRezervacijeOd = DateTime.MinValue, datumRezervacijeDo = DateTime.MinValue, datumKreiranja=DateTime.MinValue;
           
            var datumKreiranjaStr = model.DatumKreiranjaString;
            dan = datumKreiranjaStr.Substring(0, 2);
            mjesec = datumKreiranjaStr.Substring(3, 2);
            godina = datumKreiranjaStr.Substring(6, 4);
            datumKreiranja = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));

            var datumRezervacijeOdStr = model.RezervacijaOdString;
            dan = datumRezervacijeOdStr.Substring(0, 2);
            mjesec = datumRezervacijeOdStr.Substring(3, 2);
            godina = datumRezervacijeOdStr.Substring(6, 4);
            datumRezervacijeOd = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));

            dan = ""; mjesec = ""; godina = "";
            var datumRezervacijeDoStr = model.RezervacijaDoString;
            dan = datumRezervacijeDoStr.Substring(0, 2);
            mjesec = datumRezervacijeDoStr.Substring(3, 2);
            godina = datumRezervacijeDoStr.Substring(6, 4);
            datumRezervacijeDo = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));

            RezervacijaRentanjaUpsertRequest request = new RezervacijaRentanjaUpsertRequest()
            {               
               RezervacijaRentanjaId=model.RezervacijaRentanjaId,
               AutomobilId=model.AutomobilId,
               DatumKreiranja=datumKreiranja,
               Iznos=model.Iznos,
               IznosSaPopustom=model.IznosSaPopustom,
               KaskoOsiguranje=model.KaskoOsiguranje,
               KlijentId=model.KlijentId,
               LokacijaPreuzimanja=model.LokacijaPreuzimanja,
               Opis=model.Opis,
               Otkazana=model.Otkazana,
               Popust=model.Popust,
               RacunId=model.RacunId,
               RezervacijaDo=datumRezervacijeDo,
               RezervacijaOd=datumRezervacijeOd,
               VracanjeUposlovnicu=model.VracanjeUposlovnicu
            };                      
            
                request.AutomobilId = model.AutomobilId;
                await _rezervacijaService.Update<RezervacijaRentanja>(model.RezervacijaRentanjaId, request);
            
            return RedirectToAction("Index");
        }
        
    }
}