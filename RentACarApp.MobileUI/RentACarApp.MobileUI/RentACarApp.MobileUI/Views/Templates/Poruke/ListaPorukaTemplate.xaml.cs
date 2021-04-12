using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.Models;
using RentACarApp.MobileUI.ViewModels.Rezervacije;
using RentACarApp.MobileUI.ViewModels.Poruke;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Templates.Poruke
{
    /// <summary>
    /// Product list template.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPorukaTemplate : Grid
    {
        private readonly APIService _porukeService = new APIService("Poruka");
        /// <summary>
        /// Bindable property to set the parent bindingcontext.
        /// </summary>
        public static BindableProperty ParentBindingContextProperty =
         BindableProperty.Create(nameof(ParentBindingContext), typeof(object),
         typeof(ListaPorukaTemplate), null);

        /// <summary>
        /// Gets or sets the parent bindingcontext.
        /// </summary>
        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListaPorukaTemplate"/> class.
        /// </summary>
		public ListaPorukaTemplate()
        {
            InitializeComponent();
        }

        private async void DetaljiButton_Clicked(object sender, EventArgs e)
        {
            PorukaDetaljiViewModel porukaDetaljiViewModel = new PorukaDetaljiViewModel();

            //var selectedCar = _viewModel.Items.First(item =>
            //item.Id == int.Parse((sender as Button).CommandParameter.ToString()));
            try
            {
                PorukaKontaktPoruka por = null;
                SfButton x = (SfButton)sender;

                var PorukaId = int.Parse(x.CommandParameter.ToString());

                var model = (ListaPorukaViewModel)(ParentBindingContext);

                foreach (var porukaa in model.listaPoruka)
                {
                    if (porukaa.PorukaId == PorukaId)
                    {
                        por = porukaa;
                    }
                }



                PorukaSearchRequest searchRequest = new PorukaSearchRequest();
                searchRequest.PorukaId = por.PorukaId;


                var list = await _porukeService.Get<IEnumerable<Poruka>>(searchRequest);
                var poruka = list.FirstOrDefault();

                PorukaSearchRequest uposlenikPor = new PorukaSearchRequest();
                uposlenikPor.KlijentId = poruka.KlijentId;
                uposlenikPor.UposlenikId = poruka.UposlenikId;
                uposlenikPor.Naslov = poruka.Naslov;
                uposlenikPor.Posiljaoc = "Uposlenik";
                var listuposlenikPor = await _porukeService.Get<IEnumerable<Poruka>>(uposlenikPor);
                var porukaUp = list.FirstOrDefault();


                PorukaSearchRequest klijentPor = new PorukaSearchRequest();
                klijentPor.KlijentId = poruka.KlijentId;
                klijentPor.UposlenikId = poruka.UposlenikId;
                klijentPor.Naslov = poruka.Naslov;
                klijentPor.Posiljaoc = "Klijent";
                var listklijentPor = await _porukeService.Get<IEnumerable<Poruka>>(klijentPor);
                var porukaKl = listklijentPor.FirstOrDefault();

                porukaDetaljiViewModel.PorukaKlijent = porukaKl;
                porukaDetaljiViewModel.PorukaUposlenik = porukaUp;
                porukaDetaljiViewModel.KlijentId = model.KlijentId;
                porukaDetaljiViewModel.RezervacijaId = model.RezervacijaId;




                //HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Detail.PorukaDetaljiPage(porukaDetaljiViewModel));
                await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Poruke.PorukaDetaljiPage(porukaDetaljiViewModel));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
            }

        }

        private async void OdgovoriButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                SfButton x = (SfButton)sender;
                var PorukaId = int.Parse(x.CommandParameter.ToString());

                PorukaOdgovoriViewModel model = new PorukaOdgovoriViewModel();
                var parentBindingC = (ListaPorukaViewModel)(ParentBindingContext);


                if (parentBindingC.KlijentId > 0)
                {
                    PorukaSearchRequest searchRequest = new PorukaSearchRequest();
                    searchRequest.PorukaId = PorukaId;

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

                    //HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Catalog.PorukaOdgovoriPage(model));
                    await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Poruke.PorukaOdgovoriPage(model));

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");  
            }
            

        }
    }
}