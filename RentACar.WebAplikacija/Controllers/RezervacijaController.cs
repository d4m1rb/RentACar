using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.Web;
using RentACar.WebAplikacija.Helper;
using RentACar.WebAplikacija.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentACar.WebAplikacija.Controllers
{
    public class RezervacijaController : Controller
    {
        private readonly APIService _rezervacijeService = new APIService("RezervacijaRentanja");
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _ocjenaService = new APIService("Ocjena");
        private readonly APIService _kategorijaVozilaService = new APIService("KategorijaVozila");
        private readonly APIService _racuniService = new APIService("Racun");
        private readonly APIService _porukeService = new APIService("Poruka");

        public List<Automobil> listaDostupnihVozila = new List<Automobil>();
        RezervacijePretragaVM rezervacijePretragaVM = new RezervacijePretragaVM();
        public string Poruka { get; set; }
        
        public async Task<IActionResult> Index2()
        {
            var korisnik = await HttpContext.GetLogiraniKorisnik();
            RezervacijeIndexVM model = new RezervacijeIndexVM();
            if (korisnik!=null)
            {
                RezervacijaRentanjaSearchRequest searchRequest = new RezervacijaRentanjaSearchRequest();
                searchRequest.KlijentId = korisnik.KlijentId;
                searchRequest.Otkazana = false;

                var list = await _rezervacijeService.Get<IEnumerable<RezervacijaRentanja>>(searchRequest);


                model.brojRezervacija = 0;  model.uToku = 0;  model.Zavrsene = 0;
                model.ukupno = 0;

                model.RezervacijeRetanjaList = new ObservableCollection<RezervacijaRentanja>();
                model.RezervacijeRetanjaListZavrsene = new ObservableCollection<RezervacijaRentanja>();



                model.RezervacijeRetanjaList.Clear();
                model.RezervacijeRetanjaListZavrsene.Clear();



                foreach (var item in list)
                {
                    if (item.RezervacijaOd > DateTime.Now)
                    {
                        model.RezervacijeRetanjaList.Add(item);
                        model.uToku++;
                    }
                    else
                    {
                        model.RezervacijeRetanjaListZavrsene.Add(item);
                        model.Zavrsene++;
                    }
                    model.ukupno += item.IznosSaPopustom;
                    model.brojRezervacija++;
                }


                
            }
                return View(model);
        }

        public async Task<IActionResult> Index(string generalSearch)
        {
            var korisnik = await HttpContext.GetLogiraniKorisnik();
            RezervacijeIndexVM model = new RezervacijeIndexVM();
            if (korisnik != null)
            {
                RezervacijaRentanjaSearchRequest searchRequest = new RezervacijaRentanjaSearchRequest();
                searchRequest.KlijentId = korisnik.KlijentId;
                searchRequest.Otkazana = false;

                var list = await _rezervacijeService.Get<IEnumerable<RezervacijaRentanja>>(searchRequest);


                model.brojRezervacija = 0; model.uToku = 0; model.Zavrsene = 0;
                model.ukupno = 0;

                model.RezervacijeRetanjaList = new ObservableCollection<RezervacijaRentanja>();
                model.RezervacijeRetanjaListZavrsene = new ObservableCollection<RezervacijaRentanja>();
                model.ListaOcjena = new ObservableCollection<Ocjena>();
                model.ListaOcjenaZavrsene = new ObservableCollection<Ocjena>();

                model.RezervacijeRetanjaList.Clear();
                model.RezervacijeRetanjaListZavrsene.Clear();
                model.ListaOcjena.Clear();
                model.ListaOcjenaZavrsene.Clear();


                if (generalSearch != null)
                {
                    foreach (var item in list)
                    {
                        var brojPoruka = _porukeService.Get<List<Poruka>>(new PorukaSearchRequest() { RezervacijaRentanjaId = item.RezervacijaRentanjaId, Posiljaoc = "Uposlenik",ObrisanoKlijent=false }).Result.Count();
                        var ocjena = _ocjenaService.Get<List<Ocjena>>(new OcjenaSearchRequest() { RezervacijaRentanjaId = item.RezervacijaRentanjaId }).Result.FirstOrDefault();


                        if (item.VoziloInformacije.ToUpper().Contains(generalSearch.ToUpper()))
                        {
                            if (item.RezervacijaOd > DateTime.Now)
                            {

                                model.RezervacijeRetanjaList.Add(item);
                                if (ocjena != null)
                                {
                                    model.ListaOcjena.Add(ocjena);
                                }
                                else
                                {
                                    ocjena = new Ocjena()
                                    {
                                        Napomena = "Nema ocjene!",
                                        RezervacijaRentanjaId = item.RezervacijaRentanjaId,
                                        DatumEvidentiranja = DateTime.Today,
                                        KlijentId = item.KlijentId,
                                        VoziloId = item.AutomobilId,
                                        Ocjena1 = 0
                                    };
                                    model.ListaOcjena.Add(ocjena);
                                }
                                model.uToku++;
                            }
                            else
                            {
                                model.RezervacijeRetanjaListZavrsene.Add(item);
                                if (ocjena != null)
                                {
                                    model.ListaOcjenaZavrsene.Add(ocjena);
                                }
                                else
                                {
                                    ocjena = new Ocjena()
                                    {
                                        Napomena = "Nema ocjene!",
                                        RezervacijaRentanjaId = item.RezervacijaRentanjaId,
                                        DatumEvidentiranja = DateTime.Today,
                                        KlijentId = item.KlijentId,
                                        VoziloId = item.AutomobilId,
                                        Ocjena1 = 0
                                    };
                                    model.ListaOcjenaZavrsene.Add(ocjena);
                                }
                                model.ListaBrojPorukaZavrsene.Add(brojPoruka);
                                model.Zavrsene++;
                            }
                            model.ukupno += item.IznosSaPopustom;
                            model.brojRezervacija++;
                        }
                    }
                }
                else
                {
                    foreach (var item in list)
                    {
                        var brojPoruka = _porukeService.Get<List<Poruka>>(new PorukaSearchRequest() { RezervacijaRentanjaId = item.RezervacijaRentanjaId, Posiljaoc = "Uposlenik", ObrisanoKlijent = false }).Result.Count();
                        var ocjena = _ocjenaService.Get<List<Ocjena>>(new OcjenaSearchRequest() { RezervacijaRentanjaId = item.RezervacijaRentanjaId }).Result.FirstOrDefault();

                        if (item.RezervacijaOd > DateTime.Now)
                            {

                                model.RezervacijeRetanjaList.Add(item);
                            if (ocjena != null)
                            {
                                model.ListaOcjena.Add(ocjena);
                            }
                            else
                            {
                                ocjena = new Ocjena()
                                {
                                    Napomena = "Nema ocjene!",
                                    RezervacijaRentanjaId = item.RezervacijaRentanjaId,
                                    DatumEvidentiranja = DateTime.Today,
                                    KlijentId = item.KlijentId,
                                    VoziloId = item.AutomobilId,
                                    Ocjena1 = 0
                                };
                                model.ListaOcjena.Add(ocjena);
                            }
                            model.uToku++;
                            }
                            else
                            {
                                model.RezervacijeRetanjaListZavrsene.Add(item);
                                if (ocjena != null)
                                {
                                    model.ListaOcjenaZavrsene.Add(ocjena);
                                }
                                else
                                {
                                    ocjena = new Ocjena()
                                    {
                                        Napomena = "Nema ocjene!",
                                        RezervacijaRentanjaId = item.RezervacijaRentanjaId,
                                        DatumEvidentiranja = DateTime.Today,
                                        KlijentId = item.KlijentId,
                                        VoziloId = item.AutomobilId,
                                        Ocjena1 = 0
                                    };
                                    model.ListaOcjenaZavrsene.Add(ocjena);
                                }
                                model.ListaBrojPorukaZavrsene.Add(brojPoruka);
                                model.Zavrsene++;
                            }
                            model.ukupno += item.IznosSaPopustom;
                            model.brojRezervacija++;
                        
                    }
                }

            }

            return View(model);
        }

        public async Task<IActionResult> UToku(string generalSearch)
        {
            var korisnik = await HttpContext.GetLogiraniKorisnik();
            RezervacijeIndexVM model = new RezervacijeIndexVM();
            if (korisnik != null)
            {
                RezervacijaRentanjaSearchRequest searchRequest = new RezervacijaRentanjaSearchRequest();
                searchRequest.KlijentId = korisnik.KlijentId;
                searchRequest.Otkazana = false;

                var list = await _rezervacijeService.Get<IEnumerable<RezervacijaRentanja>>(searchRequest);


                model.brojRezervacija = 0; model.uToku = 0;
                model.ukupno = 0;

                model.RezervacijeRetanjaList = new ObservableCollection<RezervacijaRentanja>();
                model.ListaOcjena = new ObservableCollection<Ocjena>();
                model.ListaOcjenaZavrsene = new ObservableCollection<Ocjena>();

                model.RezervacijeRetanjaList.Clear();
                model.ListaOcjena.Clear();
                model.ListaOcjenaZavrsene.Clear();


                if (generalSearch != null)
                {
                    foreach (var item in list)
                    {
                        var ocjena = _ocjenaService.Get<List<Ocjena>>(new OcjenaSearchRequest() { RezervacijaRentanjaId = item.RezervacijaRentanjaId }).Result.FirstOrDefault();

                        if (item.VoziloInformacije.ToUpper().Contains(generalSearch.ToUpper()))
                        {
                            if (item.RezervacijaOd > DateTime.Now)
                            {

                                model.RezervacijeRetanjaList.Add(item);
                                if (ocjena != null)
                                {
                                    model.ListaOcjena.Add(ocjena);
                                }
                                else
                                {
                                    ocjena = new Ocjena()
                                    {
                                        Napomena = "Nema ocjene!",
                                        RezervacijaRentanjaId = item.RezervacijaRentanjaId,
                                        DatumEvidentiranja = DateTime.Today,
                                        KlijentId = item.KlijentId,
                                        VoziloId = item.AutomobilId,
                                        Ocjena1 = 0
                                    };
                                    model.ListaOcjena.Add(ocjena);
                                }
                                model.uToku++;
                            }                            
                            model.ukupno += item.IznosSaPopustom;
                            model.brojRezervacija++;
                        }
                    }
                }
                else
                {
                    foreach (var item in list)
                    {
                        var ocjena = _ocjenaService.Get<List<Ocjena>>(new OcjenaSearchRequest() { RezervacijaRentanjaId = item.RezervacijaRentanjaId }).Result.FirstOrDefault();

                        if (item.RezervacijaOd > DateTime.Now)
                        {

                            model.RezervacijeRetanjaList.Add(item);
                            if (ocjena != null)
                            {
                                model.ListaOcjena.Add(ocjena);
                            }
                            else
                            {
                                ocjena = new Ocjena()
                                {
                                    Napomena = "Nema ocjene!",
                                    RezervacijaRentanjaId = item.RezervacijaRentanjaId,
                                    DatumEvidentiranja = DateTime.Today,
                                    KlijentId = item.KlijentId,
                                    VoziloId = item.AutomobilId,
                                    Ocjena1 = 0
                                };
                                model.ListaOcjena.Add(ocjena);
                            }
                            model.uToku++;
                        }
                        
                        model.ukupno += item.IznosSaPopustom;
                        model.brojRezervacija++;

                    }
                }

            }

            var tempList=model.RezervacijeRetanjaList.OrderByDescending(x => x.DatumKreiranja).ToList();
            ObservableCollection<RezervacijaRentanja> oc = new ObservableCollection<RezervacijaRentanja>(tempList);

            model.ListaBrojPoruka.Clear();
            foreach (var rezervacija in oc)
            {
                var brojPoruka = _porukeService.Get<List<Poruka>>(new PorukaSearchRequest() { RezervacijaRentanjaId = rezervacija.RezervacijaRentanjaId, Posiljaoc = "Uposlenik", ObrisanoKlijent = false }).Result.Count();
                model.ListaBrojPoruka.Add(brojPoruka);
            }
            model.RezervacijeRetanjaList = oc;

            return View(model);
        }
        public IActionResult OcijeniZavrsene(int rezervacijaID)
        {
            RezervacijeOcijeniVM model = new RezervacijeOcijeniVM()
            {
                RezervacijaRentanjaId = rezervacijaID
            };

            return View(model);
        }

        public IActionResult OcijeniUToku(int rezervacijaID)
        {
            RezervacijeOcijeniVM model = new RezervacijeOcijeniVM()
            {
                RezervacijaRentanjaId = rezervacijaID
            };

            return View(model);
        }

        public async Task<IActionResult> SnimiOcjenuZapocete(RezervacijeOcijeniVM input)
        {
            var request = new OcjenaUpsertRequest()
            {
                DatumEvidentiranja = DateTime.Today,
                RezervacijaRentanjaId = input.RezervacijaRentanjaId,
                Napomena = input.Napomena,
                Ocjena1 = input.Ocjena
            };

            await _ocjenaService.Insert<Ocjena>(request);

            TempData["poruka"] = "Ocjena uspješno unesena!";
            return RedirectToAction("Index", "Rezervacija");
        }

        public async Task<IActionResult> SnimiOcjenuNezapocete(RezervacijeOcijeniVM input)
        {
            var request = new OcjenaUpsertRequest()
            {
                DatumEvidentiranja = DateTime.Today,
                RezervacijaRentanjaId = input.RezervacijaRentanjaId,
                Napomena = input.Napomena,
                Ocjena1 = input.Ocjena
            };

            await _ocjenaService.Insert<Ocjena>(request);

            TempData["poruka"] = "Ocjena uspješno unesena!";
            return RedirectToAction("UToku", "Rezervacija");
        }
        public IActionResult PretragaDatum()
        {
            return View();
        }

        public IActionResult PretragaPoKategoriji(int kategorijaID)
        {
            var temp = HttpContext.Request.GetCookieJson<TempModel>("tempMod");
            RezervacijePretragaDatumVM model = new RezervacijePretragaDatumVM()
            {
                RezervacijaOd = temp.DatumRezervacijaOd,
                RezervacijaDo = temp.DatumRezervacijaDo,
                RezervacijaOdString = temp.DatumRezervacijaOd.ToString("dd-MM-yyyy"),
                RezervacijaDoString = temp.DatumRezervacijaDo.ToString("dd-MM-yyyy"),
                kategorijaVozilaId =kategorijaID
            };

            return RedirectToAction("Pretraga",model);
        }
        public async Task<IActionResult> Pretraga(RezervacijePretragaDatumVM input)
        {
            string dan = "", mjesec = "", godina = "";
            DateTime datumRezervacijeOd=DateTime.MinValue, datumRezervacijeDo=DateTime.MinValue;

            if (input.RezervacijaOdString != null && input.RezervacijaDoString != null)
            {
                var datumRezervacijeOdStr = input.RezervacijaOdString;
                dan = datumRezervacijeOdStr.Substring(0, 2);
                mjesec = datumRezervacijeOdStr.Substring(3, 2);
                godina = datumRezervacijeOdStr.Substring(6, 4);
                datumRezervacijeOd = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));

                dan = ""; mjesec = ""; godina = "";
                var datumRezervacijeDoStr = input.RezervacijaDoString;
                dan = datumRezervacijeDoStr.Substring(0, 2);
                mjesec = datumRezervacijeDoStr.Substring(3, 2);
                godina = datumRezervacijeDoStr.Substring(6, 4);
                datumRezervacijeDo = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));
            }


            RezervacijePretragaVM model = new RezervacijePretragaVM();            
            RezervacijaRentanjaSearchRequest search = new RezervacijaRentanjaSearchRequest() { StatusAktivna = true };
            AutomobilSearchRequest searchAutomobil;

            if (input.kategorijaVozilaId!=0)
            {
                searchAutomobil = new AutomobilSearchRequest() { Dostupan = true, KategorijaId=input.kategorijaVozilaId };
            }
            else
            {
                searchAutomobil = new AutomobilSearchRequest() { Dostupan = true };
            }
            
            List<RezervacijaRentanja> sveRezervacije = await _rezervacijeService.Get<List<RezervacijaRentanja>>(search);
            listaDostupnihVozila = await _vozilaService.Get<List<Automobil>>(searchAutomobil);

            if(datumRezervacijeOd==DateTime.MinValue || datumRezervacijeDo==DateTime.MinValue)
            {
               return RedirectToAction("PretragaDatum");
            }

            foreach (var rezervacija in sveRezervacije)
            {
                if (datumRezervacijeOd < rezervacija.RezervacijaOd)
                {
                    if (datumRezervacijeDo < rezervacija.RezervacijaOd)
                    {
                        //Dodaj automobil u dostupne automobile
                        continue;
                    }
                    else
                    {
                        //Automobil nije dostupan
                        Automobil x = await _vozilaService.GetById<Automobil>(rezervacija.AutomobilId);

                        for (int i = 0; i < listaDostupnihVozila.Count; i++)
                        {
                            if (listaDostupnihVozila[i].AutomobilId == x.AutomobilId)
                            {
                                listaDostupnihVozila.RemoveAt(i);
                            }
                        }
                    }
                }
                else if (datumRezervacijeDo > rezervacija.RezervacijaDo)
                {
                    if (datumRezervacijeOd > rezervacija.RezervacijaDo)
                    {
                        //Dodaj automobil u dostupne automobile
                        continue;
                    }
                    else
                    {
                        //Automobil nije dostupan
                        Automobil x = await _vozilaService.GetById<Automobil>(rezervacija.AutomobilId);

                        for (int i = 0; i < listaDostupnihVozila.Count; i++)
                        {
                            if (listaDostupnihVozila[i].AutomobilId == x.AutomobilId)
                            {
                                listaDostupnihVozila.RemoveAt(i);
                            }
                        }
                    }
                }
                else
                {
                    //Automobil nije dostupan
                    Automobil x = await _vozilaService.GetById<Automobil>(rezervacija.AutomobilId);

                    for (int i = 0; i < listaDostupnihVozila.Count; i++)
                    {
                        if (listaDostupnihVozila[i].AutomobilId == x.AutomobilId)
                        {
                            listaDostupnihVozila.RemoveAt(i);
                        }
                    }
                }

            }
            if (listaDostupnihVozila.Count == 0)
            {
                model._porukaDostupnaVozila = "Nema dostupnih vozila.";
                model._porukaDostupnaVozilaBool = true;
            }
            if (datumRezervacijeOd >= datumRezervacijeDo)
            {
                Poruka = "Datum početka mora biti manji od datuma završetka rezervacije najmanje za jedan dan.";
               return RedirectToAction("PretragaDatum");
            }
            else
            {      

                foreach (var item in listaDostupnihVozila)
                {
                    var ocjene = await _ocjenaService.Get<List<Ocjena>>(new OcjenaSearchRequest() { VoziloId = item.AutomobilId });
                    decimal prosjecnaOcjena = 0;

                    if (ocjene.Count() != 0)
                    {
                        var sumaOcjena = 0; var brojOcjena = 0;
                        foreach (var ocjena in ocjene)
                        {
                            sumaOcjena += ocjena.Ocjena1;
                            brojOcjena++;
                        }

                        prosjecnaOcjena = sumaOcjena / (decimal)brojOcjena;
                    }

                    RezervacijePretragaVM.Row x = new RezervacijePretragaVM.Row();

                    x.AutomobilId = item.AutomobilId;
                    x.Boja = item.Boja;
                    x.BrojSjedista = item.BrojSjedista;
                    x.BrojVrata = item.BrojVrata;
                    x.CijenaIznajmljivanja = item.CijenaIznajmljivanja;
                    x.CijenaKaskoOsiguranja = item.CijenaKaskoOsiguranja;
                    x.Dostupan = item.Dostupan;
                    x.DostupanTekst = item.DostupanTekst;
                    x.EmisioniStandard = item.EmisioniStandard;
                    x.GodinaProizvodnje = item.GodinaProizvodnje;
                    x.Gorivo = item.Gorivo;
                    x.KategorijaId = item.KategorijaId;
                    x.Kubikaza = item.Kubikaza;
                    x.ModelId = item.ModelId;
                    x.Novo = item.Novo;
                    x.Potrosnja = item.Potrosnja;
                    x.ProizvodjacModel = item.ProizvodjacModel;
                    x.RegistarskaOznaka = item.RegistarskaOznaka;
                    x.RegistrovanDo = item.RegistrovanDo;
                    x.Slika = item.Slika;
                    x.SlikaThumb = item.SlikaThumb;
                    x.SnagaMotora = item.SnagaMotora;
                    x.Transmisija = item.Transmisija;
                    x.ProsjecnaOcjena = prosjecnaOcjena;
                    x.KubikazaString = (Decimal.Parse(item.Kubikaza) / 100 * 100).ToString("0.00");

                    model.listaAutomobila.Add(x);
                }
                model.DatumRezervacijaOd = datumRezervacijeOd;
                model.DatumRezervacijaDo = datumRezervacijeDo;

                TempModel temp = new TempModel() { 
                DatumRezervacijaOd=model.DatumRezervacijaOd,
                DatumRezervacijaDo=model.DatumRezervacijaDo
                };

                //var tempString = Newtonsoft.Json.JsonConvert.SerializeObject(temp);               
                //TempData["tempMod"] = tempString;
                HttpContext.Response.SetCookieJson("tempMod", temp);

                
                var listaKatVozila = await _kategorijaVozilaService.Get<List<KategorijaVozila>>(null);
                model.listaKategorija.Add(new SelectListItem() { Text = "Odaberite kategoriju", Value = null });
                foreach (var item in listaKatVozila)
                {
                    var SelIt = new SelectListItem() { Text = item.Naziv, Value = item.KategorijaId.ToString()};
                    model.listaKategorija.Add(SelIt);
                }
                

                return View("DostupnaVozila", model);
            }

            //return View("PretragaDatum");
        }

        public IActionResult DostupnaVozila()
        {
            //RezervacijePretragaVM model = new RezervacijePretragaVM();
            //return View(model);
            return View();
        }

        public IActionResult Detalji(int AutomobilID)
        {
            return View();
        }

        public async Task<IActionResult> Rezervisi(int AutomobilID)
        {
            
            var automobil = await _vozilaService.GetById<Automobil>(AutomobilID);
            RezervacijeRezervisiVM model = new RezervacijeRezervisiVM()
            {
                _automobil = automobil,
                _cijenaKaskoString=automobil.CijenaKaskoOsiguranja.ToString()                    
            };

            var temp=HttpContext.Request.GetCookieJson<TempModel>("tempMod");
            //var tempStr = TempData["tempMod"] as string;
            //var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<RentACar.WebAplikacija.ViewModels.RezervacijePretragaVM>(tempStr);

            model._datumRezervacijeOd = temp.DatumRezervacijaOd;
            model._datumRezervacijeDo = temp.DatumRezervacijaDo;

            bool dostupnostVozila = await provjeriDostupnostVozila(AutomobilID, model._datumRezervacijeOd, model._datumRezervacijeDo);
           
            if (!dostupnostVozila)
            {
                throw new Exception("Vozilo nije dostupno!");
            }
            
            

            double discount = 0;
            TimeSpan brDana = model._datumRezervacijeDo - model._datumRezervacijeOd;
            if (brDana.Days >= 3 && brDana.Days < 5)
                discount = 0.1;
            else if (brDana.Days >= 5 && brDana.Days < 10)
                discount = 0.2;
            else if (brDana.Days >= 10)
                discount = 0.3;

            var cijena = (model._automobil.CijenaIznajmljivanja) * brDana.Days;
            model._ukupnoCijenaBezKaska = cijena - cijena * (decimal)discount;
            model._popust = (decimal)discount;
            model._ukupnoCijena = model._ukupnoCijenaBezKaska;

            var cijenaKasko= (model._automobil.CijenaIznajmljivanja + model._automobil.CijenaKaskoOsiguranja) * brDana.Days;
            model._ukupnoCijenaSaKaskom = cijenaKasko - cijenaKasko * (decimal)discount;
            model.AutomobilId = AutomobilID;

            return View(model);
        }

        public async Task<IActionResult> PotvrdiRezervaciju(RezervacijeRezervisiVM input)
        {
            string dan = "", mjesec = "", godina = "";
            DateTime datumRezervacijeOd = DateTime.MinValue, datumRezervacijeDo = DateTime.MinValue;

            if (input._datumRezervacijeOdString != null && input._datumRezervacijeDoString != null)
            {
                var datumRezervacijeOdStr = input._datumRezervacijeOdString;
                dan = datumRezervacijeOdStr.Substring(0, 2);
                mjesec = datumRezervacijeOdStr.Substring(3, 2);
                godina = datumRezervacijeOdStr.Substring(6, 4);
                datumRezervacijeOd = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));

                dan = ""; mjesec = ""; godina = "";
                var datumRezervacijeDoStr = input._datumRezervacijeDoString;
                dan = datumRezervacijeDoStr.Substring(0, 2);
                mjesec = datumRezervacijeDoStr.Substring(3, 2);
                godina = datumRezervacijeDoStr.Substring(6, 4);
                datumRezervacijeDo = new DateTime(int.Parse(godina), int.Parse(mjesec), int.Parse(dan));
            }


            var automobil = await _vozilaService.GetById<Automobil>(input.AutomobilId);

            RezervacijaRentanjaUpsertRequest request = new RezervacijaRentanjaUpsertRequest()
            {
                AutomobilId = input.AutomobilId,
                DatumKreiranja = DateTime.Today,
                
                Otkazana = false,
                Popust = (double)input._popust,
                RezervacijaDo = datumRezervacijeDo,
                RezervacijaOd = datumRezervacijeOd,
                VracanjeUposlovnicu = input._vracanjeUPoslovnicu,
                KaskoOsiguranje = input._kaskoOsiguranje,                
                
            };
            if(string.IsNullOrEmpty(input._lokacijaPreuzimanja))
            {
                TempData["poruka"] = "Obavezno je unijeti lokaciju preuzimanja!";
                return Redirect("/Rezervacija/Rezervisi?AutomobilID=" + input.AutomobilId);
            }
            else
            {
                request.LokacijaPreuzimanja = input._lokacijaPreuzimanja;
            }
            

            Klijent k = await HttpContext.GetLogiraniKorisnik();
            request.KlijentId=k.KlijentId;

            TimeSpan brDana = datumRezervacijeDo - datumRezervacijeOd;
            var cijena = (automobil.CijenaIznajmljivanja) * brDana.Days;  
            var cijenaKasko = (automobil.CijenaIznajmljivanja + automobil.CijenaKaskoOsiguranja) * brDana.Days;


            if (input._kaskoOsiguranje)
            {
                request.IznosSaPopustom = input._ukupnoCijenaSaKaskom;
                request.Iznos = cijenaKasko;
            }
            else
            {
                request.IznosSaPopustom = input._ukupnoCijenaBezKaska;
                request.Iznos = cijena;
            }

            var racun = new RacunUpsertRequest()
            {
                DatumIzdavanja = DateTime.Now,
                UkupanIznos = request.IznosSaPopustom
            };

            var entityRacun = await _racuniService.Insert<Racun>(racun);

            request.RacunId = entityRacun.RacunId;

            //Dodaje rezervaciju
            await _rezervacijeService.Insert<RezervacijaRentanja>(request);

            TempData["poruka"] = "Rezervacija uspješno kreirana!";
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Obrisi(int RezervacijaID)
        {            
            await _rezervacijeService.Delete<RezervacijaRentanja>(RezervacijaID);            

            return RedirectToAction("UToku");
        }

        public async Task<bool> provjeriDostupnostVozila(int VoziloID,DateTime datumOd, DateTime datumDo)
        {

            var vozilo = await _vozilaService.GetById<Automobil>(VoziloID);
            RezervacijaRentanjaSearchRequest search = new RezervacijaRentanjaSearchRequest() { StatusAktivna = true };
            AutomobilSearchRequest searchAutomobil = new AutomobilSearchRequest() { Dostupan = true };

            List<RezervacijaRentanja> sveRezervacije = await _rezervacijeService.Get<List<RezervacijaRentanja>>(search);
            List <Automobil> dostupnaVozilaList = await _vozilaService.Get<List<Automobil>>(searchAutomobil);           

            foreach (var rezervacija in sveRezervacije)
            {
                if (datumOd < rezervacija.RezervacijaOd)
                {
                    if (datumDo < rezervacija.RezervacijaOd)
                    {
                        //Dodaj automobil u dostupne automobile
                        continue;
                    }
                    else
                    {
                        //Automobil nije dostupan
                        Automobil x = await _vozilaService.GetById<Automobil>(rezervacija.AutomobilId);

                        for (int i = 0; i < dostupnaVozilaList.Count; i++)
                        {
                            if (dostupnaVozilaList[i].AutomobilId == x.AutomobilId)
                            {
                                dostupnaVozilaList.RemoveAt(i);
                            }
                        }
                    }
                }
                else if (datumDo > rezervacija.RezervacijaDo)
                {
                    if (datumOd > rezervacija.RezervacijaDo)
                    {
                        //Dodaj automobil u dostupne automobile
                        continue;
                    }
                    else
                    {
                        //Automobil nije dostupan
                        Automobil x = await _vozilaService.GetById<Automobil>(rezervacija.AutomobilId);

                        for (int i = 0; i < dostupnaVozilaList.Count; i++)
                        {
                            if (dostupnaVozilaList[i].AutomobilId == x.AutomobilId)
                            {
                                dostupnaVozilaList.RemoveAt(i);
                            }
                        }
                    }
                }
                else
                {
                    //Automobil nije dostupan
                    Automobil x = await _vozilaService.GetById<Automobil>(rezervacija.AutomobilId);

                    for (int i = 0; i < dostupnaVozilaList.Count; i++)
                    {
                        if (dostupnaVozilaList[i].AutomobilId == x.AutomobilId)
                        {
                            dostupnaVozilaList.RemoveAt(i);
                        }
                    }
                }

            }


            if (dostupnaVozilaList.Count()!= 0)
            {
                foreach (var x in dostupnaVozilaList)
                {
                    if (x.AutomobilId==vozilo.AutomobilId)
                    {
                        return true;
                    }                    
                }               
            }
            else
            {
                return false;
            }

            return false;
        }
    }
}