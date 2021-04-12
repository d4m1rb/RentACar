using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.Models;
using RentACarApp.MobileUI.ViewModels.Rezervacije;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using RentACarApp.MobileUI.ViewModels.Poruke;

namespace RentACarApp.MobileUI.Views.Templates.Rezervacije
{
    /// <summary>
    /// Product list template.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaRezervacijaTemplate : Grid
    {

        private readonly APIService _porukeService = new APIService("Poruka");
        private readonly APIService _korisnikService = new APIService("Korisnik");
        private readonly APIService _drzavaService = new APIService("Drzava");
        private readonly APIService _gradService = new APIService("Grad");

        /// <summary>
        /// Bindable property to set the parent bindingcontext.
        /// </summary>
        public static BindableProperty ParentBindingContextProperty =
         BindableProperty.Create(nameof(ParentBindingContext), typeof(object),
         typeof(ListaRezervacijaTemplate), null);

        /// <summary>
        /// Gets or sets the parent bindingcontext.
        /// </summary>
        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListaRezervacijaTemplate"/> class.
        /// </summary>
		public ListaRezervacijaTemplate()
        {
            InitializeComponent();
        }
       
        private async void DetaljiButton_Clicked(object sender, EventArgs e)
        {

            //var selectedCar = _viewModel.Items.First(item =>
            //item.Id == int.Parse((sender as Button).CommandParameter.ToString()));
            try 
            {
                SfButton x = (SfButton)sender;
                
                var RezervacijaId = int.Parse(x.CommandParameter.ToString());

                //HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Detail.DetaljiRezervacijePage(AutomobilId));
                await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Rezervacije.DetaljiRezervacijePage(RezervacijaId));
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
            }
           
        }

        private async void PorukeButton_Clicked(object sender, EventArgs e)
        {
            SfButton x = (SfButton)sender;

            var rezId = int.Parse(x.CommandParameter.ToString());

            var parentCont = (ListaRezervacijaViewModel)(ParentBindingContext);
            var KlijentId = parentCont.KlijentID;
            
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
                        DatumVrijeme=item.DatumVrijeme,
                        Drzava=nazivDrzave,
                        Email=k.Email,
                        KlijentId=item.KlijentId,
                        Naslov=item.Naslov,
                        PorukaId=item.PorukaId,
                        Posiljaoc=item.Posiljaoc,
                        PosiljaocInfo=item.PosiljaocInfo,
                        PosiljaocSlikaThumb=item.PosiljaocSlikaThumb,
                        PrimaocInfo=item.PrimaocInfo,
                        Procitano=item.Procitano,
                        RezervacijaRentanjaId=item.RezervacijaRentanjaId,
                        Sadrzaj=item.Sadrzaj,
                        slikaThumb=slika,
                        Telefon=k.Telefon,
                        UposlenikId=item.UposlenikId                        
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
    }
}