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
    public class VoziloAdmController : Controller
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
        public async Task<IActionResult> Index()
        {
            var logiranikorisnik = HttpContext.GetLogiraniKorisnikAdm();
            VoziloAdmVM model = new VoziloAdmVM();
            var result = await _voziloService.Get<List<Automobil>>(new AutomobilSearchRequest());
           

            foreach (var x in result)
            {
                VoziloAdmVM.Row vozilo = new VoziloAdmVM.Row()
                {
                    AutomobilId = x.AutomobilId,
                    Boja=x.Boja,
                    BrojSjedista=x.BrojSjedista,
                    BrojVrata=x.BrojVrata,
                    CijenaIznajmljivanja=x.CijenaIznajmljivanja,
                    CijenaKaskoOsiguranja=x.CijenaKaskoOsiguranja,
                    Dostupan=x.Dostupan,
                    DostupanTekst=x.DostupanTekst,
                    EmisioniStandard=x.EmisioniStandard,
                    GodinaProizvodnje=x.GodinaProizvodnje,
                    Gorivo=x.Gorivo,
                    KategorijaId=x.KategorijaId,
                    Kubikaza=x.Kubikaza,
                    ModelId=x.ModelId,
                    Novo=x.Novo,
                    Potrosnja=x.Potrosnja,
                    ProizvodjacModel=x.ProizvodjacModel,
                    RegistarskaOznaka=x.RegistarskaOznaka,
                    RegistrovanDo=x.RegistrovanDo,
                    Slika=x.Slika,
                    SlikaThumb=x.SlikaThumb,
                    SnagaMotora=x.SnagaMotora,
                    Transmisija=x.Transmisija
                };
                                             
                            
                                
                model.listaVozila.Add(vozilo);
                
            }

            return View(model);
        }
        public async Task<IActionResult> Uredi(int VoziloID)
        {

            var vozilo = await _voziloService.GetById<Automobil>(VoziloID);

            VoziloAdmUrediVM model = new VoziloAdmUrediVM()
            {
                AutomobilId= vozilo.AutomobilId,
                Boja= vozilo.Boja,
                CijenaIznajmljivanja= vozilo.CijenaIznajmljivanja,
                Dostupan=vozilo.Dostupan,
                BrojSjedista=vozilo.BrojSjedista,
                BrojVrata=vozilo.BrojVrata,
                CijenaKaskoOsiguranja=vozilo.CijenaKaskoOsiguranja,
                DostupanTekst=vozilo.DostupanTekst,
                EmisioniStandard=vozilo.EmisioniStandard,
                GodinaProizvodnje=vozilo.GodinaProizvodnje,
                Gorivo=vozilo.Gorivo,
                KategorijaId=vozilo.KategorijaId,
                Kubikaza=vozilo.Kubikaza,
                ModelId=vozilo.ModelId,
                Novo=vozilo.Novo,
                Potrosnja=vozilo.Potrosnja,
                ProizvodjacModel=vozilo.ProizvodjacModel,
                RegistarskaOznaka=vozilo.RegistarskaOznaka,
                RegistrovanDo=vozilo.RegistrovanDo,
                Slika=vozilo.Slika,
                SlikaThumb=vozilo.SlikaThumb,
                SnagaMotora=vozilo.SnagaMotora,
                Transmisija=vozilo.Transmisija
            };

            var modelVozila = _modeliService.Get<List<ModelAutomobila>>(new ModelAutomobilaSearch() { ModelId = vozilo.ModelId }).Result.FirstOrDefault();
            var proizvodjacVozila = _proizvodjaciService.Get<List<Proizvodjac>>(new ProizvodjacSearchRequest() { ProizvodjacId = modelVozila.ProizvodjacId }).Result.FirstOrDefault();
            model.ProizvodjacId = proizvodjacVozila.ProizvodjacId;

            var modeli = await _modeliService.Get<List<ModelAutomobila>>(new ModelAutomobilaSearch() { ProizvodjacId=proizvodjacVozila.ProizvodjacId});
            foreach (var modelA in modeli)
            {
                var x = new SelectListItem() { Text = modelA.Naziv, Value = modelA.ModelId.ToString() };
                model.listaModel.Add(x);                
            }

            var proizvodjaci = await _proizvodjaciService.Get<List<Proizvodjac>>(null);
            foreach (var proizvodjac in proizvodjaci)
            {
                var x = new SelectListItem() { Text = proizvodjac.Naziv, Value = proizvodjac.ProizvodjacId.ToString() };
                model.listaProizvodjac.Add(x);
            }

            var kategorije = await _kategorijaService.Get<List<KategorijaVozila>>(null);
            foreach (var kategorija in kategorije)
            {
                var x = new SelectListItem() { Text = kategorija.Naziv, Value = kategorija.KategorijaId.ToString() };
                model.listaKategorija.Add(x);
            }

            string[] emisioniStandard = { "Euro 1", "Euro 2", "Euro 3", "Euro 4", "Euro 5", "Euro 6" };

            foreach (var es in emisioniStandard)
            {
                var x = new SelectListItem() { Text = es, Value = es};
                model.listaEmisioniStandard.Add(x);
            }
            string[] Transmisija = { "Automatic", "Manual"};

            foreach (var tr in Transmisija)
            {
                var x = new SelectListItem() { Text = tr, Value = tr };
                model.listaTransmisija.Add(x);
            }
            string[] Gorivo = { "Dizel", "Benzin","Plin" };

            foreach (var g in Gorivo)
            {
                var x = new SelectListItem() { Text = g, Value = g };
                model.listaGorivo.Add(x);
            }
            string[] BrojVrata = { "2/5", "4/5"};
            foreach (var vr in BrojVrata)
            {
                var x = new SelectListItem() { Text = vr, Value = vr };
                model.listaBrojVrata.Add(x);
            }

           
            return View(model);
        }
        public async Task<IActionResult> Snimi(VoziloAdmUrediVM model)
        {
           
            byte[] SlikaNew = null, SlikaThumbNew = null;

            #region SlikaProfila

            if (model.slikaVozila != null)
            {
                var filename = System.IO.Path.GetFileName(model.slikaVozila.FileName);
                Image image = null;

                //using (var localFile = System.IO.File.OpenWrite(filename))
                using (var uploadedFile = model.slikaVozila.OpenReadStream())
                {
                    //uploadedFile.CopyTo(localFile);
                    image = Image.FromStream(uploadedFile);

                }
                var img = new Bitmap(image);

                MemoryStream msImg = new MemoryStream();
                img.Save(msImg, System.Drawing.Imaging.ImageFormat.Jpeg);
                SlikaNew = msImg.ToArray();


                Image thumb = image.GetThumbnailImage(75, 75, () => false, IntPtr.Zero);
                MemoryStream ms = new MemoryStream();
                thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                SlikaThumbNew = ms.ToArray();
            }

            if (SlikaNew == null)
            {
                SlikaNew = model.Slika;
                SlikaThumbNew = model.SlikaThumb;
            }
            #endregion



            AutomobiliUPSERTtRequest request = new AutomobiliUPSERTtRequest()
            {               
                Slika=SlikaNew,
                SlikaThumb=SlikaThumbNew,
                Boja=model.Boja,
                BrojSjedista=model.BrojSjedista,
                BrojVrata=model.BrojVrata,
                CijenaIznajmljivanja=model.CijenaIznajmljivanja,
                CijenaKaskoOsiguranja=model.CijenaKaskoOsiguranja,
                Dostupan=model.Dostupan,
                EmisioniStandard=model.EmisioniStandard,
                GodinaProizvodnje=model.GodinaProizvodnje,
                Gorivo=model.Gorivo,
                KategorijaId=model.KategorijaId,
                Kubikaza=model.Kubikaza,
                ModelId=model.ModelId,
                Novo=model.Novo,
                Potrosnja=model.Potrosnja,
                SnagaMotora=model.SnagaMotora,
                Transmisija=model.Transmisija
            };

            string dan = "", mjesec = "", godina = "";
            DateTime registrovanDo = DateTime.MinValue, datumRegistracije = DateTime.MinValue;

            if (model.DatumRegistracijeStr != null)
            {
                var datumRegistracijeStr = model.DatumRegistracijeStr;
                dan = datumRegistracijeStr.Substring(0, 2);
                mjesec = datumRegistracijeStr.Substring(3, 2);
                godina = datumRegistracijeStr.Substring(6, 4);
                datumRegistracije = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));
            }

            if (model.RegistrovanDoStr != null)
            {
                dan = ""; mjesec = ""; godina = "";
                var registrovanDoStr = model.RegistrovanDoStr;
                dan = registrovanDoStr.Substring(0, 2);
                mjesec = registrovanDoStr.Substring(3, 2);
                godina = registrovanDoStr.Substring(6, 4);
                registrovanDo = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));
            }

            if (model.AutomobilId == 0)
            {
                var logiraniKorisnik =await HttpContext.GetLogiraniKorisnikAdm();
                var entityVozilo=await _voziloService.Insert<Automobil>(request);

                RegistracijaVozilaUpsertRequest req = new RegistracijaVozilaUpsertRequest()
                {
                    AutomobilId = entityVozilo.AutomobilId,
                    DatumRegistracije = datumRegistracije,
                    RegistarskeOznake = model.RegistarskaOznaka,
                    Status = true,
                    Trajanje = model.Trajanje,
                    UkupanIznos = model.IznosRegistracije,
                    UposlenikId = logiraniKorisnik.KorisnikId,
                    VaziDo = registrovanDo
                };
                await _registracijaService.Insert<RegistracijaVozila>(req);
            }
            else
            {
                request.AutomobilId = model.AutomobilId;
                await _voziloService.Update<Automobil>(model.AutomobilId, request);
            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ProduziRegistraciju(int VoziloID)
        {
            var vozilo = await _voziloService.GetById<Automobil>(VoziloID);
            VoziloAdmProduziRegistracijuVM model = new VoziloAdmProduziRegistracijuVM()
            {
                AutomobilId = vozilo.AutomobilId,
                ProizvodjacModel = vozilo.ProizvodjacModel,
                RegistarskaOznaka = vozilo.RegistarskaOznaka,
                RegistrovanDo = vozilo.RegistrovanDo,
                Slika = vozilo.Slika,
                SlikaThumb = vozilo.SlikaThumb
            };
            string[] trajanjeL = { "3 mjeseca", "6 mjeseci", "12 mjeseci" };

            model.listaTrajanje.Insert(0, new SelectListItem() { Text = "Izaberite trajanje registracije", Value = "" });
            foreach (var tr in trajanjeL)
            {
                var x = new SelectListItem() { Value = tr, Text = tr };
                model.listaTrajanje.Add(x);
            }

            model.DatumRegistracije = DateTime.Today;

            return View(model);
        }
        public async Task<IActionResult> ProduziRegSnimi(VoziloAdmProduziRegistracijuVM model)
        {
            var vozilo = await _voziloService.GetById<Automobil>(model.AutomobilId);
            var korisnik = await HttpContext.GetLogiraniKorisnikAdm();

            string dan = "", mjesec = "", godina = "";
            DateTime registrovanDo = DateTime.MinValue, datumRegistracije = DateTime.MinValue;

            if (model.DatumRegistracijeStr != null)
            {
                var datumRegistracijeStr = model.DatumRegistracijeStr;
                dan = datumRegistracijeStr.Substring(0, 2);
                mjesec = datumRegistracijeStr.Substring(3, 2);
                godina = datumRegistracijeStr.Substring(6, 4);
                datumRegistracije = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));
            }

            if (model.RegistrovanDoStr != null)
            {
                dan = ""; mjesec = ""; godina = "";
                var registrovanDoStr = model.RegistrovanDoStr;
                dan = registrovanDoStr.Substring(0, 2);
                mjesec = registrovanDoStr.Substring(3, 2);
                godina = registrovanDoStr.Substring(6, 4);
                registrovanDo = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));
            }

            RegistracijaVozilaUpsertRequest requestNew = new RegistracijaVozilaUpsertRequest()
            {
                AutomobilId = model.AutomobilId,
                DatumRegistracije = DateTime.Today,
                RegistarskeOznake = model.RegistarskaOznaka,
                Status = true,
                Trajanje=model.Trajanje,
                UkupanIznos=model.IznosRegistracije,
                UposlenikId=korisnik.KorisnikId,
                VaziDo=registrovanDo
            };

            var registracija = _registracijaService.Get<List<RegistracijaVozila>>(new RegistracijaVozilaSearchRequest() { AutomobilId = model.AutomobilId, Status = true }).Result.FirstOrDefault();
            registracija.Status = false;

            RegistracijaVozilaUpsertRequest req = new RegistracijaVozilaUpsertRequest()
            {
                Status = registracija.Status,
                AutomobilId = registracija.AutomobilId,
                DatumRegistracije = registracija.DatumRegistracije,
                RegistarskeOznake = registracija.RegistarskeOznake,
                RegistracijaVozilaId = registracija.RegistracijaVozilaId,
                Trajanje = registracija.Trajanje,
                UkupanIznos = registracija.UkupanIznos,
                UposlenikId = registracija.UposlenikId,
                VaziDo = registracija.VaziDo
            };

            await _registracijaService.Update<RegistracijaVozila>(registracija.RegistracijaVozilaId, req);
            await _registracijaService.Insert<RegistracijaVozila>(requestNew);


            return RedirectToAction("Uredi", new { VoziloID = model.AutomobilId } );
        }
        public async Task<IActionResult> NoviProizvodjac(int VoziloID)
        {
            VoziloAdmNoviProizvodjacVM model = new VoziloAdmNoviProizvodjacVM();
            var drzave = await _drzavaService.Get<List<Drzava>>(null);
            model.listaDrzava.Insert(0, new SelectListItem() { Text = "Odaberite državu proizvođača", Value = "" });

            foreach (var drzava in drzave)
            {
                var x = new SelectListItem() { Value = drzava.DrzavaId.ToString(), Text = drzava.Naziv };
                model.listaDrzava.Add(x);
            }

            if (VoziloID!=0)
            {
                var vozilo = await _voziloService.GetById<Automobil>(VoziloID);
                model.AutomobilId = vozilo.AutomobilId;                   
            }

            return View(model);
        }
        public async Task<IActionResult> NoviProizvodjacSnimi(VoziloAdmNoviProizvodjacVM model)
        {
            var proizvodjaci = await _proizvodjaciService.Get<List<Proizvodjac>>(new ProizvodjacSearchRequest() { Naziv = model.NazivProizvodjaca });
            var proizvodjac = proizvodjaci.FirstOrDefault();

            if (proizvodjac != null)
            {
                TempData["poruka"] = "Uneseni proizvođač već postoji u bazi!";

                if (model.AutomobilId != 0)
                {
                    return RedirectToAction("Uredi", new { VoziloID = model.AutomobilId });
                }
                else
                {
                    return RedirectToAction("Dodaj");
                }
            }

            ProizvodjacUpsertRequest request = new ProizvodjacUpsertRequest()
            {
                Naziv = model.NazivProizvodjaca,               
                DrzavaId = model.DrzavaId
            };

            #region SlikaProizvodjaca

            byte[] SlikaNew = null;

            if (model.slikaProizvodjaca != null)
            {
                var filename = System.IO.Path.GetFileName(model.slikaProizvodjaca.FileName);
                Image image = null;

                //using (var localFile = System.IO.File.OpenWrite(filename))
                using (var uploadedFile = model.slikaProizvodjaca.OpenReadStream())
                {
                    //uploadedFile.CopyTo(localFile);
                    image = Image.FromStream(uploadedFile);

                }
                var img = new Bitmap(image);

                MemoryStream msImg = new MemoryStream();
                img.Save(msImg, System.Drawing.Imaging.ImageFormat.Jpeg);
                SlikaNew = msImg.ToArray();
            }
            #endregion
            request.Slika = SlikaNew;

            await _proizvodjaciService.Insert<Proizvodjac>(request);

            if(model.AutomobilId!=0)
            {
                return RedirectToAction("Uredi", new { VoziloID = model.AutomobilId });
            }
            else
            {
                return RedirectToAction("Dodaj");
            }           
        }
        public async Task<IActionResult> NovaDrzava(int VoziloID)
        {
            VoziloAdmNovaDrzavaVM model = new VoziloAdmNovaDrzavaVM();
            if (VoziloID != 0)
            {
                var vozilo = await _voziloService.GetById<Automobil>(VoziloID);
                model.AutomobilId = vozilo.AutomobilId;
            }                                    

            return View(model);
        }
        public async Task<IActionResult> NovaDrzavaSnimi(VoziloAdmNovaDrzavaVM model)
        {
            var drzave = await _drzavaService.Get<List<Drzava>>(new DrzavaSearchRequest() { Naziv = model.NazivDrzave });
            var drzava = drzave.FirstOrDefault();

            if (drzava != null)
            {
                TempData["porukaV"] = "Unesena država već postoji u bazi!";

                if (model.AutomobilId != 0)
                {
                    return RedirectToAction("NoviProizvodjac", new { VoziloID = model.AutomobilId });
                }
                else
                {
                    return RedirectToAction("NoviProizvodjac");
                }
            }

            DrzavaUpsertRequest req = new DrzavaUpsertRequest()
            {
                Naziv = model.NazivDrzave
            };

            await _drzavaService.Insert<Drzava>(req);

            if(model.AutomobilId!=0)
            {
                return RedirectToAction("NoviProizvodjac", new { VoziloID = model.AutomobilId });
            }
            else
            {
                return RedirectToAction("NoviProizvodjac");
            }            
        }
        public async Task<IActionResult> NoviModel(int VoziloID)
        {
            VoziloAdmNoviModelVM model = new VoziloAdmNoviModelVM();
            if(VoziloID!=0)
            {
                var vozilo = await _voziloService.GetById<Automobil>(VoziloID);
                model.AutomobilId = vozilo.AutomobilId;                
            }
            
            var proizvodjaci = await _proizvodjaciService.Get<List<Proizvodjac>>(null);
            model.listaProizvodjaca.Insert(0, new SelectListItem() { Text = "Odaberite proizvođača vozila", Value = "" });
            foreach (var proizvodjac in proizvodjaci)
            {
                var x = new SelectListItem() { Text = proizvodjac.Naziv, Value = proizvodjac.ProizvodjacId.ToString() };
                model.listaProizvodjaca.Add(x);
            }

            return View(model);
        }
        public async Task<IActionResult> NoviModelSnimi(VoziloAdmNoviModelVM model)
        {
            var modeliVozila = await _modeliService.Get<List<ModelAutomobila>>(new ModelAutomobilaSearch() { Naziv = model.NazivModela });
            var modelVozila = modeliVozila.FirstOrDefault();

            if (modelVozila != null)
            {
                TempData["porukaV"] = "Uneseni model već postoji u bazi!";

                if (model.AutomobilId != 0)
                {
                    return RedirectToAction("Uredi", new { VoziloID = model.AutomobilId });
                }
                else
                {
                    return RedirectToAction("Dodaj");
                }
            }

            ModelAutomobilaUpsertRequest req = new ModelAutomobilaUpsertRequest()
            {
                Naziv = model.NazivModela,
                ProizvodjacId=model.ProizvodjacId
            };

            await _modeliService.Insert<ModelAutomobila>(req);

            if (model.AutomobilId != 0)
            {
                return RedirectToAction("Uredi", new { VoziloID = model.AutomobilId });
            }
            else
            {
                return RedirectToAction("Dodaj");
            }            
        }
        public async Task<IActionResult> NovaKategorija(int VoziloID)
        {
            VoziloAdmNovaKategorijaVM model = new VoziloAdmNovaKategorijaVM();
            if (VoziloID!=0)
            {
                var vozilo = await _voziloService.GetById<Automobil>(VoziloID);
                model.AutomobilId = vozilo.AutomobilId;
            }                                       

            return View(model);
        }
        public async Task<IActionResult> NovaKategorijaSnimi(VoziloAdmNovaKategorijaVM model)
        {
            var kategorije=await _kategorijaService.Get<List<KategorijaVozila>>(new KategorijaSearchRequest() { Naziv = model.NazivKategorije });
            var kategorija = kategorije.FirstOrDefault();

            if(kategorija!=null)
            {
                TempData["porukaV"] = "Unesena kategorija već postoji u bazi!";

                if (model.AutomobilId != 0)
                {
                    return RedirectToAction("Uredi", new { VoziloID = model.AutomobilId });
                }
                else
                {
                    return RedirectToAction("Dodaj");
                }
            }

            KategorijaUpsertRequest req = new KategorijaUpsertRequest()
            {
                Naziv = model.NazivKategorije,
                Opis=model.Opis                                
            };

            await _kategorijaService.Insert<KategorijaVozila>(req);

            if (model.AutomobilId != 0)
            {
                return RedirectToAction("Uredi", new { VoziloID = model.AutomobilId });
            }
            else
            {
                return RedirectToAction("Dodaj");
            }            
        }
        public async Task<IActionResult> Dodaj()
        {
            VoziloAdmUrediVM model = new VoziloAdmUrediVM();
                  
            
            var slItem = new SelectListItem() { Text ="Prvo odaberite proizvođača", Value = "" };
            model.listaModel.Add(slItem);
            

            var proizvodjaci = await _proizvodjaciService.Get<List<Proizvodjac>>(null);
            model.listaProizvodjac.Insert(0,new SelectListItem() { Text = "Odaberite proizvođača", Value = "" });
            foreach (var proizvodjac in proizvodjaci)
            {
                var x = new SelectListItem() { Text = proizvodjac.Naziv, Value = proizvodjac.ProizvodjacId.ToString() };
                model.listaProizvodjac.Add(x);
            }

            var kategorije = await _kategorijaService.Get<List<KategorijaVozila>>(null);
            model.listaKategorija.Insert(0,new SelectListItem() { Text = "Odaberite kategoriju", Value = "" });
            foreach (var kategorija in kategorije)
            {
                var x = new SelectListItem() { Text = kategorija.Naziv, Value = kategorija.KategorijaId.ToString() };
                model.listaKategorija.Add(x);
            }

            string[] emisioniStandard = { "Euro 1", "Euro 2", "Euro 3", "Euro 4", "Euro 5", "Euro 6" };
            model.listaEmisioniStandard.Insert(0, new SelectListItem() { Text = "Odaberite emisioni standard", Value = "" });
            foreach (var es in emisioniStandard)
            {
                var x = new SelectListItem() { Text = es, Value = es };
                model.listaEmisioniStandard.Add(x);
            }
            string[] Transmisija = { "Automatic", "Manual" };
            model.listaTransmisija.Insert(0, new SelectListItem() { Text = "Odaberite transmisiju", Value = "" });
            foreach (var tr in Transmisija)
            {
                var x = new SelectListItem() { Text = tr, Value = tr };
                model.listaTransmisija.Add(x);
            }
            string[] Gorivo = { "Dizel", "Benzin", "Plin" };
            model.listaGorivo.Insert(0, new SelectListItem() { Text = "Odaberite gorivo", Value = "" });
            foreach (var g in Gorivo)
            {
                var x = new SelectListItem() { Text = g, Value = g };
                model.listaGorivo.Add(x);
            }
            string[] BrojVrata = { "2/3", "4/5" };
            model.listaBrojVrata.Insert(0, new SelectListItem() { Text = "Odaberite broj vrata", Value = "" });
            foreach (var vr in BrojVrata)
            {
                var x = new SelectListItem() { Text = vr, Value = vr };
                model.listaBrojVrata.Add(x);
            }
            string[] trajanjeL = { "3 mjeseca", "6 mjeseci", "12 mjeseci" };

            model.listaTrajanje.Insert(0, new SelectListItem() { Text = "Izaberite trajanje registracije", Value = "" });
            foreach (var tr in trajanjeL)
            {
                var x = new SelectListItem() { Value = tr, Text = tr };
                model.listaTrajanje.Add(x);
            }

            return View(model);
        }

        public async Task<IActionResult> PostaviStatusNedostupan(int VoziloID)
        {
            var vozilo = await _voziloService.GetById<Automobil>(VoziloID);

            AutomobiliUPSERTtRequest request = new AutomobiliUPSERTtRequest()
            {
                AutomobilId = vozilo.AutomobilId,
                Boja=vozilo.Boja,
                BrojSjedista=vozilo.BrojSjedista,
                BrojVrata=vozilo.BrojVrata,
                CijenaIznajmljivanja=vozilo.CijenaIznajmljivanja,
                CijenaKaskoOsiguranja=vozilo.CijenaKaskoOsiguranja,
                Dostupan=false,
                EmisioniStandard=vozilo.EmisioniStandard,
                GodinaProizvodnje=vozilo.GodinaProizvodnje,
                Gorivo=vozilo.Gorivo,
                KategorijaId=vozilo.KategorijaId,
                Kubikaza=vozilo.Kubikaza,
                ModelId=vozilo.ModelId,
                Novo=vozilo.Novo,
                Potrosnja=vozilo.Potrosnja,
                Slika=vozilo.Slika,
                SlikaThumb=vozilo.SlikaThumb,
                SnagaMotora=vozilo.SnagaMotora,
                Transmisija=vozilo.Transmisija
            };

            await _voziloService.Update<Automobil>(VoziloID, request);
            TempData["Poruka"] = "Vozilo uspjesno postavljeno kao nedostupno!";

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> PostaviStatusDostupan(int VoziloID)
        {
            var vozilo = await _voziloService.GetById<Automobil>(VoziloID);

            AutomobiliUPSERTtRequest request = new AutomobiliUPSERTtRequest()
            {
                AutomobilId = vozilo.AutomobilId,
                Boja = vozilo.Boja,
                BrojSjedista = vozilo.BrojSjedista,
                BrojVrata = vozilo.BrojVrata,
                CijenaIznajmljivanja = vozilo.CijenaIznajmljivanja,
                CijenaKaskoOsiguranja = vozilo.CijenaKaskoOsiguranja,
                Dostupan = true,
                EmisioniStandard = vozilo.EmisioniStandard,
                GodinaProizvodnje = vozilo.GodinaProizvodnje,
                Gorivo = vozilo.Gorivo,
                KategorijaId = vozilo.KategorijaId,
                Kubikaza = vozilo.Kubikaza,
                ModelId = vozilo.ModelId,
                Novo = vozilo.Novo,
                Potrosnja = vozilo.Potrosnja,
                Slika = vozilo.Slika,
                SlikaThumb = vozilo.SlikaThumb,
                SnagaMotora = vozilo.SnagaMotora,
                Transmisija = vozilo.Transmisija
            };

            await _voziloService.Update<Automobil>(VoziloID, request);
            TempData["Poruka"] = "Vozilo uspjesno postavljeno kao dostupno!";

            return RedirectToAction("Index");
        }

        public async Task<JsonResult> getModelById(int id)
        {
            List<ModelAutomobila> list = new List<ModelAutomobila>();
            List<SelectListItem> selectLista = new List<SelectListItem>();
            if (id != 0)
            {
                list = await _modeliService.Get<List<ModelAutomobila>>(new ModelAutomobilaSearch() { ProizvodjacId = id });
                foreach (var item in list)
                {
                    var x = new SelectListItem() { Text = item.Naziv, Value = item.ModelId.ToString() };
                    selectLista.Add(x);
                }

                selectLista.Insert(0, new SelectListItem { Text = "Odaberite model", Value = "" });
            }
            else
            {
                selectLista.Insert(0, new SelectListItem { Text = "Prvo odaberite proizvođača", Value = "" });
            }
            return Json(new SelectList(selectLista,"Value", "Text"));
        }
    }
}