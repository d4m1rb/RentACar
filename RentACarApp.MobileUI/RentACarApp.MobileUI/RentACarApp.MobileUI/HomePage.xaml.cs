using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MasterDetailPage
    {
        public static HomePage HomeStranicaInstanca;
        public int KlijentID;
        public HomePage(int Klijent)
        {
            InitializeComponent();
            KlijentID = Klijent;
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            HomeStranicaInstanca = this;
            Detail =new NavigationPage(new RentACarApp.MobileUI.Views.Pocetna.PocetnaPage(KlijentID));
            
        }
        
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HomePageMenuItemMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            var id = item.Id;
            page.Title = item.Title;


            switch (id)
            {

                case 0:
                    //Detail=new NavigationPage(new RentACarApp.MobileUI.Views.Dashboard.PocetnaPage());
                   this.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Pocetna.PocetnaPage(KlijentID));                   
                    break;
                case 1:
                    //Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Catalog.ListaVozilaPage());
                    this.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Vozila.ListaVozilaPage());
                    break;
                case 2:
                    // Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Catalog.RezervacijaDatumPage());
                    this.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Rezervacije.RezervacijaDatumPage());
                    break;
                case 3:
                    // Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Catalog.ListaRezervacijaPage(KlijentID));
                    this.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Rezervacije.ListaRezervacijaPage(KlijentID));
                    break;
                case 4:
                    // Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Settings.PostavkePage(KlijentID));
                    this.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Postavke.PostavkePage(KlijentID));
                    break;
                case 5:
                    var properties = App.Current.Properties;
                    properties.Remove("username");
                    properties.Remove("password");
                    App.Current.MainPage = new RentACarApp.MobileUI.Views.Login.LoginPage();
                    break;
            }


           // Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}