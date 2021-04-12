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

namespace RentACarApp.MobileUI.Views.Rezervacije
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RezervacijaPotvrdaPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RezervacijaDatumPage" /> class.
        /// </summary>

        private readonly APIService _rezervacijaService = new APIService("RezervacijaRentanja");
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _racuniService = new APIService("Racun");

        private RezervacijaPotvrdaViewModel model = null;
        public int KlijentID;
        public List<Automobil> listaDostupnihVozila=new List<Automobil>();
        public RezervacijaPotvrdaPage(InputModel inputM)
        {
            InitializeComponent();
           // this.BindingContext = CatalogDataService.Instance.ListaVozilaViewModel;
            BindingContext = model = new RezervacijaPotvrdaViewModel() { RezervacijaOd = DateTime.Now.Date, RezervacijaDo=DateTime.Now.Date };
            model.InputMod = inputM;
            model.InputMod._cijenaKaskoString = model.InputMod._automobil.CijenaKaskoOsiguranja + " KM / dan";
            KlijentID = HomePage.HomeStranicaInstanca.KlijentID;

            double discount = 0;
            TimeSpan brDana = model.InputMod._datumRezervacijeDo - model.InputMod._datumRezervacijeOd;
            if (brDana.Days >= 3 && brDana.Days < 5)
                discount = 0.1;
            else if (brDana.Days >= 5 && brDana.Days < 10)
                discount = 0.2;
            else if (brDana.Days >= 10)
                discount = 0.3;     

            var cijena = (model.InputMod._automobil.CijenaIznajmljivanja) * brDana.Days;
            model.InputMod._ukupnoCijena = cijena - cijena * (decimal)discount;
            model.InputMod._popust = (decimal)discount;
            model.InputMod._popustString = (discount * 100).ToString("0.00") + " %";

            var PopustLabel = (Label)FindByName("popustLabel");
            PopustLabel.Text = model.InputMod._popustString;
            var CijenaLabel = (Label)FindByName("cijenaLabel");
            CijenaLabel.Text = model.InputMod._ukupnoCijena.ToString("0.00")+" KM";          


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //await model.Init();
        }

        private void kaskoSwitch_Toggled(object sender, ToggledEventArgs e)
        {
           
            TimeSpan brDana = model.InputMod._datumRezervacijeDo - model.InputMod._datumRezervacijeOd;
            double discount = 0;

            //Automatsko definisanje popusta
            //Uposlenik (Desktop) može izmjeniti iznos popusta ureðivanjem rezervacije
            if (brDana.Days >= 3 && brDana.Days < 5)
                discount = 0.1;
            else if (brDana.Days >= 5 && brDana.Days < 10)
                discount = 0.2;
            else if (brDana.Days >= 10)
                discount = 0.3;

            var CijenaLabel = (Label)FindByName("cijenaLabel");

            if (!model.InputMod._kaskoOsiguranje==false)
            {

                var cijena= (model.InputMod._automobil.CijenaIznajmljivanja + model.InputMod._automobil.CijenaKaskoOsiguranja) * brDana.Days;
                model.InputMod._ukupnoCijena = cijena - cijena * (decimal)discount;
                model.InputMod._popust = (decimal)discount;
                model.InputMod._popustString = (discount * 100).ToString("0.00") + " %";
                model.InputMod._kaskoOsiguranje = true;
                CijenaLabel.Text = model.InputMod._ukupnoCijena.ToString("0.00") + " KM";
            }
            else
            {
                var cijena = (model.InputMod._automobil.CijenaIznajmljivanja) * brDana.Days;
                model.InputMod._ukupnoCijena = cijena - cijena * (decimal)discount;
                model.InputMod._popust = (decimal)discount;
                model.InputMod._popustString = (discount * 100).ToString("0.00") + " %";

                model.InputMod._kaskoOsiguranje = false;
                CijenaLabel.Text = model.InputMod._ukupnoCijena.ToString("0.00") + " KM";
            }

            
        }

        private async void btnRezervisi_Clicked(object sender, EventArgs e)
        {
            var lokacijaEntry = (Entry)FindByName("lokacijaPreuzimanja");
            if(string.IsNullOrEmpty(lokacijaEntry.Text))
            {
               await App.Current.MainPage.DisplayAlert("Greška", "Obavezno je unijeti lokaciju preuzimanja.", "OK");
                return;
            }

            

            RezervacijaRentanja novaRezervacija = new RezervacijaRentanja();

            
           
                novaRezervacija.KlijentId = HomePage.HomeStranicaInstanca.KlijentID;
                novaRezervacija.DatumKreiranja = DateTime.Today;
                if (model.InputMod._datumRezervacijeOd != null && model.InputMod._datumRezervacijeDo != null)
                {
                    novaRezervacija.RezervacijaOd = model.InputMod._datumRezervacijeOd;
                    novaRezervacija.RezervacijaDo = model.InputMod._datumRezervacijeDo;

                    TimeSpan brojDana = model.InputMod._datumRezervacijeDo - model.InputMod._datumRezervacijeOd;
                    novaRezervacija.Popust = 0;

                    //Automatsko definisanje popusta
                    //Uposlenik (Desktop) može izmjeniti iznos popusta ureðivanjem rezervacije
                    if (brojDana.Days >= 3 && brojDana.Days < 5)
                        novaRezervacija.Popust = 0.1;
                    else if (brojDana.Days >= 5 && brojDana.Days < 10)
                        novaRezervacija.Popust = 0.2;
                    else if (brojDana.Days >= 10)
                        novaRezervacija.Popust = 0.3;

                    if (model.InputMod._automobil.AutomobilId != 0)
                    {
                        var vozilo = await _vozilaService.GetById<Automobil>(model.InputMod._automobil.AutomobilId);

                        if (brojDana.Days == 0)
                            novaRezervacija.Iznos = (vozilo.CijenaIznajmljivanja * 1);

                        else
                            novaRezervacija.Iznos = (vozilo.CijenaIznajmljivanja * brojDana.Days);

                        novaRezervacija.IznosSaPopustom = novaRezervacija.Iznos - (novaRezervacija.Iznos * (decimal)novaRezervacija.Popust);

                        //Ako je ukluèeno kasko osiguranje
                        if (model.InputMod._kaskoOsiguranje)
                        {                        
                            novaRezervacija.Iznos = (vozilo.CijenaIznajmljivanja+vozilo.CijenaKaskoOsiguranja) * brojDana.Days;
                            novaRezervacija.IznosSaPopustom = novaRezervacija.Iznos - (novaRezervacija.Iznos * (decimal)novaRezervacija.Popust);
                        }

                        novaRezervacija.AutomobilId = vozilo.AutomobilId;
                        //SlikaThumb = vozilo.Slika;
                        //CijenaIznajmljivanja = vozilo.CijenaIznajmljivanja;
                        //VoziloInformacije = vozilo.ProizvodjacModel;
                    }
                }
                novaRezervacija.KaskoOsiguranje = model.InputMod._kaskoOsiguranje;
                novaRezervacija.VracanjeUposlovnicu = model.InputMod._vracanjeUPoslovnicu;
                novaRezervacija.LokacijaPreuzimanja = model.InputMod._lokacijaPreuzimanja;

                try
                {
                    //Dodaje raèun za rezervaciju u bazu
                    var racun = new RacunUpsertRequest()
                    {
                        DatumIzdavanja = DateTime.Today,
                        UkupanIznos = novaRezervacija.IznosSaPopustom
                    };

                    var entityRacun = await _racuniService.Insert<Racun>(racun);

                    novaRezervacija.RacunId = entityRacun.RacunId;

                    //Dodaje rezervaciju
                    var entity = await _rezervacijaService.Insert<RezervacijaRentanja>(novaRezervacija);
                await Application.Current.MainPage.DisplayAlert("Uspješna rezervacija","Uspješno ste rezervisali vozilo", "OK");
                //HomePage.HomeStranicaInstanca.Detail= new NavigationPage(new RentACarApp.MobileUI.Views.Catalog.ListaRezervacijaPage(KlijentID));
                await HomePage.HomeStranicaInstanca.Detail.Navigation.PopToRootAsync();

            }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
                }
            
        }
    }
}