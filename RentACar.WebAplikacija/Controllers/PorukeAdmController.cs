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
    public class PorukeAdmController : Controller
    {            
        private readonly APIService _PorukaService = new APIService("Poruka");
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Poslane()
        {
            PorukaSearchRequest porukaSearchRequest = new PorukaSearchRequest
            {
                UposlenikId =HttpContext.GetLogiraniKorisnikAdm().Result.KorisnikId,
                Posiljaoc = "Uposlenik",
                ObrisanoUposlenik = false

            };
            var result = await _PorukaService.Get<List<Poruka>>(porukaSearchRequest);

            PorukaAdmPoslaneVM model = new PorukaAdmPoslaneVM();

            foreach (var x in result)
            {
                PorukaAdmPoslaneVM.Row poruka = new PorukaAdmPoslaneVM.Row()
                {
                    DatumVrijeme = x.DatumVrijeme,
                    KlijentId = x.KlijentId,
                    Naslov = x.Naslov,
                    PorukaId = x.PorukaId,
                    Posiljaoc = x.Posiljaoc,
                    PosiljaocInfo = x.PosiljaocInfo,
                    PosiljaocSlikaThumb = x.PosiljaocSlikaThumb,
                    PrimaocInfo = x.PrimaocInfo,
                    Procitano = x.Procitano,
                    RezervacijaRentanjaId = x.RezervacijaRentanjaId,
                    Sadrzaj = x.Sadrzaj,
                    UposlenikId = x.UposlenikId
                };

                model.listaPoruka.Add(poruka);
            }
            return View(model);
        }
        public async Task<IActionResult> PoslaneDetalji(int PorukaID)
        {
            var poruka = await _PorukaService.GetById<Poruka>(PorukaID);
            PorukaAdmPoslaneDetaljiVM model = new PorukaAdmPoslaneDetaljiVM()
            {
                DatumVrijeme = poruka.DatumVrijeme,
                PorukaId = poruka.PorukaId,
                KlijentId = poruka.KlijentId,
                Naslov = poruka.Naslov,
                Posiljaoc = poruka.Posiljaoc,
                PosiljaocInfo = poruka.PosiljaocInfo,
                PosiljaocSlikaThumb = poruka.PosiljaocSlikaThumb,
                PrimaocInfo = poruka.PrimaocInfo,
                Procitano = poruka.Procitano,
                RezervacijaRentanjaId = poruka.RezervacijaRentanjaId,
                Sadrzaj = poruka.Sadrzaj,
                UposlenikId = poruka.UposlenikId
            };


            return View(model);
        }

        public async Task<IActionResult> ObrisiPoslane(int PorukaID)
        {
            var poruka = await _PorukaService.GetById<Poruka>(PorukaID);

            PorukaUpsertRequest request = new PorukaUpsertRequest()
            {
                DatumVrijeme = poruka.DatumVrijeme,
                PorukaId = poruka.PorukaId,
                KlijentId = poruka.KlijentId,
                Naslov = poruka.Naslov,
                Posiljaoc = poruka.Posiljaoc,
                Procitano = poruka.Procitano,
                ObrisanoKlijent = poruka.ObrisanoKlijent,
                ObrisanoUposlenik = true,
                RezervacijaRentanjaId = poruka.RezervacijaRentanjaId,
                Sadrzaj = poruka.Sadrzaj,
                UposlenikId = poruka.UposlenikId                
            };

            await _PorukaService.Update<Poruka>(PorukaID, request);

            return RedirectToAction("Poslane");
        }
        public async Task<IActionResult> ObrisiPrimljene(int PorukaID)
        {
            var poruka = await _PorukaService.GetById<Poruka>(PorukaID);

            PorukaUpsertRequest request = new PorukaUpsertRequest()
            {
                DatumVrijeme = poruka.DatumVrijeme,
                PorukaId = poruka.PorukaId,
                KlijentId = poruka.KlijentId,
                Naslov = poruka.Naslov,
                Posiljaoc = poruka.Posiljaoc,
                Procitano = poruka.Procitano,
                ObrisanoUposlenik=true,
                ObrisanoKlijent=poruka.ObrisanoKlijent,
                RezervacijaRentanjaId = poruka.RezervacijaRentanjaId,
                Sadrzaj = poruka.Sadrzaj,
                UposlenikId = poruka.UposlenikId
            };

            await _PorukaService.Update<Poruka>(PorukaID, request);

            return RedirectToAction("Primljene");
        }
        public async Task<IActionResult> Primljene()
        {
            PorukaSearchRequest porukaSearchRequest = new PorukaSearchRequest
            {
                UposlenikId = HttpContext.GetLogiraniKorisnikAdm().Result.KorisnikId,
                Posiljaoc = "Klijent",
                ObrisanoUposlenik=false
            };
            var result = await _PorukaService.Get<List<Poruka>>(porukaSearchRequest);
                                          
            PorukaAdmPrimljeneVM model = new PorukaAdmPrimljeneVM();

            foreach (var x in result)
            {
                PorukaAdmPrimljeneVM.Row poruka = new PorukaAdmPrimljeneVM.Row()
                {
                    DatumVrijeme = x.DatumVrijeme,
                    KlijentId = x.KlijentId,
                    Naslov = x.Naslov,
                    PorukaId = x.PorukaId,
                    Posiljaoc = x.Posiljaoc,
                    PosiljaocInfo = x.PosiljaocInfo,
                    PosiljaocSlikaThumb = x.PosiljaocSlikaThumb,
                    PrimaocInfo = x.PrimaocInfo,
                    Procitano = x.Procitano,
                    RezervacijaRentanjaId = x.RezervacijaRentanjaId,
                    Sadrzaj = x.Sadrzaj,
                    UposlenikId = x.UposlenikId
                };

                

                var listaOdgovorenihPoruka = await _PorukaService.Get<List<Poruka>>(new PorukaSearchRequest()
                {
                    KlijentId = poruka.KlijentId,
                    UposlenikId = poruka.UposlenikId,
                    RezervacijaRentanjaId = poruka.RezervacijaRentanjaId,
                    Naslov = poruka.Naslov,
                    Posiljaoc = "Uposlenik"
                });
                if (listaOdgovorenihPoruka.Count() == 0)
                {
                    poruka.Odgovoreno = false;
                }
                else
                {
                    poruka.Odgovoreno = true;
                }

                model.listaPoruka.Add(poruka);
            }


            return View(model);
        }
        public async Task<IActionResult> PrimljeneDetalji(int PorukaID)
        {
            var poruka = await _PorukaService.GetById<Poruka>(PorukaID);
            PorukaAdmPrimljeneDetaljiVM model = new PorukaAdmPrimljeneDetaljiVM()
            {
                DatumVrijeme = poruka.DatumVrijeme,
                PorukaId = poruka.PorukaId,
                KlijentId = poruka.KlijentId,
                Naslov = poruka.Naslov,
                Posiljaoc = poruka.Posiljaoc,
                PosiljaocInfo = poruka.PosiljaocInfo,
                PosiljaocSlikaThumb = poruka.PosiljaocSlikaThumb,
                PrimaocInfo = poruka.PrimaocInfo,
                Procitano = poruka.Procitano,
                RezervacijaRentanjaId = poruka.RezervacijaRentanjaId,
                Sadrzaj = poruka.Sadrzaj,
                UposlenikId = poruka.UposlenikId
            };

            var listaOdgovorenihPoruka = await _PorukaService.Get<List<Poruka>>(new PorukaSearchRequest()
            {
                KlijentId = poruka.KlijentId,
                UposlenikId = poruka.UposlenikId,
                RezervacijaRentanjaId = poruka.RezervacijaRentanjaId,
                Naslov = poruka.Naslov,
                Posiljaoc = "Uposlenik"
            });
            if(listaOdgovorenihPoruka.Count()==0)
            {
                model.Odgovoreno = false;
            }
            else
            {
                model.Odgovoreno = true;
            }

            if (model.Procitano == false)
            {
                PorukaUpsertRequest request = new PorukaUpsertRequest()
                {
                    DatumVrijeme = poruka.DatumVrijeme,
                    Procitano = true,
                    KlijentId = poruka.KlijentId,
                    Naslov = poruka.Naslov,
                    PorukaId = poruka.PorukaId,
                    Posiljaoc = poruka.Posiljaoc,
                    RezervacijaRentanjaId = poruka.RezervacijaRentanjaId,
                    Sadrzaj = poruka.Sadrzaj,
                    UposlenikId = poruka.UposlenikId
                };

                await _PorukaService.Update<Poruka>(poruka.PorukaId, request);
            }

            return View(model);
        }
        public async Task<IActionResult> Posalji(PorukaAdmOdgovoriVM model)
        {
            var poruke=await _PorukaService.Get<List<Poruka>>(new PorukaSearchRequest { PorukaId = model.PorukaId });
            var poruka = poruke.FirstOrDefault();
            poruka.Procitano = true;

            await _PorukaService.Update<Poruka>(model.PorukaId, poruka);

            Poruka p = new Poruka()
            {
                UposlenikId = model.UposlenikId,
                KlijentId = model.KlijentId,
                Sadrzaj = model.Sadrzaj,
                Naslov = model.Naslov,
                RezervacijaRentanjaId = model.RezervacijaRentanjaId,
                DatumVrijeme = DateTime.Now,
                Procitano=false,
                Posiljaoc="Uposlenik"
            };

            await _PorukaService.Insert<Poruka>(p);
                                   
            return RedirectToAction("Poslane");
        }
        public async Task<IActionResult> Odgovori(int PorukaID)
        {
            PorukaAdmOdgovoriVM model = new PorukaAdmOdgovoriVM();
            var korisnik = await HttpContext.GetLogiraniKorisnikAdm();
            if (korisnik.KorisnikId > 0)
            {
                PorukaSearchRequest searchRequest = new PorukaSearchRequest();
                searchRequest.PorukaId = PorukaID;

                var list = await _PorukaService.Get<IEnumerable<Poruka>>(searchRequest);
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
               

                var list = await _PorukaService.Get<IEnumerable<Poruka>>(searchRequest);
                var poruka = list.FirstOrDefault();

                PorukaSearchRequest uposlenikPor = new PorukaSearchRequest();
                uposlenikPor.KlijentId = poruka.KlijentId;
                uposlenikPor.UposlenikId = poruka.UposlenikId;
                uposlenikPor.Naslov = poruka.Naslov;
                uposlenikPor.Posiljaoc = "Uposlenik";
                var listuposlenikPor = await _PorukaService.Get<IEnumerable<Poruka>>(uposlenikPor);
                var porukaUp = list.FirstOrDefault();


                PorukaSearchRequest klijentPor = new PorukaSearchRequest();
                klijentPor.KlijentId = poruka.KlijentId;
                klijentPor.UposlenikId = poruka.UposlenikId;
                klijentPor.Naslov = poruka.Naslov;
                klijentPor.Posiljaoc = "Klijent";
                var listklijentPor = await _PorukaService.Get<IEnumerable<Poruka>>(klijentPor);
                var porukaKl = listklijentPor.FirstOrDefault();

                model.porukaKlijent = porukaKl;
                model.porukaUposlenik = porukaUp;
                model.RezervacijaID = RezervacijaID;
               
            }
            return View(model);
        }
                       
    }
}