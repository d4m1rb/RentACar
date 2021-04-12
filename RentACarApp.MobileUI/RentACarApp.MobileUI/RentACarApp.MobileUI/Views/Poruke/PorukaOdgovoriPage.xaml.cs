using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.DataService;
using RentACarApp.MobileUI.Models;
using RentACarApp.MobileUI.ViewModels.Poruke;
using RentACarApp.MobileUI.ViewModels.Poruke;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Poruke
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PorukaOdgovoriPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PorukaOdgovoriPage" /> class.
        /// </summary>
        private readonly APIService _porukeService = new APIService("Poruka");
        private readonly APIService _rezervacijaService = new APIService("RezervacijaRentanja");
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _korisnikService = new APIService("Korisnik");
        private readonly APIService _drzavaService = new APIService("Drzava");
        private readonly APIService _gradService = new APIService("Grad");

        private PorukaOdgovoriViewModel model = null;
        public int KlijentID;
        public List<Automobil> listaDostupnihVozila=new List<Automobil>();
        public PorukaOdgovoriPage(PorukaOdgovoriViewModel mod)
        {
            InitializeComponent();
           // this.BindingContext = CatalogDataService.Instance.ListaVozilaViewModel;
            BindingContext = model = mod;
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //await model.Init();
        }

        private async void btnDostupnaVozila_Clicked(object sender, EventArgs e)
        {
        }

        private async void btnPosalji_Clicked(object sender, EventArgs e)
        {
            try
            {
                SfButton x = (SfButton)sender;
                var PorukaId = int.Parse(x.CommandParameter.ToString());

                var poruke = await _porukeService.Get<List<Poruka>>(new PorukaSearchRequest { PorukaId = model.PorukaId });
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
                    Procitano = false,
                    Posiljaoc = "Klijent"
                };

                await _porukeService.Insert<Poruka>(p);
                await App.Current.MainPage.DisplayAlert("Poruka poslana!","Uspješno ste odgovorili na poruku.", "OK");

                

                #region LoadListaPorukaViewModel

                var rezId = model.RezervacijaRentanjaId;
                var KlijentId = model.KlijentId;

                ListaPorukaViewModel modeL = new ListaPorukaViewModel();
                modeL.KlijentId = KlijentId;

                if (KlijentId > 0)
                {
                    PorukaSearchRequest searchRequest = new PorukaSearchRequest();
                    searchRequest.KlijentId = KlijentId;
                    searchRequest.RezervacijaRentanjaId = rezId;
                    searchRequest.Posiljaoc = "Uposlenik";



                    var list = await _porukeService.Get<IEnumerable<Poruka>>(searchRequest);



                    modeL.listaPoruka.Clear();

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
                        var listaGrad = await _gradService.Get<IEnumerable<Grad>>(searchGrad);
                        int drzavaId = listaGrad.FirstOrDefault().DrzavaId;

                        DrzavaSearchRequest searchDrzava = new DrzavaSearchRequest();
                        searchDrzava.DrzavaId = drzavaId;
                        var listaDrzava = await _drzavaService.Get<IEnumerable<Drzava>>(searchDrzava);
                        var nazivDrzave = listaDrzava.FirstOrDefault().Naziv;

                        PorukaKontaktPoruka xvar = new PorukaKontaktPoruka()
                        {
                            DatumVrijeme = item.DatumVrijeme,
                            Drzava = nazivDrzave,
                            Email = k.Email,
                            KlijentId = item.KlijentId,
                            Naslov = item.Naslov,
                            PorukaId = item.PorukaId,
                            Posiljaoc = item.Posiljaoc,
                            PosiljaocInfo = item.PosiljaocInfo,
                            PosiljaocSlikaThumb = item.PosiljaocSlikaThumb,
                            PrimaocInfo = item.PrimaocInfo,
                            Procitano = item.Procitano,
                            RezervacijaRentanjaId = item.RezervacijaRentanjaId,
                            Sadrzaj = item.Sadrzaj,
                            slikaThumb = slika,
                            Telefon = k.Telefon,
                            UposlenikId = item.UposlenikId
                        };
                        if (!xvar.Procitano)
                            xvar.NijeProcitano = true;
                        else
                            xvar.NijeProcitano = false;

                        modeL.RezervacijaId = xvar.RezervacijaRentanjaId;
                        modeL.listaPoruka.Add(xvar);
                    }

                }

                #endregion


               // HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Catalog.ListaPorukaPage(modeL));
                await HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();

            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
            }
            
        }

    }

    
}