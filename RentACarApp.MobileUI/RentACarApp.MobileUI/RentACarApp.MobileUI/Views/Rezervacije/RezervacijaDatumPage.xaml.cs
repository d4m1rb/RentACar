using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.DataService;
using RentACarApp.MobileUI.Models;
using RentACarApp.MobileUI.ViewModels.Rezervacije;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.Buttons;
using RentACarApp.MobileUI.ViewModels.Vozila;

namespace RentACarApp.MobileUI.Views.Rezervacije
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RezervacijaDatumPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RezervacijaDatumPage" /> class.
        /// </summary>

        private readonly APIService _rezervacijaService = new APIService("RezervacijaRentanja");
        private readonly APIService _ocjenaService = new APIService("Ocjena");
        private readonly APIService _vozilaService = new APIService("Automobil");

        private RezervacijaDatumViewModel model = null;
        public int KlijentID;
        public List<Automobil> listaDostupnihVozila=new List<Automobil>();
        public RezervacijaDatumPage()
        {
            InitializeComponent();
           // this.BindingContext = CatalogDataService.Instance.ListaVozilaViewModel;
            BindingContext = model = new RezervacijaDatumViewModel();

            DateTime serverTime = DateTime.Today; // gives you current Time in server timeZone
            DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer
            TimeZoneInfo localZone = TimeZoneInfo.Local;

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(localZone.Id);
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

            model.RezervacijaOd = localTime.Date;
            model.RezervacijaDo = localTime.Date;

            //model.RezervacijaOd = DateTime.Today;
            //model.RezervacijaDo = DateTime.Today;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //await model.Init();
        }

        private async void btnDostupnaVozila_Clicked(object sender, EventArgs e)
        {
            SfButton dostupnaVozilaButton = (SfButton)FindByName("btnDostupnaVozila");
            Label PretragaPorukaLabel = (Label)FindByName("labelPorukaPretraga");
            dostupnaVozilaButton.IsEnabled = false;
            PretragaPorukaLabel.IsVisible = true;

            RezervacijaRentanjaSearchRequest search = new RezervacijaRentanjaSearchRequest() { StatusAktivna = true };
            AutomobilSearchRequest searchAutomobil = new AutomobilSearchRequest() { Dostupan = true };
            List<RezervacijaRentanja> sveRezervacije = await _rezervacijaService.Get<List<RezervacijaRentanja>>(search);
            listaDostupnihVozila = await _vozilaService.Get<List<Automobil>>(searchAutomobil);

            foreach (var rezervacija in sveRezervacije)
            {
                if (model.RezervacijaOd<rezervacija.RezervacijaOd)
                {
                    if(model.RezervacijaDo<rezervacija.RezervacijaOd)
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
                            if (listaDostupnihVozila[i].AutomobilId==x.AutomobilId)
                            {
                                listaDostupnihVozila.RemoveAt(i);
                            }
                        }
                    }
                }
                else if (model.RezervacijaDo>rezervacija.RezervacijaDo)
                {
                    if (model.RezervacijaOd > rezervacija.RezervacijaDo)
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
            if(listaDostupnihVozila.Count==0)
            {
                model.Poruka = "Nema dostupnih vozila za odabrani datum.";
                model.PorukaBool = true;
            }
            if (model.RezervacijaOd>= model.RezervacijaDo)
            {
                await App.Current.MainPage.DisplayAlert("Nepravilan datum!", "Datum poèetka mora biti manji od datuma završetka rezervacije najmanje za jedan dan.", "OK");
            }
            else
            {
                InputModel inputM = new InputModel() {
                    _datumRezervacijeOd=model.RezervacijaOd,
                    _datumRezervacijeDo=model.RezervacijaDo,
                    _porukaDostupnaVozila=model.Poruka,
                    _porukaDostupnaVozilaBool=model.PorukaBool
                };
                inputM._listaDostupnihVozila = new List<AutomobilVM>();

                foreach (var dostVozilo in listaDostupnihVozila)
                {
                    var ocjene = await _ocjenaService.Get<List<Ocjena>>(new OcjenaSearchRequest() { VoziloId = dostVozilo.AutomobilId });
                    decimal prosjecnaOcjena = 0;
                    bool prosjecnaBool = false;

                    if (ocjene.Count != 0)
                    {
                        var sumaOcjena = 0; var brojOcjena = 0;
                        foreach (var ocjena in ocjene)
                        {
                            sumaOcjena += ocjena.Ocjena1;
                            brojOcjena++;
                        }

                        prosjecnaOcjena = sumaOcjena / (decimal)brojOcjena;
                        prosjecnaBool = true;
                    }

                    AutomobilVM vozilox = new AutomobilVM();

                    vozilox.AutomobilId = dostVozilo.AutomobilId;
                    vozilox.Boja = dostVozilo.Boja;
                    vozilox.BrojSjedista = dostVozilo.BrojSjedista;
                    vozilox.BrojVrata = dostVozilo.BrojVrata;
                    vozilox.CijenaIznajmljivanja = dostVozilo.CijenaIznajmljivanja;
                    vozilox.CijenaKaskoOsiguranja = dostVozilo.CijenaKaskoOsiguranja;
                    vozilox.Dostupan = dostVozilo.Dostupan;
                    vozilox.DostupanTekst = dostVozilo.DostupanTekst;
                    vozilox.EmisioniStandard = dostVozilo.EmisioniStandard;
                    vozilox.GodinaProizvodnje = dostVozilo.GodinaProizvodnje;
                    vozilox.Gorivo = dostVozilo.Gorivo;
                    vozilox.KategorijaId = dostVozilo.KategorijaId;
                    vozilox.Kubikaza = dostVozilo.Kubikaza;
                    vozilox.ModelId = dostVozilo.ModelId;
                    vozilox.Novo = dostVozilo.Novo;
                    vozilox.Potrosnja = dostVozilo.Potrosnja;
                    vozilox.ProizvodjacModel = dostVozilo.ProizvodjacModel;
                    vozilox.RegistarskaOznaka = dostVozilo.RegistarskaOznaka;
                    vozilox.RegistrovanDo = dostVozilo.RegistrovanDo;
                    vozilox.Slika = dostVozilo.Slika;
                    vozilox.ProsjecnaOcjena = 0;
                    vozilox.SlikaThumb = dostVozilo.SlikaThumb;
                    vozilox.SnagaMotora = dostVozilo.SnagaMotora;
                    vozilox.Transmisija = dostVozilo.Transmisija;

                    if (prosjecnaBool)
                    {
                        vozilox.ProsjecnaOcjena = prosjecnaOcjena;
                        vozilox.ImaProsjecnuOcjenu = true;
                        vozilox.NemaProsjecnuOcjenu = false;
                    }
                    else
                    {
                        vozilox.ImaProsjecnuOcjenu = false;
                        vozilox.NemaProsjecnuOcjenu = true;
                    }

                    inputM._listaDostupnihVozila.Add(vozilox);
                }
                model.InputMod = inputM;
                //HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new DiplomskiMobileApp.Views.Catalog.ListaDostupnihVozilaPage(inputM));
                await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Vozila.ListaDostupnihVozilaPage(inputM));
            }

            dostupnaVozilaButton.IsEnabled = true;
            PretragaPorukaLabel.IsVisible = false;
        }
    }

    
}