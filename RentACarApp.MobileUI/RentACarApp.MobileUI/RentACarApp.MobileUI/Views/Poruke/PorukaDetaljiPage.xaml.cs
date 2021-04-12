using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.ViewModels.Rezervacije;
using RentACarApp.MobileUI.ViewModels.Poruke;
using Syncfusion.XForms.Buttons;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Poruke
{
    /// <summary>
    /// The Detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PorukaDetaljiPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetaljiVozilaPage" /> class.
        /// </summary>
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _porukeService = new APIService("Poruka");
        private readonly APIService _korisnikService = new APIService("Korisnik");
        private readonly APIService _drzavaService = new APIService("Drzava");
        private readonly APIService _gradService = new APIService("Grad");
        int AutomobilID;
        public PorukaDetaljiViewModel model;
        public PorukaDetaljiPage(PorukaDetaljiViewModel porDetaljiVM)
        {
            InitializeComponent();



            this.BindingContext = model = new PorukaDetaljiViewModel() { PorukaUposlenik=porDetaljiVM.PorukaUposlenik, PorukaKlijent=porDetaljiVM.PorukaKlijent,KlijentId=porDetaljiVM.KlijentId};

            
        }



        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height);

        //    if (width > height)
        //    {
        //        //Rotator.ItemTemplate = (DataTemplate)this.Resources["LandscapeTemplate"];
        //    }
        //    else
        //    {
        //        //Rotator.ItemTemplate = (DataTemplate)this.Resources["PortraitTemplate"];
        //    }
        //}

        protected override async void OnAppearing()
        {
           // await model.InitMethod();
            base.OnAppearing();
            
            
        }

        private void SfButton_Clicked(object sender, System.EventArgs e)
        {

        }

        private async void OkButton_Clicked(object sender, System.EventArgs e)
        {
            SfButton x = (SfButton)sender;

            var RezervacijaId = int.Parse(x.CommandParameter.ToString());

            var rezId = RezervacijaId;


            var KlijentId = model.KlijentId;

            ListaPorukaViewModel modellp = new ListaPorukaViewModel();

            if (KlijentId > 0)
            {
                PorukaSearchRequest searchRequest = new PorukaSearchRequest();
                searchRequest.KlijentId = KlijentId;
                searchRequest.RezervacijaRentanjaId = rezId;
                searchRequest.Posiljaoc = "Uposlenik";



                var list = await _porukeService.Get<IEnumerable<Poruka>>(searchRequest);



                modellp.listaPoruka.Clear();

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

                    modellp.RezervacijaId = xvar.RezervacijaRentanjaId;
                    modellp.listaPoruka.Add(xvar);
                    modellp.KlijentId = KlijentId;
                }

            }

            //HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Catalog.ListaPorukaPage(modellp));
            await HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();
        }
    }
}