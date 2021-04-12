using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.ViewModels.Rezervacije;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.Buttons;
using RentACarApp.MobileUI.ViewModels.Poruke;

namespace RentACarApp.MobileUI.Views.Rezervacije
{
    /// <summary>
    /// The Detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetaljiRezervacijePage
    {
        private readonly APIService _porukeService = new APIService("Poruka");
        private readonly APIService _korisnikService = new APIService("Korisnik");
        private readonly APIService _drzavaService = new APIService("Drzava");
        private readonly APIService _gradService = new APIService("Grad");
        private readonly APIService _rezervacijaService = new APIService("RezervacijaRentanja");
        /// <summary>
        /// Initializes a new instance of the <see cref="DetaljiRezervacijePage" /> class.
        /// </summary>

        int RezervacijaID;
        DetaljiRezervacijeViewModel model;
        public DetaljiRezervacijePage(int RezervacijaId)
        {
            InitializeComponent();
            RezervacijaID = RezervacijaId;
            

            this.BindingContext = model=new DetaljiRezervacijeViewModel() {
                RezervacijaRentanjaId = RezervacijaId,                
            };
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
            await model.InitMethod();
            base.OnAppearing();
            
            
        }
       

        private async void btnProcitajPoruke_Clicked(object sender, System.EventArgs e)
        {
            SfButton x = (SfButton)sender;

            var rezId = int.Parse(x.CommandParameter.ToString());

            var parentCont = (DetaljiRezervacijeViewModel)(BindingContext);
            var KlijentId = parentCont.KlijentId;

            ListaPorukaViewModel model = new ListaPorukaViewModel();
            model.KlijentId = KlijentId;

            if (KlijentId > 0)
            {
                PorukaSearchRequest searchRequest = new PorukaSearchRequest();
                searchRequest.KlijentId = KlijentId;
                searchRequest.RezervacijaRentanjaId = rezId;
                searchRequest.Posiljaoc = "Uposlenik";



                var list = await _porukeService.Get<IEnumerable<Poruka>>(searchRequest);



                model.listaPoruka.Clear();

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

                    model.RezervacijaId = xvar.RezervacijaRentanjaId;
                    model.listaPoruka.Add(xvar);
                }

            }

            //HomePage.HomeStranicaInstanca.Detail= new NavigationPage(new RentACarApp.MobileUI.Views.Catalog.ListaPorukaPage(model));
            await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Poruke.ListaPorukaPage(model));
        }

        private async void btnObrisiRezervaciju_Clicked(object sender, System.EventArgs e)
        {
            bool odg = await DisplayAlert("Brisanje rezervacije!", "Da li ste sigurni da želite obrisati rezervaciju?", "Da", "Ne");
            if(odg)
            {
                await _rezervacijaService.Delete<RezervacijaRentanja>(model.RezervacijaRentanjaId);
                await DisplayAlert("Rezervacija obrisana!", "Obrisali ste rezervaciju.", "OK");
                await HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Rezervacija nije obrisana!", "Odustali ste od brisanja rezervacije.", "OK");
            }
        }
    }
}