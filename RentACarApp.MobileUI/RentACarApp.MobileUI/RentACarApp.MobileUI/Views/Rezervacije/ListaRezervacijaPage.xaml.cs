using RentACarApp.MobileUI.DataService;
using RentACarApp.MobileUI.Models;
using RentACarApp.MobileUI.ViewModels.Rezervacije;
using RentACarApp.Model.Models;
using Syncfusion.ListView.XForms;
using System;
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
    public partial class ListaRezervacijaPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListaRezervacijaPage" /> class.
        /// </summary>
        
        private ListaRezervacijaViewModel model = null;
        public int KlijentID;
        public ListaRezervacijaPage(int klijent)
        {
            InitializeComponent();
            KlijentID = klijent;
           // this.BindingContext = CatalogDataService.Instance.ListaVozilaViewModel;
            BindingContext = model = new ListaRezervacijaViewModel(KlijentID);

            model.uTokuRezervacijeList = (SfListView)FindByName("UTokuRezervacije");
            model.zavrseneRezervacijeList = (SfListView)FindByName("ZavrseneRezervacije");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
            SfListView uToku = (SfListView)FindByName("UTokuRezervacije");
            uToku.BackgroundColor = Color.LightGray;

        }

        private void UTokuRezervacije_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

            SfListView zavrsene = (SfListView)FindByName("ZavrseneRezervacije");
            SfListView uToku = (SfListView)FindByName("UTokuRezervacije");

            if(zavrsene.IsVisible==true)
            {
                zavrsene.IsVisible = false;
                uToku.IsVisible = true;
                model.switchToggledZavrsene = false;
            }
            else
            {
                zavrsene.IsVisible = true;
                uToku.IsVisible = false;
                model.switchToggledZavrsene = true;
            }

            //StackLayout sl = (StackLayout)FindByName("sl");
            //if (e.Value)
            //{
            //    sl.Children.Remove(uToku);
            //    if (!sl.Children.Contains(zavrsene))
            //        sl.Children.Add(zavrsene);

            //    zavrsene.IsVisible = true;
            //    //zavrsene.HeightRequest = 130;
            //    //uToku.HeightRequest = 0;
            //    uToku.IsVisible = false;
            //}
            //else
            //{
            //    sl.Children.Remove(zavrsene);

            //    if (!sl.Children.Contains(uToku))
            //        sl.Children.Add(uToku);

            //    uToku.IsVisible = true;
            //    //uToku.HeightRequest = 130;
            //    //zavrsene.HeightRequest = 0;
            //    zavrsene.IsVisible = false;
            //}
        }

        private async void UTokuRezervacije_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            try
            {
                var rezervacija = e.ItemData as RezervacijaRentanja;
                if (rezervacija != null)
                {                    
                    var RezervacijaId = rezervacija.RezervacijaRentanjaId;

                    //HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Detail.DetaljiRezervacijePage(AutomobilId));
                    await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Rezervacije.DetaljiRezervacijePage(RezervacijaId));
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
            }
        }

        private async void ZavrseneRezervacije_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            try
            {

                var rezervacija = e.ItemData as RezervacijaRentanja;

                if (rezervacija != null)
                {
                    var RezervacijaId = rezervacija.RezervacijaRentanjaId;

                    //HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Detail.DetaljiRezervacijePage(AutomobilId));
                    await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Rezervacije.DetaljiRezervacijePage(RezervacijaId));
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
            }
        }
    }
}