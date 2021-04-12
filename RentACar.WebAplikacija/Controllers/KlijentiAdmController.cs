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
    public class KlijentiAdmController : Controller
    {
        private readonly APIService _porukeService = new APIService("Poruka");
        private readonly APIService _klijentiService = new APIService("Klijent");
        private readonly APIService _korisnikService = new APIService("Korisnik");
        private readonly APIService _korisniciUlogaService = new APIService("KorisniciUloga");
        private readonly APIService _ulogaService = new APIService("Uloga");
        private readonly APIService _drzavaService = new APIService("Drzava");
        private readonly APIService _gradService = new APIService("Grad");
        private readonly APIService _PorukaService = new APIService("Poruka");
        public async Task<IActionResult> Index()
        {
            var logiranikorisnik = HttpContext.GetLogiraniKorisnikAdm();
            KlijentiAdmVM model = new KlijentiAdmVM();
            var result1 = await _klijentiService.Get<List<Klijent>>(new KlijentSearchRequest() { Status = true });
            var result2 = await _klijentiService.Get<List<Klijent>>(new KlijentSearchRequest() { Status = false });

            var result = new List<Klijent>();
            foreach (var klijent in result1)
            {
               
                result.Add(klijent);
                
            }
            foreach (var klijent in result2)
            {
                result.Add(klijent);
            }

            foreach (var x in result)
            {
                KlijentiAdmVM.Row klijent = new KlijentiAdmVM.Row()
                {
                    Adresa = x.Adresa,
                    Status = x.Status,
                    DatumRegistracije = x.DatumRegistracije,
                    DatumRodjenja = x.DatumRodjenja,
                    Email = x.Email,
                    GradId = x.GradId,
                    Ime = x.Ime,
                    ImePrezime = x.Ime+" "+x.Prezime,
                    KlijentId = x.KlijentId,
                    LozinkaHash = x.LozinkaHash,
                    LozinkaSalt = x.LozinkaSalt,
                    Prezime = x.Prezime,
                    Slika = x.Slika,
                    SlikaThumb = x.SlikaThumb,
                    Telefon = x.Telefon,
                    UserName = x.UserName
                };
                                                
               
                var grad = await _gradService.GetById<Grad>(x.GradId);
                klijent.NazivGrada = grad.Naziv;

                                
                model.listaKlijenata.Add(klijent);
                
            }

            return View(model);
        }

        public async Task<IActionResult> Dodaj()
        {     
            
            var listaUlogaAll = await _ulogaService.Get<List<Uloge>>(null);
            var result1 = await _klijentiService.Get<List<Klijent>>(new KlijentSearchRequest() { Status = true });
            var result2 = await _klijentiService.Get<List<Klijent>>(new KlijentSearchRequest() { Status = false });
            var result3 = await _korisnikService.Get<List<Korisnici>>(new KorisniciSearchRequest() { Status = true });
            var result4 = await _korisnikService.Get<List<Korisnici>>(new KorisniciSearchRequest() { Status = false });

            var result = new List<string>();
            foreach (var klijent in result1)
            {
                result.Add(klijent.UserName);
            }
            foreach (var klijent in result2)
            {
                result.Add(klijent.UserName);
            }
            foreach (var korisnik in result3)
            {
                result.Add(korisnik.UserName);
            }
            foreach (var korisnik in result4)
            {
                result.Add(korisnik.UserName);
            }

            result.Sort();
            var listaUsername = result;

            KlijentiAdmDodajVM model = new KlijentiAdmDodajVM();
            


            var gradovi = await _gradService.Get<List<Grad>>(null);
            foreach (var x in gradovi)
            {
                var gr = new SelectListItem() { Text = x.Naziv, Value = x.GradId.ToString() };
                model.listaGradova.Add(gr);
            }
            model.listaGradova.Insert(0, new SelectListItem() { Text = "Izaberi grad", Value = "" });
                        

            model.listaUserName = listaUsername;
                        
            model.DatumRegistracije = DateTime.Today;
            model.Status = false;

            return View("Dodaj",model);
        }
        public async Task<IActionResult> Uredi(int KlijentID)
        {
           
            var klijent = await _klijentiService.GetById<Klijent>(KlijentID);

            KlijentiAdmUrediVM model = new KlijentiAdmUrediVM()
            {
                Adresa = klijent.Adresa,
                Status = klijent.Status,
                DatumRegistracije = klijent.DatumRegistracije,
                DatumRodjenja = klijent.DatumRodjenja,
                Email = klijent.Email,
                GradId = klijent.GradId,
                Ime = klijent.Ime,
                ImePrezime = klijent.Ime+" "+klijent.Prezime,
                KlijentId = klijent.KlijentId,
                LozinkaHash = klijent.LozinkaHash,
                LozinkaSalt = klijent.LozinkaSalt,
                Prezime = klijent.Prezime,
                Slika = klijent.Slika,
                SlikaThumb = klijent.SlikaThumb,
                Telefon = klijent.Telefon,
                UserName = klijent.UserName
            };
                       
            var grad = await _gradService.GetById<Grad>(klijent.GradId);
            model.NazivGrada = grad.Naziv;
                        
            var gradovi = await _gradService.Get<List<Grad>>(null);
            foreach (var x in gradovi)
            {
                var gr = new SelectListItem() { Text = x.Naziv, Value = x.GradId.ToString() };
                model.listaGradova.Add(gr);
            }
           
            return View(model);
        }

        public async Task<IActionResult> Obrisi(int KlijentID)
        {

            var korisnik = await _klijentiService.GetById<Korisnici>(KlijentID);
            try
            {
                await _klijentiService.Delete<Klijent>(KlijentID);
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    TempData["Poruka"] = "Nemate ulogu administratora i ne mozete obrisati klijenta!";
                    return RedirectToAction("Index");
                }                                  
            }

            TempData["PorukaObrisan"] = "Klijent je obrisan!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Deaktiviraj(int KlijentID)
        {

           var korisnik = await _klijentiService.GetById<Klijent>(KlijentID);

            KlijentUpsertRequest request = new KlijentUpsertRequest()
            {
                Adresa = korisnik.Adresa,
                DatumRegistracije = korisnik.DatumRegistracije,
                DatumRodjenja = korisnik.DatumRodjenja,
                Email = korisnik.Email,
                GradId = korisnik.GradId,
                Ime = korisnik.Ime,
                KlijentId = korisnik.KlijentId,
                Prezime = korisnik.Prezime,
                Status = false,
                Telefon = korisnik.Telefon,
                UserName = korisnik.UserName,
                Slika = korisnik.Slika,
                SlikaThumb = korisnik.SlikaThumb                
            };

            await _klijentiService.Update<Klijent>(KlijentID, request);
            TempData["Poruka"] = "Klijent uspjesno deaktiviran!";

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Aktiviraj(int KlijentID)
        {

            var korisnik = await _klijentiService.GetById<Klijent>(KlijentID);

            KlijentUpsertRequest request = new KlijentUpsertRequest()
            {
                Adresa = korisnik.Adresa,
                DatumRegistracije = korisnik.DatumRegistracije,
                DatumRodjenja = korisnik.DatumRodjenja,
                Email = korisnik.Email,
                GradId = korisnik.GradId,
                Ime = korisnik.Ime,
                KlijentId = korisnik.KlijentId,
                Prezime = korisnik.Prezime,
                Status = true,
                Telefon = korisnik.Telefon,
                UserName = korisnik.UserName,
                Slika = korisnik.Slika,
                SlikaThumb = korisnik.SlikaThumb                
            };

            await _klijentiService.Update<Klijent>(KlijentID, request);
            TempData["Poruka"] = "Klijent uspjesno aktiviran!";

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Snimi(KlijentiAdmUrediVM model)
        {
            
                Korisnici logiranik = await HttpContext.GetLogiraniKorisnikAdm();
            Klijent klijent=null;

            if (model.KlijentId!=0)
                klijent = await _klijentiService.GetById<Klijent>(model.KlijentId);
            
                byte[] SlikaNew = null, SlikaThumbNew = null;

                #region SlikaProfila

                if (model.slikaProfila != null)
                {
                    var filename = System.IO.Path.GetFileName(model.slikaProfila.FileName);
                    Image image = null;

                    //using (var localFile = System.IO.File.OpenWrite(filename))
                    using (var uploadedFile = model.slikaProfila.OpenReadStream())
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
                    if (model.KlijentId == 0)
                    {
                        Image image = null;
                        image = Image.FromFile("wwwroot/metronic/themes/metronic/theme/default/demo12/dist/assets/media/users/profile_pic.jpg");

                        MemoryStream msImg = new MemoryStream();
                        image.Save(msImg, System.Drawing.Imaging.ImageFormat.Jpeg);
                        SlikaNew = msImg.ToArray();

                        Image thumb = image.GetThumbnailImage(75, 75, () => false, IntPtr.Zero);
                        MemoryStream ms = new MemoryStream();
                        thumb.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        SlikaThumbNew = ms.ToArray();
                    }
                    else
                    {
                        SlikaNew = klijent.Slika;
                        SlikaThumbNew = klijent.SlikaThumb;
                    }
                    
                }
            #endregion


                string dan="", mjesec="", godina = "";

                var datumRodjenjastr = model.DatumRodjenjaString;
                dan = datumRodjenjastr.Substring(0, 2);
                mjesec = datumRodjenjastr.Substring(3, 2);
                godina = datumRodjenjastr.Substring(6, 4);
                var datumRodjenja = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));

                dan = "";  mjesec = "";  godina = "";
                var datumRegistracijeStr = model.DatumRegistracijeString;
                dan = datumRegistracijeStr.Substring(0, 2);
                mjesec = datumRegistracijeStr.Substring(3, 2);
                godina = datumRegistracijeStr.Substring(6, 4);
                var datumRegistracije = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));


            KlijentUpsertRequest request = new KlijentUpsertRequest()
                {
                    Adresa = model.Adresa,
                    DatumRegistracije = datumRegistracije,
                    DatumRodjenja = datumRodjenja,
                    Email = model.Email,
                    GradId = model.GradId,
                    Ime = model.Ime,
                    KlijentId = model.KlijentId,
                    Prezime = model.Prezime,
                    Status = model.Status,
                    Telefon = model.Telefon,
                    UserName = model.UserName,
                    Slika = SlikaNew,
                    SlikaThumb = SlikaThumbNew
                };

            if(model.KlijentId==0)
            {
                request.Password = model.Password;
                request.PasswordPotvrda = model.PasswordPotvrda;
                await _klijentiService.Insert<Klijent>(request);
            }
            else
            {
                await _klijentiService.Update<Klijent>(model.KlijentId, request);
            }
                           
            
            return RedirectToAction("Index");
        }




        public async Task<IActionResult> Poslane()
        {
            PorukaSearchRequest porukaSearchRequest = new PorukaSearchRequest
            {
                UposlenikId =HttpContext.GetLogiraniKorisnikAdm().Result.KorisnikId,
                Posiljaoc = "Uposlenik"

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
        public async Task<IActionResult> Primljene()
        {
            PorukaSearchRequest porukaSearchRequest = new PorukaSearchRequest
            {
                UposlenikId = HttpContext.GetLogiraniKorisnikAdm().Result.KorisnikId,
                Posiljaoc = "Klijent"
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
                Posiljaoc="Uposlenik"
            };

            await _porukeService.Insert<Poruka>(p);
                                   
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
    }
}